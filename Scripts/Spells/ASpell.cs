using Godot;

/// <summary>
/// Abstract that all Spell used by entity should implement
/// </summary>
public abstract partial class ASpell: Node2D, ISpell
{
    [Signal]
    public delegate void SpellCastedEventHandler();

    // Cast time for the spell in second
    [Export]
    public float castTime = 0f;

    public abstract void CastStart();
    public abstract void CastEnd();
    public abstract void Destroy();
}
