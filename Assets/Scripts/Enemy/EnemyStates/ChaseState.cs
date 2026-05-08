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
        Debug.Log("Chasing");
    }
}
