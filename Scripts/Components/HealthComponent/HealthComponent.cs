using System;
using Godot;

public partial class HealthComponent : Node
{
	[Signal]
	public delegate void HealthChangedEventHandler(int newValue);

	[Export]
	public int maxHealth = 100;
	
	[Export]
	private int _health = 0;
	public int Health
	{
		get => _health;
		set 
		{
			_health = Math.Clamp(value, 0, maxHealth);
			EmitSignal(SignalName.HealthChanged, _health);
		}
	}

	public void Damage(int value)
	{
		Health -= value;
	}

	public void Heal(int value)
	{
		Health += value;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("DebugKey")) {
			GD.Print("[DEBUG KEY]: Healing player of 10 HP");
			Heal(10);
		}
	}
}
