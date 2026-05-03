using UnityEngine;
namespace Player
{
    public class WalkState : State
    {
        public WalkState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("Run");
            //Debug.Log("walk entered");
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (inputHandler.attackTriggered && combat.canAttack)
            {
                sm.ChangeState(player.attackState);
            }

            if (inputHandler.moveInput == Vector2.zero)
            {
                sm.ChangeState(player.idleState);
            }

            rb.linearVelocity = new Vector2(inputHandler.moveInput.x * player.walkSpeed,
            rb.linearVelocity.y);

            player.Flip();
            player.CheckForJump();
            player.CheckForFall();
            player.CheckForDash();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }


     
    }
}
