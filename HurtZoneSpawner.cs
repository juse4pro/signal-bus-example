using Godot;
using System;

public partial class HurtZoneSpawner : Node
{
    [Export]
    public PackedScene HurtZoneScene;


    public override void _Ready()
    {
        Timer timer = new Timer()
        {
            Autostart = true,
            WaitTime = 0.25f,
            OneShot = false
        };
        
        this.AddChild(timer);
        timer.Timeout += () =>
        {
            Node2D zone = this.HurtZoneScene.Instantiate<Node2D>();
            zone.Position = new Vector2(GD.RandRange(0, 1280), GD.RandRange(0, 720));
            this.GetParent().AddChild(zone);
            this.GetParent().MoveChild(zone, 0);
        };
        timer.Start();
    }
}
