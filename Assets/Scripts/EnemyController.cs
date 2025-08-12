using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 5f;
    Transform target;
    NavMeshAgent agent;
    private float rotationSmoothness = 3f;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distanceToPlayer <= agent.stoppingDistance)
            {
                // face the player
                FaceTarget();
                // attack the player
                AttackTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(
            new Vector3(direction.x, 0, direction.z));

        // smoothing the rotation to the target
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 
            Time.deltaTime * this.rotationSmoothness);
    }

    void AttackTarget()
    {
        return;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
