using UnityEngine;

public class PatrolState : EState
{
    public PatrolState(Enemy enemy, EStateMachine esm) : base(enemy, esm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Patrol State");
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
        rb.linearVelocity = new Vector2(5, rb.linearVelocity.y);

    }
}
