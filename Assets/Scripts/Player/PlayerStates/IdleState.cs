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

            player.CheckForJump();
            CheckForWalk();
            player.CheckForDash();
            base.LogicUpdate();
        }
        public override void PhysicsUpdate()
        {
            player.anim.SetFloat("xVelocity", Mathf.Abs(player.rb.linearVelocity.x));
            base.PhysicsUpdate();
        }

       

        void CheckForWalk()
        {
            if (PlayerInputHandler.Instance.moveInput != Vector2.zero)
            {
                sm.ChangeState(player.walkState);
            }
            
        }

      

    }
}