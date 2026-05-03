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



            rb.linearVelocity = new Vector2 (0, 0);

        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void LogicUpdate()
        {
            if(inputHandler.attackTriggered && combat.canAttack)
            {
                sm.ChangeState(player.attackState);
            }


            player.CheckForJump();
            player.CheckForWalk();
            player.CheckForDash();

            base.LogicUpdate();
        }
        public override void PhysicsUpdate()
        {
            //player.anim.SetFloat("xVelocity", Mathf.Abs(player.rb.linearVelocity.x));
            base.PhysicsUpdate();
        }
    }
}