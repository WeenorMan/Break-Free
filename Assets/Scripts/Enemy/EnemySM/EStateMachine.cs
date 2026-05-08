using UnityEngine;

public class EStateMachine 
{
    public EState CurrentState { get; private set; }
    public EState LastState { get; private set; }
    private int lastChangeFrame = -1;

    public void Init(EState startingState)
    {
        CurrentState = startingState;
        LastState = null;
        startingState.Enter();
    }

    public void ChangeState(EState newState)
    {
        //ensures that only one state change can happen per frame
        if (lastChangeFrame == Time.frameCount) return;
        lastChangeFrame = Time.frameCount;

        if (CurrentState != null) CurrentState.Exit();

        LastState = CurrentState;
        CurrentState = newState;
        newState.Enter();
    }

    public EState GetState()
    {
        return CurrentState;
    }

}
