using UnityEngine;

public class PlayerStateMachine : StateManager<PlayerStateMachine.PlayerState>
{
    public enum PlayerState
    {
        PlayerIdle,
        PlayerWalk,
        Jump,
        Attack,
    }

    private void Awake()
    {
        InitializeStates();
        CurrentState = States[PlayerState.PlayerIdle];

    }

    private void InitializeStates()
    {
        States.Add(PlayerState.PlayerIdle, new PlayerIdle(PlayerState.PlayerIdle));
        States.Add(PlayerState.PlayerWalk, new PlayerWalk(PlayerState.PlayerWalk));

    }
}
