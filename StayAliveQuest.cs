using Godot;

namespace SignalBusPatternExample;

public partial class StayAliveQuest : Quest
{
	public float RequiredAliveTime { get; init; } = 60;
	private float _aliveTime;


	public override void OnStart()
	{
		QuestManager.Instance.Connect(
			QuestManager.SignalName.PlayerAliveTimeChange,
			new Callable(this, nameof(this.OnAliveTimeChanged))
		);
	}


	private void OnAliveTimeChanged(float aliveTime)
	{
		this._aliveTime = aliveTime;

		if (this._aliveTime >= this.RequiredAliveTime)
			this.OnComplete();

		this.EmitSignalProgress();
	}


	public override void OnComplete()
	{
		base.OnComplete();
		QuestManager.Instance.Disconnect(
			QuestManager.SignalName.PlayerAliveTimeChange,
			new Callable(this, nameof(this.OnAliveTimeChanged))
		);
	}


	public override string GetUiText()
	{
		return $"Stay alive for {this.RequiredAliveTime} seconds: {this._aliveTime:F1} / {this.RequiredAliveTime}";
	}
}