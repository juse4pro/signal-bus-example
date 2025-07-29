using Godot;

namespace SignalBusPatternExample;

public partial class WalkDistanceQuest : Quest
{
    public float RequiredDistance { get; init; }
    private float _totalDistance;
    
    
    public override void OnStart()
    {
        QuestManager.Instance.Connect(
            QuestManager.SignalName.WalkedDistanceChange,
            new Callable(this, nameof(this.OnDistanceWalkedChanged))
        );
    }

    private void OnDistanceWalkedChanged(int totalCoinsCollected)
    {
        this._totalDistance = totalCoinsCollected;
        if (this._totalDistance >= this.RequiredDistance)
        {
            this.OnComplete();
        }
        
        this.EmitSignalProgress();
    }

    public override void OnComplete()
    {
        base.OnComplete();
        QuestManager.Instance.Disconnect(
            QuestManager.SignalName.WalkedDistanceChange,
            new Callable(this, nameof(this.OnDistanceWalkedChanged))
        );
    }

    public override string GetUiText()
    {
        return $"Walk {this.RequiredDistance} units: {this._totalDistance} / {this.RequiredDistance}";
    }
}