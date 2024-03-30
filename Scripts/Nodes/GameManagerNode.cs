using Godot;
using System;
using System.Collections.Generic;

public partial class GameManagerNode : Node2D
{
	[Export] public Team.ColorEnum PlayerColor = Team.ColorEnum.White;

	public static GameManagerNode Instance;

	public UnitManagerNode UnitManager;

	private List<UnitNode> _selectedUnits = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		UnitManager = GetNode<UnitManagerNode>("World/UnitManager");
	}
}
