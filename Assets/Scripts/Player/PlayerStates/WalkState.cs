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
            anim.Play("run");
            //Debug.Log("walk entered");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
           
            rb.linearVelocity = new Vector2(PlayerInputHandler.Instance.moveInput.x * player.walkSpeed,
            rb.linearVelocity.y);

            if (PlayerInputHandler.Instance.moveInput == Vector2.zero)
            {
                sm.ChangeState(player.idleState);
            }

            /*if (player.rb.linearVelocity.y <= 0f && !player.GetIsGrounded())
            {
                sm.ChangeState(player.fallState);
            }*/

            player.CheckForJump();
            player.CheckForFall();
            player.CheckForDash();
        }

        public override void PhysicsUpdate()
        {
            anim.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
            base.PhysicsUpdate();
        }


     
    }
}
