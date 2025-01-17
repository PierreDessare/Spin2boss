using System;
using Godot;

public partial class HealthComponent : Node
{
	[Signal] public delegate void HealthChangedEventHandler(float newValue);
	[Signal] public delegate void DeathStatusChangedEventHandler(bool isDead);

	[Export] public float maxHealth = 100;	
	[Export] private float _health = 0;
	public float Health
	{
		get => _health;
		set
		{
			_health = Math.Clamp(value, 0, maxHealth);
			if (0 >= Health) {
				IsDead = true;
			}
			if (0 < Health && IsDead) {
				IsDead = false;
			}
			EmitSignal(SignalName.HealthChanged, _health);
		}
	}
	[Export] private bool _isDead = true;
	public bool IsDead
	{
		get => _isDead;
		private set
		{
			_isDead = value;
			EmitSignal(SignalName.DeathStatusChanged, _isDead);
		}
	}

	public void Damage(float value)
	{
		Health -= value;
	}
	public void Heal(float value)
	{
		Health += value;
	}
	public void Kill()
	{
		Health = 0;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("DebugKey")) {
			GD.Print("[DEBUG KEY]: Healing of 10 HP");
			Heal(10);
		} else if (@event.IsActionPressed("SecondDebugKey")) {
			GD.Print("[SECOND DEBUG KEY]: Killing");
			Kill();
		}
	}
}
