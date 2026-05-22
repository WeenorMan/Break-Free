using UnityEngine;

public class ChaseState : EState
{
    private Transform target;

    public ChaseState(Enemy enemy, EStateMachine esm) : base(enemy, esm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        anim.Play("Run");
    }

    public override void Exit()
    {
        base.Exit();
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
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
        if(distance <= config.turnThreshold)
        {
            esm.ChangeState(new EnIdleState(enemy, esm));
            return;
        }

        //check for obstacles
        if(senses.IsHittingWall() || senses.IsAtCliff())
        {
            esm.ChangeState(new EnIdleState(enemy, esm));
            return;
        }

        //move towards target
        rb.linearVelocity = new Vector2(config.chaseSpeed * enemy.FacingDirection, rb.linearVelocity.y);
    }


}
