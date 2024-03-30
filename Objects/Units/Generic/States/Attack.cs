using Godot;
using Godot.Collections;
using System;

/// <summary>
/// Move to a target position
/// </summary>
namespace State
{
    public partial class Attack : StateComponent
    {
        public new const string StateName = "Attack";

        public const string TARGET_KEY = "Target";

        [Export] public UnitAnimationComponent Animation;
        [Export] public DetectionComponent Detection;
        [Export] public float DamageMin;
        [Export] public float DamageMax;

        private Node2D _target;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void Enter(Dictionary message = null)
        {
            if (message.ContainsKey(TARGET_KEY))
            {
                _target = (Node2D)message[TARGET_KEY];
                Animation.PlayActionAnimation(_target);
                Animation.AnimationFinished += _on_attack_finished;
                Animation.ActionTriggered += _on_action_triggered;
            }
        }

        /// <summary>
        /// Do damage!
        /// </summary>
        private void _on_action_triggered()
        {
           if (_target is UnitNode unitTarget)
           {
                unitTarget.TakeDamage(RNG.RandF(DamageMin, DamageMax));
           }
        }

        /// <summary>
        /// Wait until the animation is finished, then look for the next close enemy
        /// </summary>
        /// <param name="animName"></param>
        private void _on_attack_finished(StringName animName)
        {
            Animation.AnimationFinished -= _on_attack_finished;
            Animation.ActionTriggered -= _on_action_triggered;
            StateMachine.ChangeTo(ChaseEnemy.StateName);
        }
    }
}
