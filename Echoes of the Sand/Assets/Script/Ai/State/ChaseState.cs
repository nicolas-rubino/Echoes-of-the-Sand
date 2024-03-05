using UnityEngine;

public class ChaseState : IState
{
    private Transform cible;
    private AIControler aiControler;

    public ChaseState(AIControler newAIControler)
    {
        this.aiControler = newAIControler;
    }

    public void Enter()
    { 
    
    }

    public void Exit() 
    { 
    
    }

    public void Execute()
    {


    }



}
