using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerDeathState : State
    {
        private float deathDuration = 1.5f;


        public PlayerDeathState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("Death");


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

        private IEnumerator PlayerDeath()
        {
            rb.linearVelocity = Vector2.zero;
            AudioManager.instance.PlaySFXClip(8);

            yield return new WaitForSeconds(deathDuration);



        }
    }
}

