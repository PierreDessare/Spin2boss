
using Godot;
using Godot.Collections;


/// <summary>
/// Interface that all aoe that will appear should implement
/// </summary>
public interface IAoe
{
    [Signal]
    public delegate void SpellHitEventHandler();
    public void TriggerEffect();
    public void Destroy();
}
