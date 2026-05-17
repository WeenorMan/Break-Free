using UnityEngine;
namespace Player
{
    public class DamagedState : State
    {
        private float timer;
        private float knockbackVelocity;
        private float knockbackDuration;

        public DamagedState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public void SetParameters(int knockbackDirection)
        {
            knockbackVelocity = knockbackDirection * damage.knockbackForce;
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("Hurt");
            AudioManager.instance.PlaySFXClip(9);

            knockbackDuration = damage.knockbackDuration;
            rb.linearVelocity = new Vector2(knockbackVelocity, rb.linearVelocity.y);
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
            knockbackDuration -= Time.fixedDeltaTime;
            if(knockbackDuration <= 0)
            {
                rb.linearVelocity = Vector2.zero;
                sm.ChangeState(player.idleState);
            }
            else if(knockbackDuration <= 0 && !player.GetIsGrounded())
            {
                rb.linearVelocity = Vector2.zero;
                sm.ChangeState(player.fallState);
            }
        }
    }
}

