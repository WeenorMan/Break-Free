using UnityEngine;
namespace Player
{
    public class WalkState : State
    {
        public WalkState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("walk entered");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
           
            player.rb.linearVelocity = new Vector2(PlayerInputHandler.Instance.moveInput.x * player.walkSpeed,
            player.rb.linearVelocity.y);

            if (PlayerInputHandler.Instance.moveInput == Vector2.zero)
            {
                sm.ChangeState(player.idleState);
            }

            player.CheckForJump();
            player.CheckForDash();
        }

        public override void PhysicsUpdate()
        {
            player.anim.SetFloat("xVelocity", Mathf.Abs(player.rb.linearVelocity.x));
            base.PhysicsUpdate();
        }

        void CheckForJump()
        {
            if (PlayerInputHandler.Instance.jumpTriggered && player.GetIsGrounded())
            {
                sm.ChangeState(player.jumpState);
            }
        }

     
    }
}
