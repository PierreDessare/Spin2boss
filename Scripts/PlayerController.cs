using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	
	//Exportables stats her
	[Export] private float _baseSpeed = 500f;
	//Exportables object here(managers/handlers...)
	[Export]private ShootManager _shootManager;

	[ExportGroup("Health Component")]
	[Export] private HealthComponent _health;
	[Export] private UI_HealthComponent _healthBar;

	//Non exportables here
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_health.DeathStatusChanged += OnDeathStatusChangedSignal;
		_healthBar.Connect(_health);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//PhysicsProcess is called at fixed interval so no need to worry about delta
	//except in very rare edge cases
	public override void _PhysicsProcess(double delta)
	{
		LookAt(GetGlobalMousePosition());
		
		Vector2 move_input = Input.GetVector("Left","Right","Up","Down");

		Velocity = move_input * _baseSpeed;

		MoveAndSlide();
	}

	private void OnDeathStatusChangedSignal(bool isDead)
	{
		if (isDead) GD.Print("Player Character died.");
		else GD.Print("Player Character is back in the land of the living. Stay determined !");
		
	}

	//Things that are not continuous(i.e input where the player does not hold the key down)
	//should be handled here,instead of doing Input.XX do @event.XX
	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("Fire1"))
		{
			_shootManager.FireActive();
		}
	}
}
