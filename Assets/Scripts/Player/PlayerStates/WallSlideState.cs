using UnityEngine;

namespace Player
{
    public class WallSlideState : State
    {
        private float wallSlideSpeed = -2f;

        // constructor
        public WallSlideState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("WallSlide");

            //player.ForceFlip();
        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void LogicUpdate()
        {
            if (inputHandler.jumpTriggered && player.GetIsOnWall())
            {
                sm.ChangeState(player.wallJumpState);
                return;
            }

            else if (player.GetIsGrounded())
            {
                sm.ChangeState(player.idleState);
            }

            player.CheckForDash();


            base.LogicUpdate();
        }
        public override void PhysicsUpdate()
        {
            rb.linearVelocity = new Vector2(0, wallSlideSpeed);
            base.PhysicsUpdate();
        }
    }
}