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

            CheckForJump();
            CheckForDash();
        }

        void CheckForJump()
        {
            if (PlayerInputHandler.Instance.jumpTriggered && player.GetIsGrounded())
            {
                sm.ChangeState(player.jumpState);
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
