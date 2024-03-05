public class StateMachine
{
    private IState currentState;

    public StateMachine(IState initialState)
    {
        currentState = initialState;
        currentState.Enter();
    }

    public void SetState(IState newState)
    {
        //same as if !null
        currentState?.Exit();

        currentState = newState;
        currentState.Enter();
    }

    public void ExecuteState()
    {
        currentState?.Execute();
    }

    public IState GetState()
    { return currentState; }




}
