using Godot;
using Godot.Collections;
using System;
using System.Collections;

public partial class BaseAoe : AAoe
{
    [Export]
	Area2D col;
	Array<Node2D> hitsArray;
    public override void TriggerEffect()
	{
        hitsArray = col.GetOverlappingBodies();
        foreach (Node2D hit in hitsArray)
        {
            if (hit.IsInGroup("player"))
            {
                // Trigger hit effect on bodies
                GD.Print("Player hit");;
                EmitSignal(SignalName.SpellHit);
            }
        }
	}
    public override void Destroy()
	{
		QueueFree();
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		//GD.Print("Somebody entered");
	}
}