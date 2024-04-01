using Godot;
using System;

public partial class BuildingNode : Area2D
{
	[Export] public BuildingResource Resource;

	public BuildingSpriteComponent BuildingSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BuildingSprite = GetNode<BuildingSpriteComponent>("BuildingSprite");
		BuildingSprite.Texture = Resource.SpriteSheet;
		BuildingSprite.Constructed = true;
		BuildingSprite.Color = Team.ColorEnum.Green;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
