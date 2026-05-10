using UnityEngine;

namespace Player
{
    public abstract class State
    {
        protected PlayerScript player;
        protected StateMachine sm;
        protected Animator anim;
        protected Rigidbody2D rb;
        protected PlayerInputHandler inputHandler;
        protected PlayerCombat combat;
        protected PlayerDamage damage;

        // base constructor
        protected State(PlayerScript player, StateMachine sm)
        {
            this.player = player;
            this.sm = sm;
            this.anim = player.anim;
            this.rb = player.rb;
            this.inputHandler = player.inputHandler;
            combat = player.combat;
            damage = player.damage;
        }

        // These methods are common to all states
        public virtual void Enter() { }
        public virtual void HandleInput() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void AttackAnimationFinished() { }
        public virtual void Exit() { }
        

    }

}