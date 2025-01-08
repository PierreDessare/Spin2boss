using Godot;

/// <summary>
/// Abstract that all Spell used by entity should implement
/// </summary>
public abstract partial class AAoe: Node2D, IAoe
{
    [Signal]
    public delegate void SpellHitEventHandler();
    public abstract void TriggerEffect();
    public abstract void Destroy();
}
