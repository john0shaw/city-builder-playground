using Godot;
using System;

/// <summary>
/// Handles basic unit animation based on velocity and if an action is being done
/// </summary>
public partial class UnitAnimationComponent : AnimationPlayer
{
	private UnitNode _unitNode;
	private bool _inActionAnimation = false;

	/// <summary>
	/// Pass the unit to the animation node from the parent
	/// </summary>
	/// <param name="unitNode"></param>
	public void Initialize(UnitNode unitNode)
	{
		_unitNode = unitNode;
	}

	/// <summary>
	/// Play walking animation if no action and moving
	/// </summary>
	/// <param name="delta"></param>
    public override void _Process(double delta)
    {
		if (_inActionAnimation) return;

		if (_unitNode.Velocity == Vector2.Zero)
		{
			Play("Idle/Down");
		}
		else
		{
			if (Mathf.Abs(_unitNode.Velocity.X) > Mathf.Abs(_unitNode.Velocity.Y))
			{
				if (_unitNode.Velocity.X > 0)
				{
					Play("Walk/Right");
				}
				else
				{
					Play("Walk/Left");
				}
			}
			else
			{
				if (_unitNode.Velocity.Y > 0)
				{
					Play("Walk/Down");
				}
				else
				{
					Play("Walk/Up");
				}
			}
		}
    }

	/// <summary>
	/// Play the action animation and block other animations until complete.  Relevant
	/// action animation is based on position to nodeForAction
	/// </summary>
	/// <param name="nodeForAction"></param>
	public async void PlayActionAnimation(Node2D nodeForAction)
	{
		_inActionAnimation = true;

        if (_unitNode.GlobalPosition.Y > nodeForAction.GlobalPosition.Y)
        {
            Play("ActionUp");
        }
        else if (_unitNode.GlobalPosition.Y < nodeForAction.GlobalPosition.Y)
        {
            Play("ActionDown");
        }
        else if (_unitNode.GlobalPosition.X > nodeForAction.GlobalPosition.X)
        {
            Play("ActionRight");
        }
        else if (_unitNode.GlobalPosition.X < nodeForAction.GlobalPosition.X)
        {
            Play("ActionLeft");
        }
        else
        {
            Play("ActionDown");
        }

        await ToSignal(this, "animation_finished");
        _inActionAnimation = false;
    }
}
