using UnityEngine;

public class EnIdleState : EState
{
    private Transform target;

    public EnIdleState(Enemy enemy, EStateMachine esm) : base(enemy, esm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.Play("Idle");
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
        //check for target
        target = senses.GetChaseTarget();

        if (!target)
        {
            esm.ChangeState(new PatrolState(enemy, esm));
            return;
        }

        enemy.FaceTarget(target);

        //check if attack is possible
        if (senses.IsInMeleeRange(target) && combat.CanMeleeAttack())
        {
            esm.ChangeState(new MeleeAttackState(enemy, esm));
            return;
        }

        //check if target has been reached
        float distance = Mathf.Abs(target.position.x - enemy.transform.position.x);
        if (distance <= config.turnThreshold)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        //check for obstacles
        if (senses.IsHittingWall() || senses.IsAtCliff())
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        //change to chase state if target has not been reached and there are no obstacles in the way
        esm.ChangeState(new ChaseState(enemy, esm));
    }


}
