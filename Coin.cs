using Godot;
using System;

public partial class Coin : Node2D
{
    public override void _Process(double delta)
    {
        this.RotationDegrees += (float)delta * 300f;
    }
}
