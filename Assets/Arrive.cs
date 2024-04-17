using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum QueAccion
{
    wander,
    SensorDetected
}

public class Arrive : MonoBehaviour
{
    public QueAccion LaAccion = QueAccion.wander;
    #region arrive
    [Header("Arrive")]
    public Transform target;
    public NavMeshAgent agent;
    #endregion
    [Space(30)]
    #region Wander
    [Header("Wander")]
    [SerializeField] Vector3 result;
    Vector3 rp;
    [SerializeField] float range;
    #endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
         
    }



    public void CambiarPuntoDeSeguimiento()
    {
        agent.SetDestination(target.position);
    }



    public void randomPoint()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 ElRandomPoint = Vector3.zero + Random.insideUnitSphere * range;
            NavMeshHit Hit;
            if (NavMesh.SamplePosition(ElRandomPoint, out Hit, 1f, NavMesh.AllAreas))
            {
                result = Hit.position;
                break;
            }
            else
            {
                result = Vector3.zero;
            }
        }

        agent.SetDestination(result);
    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Vector3.zero, range);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rp, 1);
        Gizmos.DrawLine(transform.position + Vector3.up * 0.52f, rp + Vector3.up * 0.52f);
    }


}
