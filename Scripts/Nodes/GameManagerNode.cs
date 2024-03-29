using Godot;
using System;

public partial class GameManagerNode : Node2D
{
	[Export] public Team.ColorEnum PlayerColor = Team.ColorEnum.White;

	public static GameManagerNode Instance;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
}
