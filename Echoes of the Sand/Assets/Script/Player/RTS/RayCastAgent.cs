using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RayCastAgent : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    public Vector3 target;

    public void Update()
    {
        agent.SetDestination(target);

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                target = hit.point;
            }
        }
    }
}
