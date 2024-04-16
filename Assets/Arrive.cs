using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Arrive : MonoBehaviour
{
    public Transform target;
 

    public NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
         
    }



    public void CambiarPuntoDeSeguimiento()
    {
        agent.SetDestination(target.position);
    }
}
