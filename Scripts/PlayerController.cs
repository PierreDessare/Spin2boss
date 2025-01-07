using Godot;
using System;

public partial class PlayerController : Sprite2D
{
	private float _baseSpeed = 500f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//Things that are continuous(i.e where the player hold the key down) are handled here
	//If you don't multiply stuff by delta it will be frame-rate tied
	//meaning it will be called as many times as frames are drawn(60 fps=60 execution per sec)
	//this is not always a problem but good to keep in mind for later
	public override void _Process(double delta)
	{
		//So for example here I will _baseSpeed and multiply it by delta
		//this will mean that the speed value will be scaled down if the fps is high(delta is low)
		//and scaled up if the fps is low(delta is high)
		float _tmpSpeed = _baseSpeed * (float)delta;
		if(Input.IsActionPressed("Up",true))
		{
			Position += new Vector2(0, -_tmpSpeed);
		}

		if (Input.IsActionPressed("Down",true))
		{
			Position += new Vector2(0, _tmpSpeed);
		}

		if (Input.IsActionPressed("Left",true))
		{
			Position += new Vector2(-_tmpSpeed, 0);
		}

		if (Input.IsActionPressed("Right",true))
		{
			Position += new Vector2(_tmpSpeed, 0);
		}
		
	}

	//Things that are not continuous(i.e input where the player does not hold the key down)
	//should be handled here,instead of doing Input.XX do @event.XX
	public override void _UnhandledInput(InputEvent @event)
	{
		
	}
}
