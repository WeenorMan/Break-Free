using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class PlayerDeathState : State
    {
        private float deathDuration = 1.5f;
        private PauseScript pauseScript;


        public PlayerDeathState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            anim.Play("Death");
            if (player.isDying)
            {
                player.StartCoroutine(PlayerDeath());
            }
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

            if (pauseScript == null)
            {
                pauseScript = Object.FindFirstObjectByType<PauseScript>();
            }

            if (pauseScript != null)
            {
                pauseScript.OpenDeathMenu();
            }

            player.isDying = false;
        }
    }
}

