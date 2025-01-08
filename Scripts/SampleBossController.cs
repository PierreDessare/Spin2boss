using Godot;
using System;

public partial class SampleBossController : Node
{

    [Export]
    public PackedScene PackedSpellScene { get; set; }
    ASpell _spell;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("Fire1"))
        {
            _spell = PackedSpellScene.Instantiate<ASpell>();
            AddChild(_spell);
            _spell.SpellCasted += OnSpellCastEnded;
        }
    }

    private void OnSpellCastEnded() 
    {
        GD.Print("Cast ended");
        _spell.SpellCasted -= OnSpellCastEnded;
        _spell.Destroy();
    }
}
