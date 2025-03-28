using System;
using UnityEngine;

public abstract class BaseStateMachine<T> where T : MonoBehaviour
{
    public IState<T> CurrentState { get; protected set; }
    public event Action<IState<T>> OnStateChanged;

    public void Initialize(IState<T> state)
    {
        CurrentState = state;
        CurrentState.OnEnter();
        OnStateChanged?.Invoke(state);
    }

    public void TransitionTo(IState<T> nextState)
    {
        CurrentState?.OnExit();
        CurrentState = nextState;
        CurrentState.OnEnter();
        OnStateChanged?.Invoke(nextState);
    }

    public void Update() => CurrentState?.OnUpdate();
}
