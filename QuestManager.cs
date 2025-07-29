using System.Collections.Generic;
using Godot;

namespace SignalBusPatternExample;

public partial class QuestManager : Node
{
    [Signal]
    public delegate void QuestAddedEventHandler(Quest newQuest);
    
    [Signal]
    public delegate void CoinsCollectedChangeEventHandler(int totalCoinsCollected);
    
    [Signal]
    public delegate void WalkedDistanceChangeEventHandler(float totalDistance);
    
    public static QuestManager Instance;

    public List<Quest> Quests { private set; get; } = [];

    private float _walkedDistance;
    
    
    public override void _Ready()
    {
        Instance = this;
        
        CollectCoinsQuest coinsQuest = new CollectCoinsQuest()
        {
            RequiredCoins = 5
        };
        WalkDistanceQuest walkQuest = new WalkDistanceQuest()
        {
            RequiredDistance = 1000
        };
        
        this.Quests.AddRange([coinsQuest, walkQuest]);
        foreach (Quest quest in this.Quests)
        {
            quest.OnStart();
        }
    }


    public void AddQuest(Quest quest)
    {
        this.Quests.Add(quest);
        quest.OnStart();
        this.EmitSignalQuestAdded(quest);
    }


    public void NotifyCoinsCollected(int totalCoinsCollected)
    {
        this.EmitSignalCoinsCollectedChange(totalCoinsCollected);
    }


    public void NotifyPlayerMoved(float distance)
    {
        this._walkedDistance += distance;
        this.EmitSignalWalkedDistanceChange(this._walkedDistance);
    }
}