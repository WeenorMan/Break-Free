using UnityEngine;

public abstract class EState 
{
    protected Enemy enemy;
    protected EStateMachine esm;
    protected Rigidbody2D rb;
    protected EnemyConfig config;
    protected Animator anim;
    protected EnemySenses senses;
    protected EnemyCombat combat;
    protected EState(Enemy enemy, EStateMachine esm)
    {
        this.enemy = enemy;
        this.esm = esm;
        this.rb = enemy.RB;
        this.config = enemy.Config;
        this.senses = enemy.Senses;
        this.anim = enemy.Animator;
        combat = enemy.Combat;
    }

    public virtual void Enter() { }
    public virtual void LogicUpdate() { }
    public virtual void PhysicsUpdate() { }
    public virtual void OnAnimationFinished() { }
    public virtual void Exit() { }
}
