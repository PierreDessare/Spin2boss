using Godot;
using System;

public partial class GameManager : Node
{
    private PackedScene Player = GD.Load<PackedScene>("res://Scenes/Player.tscn");
    
    //can have some vars here like hp modifier,player spawn location etc...
    [Export] private Vector2 _playerSpawnPosition = Vector2.Zero;
    public override void _Ready()
    {
        CharacterBody2D ply = (CharacterBody2D)Player.Instantiate();
        GetTree().CurrentScene.AddChild(ply);
        ply.Position = _playerSpawnPosition;
    }
    
    
}