using Godot;
using System;

public partial class EnemyController : CharacterBody2D
{
	
	
	public enum EnemyState {
	  MOVING, // Movement from self.position to movementTarget.position with no action
	  ATTACKING, // Movement from self.position to movementTarget.position during an action
	  STATIC, // Stay immobile with no action
	  CASTING, // Stay immobile during an action
	}

	private EnemyState _state = EnemyState.STATIC;
	private float _health;
	private float _movementSpeed;
	private Vector2 _movementTarget;
	private Vector2 _lookTarget;

	public Vector2 LookTarget { get => _lookTarget; set => _lookTarget = value; }
	public Vector2 MovementTarget { get => _movementTarget; set => _movementTarget = value; }
	public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_state = EnemyState.STATIC;
		_health = 1;
		_movementSpeed = 0.5f;
		MovementTarget = new Vector2(0,0);
		_lookTarget = new Vector2(0,0);
	}
	
	public void TakeDamage(float damage) {
		_health -= damage;
		
		if(_health == 0){
			Death();
		}
	}
	
	public void Death() {
		QueueFree();
	}
	
	public void Death(string type) {
		QueueFree();
	}
	
	private void _Moving()
	{
		Vector2 Direction = MovementTarget-this.Position;
		Velocity = Direction*_movementSpeed;
		LookAt(_lookTarget);
		MoveAndSlide();
		
		GD.Print("Boss Moving in " + Direction[0] +";"+ Direction[1] );
		
	}

	private void _Attacking()
	{
		Vector2 Direction = MovementTarget-this.Position;
		Velocity = Direction*_movementSpeed;
		LookAt(_lookTarget);
		MoveAndSlide();
	}

	private void _Static()
	{
		LookAt(_lookTarget);
	}

	private void _Casting()
	{
		LookAt(_lookTarget);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//PhysicsProcess is called at fixed interval so no need to worry about delta
	//except in very rare edge cases
	public override void _PhysicsProcess(double delta)
	{
		
		switch (_state) 
		{
			case EnemyState.MOVING:
				_Moving();
				break;
			case EnemyState.ATTACKING :
				_Attacking();
				break;
			case EnemyState.CASTING:
				_Casting();
				break;
			case EnemyState.STATIC:
				_Static();
				break;
			default :
				break;
		}
		
	}
	
}
