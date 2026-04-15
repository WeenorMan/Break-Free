using UnityEngine;
namespace Player
{
    public class FallState : State
    {
        public FallState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }
        public override void Enter()
        {
            base.Enter();
            player.anim.Play("fall");
            Debug.Log("fall entered");
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            player.anim.SetBool("isJumping", !player.GetIsGrounded());

            CheckForLand();
            player.CheckForDash();
            player.CheckForJump();
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            if (player.rb.linearVelocity.y <= 0f && !player.GetIsGrounded())
            {
                player.rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (player.fallMultiplier - 1f) * Time.fixedDeltaTime;
            }

            if (PlayerInputHandler.Instance != null)
            {
                float inputX = PlayerInputHandler.Instance.moveInput.x;
                player.rb.linearVelocity = new Vector2(inputX * player.walkSpeed, player.rb.linearVelocity.y);
            }

        }

        void CheckForLand()
        {
            if (player.GetIsGrounded())
            {
                sm.ChangeState(player.idleState);
            }
        }

   
    }
}
      