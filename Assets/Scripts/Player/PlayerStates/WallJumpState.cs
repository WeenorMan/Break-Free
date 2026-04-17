using UnityEditor.Tilemaps;
using UnityEngine;

namespace Player
{
    public class WallJumpState : State
    {
        // constructor
        public WallJumpState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("WallJump");

            rb.linearVelocity = Vector2.zero;
            rb.linearVelocity = new Vector2(-PlayerInputHandler.Instance.moveInput.x, 1f) * player.jumpForce;

            player.ForceFlip(); //flip player regardless of input state
        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void LogicUpdate()
        {
            
            base.LogicUpdate();
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
