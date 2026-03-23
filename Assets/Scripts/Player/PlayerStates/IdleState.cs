using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Player
{
    public class IdleState : State
    {
        // constructor
        public IdleState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("idle entered");
        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void LogicUpdate()
        {
            CheckForJump();
            CheckForWalk();
            CheckForDash();
            base.LogicUpdate();
        }

        void CheckForJump()
        {
            if (PlayerInputHandler.Instance.jumpTriggered && player.GetIsGrounded())
            {
                sm.ChangeState(player.jumpState);
            }
        }

        void CheckForWalk()
        {
            if (PlayerInputHandler.Instance.moveInput != Vector2.zero)
            {
                sm.ChangeState(player.walkState);
            }
            
        }

        void CheckForDash()
        {
            if (PlayerInputHandler.Instance.dashTriggered)
            {
                Debug.Log("dash triggered");
                sm.ChangeState(player.dashState);
            }
        }

    }
}