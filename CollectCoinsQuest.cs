using Godot;

namespace SignalBusPatternExample;

public partial class CollectCoinsQuest : Quest
{
	public int RequiredCoins { get; init; } = 10;
	private int _coinsCollected;


	public override void OnStart()
	{
		QuestManager.Instance.Connect(
			QuestManager.SignalName.CoinsCollectedChange,
			new Callable(this, nameof(this.OnCoinsCollectedChanged))
		);
	}

	private void OnCoinsCollectedChanged(int totalCoinsCollected)
	{
		this._coinsCollected = totalCoinsCollected;
		if (this._coinsCollected >= this.RequiredCoins)
		{
			this.OnComplete();
			QuestManager.Instance.AddQuest(new StayAliveQuest());
		}

		this.EmitSignalProgress();
	}

	public override void OnComplete()
	{
		base.OnComplete();
		QuestManager.Instance.Disconnect(
			QuestManager.SignalName.CoinsCollectedChange,
			new Callable(this, nameof(this.OnCoinsCollectedChanged))
		);
	}

	public override string GetUiText()
	{
		return $"Collect {this.RequiredCoins} coins: {this._coinsCollected} / {this.RequiredCoins}";
	}
}