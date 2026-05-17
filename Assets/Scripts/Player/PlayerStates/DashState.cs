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
            anim.Play("Dash");
            player.gravity = rb.gravityScale;
            Debug.Log("dash entered");

            if (player.canDash)
            {
                player.StartCoroutine(Dash());
            }

        }
        public override void Exit()
        {
            base.Exit();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * 0.1f, rb.linearVelocity.y);
        }
        public override void LogicUpdate()
        {
            if (player.isDashing) return;

            player.CheckForJump();
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
            rb.gravityScale = 0f;
            AudioManager.instance.PlaySFXClip(7);

            rb.linearVelocity = new Vector2(player.transform.localScale.x * player.dashSpeed, 0f);
            Physics2D.IgnoreLayerCollision(6, 9, true);

            yield return new WaitForSeconds(player.dashDuration);
            Physics2D.IgnoreLayerCollision(6, 9, false);

            rb.gravityScale = originalGravity;
            player.isDashing = false;


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
