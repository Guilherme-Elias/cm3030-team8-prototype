using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public PlayerAction playerActions;
    
    public float lookRadius = 5f;
    private float rotationSmoothness = 3f;
    private float attackPower = 10f;
    private float attackRange = 2f;
    private float attackCooldown = 2f;
    private float lastAttackTime = -Mathf.Infinity;

    private NavMeshAgent[] agents;

    private void Update()
    {
        if (this.agents == null) return;
        foreach (var agent in agents)
        {
            this.FollowTargetLogic(agent, target);  
        }
    }

    public void SetAgents(NavMeshAgent[] agents)
    {
        if (agents.Length == 0) throw new EmptyAgentsException("No agents were found!");
        this.agents = agents;
    }

    private void FollowTargetLogic(NavMeshAgent agent, Transform target)
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
            this.FaceTarget(agent, target);

            if (TargetIsInAttackRange(distanceToPlayer) && IsTimeToAttack())
            {
                this.AttackTarget();
            }
        }
    }

    private void FaceTarget(NavMeshAgent agent, Transform target)
    {
        Vector3 agentPosition = agent.transform.position;
        Vector3 targetPosition = target.position;
        Quaternion agentRotation = agent.transform.rotation;

        Vector3 direction = (targetPosition - agentPosition).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // smoothing the rotation to the target
        transform.rotation = Quaternion.Slerp(agentRotation, lookRotation, Time.deltaTime * this.rotationSmoothness);
    }

    private void AttackTarget()
    {
        playerActions.TakeDamage(this.attackPower);
        this.lastAttackTime = Time.time; // reset cooldown
        return;
    }

    private bool TargetIsInAttackRange(float distanceToTarget)
    {
        if (distanceToTarget <= this.attackRange)
            return true;
        return false;
    }

    private bool IsTimeToAttack()
    {
        if (Time.time >= this.lastAttackTime + this.attackCooldown)
            return true;
        return false;
    }

    public class EmptyAgentsException : Exception
    {
        public EmptyAgentsException() { }
        public EmptyAgentsException(string message) : base(message) { }
    }
}
