using UnityEditor.Tilemaps;
using UnityEngine;

namespace Player
{
    public class WallJumpState : State
    {
        private float horizontalJumpPercent = 0.25f;
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
            float wallDir = player.GetIsOnWall() ? 1f : -1f;

            //rb.linearVelocity = Vector2.zero;
            rb.linearVelocity = new Vector2(-wallDir * horizontalJumpPercent, 1f) * player.jumpForce;
            wallJumpTimer = wallJumpDuration;

            player.ForceFlip(); //flip player regardless of input state
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
                return;
            } 


            if (inputHandler != null)
            {
                float inputX = inputHandler.moveInput.x;
                rb.linearVelocity = new Vector2(inputX * player.walkSpeed, rb.linearVelocity.y);
            }

            /*bool jumpHeld = inputHandler != null && inputHandler.jumpHeld;
            if (rb.linearVelocity.y > 0f && !jumpHeld)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }*/

            base.PhysicsUpdate();
        }
    }
}
