using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Player
{
    public class IdleState : State
    {
        // constructor
        public IdleState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("Idle");
            player.Flip();



            rb.linearVelocity = Vector2.zero;

        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void LogicUpdate()
        {
            

            player.CheckForAttack();
            player.CheckForJump();
            player.CheckForWalk();
            player.CheckForDash();

            base.LogicUpdate();
        }
        public override void PhysicsUpdate()
        {

            base.PhysicsUpdate();
        }
    }
}