using Player;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class DashState : State
    {
        public DashState(PlayerScript player, StateMachine sm) : base(player, sm)
        {

        }

        

        public override void Enter()
        {
            base.Enter();
            //canDash = true;
            player.gravity = player.rb.gravityScale;
            Debug.Log("dash entered");

            //DoDash();
            if (player.canDash)
            {
                player.StartCoroutine(Dash());
            }

        }
        public override void Exit()
        {
            base.Exit();
            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x * 0.1f, player.rb.linearVelocity.y);
        }
        public override void LogicUpdate()
        {
            if (player.isDashing) return;

            base.LogicUpdate();
        }
        public override void PhysicsUpdate()
        {
            if (player.isDashing )
            {
                return;
            }
            base.PhysicsUpdate();
        }

        private IEnumerator Dash()
        {
            player.canDash = false;
            player.isDashing = true;

            float originalGravity = player.rb.gravityScale;
            player.rb.gravityScale = 0f;
            player.rb.linearVelocity = new Vector2(player.transform.localScale.x * player.dashSpeed, 0f);
            player.anim.SetBool("isDashing", true);


            yield return new WaitForSeconds(player.dashDuration);
            player.rb.gravityScale = originalGravity;
            player.isDashing = false;
            player.anim.SetBool("isDashing", false);


            if (!player.GetIsGrounded())
            {
                sm.ChangeState(player.fallState);
            }
            else
            {
                sm.ChangeState(player.idleState);

            }

            yield return new WaitForSeconds(player.dashCooldown);
            player.canDash = true;
        }
    }

}
