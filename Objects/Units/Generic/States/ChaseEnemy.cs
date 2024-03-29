using Godot;
using Godot.Collections;
using System;
using System.Runtime.Serialization;

namespace State
{
    /// <summary>
    /// Chase down the nearest enemy
    /// </summary>
    public partial class ChaseEnemy : StateComponent
    {
        public new const string StateName = "ChaseEnemy";

        [Export] public DetectionComponent Detection;
        [Export] public NavigationComponent Navigation;
        [Export] public float AttackRange = 25f;

        /// <summary>
        /// Change to different states if the appropriate conditions are met
        /// </summary>
        /// <param name="delta"></param>
        public override void _PhysicsProcess(double delta)
        {
            UnitNode closestEnemy = Detection.GetClosestEnemy();

            // If we're out of range, return to idle
            if (closestEnemy is null)
            {
                StateMachine.ChangeTo(Idle.StateName);
                return;
            }

            // If we're in attacking range, start attacking
            if (closestEnemy.GlobalPosition.DistanceTo(Detection.GlobalPosition) <= AttackRange)
            {
                StateMachine.ChangeTo(Attack.StateName);
                return;
            }

            // Otherwise move towards the closest enemy
            Navigation.MoveTo(closestEnemy.GlobalPosition);
        }
    }
}
