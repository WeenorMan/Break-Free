using Player;
using Unity.VisualScripting;
using UnityEngine;
namespace Player
{
    public class JumpState : State
    {
        public float initVelocity;

        public JumpState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("Jump");
            rb.linearVelocity = new Vector2 (rb.linearVelocity.x, 0);

            rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);

            initVelocity = rb.linearVelocity.x;
        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void LogicUpdate()
        {
            if(!player.GetIsGrounded() && player.GetIsOnWall() && inputHandler.moveInput.x == player.facingDirection && rb.linearVelocity.y < 0.1f)
            {
                sm.ChangeState(player.wallSlideState);
                return;
            } 

            else if(inputHandler.jumpTriggered && player.GetIsOnWall())
            {
                sm.ChangeState(player.wallJumpState); 
                return;
            }

            else if (player.GetIsGrounded() && rb.linearVelocity.y <= 0f)
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
            bool jumpHeld = inputHandler != null && inputHandler.jumpHeld;
            if (rb.linearVelocity.y > 0f && !jumpHeld)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }

            if (inputHandler != null)
            {
                float inputX = inputHandler.moveInput.x;
                rb.linearVelocity = new Vector2(inputX * player.walkSpeed, rb.linearVelocity.y);
            }


            base.PhysicsUpdate();
        }

      
    }
}