using Godot;
using Godot.Collections;

/// <summary>
/// Move to a target position
/// </summary>
namespace State
{
    public partial class MoveToPosition : StateComponent
    {
        public const string POSITION_KEY = "position";

        public new const string StateName = "MoveToPosition";

        [Export] public NavigationComponent Navigation;

        private Vector2 _target = Vector2.Inf;
        private UnitNode _unitNode;

        /// <summary>
        /// Set navigation to the target
        /// </summary>
        /// <param name="message"></param>
        public override void Enter(Dictionary message = null)
        {
            if (message.ContainsKey(POSITION_KEY))
            {
                Navigation.MoveTo((Vector2)message[POSITION_KEY]);
                Navigation.NavigationFinished += _on_arrived_at_target;
            }
        }

        /// <summary>
        /// Deregister for arrival notification
        /// </summary>
        public override void Exit()
        {
            Navigation.NavigationFinished -= _on_arrived_at_target;
        }

        /// <summary>
        /// Go back to idling when we've reached our target
        /// </summary>
        public void _on_arrived_at_target()
        {
            StateMachine.ChangeTo(Idle.StateName);
        }
    }
}
