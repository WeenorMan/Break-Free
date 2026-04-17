using UnityEditor.Tilemaps;
using UnityEngine;

namespace Player
{
    public class WallJumpState : State
    {
        private float horizontalJumpPercent = .5f;

        // constructor
        public WallJumpState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("WallJump");

            rb.linearVelocity = Vector2.zero;
            rb.linearVelocity = new Vector2(-inputHandler.moveInput.x * horizontalJumpPercent, 1f) * player.jumpForce;

            anim.Play("Jump");

            player.ForceFlip(); //flip player regardless of input state
        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void LogicUpdate()
        {
            if (!player.GetIsGrounded() && player.GetIsOnWall() && inputHandler.moveInput.x == player.facingDirection && rb.linearVelocity.y < 0.1f)
            {
                sm.ChangeState(player.wallSlideState);
                return;
            }

            else if (inputHandler.jumpTriggered && player.GetIsOnWall())
            {
                sm.ChangeState(player.wallJumpState);
                return;
            }

            else if(player.GetIsGrounded())
            {
                sm.ChangeState(player.idleState);
            }

            player.CheckForFall();
            player.CheckForDash();
            base.LogicUpdate();
        }
        public override void PhysicsUpdate()
        {
            bool jumpHeld = inputHandler != null && inputHandler.jumpHeld;
            if (rb.linearVelocity.y > 0f && !jumpHeld)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }

            base.PhysicsUpdate();
        }
    }
}
