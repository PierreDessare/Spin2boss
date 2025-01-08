using Godot;
using System;

/// <summary>
/// Class that handles projectiles spawning for the player only
/// </summary>
public partial class ShootManager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private BaseGun _activeWeapon;


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
		var _tmpGunScene = GD.Load<PackedScene>("res://Scenes/Weapon/BaseGun.tscn");
		_activeWeapon = _tmpGunScene.Instantiate<BaseGun>();
		AddChild(_activeWeapon);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
