using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Provide state machine functionality to a node
/// </summary>
[GlobalClass]
public partial class StateMachineComponent : Node
{
    [Signal] public delegate void state_changeEventHandler(string stateName, Dictionary payload = null);

    [Export] protected NodePath InitialState;

    public State State;
    public Node RootNode;

    /// <summary>
    /// Set up the state machine
    /// </summary>
    public override void _Ready()
    {
        state_change += ChangeTo;
        
        RootNode = GetOwner<Node>();

        State = GetNode<State>(InitialState);

        foreach (State childState in GetChildren())
        {
            childState.StateMachine = this;
            childState.Initialize();
        }
        State.Enter();
    }

    /// <summary>
    /// Gets the name of the current state
    /// </summary>
    /// <returns></returns>
    public string StateName()
    {
        return State.Name;
    }

    /// <summary>
    /// Call the current states _Process function
    /// </summary>
    /// <param name="delta"></param>
    public override void _Process(double delta)
    {
        State._Process(delta);
    }

    /// <summary>
    /// Call the current states _PhysicsProcess function
    /// </summary>
    /// <param name="delta"></param>
    public override void _PhysicsProcess(double delta)
    {
        State._PhysicsProcess(delta);
    }

    /// <summary>
    /// Transition to a new state
    /// </summary>
    /// <param name="toState"></param>
    /// <param name="message"></param>
    public void ChangeTo(string toState, Dictionary message = null)
    {
        if (!HasNode(toState))
        {
            Log.Error(Name + ": No state matching " + toState);
            return;
        }

        State.Exit();
        State = GetNode<State>(toState);
        State.Enter(message);
        Log.Debug($"{RootNode.Name} entered state {toState}");
        EmitSignal(SignalName.state_change, toState, message);
    }
}
