using Godot;
using System;

namespace States.Worker
{
    public partial class IdleState : StateComponent
    {
        private WorkerUnitNode _worker;

        public override void Initialize()
        {
            _worker = (WorkerUnitNode)StateMachine.RootNode;

            int i = 1;
        }
    }
}

