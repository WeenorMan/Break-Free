using UnityEngine;

public class PlayerWalk : BaseState<PlayerStateMachine.PlayerState>
{
    public PlayerWalk(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Walk");
        PlayerStateMachine playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        playerStateMachine.rb.linearVelocity = new Vector2(playerStateMachine.walkSpeed
            * PlayerInputHandler.Instance.MoveInput.x, playerStateMachine.rb.linearVelocity.y);
    }
    public override void ExitState()
    {
        Debug.Log("Exit Walk");
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
