using UnityEngine;


namespace Player
{
    public class StateMachine : MonoBehaviour
    {
        public State CurrentState { get; private set; }
        public State LastState { get; private set; }
        private int lastChangeFrame = -1;

        public void Init(State startingState)
        {
            CurrentState = startingState;
            LastState = null;
            startingState.Enter();
        }

        public void ChangeState(State newState)
        {
            //ensures that only one state change can happen per frame
            if (lastChangeFrame == Time.frameCount) return;
            lastChangeFrame = Time.frameCount;

            if (CurrentState != null) CurrentState.Exit();

            LastState = CurrentState;
            CurrentState = newState;
            newState.Enter();
        }

        public State GetState()
        {
            return CurrentState;
        }

    }
}