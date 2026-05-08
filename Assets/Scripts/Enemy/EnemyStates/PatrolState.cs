using UnityEngine;

public class PatrolState : EState
{
    public PatrolState(Enemy enemy, EStateMachine esm) : base(enemy, esm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.Play("Walk");

    }

    public override void Exit()
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        if (senses.GetChaseTarget())
        {
            esm.ChangeState(new ChaseState(enemy, esm));
            return;
        }

        if(senses.IsHittingWall() || senses.IsAtCliff())
        {
            enemy.Flip();
            return;
        }

        rb.linearVelocity = new Vector2(config.patrolSpeed * enemy.FacingDirection, rb.linearVelocity.y);

    }
}
