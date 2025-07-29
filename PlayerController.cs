using Godot;
using System;

public partial class PlayerController : Node
{
	public static PlayerController Instance { private set; get; }
	private Player _currentPlayer;


	public override void _Ready()
	{
		Instance = this;
	}


	public void SetCurrentPlayer(Player player)
	{
		if (IsInstanceValid(this._currentPlayer)
		    && this._currentPlayer.IsConnected(Player.SignalName.Died, new Callable(this, nameof(this.OnCurrentPlayerDied))))
			this._currentPlayer.Disconnect(Player.SignalName.Died, new Callable(this, nameof(this.OnCurrentPlayerDied)));

		this._currentPlayer = player;

		this._currentPlayer.Connect(Player.SignalName.Died, new Callable(this, nameof(this.OnCurrentPlayerDied)));
	}


	private void OnCurrentPlayerDied()
	{
		SceneTreeTimer timer = this.GetTree().CreateTimer(5f);
		timer.Timeout += () =>
		{
			this._currentPlayer.QueueFree();
			PackedScene playerScene = ResourceLoader.Load<PackedScene>("uid://gp3v2osx0b52");
			Player newPlayer = playerScene.Instantiate<Player>();

			Node root = this.GetTree().Root;
			Node main = root.GetNode("Main");
			main.AddChild(newPlayer);
		};
	}


	public Player GetCurrentPlayer()
	{
		return this._currentPlayer;
	}
}