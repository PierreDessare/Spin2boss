using Godot;
using System;
using System.Collections.Generic;

public partial class EnemyController : CharacterBody2D
{
	
	
	public enum EnemyState {
	  MOVING, // Movement from self.position to movementTarget.position with no action
	  ATTACKING, // Movement from self.position to movementTarget.position during an action
	  IDLE, // Stay immobile with no action
	  CASTING, // Stay immobile during an action
	}

	private EnemyState _state = EnemyState.IDLE;
	private float _health;
	private float _movementSpeed;
	private Vector2 _movementTarget;
	private Vector2 _lookTarget;
	private Dictionary<int,Action> _patternMap; // add action patterns in _Ready.
	private bool _patternActive = false; // Used to check if the boss is currently in an action pattern.

	public Vector2 LookTarget { get => _lookTarget; set => _lookTarget = value; }
	public Vector2 MovementTarget { get => _movementTarget; set => _movementTarget = value; }
	public float MovementSpeed { get => _movementSpeed; set => _movementSpeed = value; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_state = EnemyState.IDLE;
		_health = 1;
		_movementSpeed = 100f;
		_movementTarget = new Vector2(0,0);
		_lookTarget = new Vector2(0,0);
		
		_patternMap = new Dictionary<int,Action>{}; // add attack patterns here 
	}
	
	public void TakeDamage(float damage) {
		_health -= damage;
		
		if(_health == 0){
			Death();
		}
	}
	
	//Generic Death. Should be enough in most cases.
	public void Death() {
		QueueFree();
	}
	
	//Specific Death. Could be used to have special effects on death
	//(e.g. : A plant dying from fire could have a death animation where it burns out)
	public void Death(string type) {
		QueueFree();
	}
	
	private void _Moving()
	{
		Vector2 Direction =  _movementTarget-this.Position;
		
		float correctionX = 1f;
		float correctionY = 1f;
		if(Math.Abs(Direction[0]) > Math.Abs(Direction[1]) + _movementSpeed/10)
		{
			correctionX = 1.5f;
			correctionY= 0.75f;
		}
		else if(Math.Abs(Direction[1]) > Math.Abs(Direction[0]) + _movementSpeed/10)
		{
			correctionY = 1.5f;
			correctionX = 0.75f;
		}
		if (Math.Abs(Direction[0]) <= _movementSpeed/25){
			correctionX = 0;
			SetPosition(new Vector2(MovementTarget[0], Position[1]));
		}
		if (Math.Abs(Direction[1]) <= _movementSpeed/25){
			correctionY = 0;
			SetPosition(new Vector2(Position[0], MovementTarget[1]));
		}
		
		Velocity = new Vector2(	_movementSpeed * Math.Sign(Direction[0]) * correctionX,
								_movementSpeed * Math.Sign(Direction[1]) * correctionY);
		LookAt(_lookTarget);
		MoveAndSlide();
		
	}

	private void _Attacking()
	{
		Vector2 Direction =  _movementTarget-this.Position;
		
		float correctionX = 1f;
		float correctionY = 1f;
		if(Math.Abs(Direction[0]) > Math.Abs(Direction[1]) + _movementSpeed/10)
		{
			correctionX = 1.5f;
			correctionY= 0.75f;
		}
		else if(Math.Abs(Direction[1]) > Math.Abs(Direction[0]) + _movementSpeed/10)
		{
			correctionY = 1.5f;
			correctionX = 0.75f;
		}
		if (Math.Abs(Direction[0]) <= _movementSpeed/25){
			correctionX = 0;
			SetPosition(new Vector2(MovementTarget[0], Position[1]));
		}
		if (Math.Abs(Direction[1]) <= _movementSpeed/25){
			correctionY = 0;
			SetPosition(new Vector2(Position[0], MovementTarget[1]));
		}
		
		Velocity = new Vector2(	_movementSpeed * Math.Sign(Direction[0]) * correctionX,
								_movementSpeed * Math.Sign(Direction[1]) * correctionY);
		LookAt(_lookTarget);
		MoveAndSlide();
	}

	private void _Idle()
	{
		LookAt(_lookTarget);
	}

	private void _Casting()
	{
		LookAt(_lookTarget);
	}
	
	private void _RandomActionSelection(){
		int value = GD.RandRange(0,_patternMap.Count);
		if (_patternMap.TryGetValue(value, out var method))
		{
			method();
		}
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
			case EnemyState.IDLE:
				_Idle();
				break;
			default :
				break;
		}

		if (!_patternActive){
			_RandomActionSelection();
		}

	}
	
}
