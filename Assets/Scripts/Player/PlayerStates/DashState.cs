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

        [Header("Dash Settings")]
        public float dashDuration = 0.2f;
        public float dashCooldown = 1.5f;
        public float dashSpeed = 5f;

        private bool isDashing;
        private bool hasDashed;
        private bool canDash = true;
        private float gravity;

        public override void Enter()
        {
            base.Enter();
            gravity = player.rb.gravityScale;
            Debug.Log("dash entered");

            if (canDash && !hasDashed)
            {
                isDashing = true;
                player.StartCoroutine(Dash(dashDuration, gravity, dashCooldown, dashCooldown));
            }

        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {
            if (isDashing) return;
            base.LogicUpdate();

            if (PlayerInputHandler.Instance != null && PlayerInputHandler.Instance.dashReleased)
            {
                
                sm.ChangeState(player.idleState);
            }
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        IEnumerator Dash(float dashTime, float gravity, float timeInAir, float cooldown)
        {
            player.rb.gravityScale = 0;
            
            canDash = false;
            
            while(dashDuration > 0)
            {
                if (player.isFacingRight)
                {
                    player.rb.linearVelocity = new Vector2(dashSpeed, 0f);
                }
                else
                {
                    player.rb.linearVelocity = new Vector2(-dashSpeed, 0f);
                }

                dashDuration -= Time.deltaTime;
                yield return null;
            }

            player.rb.linearVelocity = Vector2.zero;
            hasDashed = true;
            isDashing = false;
            yield return new WaitForSeconds(timeInAir);

            player.rb.gravityScale = gravity;

            if(player.GetIsGrounded())
            {
                canDash = true;
            }
           
        }

    }
}          
