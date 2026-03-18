using UnityEngine;

public class PlayerJump : BaseState<PlayerStateMachine.PlayerState>
{
    public PlayerJump(PlayerStateMachine.PlayerState key) : base(key)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Jump");
        PlayerStateMachine playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        playerStateMachine.rb.linearVelocity = new Vector2(playerStateMachine.rb.linearVelocity.x, playerStateMachine.jumpForce);
    }
    public override void ExitState()
    {
        Debug.Log("Exit Jump");
    }
    public override void UpdateState()
    {

    }
    public override PlayerStateMachine.PlayerState GetNextState()
    {
        Debug.Log(StateKey);
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
