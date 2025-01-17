using Godot;
using System;

public partial class GameManager : Node
{
    private PackedScene Player = GD.Load<PackedScene>("res://Scenes/Player.tscn");
    private PackedScene HUD = GD.Load<PackedScene>("res://Scenes/HUD/HUD.tscn");
    //can have some vars here like hp modifier,player spawn location etc...
    [Export] private Vector2 _playerSpawnPosition = Vector2.Zero;
    public override void _Ready()
    {
        PlayerController ply = (PlayerController)Player.Instantiate();
        GetTree().CurrentScene.AddChild(ply);
        ply.Position = _playerSpawnPosition;
        CanvasLayer hud = (CanvasLayer)HUD.Instantiate();
        GetTree().CurrentScene.AddChild(hud);
        hud.GetNode<HealthComponent>("HealthComponent").DeathStatusChanged += OnDeathStatusChanged;
        ply.SetHealthComponent(hud.GetNode<HealthComponent>("HealthComponent"));
        ply.SetUIHealthComponent(hud.GetNode<UI_HealthComponent>("HealthBar"));
    }

    private void OnDeathStatusChanged(bool isdead)
    {
        if (isdead)
        {
            GetTree().ChangeSceneToFile("res://Scenes/TitleScreen/Title.tscn");
        }
    }
}