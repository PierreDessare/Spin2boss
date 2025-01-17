using Godot;
using System;

public partial class UI_HealthComponent : ProgressBar
{
	private HealthComponent _healthComponent;

	public void Connect(HealthComponent healthComponent)
	{
		_healthComponent = healthComponent;
		MaxValue = healthComponent.maxHealth;
		Value = healthComponent.Health;
		_healthComponent.HealthChanged += OnHealthChangedSignal;
	}

	public void OnHealthChangedSignal(float newValue)
	{
		Value = newValue;
	}

	public override void _ExitTree()
	{
		_healthComponent.HealthChanged -= OnHealthChangedSignal;
	}
}
