using Godot;
using System;

/// <summary>
/// Very basic gun meant for debugging/testing purposes
/// </summary>
public partial class BaseGun : Node2D
{
    [Export] private string gun_name = "BaseGun";
    [Export] PackedScene base_bullet_scene = GD.Load<PackedScene>("res://Scenes/Projectiles/BaseBullet.tscn");
    [Export] float bullet_speed = 600f;
    [Export] float fire_rate = 1/5f;
    [Export] float damage = 1f;
    
    float _time_to_next_bullet = 0f;
    public void Fire()
    {
        if (true)
        {
            RigidBody2D bullet = base_bullet_scene.Instantiate<RigidBody2D>();
            bullet.Rotation = GlobalRotation;
            bullet.GlobalPosition = GlobalPosition;
            bullet.LinearVelocity = bullet.Transform.X * bullet_speed;
            //this line might not be needed but if we want bullet later to have
            //deeper interaction like on hit effect they need to be in the scene tree
            GetTree().Root.AddChild(bullet);
            _time_to_next_bullet = 0f;
        }
    }

    public override void _Process(double delta)
    {
        _time_to_next_bullet += (float)delta;
    }

    public void Reload()
    {
        throw new NotImplementedException();
    }

    public string GetGunName()
    {
        return gun_name;
    }
}
