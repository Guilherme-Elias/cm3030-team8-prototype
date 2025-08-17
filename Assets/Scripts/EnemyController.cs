using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    
    public float lookRadius = 5f;
    public float rotationSmoothness = 3f;

    private void Update()
    {
        if (agent == null)
        {
            return; // when the agent is destroyed by the player
        }

        Vector3 agentPosition = agent.transform.position;
        Vector3 targetPosition = target.position;

        float distanceToPlayer = Vector3.Distance(agentPosition, targetPosition);

        if (distanceToPlayer <= lookRadius)
        {
            agent.SetDestination(targetPosition);

            if (distanceToPlayer <= agent.stoppingDistance)
            {                
                FaceTarget(agent, target);
                AttackTarget();
            }
        }
    }

    void FaceTarget(NavMeshAgent agent, Transform target)
    {
        Vector3 agentPosition = agent.transform.position;
        Vector3 targetPosition = target.position;
        Quaternion agentRotation = agent.transform.rotation;

        Vector3 direction = (targetPosition - agentPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // smoothing the rotation to the target
        transform.rotation = Quaternion.Slerp(agentRotation, lookRotation, Time.deltaTime * this.rotationSmoothness);
    }

    void AttackTarget()
    {
        return;
    }

    // just for debugging purposes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(agent.transform.position, lookRadius);
    }
}
