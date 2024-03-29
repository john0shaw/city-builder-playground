using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Base state class - not intended for specific use
/// </summary>
[GlobalClass]
public partial class StateComponent : Node
{
    public const string DEFAULT_STATE_NAME = "StateComponent";

    public StateMachineComponent StateMachine;
    public const string StateName = "StateComponent";

    /// <summary>
    /// Change the node name to match the given state name
    /// </summary>
    public override void _Ready()
    {
        if (StateName == "StateComponent")
            Log.Error($"'{StateMachine.RootNode.Name}' has default named StateComponent class on '{Name}'");

        Name = StateName;
    }

    /// <summary>
    /// Non-Physics tasks for the state
    /// </summary>
    /// <param name="delta"></param>
    public new virtual void _Process(double delta)
    {
        return;
    }

    /// <summary>
    /// Physics related tasks for the state
    /// </summary>
    /// <param name="delta"></param>
    public new virtual void _PhysicsProcess(double delta)
    {
        return;
    }

    /// <summary>
    /// Initialize the state
    /// </summary>
    public virtual void Initialize()
    {
        return;
    }

    /// <summary>
    /// Set up the state for processing
    /// </summary>
    /// <param name="message"></param>
    public virtual void Enter(Dictionary message = null)
    {
        return;
    }

    /// <summary>
    /// Clean up or do additional tasks when the state ends
    /// </summary>
    public virtual void Exit()
    {
        return;
    }
}
