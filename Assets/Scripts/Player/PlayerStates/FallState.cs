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
            Debug.Log("fall entered");
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            CheckForLand();
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

        void CheckForDash()
        {
            if (PlayerInputHandler.Instance.dashTriggered)
            {
                sm.ChangeState(player.dashState);
            }
        }
    }
}
      