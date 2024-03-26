using Godot;
using System;
using Godot.Collections;

public partial class UnitNode : CharacterBody2D
{
	[Export] public UnitResource Resource;

	public UnitAnimationComponent Animation;
    public SpriteComponent Sprite;
    public StateMachineComponent StateMachine;
    public NavigationComponent Navigation;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		Animation = GetNode<UnitAnimationComponent>("AnimationComponent");
		StateMachine = GetNode<StateMachineComponent>("StateMachineComponent");
		Navigation = GetNode<NavigationComponent>("NavigationComponent");
		Sprite = GetNode<SpriteComponent>("SpriteComponent");

        Animation.Initialize(this);
		Navigation.Initialize(this);
        Sprite.Initialize(Resource);
		
	}

    public override void _Input(InputEvent @event)
    {
        if (Mouse.IsRightClick(@event))
		{
			StateMachine.ChangeTo("MoveToPosition", new Dictionary { { MoveToPosition.POSITION_KEY, GetGlobalMousePosition() }  } );
		}
    }
}
