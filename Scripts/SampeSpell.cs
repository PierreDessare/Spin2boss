using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class SampeSpell : ASpell
{

    [Export]
    public PackedScene PackedAOEScene { get; set; }

    [Export]
    public Timer CastTimer { get; set; }

    private BaseAoe _aoe;
    private Array<AAoe> _aaoe = new Array<AAoe>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        //_aoe = PackedAOEScene.Instantiate<BaseAoe>();
		CastTimer.WaitTime = castTime;
		CastTimer.Start();
		CastStart();
    }

	public override void CastStart()
	{
        foreach (Marker2D item in GetTree().Root.GetNode("Main/Arena/Marker").GetChildren())
		{
            //AAoe aled = PackedAOEScene.Instantiate<AAoe>();
            GD.Print(item);
            //GD.Print(_aaoe.si);
            _aaoe.Add(PackedAOEScene.Instantiate<AAoe>());
            //_aaoe[count] = PackedAOEScene.Instantiate<AAoe>();
			AddChild(_aaoe.Last());
            //_aoe.Position = item.Position;
            _aaoe.Last().GlobalPosition = item.Position;
		}
		GD.Print("Sample boss is casting");
    }

	public override void CastEnd()
	{
        foreach (AAoe item in _aaoe)
        {
			item.TriggerEffect();
        }
        //_aoe.TriggerEffect();
        EmitSignal(SignalName.SpellCasted);
	}

	public override void Destroy()
	{
        foreach (AAoe item in _aaoe)
        {
            item.Destroy();
        }
        //_aoe.Destroy();
		QueueFree();
	}
}
