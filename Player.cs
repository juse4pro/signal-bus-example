using Godot;
using System;
using SignalBusPatternExample;

public partial class Player : Node2D
{
	private int _coinCounter = 0;
	private float _aliveTime = 0f;
	private bool _isAlive = true;


	public override void _Ready()
	{
		QuestManager.Instance.NotifyCoinsCollected(this._coinCounter);
	}


	public override void _Process(double delta)
	{
		if (!this._isAlive)
			return;

		this._aliveTime += (float)delta;
		QuestManager.Instance.NotifyPlayerAliveTime(this._aliveTime);

		const float speed = 200f;
		Vector2 intendedMovement = Vector2.Zero;

		if (Input.IsKeyPressed(Key.A))
			intendedMovement += Vector2.Left;

		if (Input.IsKeyPressed(Key.D))
			intendedMovement += Vector2.Right;

		if (Input.IsKeyPressed(Key.W))
			intendedMovement += Vector2.Up;

		if (Input.IsKeyPressed(Key.S))
			intendedMovement += Vector2.Down;

		Vector2 deltaMovement = intendedMovement.Normalized() * speed * (float)delta;
		this.Position += deltaMovement;
		QuestManager.Instance.NotifyPlayerMoved(deltaMovement.Length());
	}


	private void OnCollectAreaEntered(Area2D enteredArea)
	{
		if (enteredArea.Owner is not Coin coin)
			return;

		this._coinCounter++;
		QuestManager.Instance.NotifyCoinsCollected(this._coinCounter);
		coin.QueueFree();
	}
}