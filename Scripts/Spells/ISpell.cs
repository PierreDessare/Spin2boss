using Godot;

/// <summary>
/// Interface that all Spell used by entity should implement
/// </summary>
public interface ISpell
{
    [Signal]
    public delegate void SpellCastedEventHandler();
    public void CastStart();
    public void CastEnd();
    public void Destroy();
}
