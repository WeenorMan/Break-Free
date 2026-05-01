using UnityEditor.Tilemaps;
using UnityEngine;

namespace Player
{
    public class WallJumpState : State
    {
        private float horizontalJumpPercent = 0.3f;
        private float wallJumpDuration = 0.3f;        
        private float wallJumpTimer;

        // constructor
        public WallJumpState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            anim.Play("Jump");
            if (inputHandler.moveInput.x <= 0)
            {
                rb.linearVelocity = new Vector2(-player.facingDirection * horizontalJumpPercent, 1f) * player.jumpForce;

            }
            else
            {
                rb.linearVelocity = new Vector2(-inputHandler.moveInput.x * horizontalJumpPercent, 1f) * player.jumpForce;

            }
            wallJumpTimer = wallJumpDuration;

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

            else if(player.GetIsGrounded())
            {
                sm.ChangeState(player.idleState);
            }

            player.Flip();
            player.CheckForFall();
            player.CheckForDash();
            base.LogicUpdate();
        }
        public override void PhysicsUpdate()
        {
            if(wallJumpTimer > 0)
            {
                wallJumpTimer -= Time.deltaTime;
            } 
            else
            {
                // after the lock expires allow horizontal control
                if (inputHandler != null)
                {
                    float inputX = inputHandler.moveInput.x;
                    rb.linearVelocity = new Vector2(inputX * player.walkSpeed, rb.linearVelocity.y);
                }
            }

            // Variable jump height: if player released jump while ascending, cancel upward velocity.
            bool jumpHeld = inputHandler != null && inputHandler.jumpHeld;
            if (rb.linearVelocity.y > 0f && !jumpHeld)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            }

            base.PhysicsUpdate();
        }
    }
}
