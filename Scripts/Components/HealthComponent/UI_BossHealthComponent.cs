using Godot;
using System;

public partial class UI_BossHealthComponent : ProgressBar
{
	
	private RichTextLabel _progressText;
	private string _bossName = "placeholder";
	private HealthComponent _healthComponent;
	
	public override void _Ready()
	{
		_progressText = GetNode<RichTextLabel>("BossHealthBarText");
	}

	private void UpdateProgressText()
	{
		_progressText.Text = "[center]"+ _bossName + " " + (int)Math.Round(Value/MaxValue*100) +"% [/center]";
	}
	
	public void Connect(HealthComponent healthComponent, string _bossName)
	{
		GD.Print("Initiating boss health bar");
		_healthComponent = healthComponent;
		MaxValue = healthComponent.maxHealth;
		Value = healthComponent.Health;
		_healthComponent.HealthChanged += OnHealthChangedSignal;
		this._bossName = _bossName;
		UpdateProgressText();
	}

	public void OnHealthChangedSignal(float newValue)
	{
		Value = newValue;
		UpdateProgressText();
	}

	public override void _ExitTree()
	{
		_healthComponent.HealthChanged -= OnHealthChangedSignal;
	}

}
