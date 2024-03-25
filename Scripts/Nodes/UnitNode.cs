using Godot;
using System;

public partial class UnitNode : CharacterBody2D
{
	[Export] public UnitResource Resource;

	protected AnimationPlayer _animationPlayer;
	protected SpriteComponent _sprite;
	protected StateMachineComponent _stateMachine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_stateMachine = GetNode<StateMachineComponent>("StateMachineComponent");

		_sprite = GetNode<SpriteComponent>("SpriteComponent");
		_sprite.Configure(Resource);

		_animationPlayer.Play("Idle/Down");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
