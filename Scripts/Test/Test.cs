using Godot;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

public partial class Test : Node2D
{
	private WorldNode WorldNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WorldNode = GetNode<WorldNode>("World");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
