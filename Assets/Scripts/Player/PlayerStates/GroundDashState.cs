using Player;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class GroundDashState : State
    {
        public GroundDashState(PlayerScript player, StateMachine sm) : base(player, sm)
        {

        }

        [Header("Dash Settings")]
        public float dashDuration = 0.2f;
        public float dashCooldown = 1f;
        public float dashSpeed = 10f;

        private bool isDashing;
        private bool hasDashed;
        private bool canDash = true;
        private float gravity;

        public override void Enter()
        {
            base.Enter();
            //canDash = true;
            gravity = player.rb.gravityScale;
            Debug.Log("dash entered");

            DoDash();

        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void LogicUpdate()
        {

            base.LogicUpdate();

            //do cooldown
            dashCooldown -= Time.deltaTime;
            if (dashCooldown <= 0)
            {
                //check button
                if (PlayerInputHandler.Instance.dashTriggered)
                {
                    DoDash();
                }

                //count down dash duration
                dashDuration -= Time.deltaTime;

                if(dashDuration <= 0)
                {
                    if (PlayerInputHandler.Instance.dashTriggered)
                    {
                        DoDash();
                    }
                    else
                    {
                        sm.ChangeState(player.idleState);
                    }
                }
            }

        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        /*
        IEnumerator Dash(float dashTime, float gravity, float timeInAir, float cooldown)
        {
            player.rb.gravityScale = 0;

            canDash = false;

            while (dashDuration > 0)
            {


                dashDuration -= Time.deltaTime;
                yield return null;
            }

            player.rb.linearVelocity = Vector2.zero;
            hasDashed = true;
            isDashing = false;
            yield return new WaitForSeconds(timeInAir);

            player.rb.gravityScale = gravity;

            if (player.GetIsGrounded())
            {
                canDash = true;
            }

        }
        */


        void DoDash()
        {
            dashCooldown = 1f;
            dashDuration = 0.2f;

            if (player.isFacingRight)
            {
                player.rb.linearVelocity = new Vector2(dashSpeed, 0f);

            }
            else
            {
                player.rb.linearVelocity = new Vector2(-dashSpeed, 0f);
            }
        }
    }

}
