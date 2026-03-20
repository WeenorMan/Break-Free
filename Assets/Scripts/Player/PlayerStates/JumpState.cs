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
            Debug.Log("Jump");
            player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);

            initVelocity = player.rb.linearVelocity.x;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            if (player.GetIsGrounded() && player.rb.linearVelocity.y <= 0f)
            {
                sm.ChangeState(player.idleState);
            }

            if (player.rb.linearVelocity.y <= 0f && !player.GetIsGrounded())
            {
                sm.ChangeState(player.fallState);
            }

            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            // If ascending but the player released jump, apply extra gravity to shorten the jump.
            bool jumpHeld = PlayerInputHandler.Instance != null && PlayerInputHandler.Instance.jumpHeld;
            if (player.rb.linearVelocity.y > 0f && !jumpHeld)
            {
                player.rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (player.lowJumpMultiplier - 1f) * Time.fixedDeltaTime;
            }

            base.PhysicsUpdate();
        }

       
    }
}