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
            anim.Play("jump");
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
            if(PlayerInputHandler.Instance.jumpTriggered && player.GetIsOnWall())
            {
                sm.ChangeState(player.wallJumpState);
            }

            else if (player.GetIsGrounded() && rb.linearVelocity.y <= 0f)
            {
                sm.ChangeState(player.idleState);
            }

            player.CheckForFall();
            player.CheckForDash();

            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            bool jumpHeld = PlayerInputHandler.Instance != null && PlayerInputHandler.Instance.jumpHeld;
            if (rb.linearVelocity.y > 0f && !jumpHeld)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }

            if (PlayerInputHandler.Instance != null)
            {
                float inputX = PlayerInputHandler.Instance.moveInput.x;
                rb.linearVelocity = new Vector2(inputX * player.walkSpeed, rb.linearVelocity.y);
            }


            base.PhysicsUpdate();
        }

      
    }
}