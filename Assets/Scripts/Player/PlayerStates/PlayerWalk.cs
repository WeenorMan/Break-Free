using UnityEngine;

public class PlayerWalk : BaseState<PlayerStateMachine.PlayerState>
{
    public PlayerWalk(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {

    }
    public override void ExitState()
    {

    }
    public override void UpdateState()
    {

    }
    public override PlayerStateMachine.PlayerState GetNextState()
    {
        return StateKey;
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {

    }
    public override void OnTriggerStay2D(Collider2D other)
    {

    }
    public override void OnTriggerExit2D(Collider2D other)
    {

    }
}
