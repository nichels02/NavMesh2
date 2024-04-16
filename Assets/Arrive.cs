using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Arrive : MonoBehaviour
{
    public Transform target;
 

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
         
    }

    private void Update()
    {
        if (agent == null || target == null)
            return;

        agent.SetDestination(target.position);
    }
}
