using Godot;
using System;

public partial class HurtZone : Node2D
{
	private Sprite2D _marker;
	private Sprite2D _charge;
	private float _timer;


	public override void _Ready()
	{
		this._marker = this.GetNode<Sprite2D>("%Marker");
		this._charge = this.GetNode<Sprite2D>("%Charge");
		this._charge.Scale = Vector2.Zero;
	}


	public override void _Process(double delta)
	{
		this._timer += (float)delta;

		if (this._timer >= 1)
		{
			this.Explode();
			this._timer = 1;
		}

		this._charge.Scale = Vector2.One * this._timer;
	}

	private void Explode()
	{
		foreach (Area2D overlappingArea in this.GetNode<Area2D>("%ExplosionArea").GetOverlappingAreas())
		{
			if (overlappingArea.Owner is not Player player)
				continue;

			player.Hurt();
		}

		this.QueueFree();
	}
}