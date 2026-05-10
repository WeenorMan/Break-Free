using UnityEngine;

namespace Player
{
    public class PlayerDeathState : State
    {

        public PlayerDeathState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
           
            anim.Play("Death");
            rb.linearVelocity = Vector2.zero;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            
        }
    }
}

