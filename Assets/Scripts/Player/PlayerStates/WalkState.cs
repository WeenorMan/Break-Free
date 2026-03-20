using UnityEngine;
namespace Player
{
    public class WalkState : State
    {
        float walkSpeed;
        public WalkState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {

            base.Exit();
        }

        
       
    }
}
