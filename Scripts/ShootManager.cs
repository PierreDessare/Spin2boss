using Godot;
using System;

/// <summary>
/// Class that handles projectiles spawning for the player only
/// </summary>
public partial class ShootManager : Node
{
	// Called when the node enters the scene tree for the first time.
	private IWeapon _activeWeapon;


	public void FireActive()
	{
		_activeWeapon.Fire();
	}

	public string GetActiveName()
	{
		return _activeWeapon.GetGunName();
	}
	public override void _Ready()
	{
		//for now hard coded init
		_activeWeapon = new BaseGun();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
