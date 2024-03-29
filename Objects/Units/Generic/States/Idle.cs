using Godot;
using Godot.Collections;
using System;

namespace State
{
    /// <summary>
    /// Standby state - determine action based on unit type
    /// </summary>
    public partial class Idle : StateComponent
    {
        public new const string StateName = "Idle";

        [Export] public DetectionComponent Detection;
        [Export] public bool IsAttacker;

        /// <summary>
        /// Change to different states if the appropriate conditions are met
        /// </summary>
        /// <param name="delta"></param>
        public override void _PhysicsProcess(double delta)
        {
            if (IsAttacker && Detection.GetClosestEnemy() is UnitNode)
            {
                StateMachine.ChangeTo(ChaseEnemy.StateName);
            }
        }
    }
}
