using UnityEngine;

public class MeleeAttackState : EState
{

    public MeleeAttackState(Enemy enemy, EStateMachine esm) : base(enemy, esm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.Play("Attack");
        rb.linearVelocity = Vector2.zero;
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
        
    }

    public override void OnAnimationFinished()
    {
        esm.ChangeState(new EnIdleState(enemy, esm));
    }
}
