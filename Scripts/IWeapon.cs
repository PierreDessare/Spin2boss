
/// <summary>
/// Interface that all weapon that the player can use should implement
/// </summary>
public interface IWeapon
{
    public void Fire();
    public void Reload();

    public string GetGunName();
}
