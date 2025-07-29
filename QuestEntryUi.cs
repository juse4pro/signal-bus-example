using Godot;
using System;
using SignalBusPatternExample;

public partial class QuestEntryUi : Node
{
    public Quest QuestReference;


    public override void _Ready()
    {
        this.QuestReference.Connect(Quest.SignalName.Progress, new Callable(this, nameof(this.Update)));
        this.Update();
    }


    private void Update()
    {
        this.GetNode<Label>("%QuestText").Text = this.QuestReference.GetUiText();
        this.GetNode<ColorRect>("%StrikeThrough").Visible = this.QuestReference.IsCompleted;
    }
}
