using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControler : MonoBehaviour
{
    
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    private StateMachine stateMachine;
    [SerializeField] private Transform cible;

    public void Start()
    {
        
    }

    public void Update()
    {
        stateMachine.ExecuteState();
    }

    public Transform GetCible()
    {
        return cible;
    }

    public void OnConnectedToServer()
    {
        agent.SetDestination(cible.position);
    }

    private void HandleTransition()
    {
        IState currentState = stateMachine.GetState();
        switch(currentState)
        {
            case ChaseState:
                break;
        }
    }
}
