using Player;
using Unity.VisualScripting;
using UnityEngine;
namespace Player
{
    public class JumpState : State
    {
        public JumpState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Jump");
            player.rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void LogicUpdate()
        {
            if (!PlayerInputHandler.Instance.jumpTriggered && player.rb.linearVelocity.y > 0)
            {
                
                player.rb.AddForce(Vector2.down * player.jumpForce * 0.5f, ForceMode2D.Impulse);

            }


            if (player.GetIsGrounded())
            {
                sm.ChangeState(player.idleState);
            }

        }
    }
}