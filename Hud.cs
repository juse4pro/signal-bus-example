using Godot;
using System;

public partial class Hud : Control
{
	private Player _currentlyObservedPlayer;


	public override void _Ready()
	{
		this.GetNode<Control>("%DeathPanel").Hide();
		this._currentlyObservedPlayer = PlayerController.Instance.GetCurrentPlayer();

		this._currentlyObservedPlayer.Connect(
			Player.SignalName.Died,
			new Callable(this, nameof(this.OnPlayerDied))
		);
	}


	private void OnPlayerDied()
	{
		this.GetNode<Control>("%DeathPanel").Show();
	}
}