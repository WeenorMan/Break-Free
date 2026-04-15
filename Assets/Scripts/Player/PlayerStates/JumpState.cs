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
            //Debug.Log("Jump");
            player.anim.Play("jump");
            player.rb.linearVelocity = new Vector2 (player.rb.linearVelocity.x, 0);

            player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);

            initVelocity = player.rb.linearVelocity.x;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            player.anim.SetBool("isJumping", !player.GetIsGrounded());


            if (player.GetIsGrounded() && player.rb.linearVelocity.y <= 0f)
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
            if (player.rb.linearVelocity.y > 0f && !jumpHeld)
            {
                player.rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }

            if (PlayerInputHandler.Instance != null)
            {
                float inputX = PlayerInputHandler.Instance.moveInput.x;
                player.rb.linearVelocity = new Vector2(inputX * player.walkSpeed, player.rb.linearVelocity.y);
            }

            player.anim.SetFloat("yVelocity", player.rb.linearVelocity.y);


            base.PhysicsUpdate();
        }

      
    }
}