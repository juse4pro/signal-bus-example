using Godot;

namespace SignalBusPatternExample;

public abstract partial class Quest : GodotObject
{
    [Signal]
    public delegate void ProgressEventHandler();
    
    public bool IsCompleted { protected set; get; }
    
    public abstract void OnStart();
    
    public virtual void OnComplete()
    {
        IsCompleted = true;
    }

    public abstract string GetUiText();
}