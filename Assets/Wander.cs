using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    [Range(1,100)]
    public float rango;
    Vector3 rp;
    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        randomPoint(transform.position, rango, out rp);
    }
    bool randomPoint(Vector3 center, float range, out Vector3 result)
    {
        for(int i = 0; i < 30; i++)
        {
            Vector3 ElRandomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit Hit;
            if (NavMesh.SamplePosition(ElRandomPoint, out Hit, 1f, NavMesh.AllAreas))
            {
                result = Hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
        

    }
    private void Update()
    {
        if (agent == null)
            return;

        agent.SetDestination(rp);
        if(agent.remainingDistance<agent.stoppingDistance)
            randomPoint(transform.position, rango, out rp);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rango);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rp, 1);
        Gizmos.DrawLine(transform.position+Vector3.up*0.52f, rp+Vector3.up * 0.52f);
    }
}
