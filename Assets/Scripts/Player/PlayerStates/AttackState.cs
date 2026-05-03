using Player;
using UnityEngine;

namespace Player
{
    public class AttackState : State
    {

        // constructor
        public AttackState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            if (anim == null)
            {
                Debug.LogError("AttackState: Animator is null!");
                return;
            }
            if (anim.runtimeAnimatorController == null)
            {
                Debug.LogError("AttackState: Animator controller is not assigned!");
                return;
            }
            anim.Play("Player Attack");
        }

        public override void Exit()
        {

            base.Exit();
        }

        public override void AttackAnimationFinished()
        {

            if(Mathf.Abs(inputHandler.moveInput.x) > 0.1f)
            {
                sm.ChangeState(player.walkState);
            }
            else
            {
                sm.ChangeState(player.idleState);
            }
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}