using Godot;
using System;
using SignalBusPatternExample;

public partial class QuestLogUi : Node
{
	[Export] public PackedScene QuestEntryUiScene;


	public override void _Ready()
	{
		QuestManager.Instance.Connect(
			QuestManager.SignalName.QuestAdded,
			new Callable(this, nameof(this.CreateQuestEntry))
		);

		foreach (Quest quest in QuestManager.Instance.Quests)
			this.CreateQuestEntry(quest);
	}


	private void CreateQuestEntry(Quest quest)
	{
		QuestEntryUi entry = this.QuestEntryUiScene.Instantiate<QuestEntryUi>();
		entry.QuestReference = quest;
		this.AddChild(entry);
	}
}