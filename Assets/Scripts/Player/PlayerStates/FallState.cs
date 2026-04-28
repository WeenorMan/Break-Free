using UnityEngine;
namespace Player
{
    public class FallState : State
    {
        private float fallSpeed = 20f;

        public FallState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }
        public override void Enter()
        {
            base.Enter();
            anim.Play("Fall");
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {

            if( player.GetIsGrounded() == false )
            {
                if( player.GetIsOnWall() == true )
                {
                    if( ( inputHandler.moveInput.x > 0 && player.facingDirection == 1 ) || (inputHandler.moveInput.x < 0 && player.facingDirection == -1) )
                    {
                        if( rb.linearVelocity.y < 0.1f )
                        {
                            sm.ChangeState(player.wallSlideState);
                            return;
                        }

                    }
                }
            }


            else if (inputHandler.jumpTriggered && player.GetIsOnWall())
            {
                sm.ChangeState(player.wallJumpState);
                return;
            }

            else if (player.GetIsGrounded())
            {
                sm.ChangeState(player.idleState);
            }

            player.Flip();
            player.CheckForDash();
            player.CheckForJump();
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            if (rb.linearVelocity.y < -Mathf.Abs(fallSpeed))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -Mathf.Abs(fallSpeed), Mathf.Infinity));
            }

            if (player.rb.linearVelocity.y <= 0f && !player.GetIsGrounded())
            {
                player.rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (player.fallMultiplier - 1f) * Time.fixedDeltaTime;
            }

            if (inputHandler != null)
            {
                float inputX = inputHandler.moveInput.x;
                player.rb.linearVelocity = new Vector2(inputX * player.walkSpeed, player.rb.linearVelocity.y);
            }

        }
   
    }
}
      