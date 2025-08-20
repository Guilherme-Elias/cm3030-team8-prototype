using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public int enemiesPerWave = 3;
    public float spawnInterval = 5f;
    public int maxAliveEnemies = 3;
    public Transform spawnLocation;
    public GameObject enemyPrefab; // must have a NavMeshAgent component

    [Header("Required Managers")]
    public EnemyController enemyController;
    public BulletTargetBehaviour bulletTargetBehaviour;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private float nextTimeToSpawn = 0f;

    private void Update()
    {
        if (Time.time >= this.nextTimeToSpawn)
        {
            this.SpawnWave();
            this.nextTimeToSpawn = Time.time + spawnInterval;
        }

        this.CleanDeadEnemies();
    }

    private void SpawnWave()
    {
        int aliveEnemies = this.spawnedEnemies.FindAll(e => e != null).Count;
        if (aliveEnemies >= maxAliveEnemies) return;

        int toSpawn = Mathf.Min(enemiesPerWave, maxAliveEnemies - aliveEnemies);

        for (int i = 0; i < toSpawn; i++)
        {
            this.SpawnNewEnemy();
        }

        this.UpdateEnemyController();
        this.UpdateBulletTargetBehaviour();
    }

    private void SpawnNewEnemy()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f)); // not overlap positions
        Vector3 spawnPosition = spawnLocation.position + randomOffset;
        Quaternion spawnRotation = Quaternion.identity;

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);

        if (!newEnemy.CompareTag("Enemy"))
            newEnemy.tag = "Enemy";

        spawnedEnemies.Add(newEnemy);
    }

    private void UpdateEnemyController()
    {
        List<NavMeshAgent> agents = new List<NavMeshAgent>();
        foreach (var spawnedEnemy in this.spawnedEnemies)
        {
            if (spawnedEnemy != null)
            {
                NavMeshAgent agent = spawnedEnemy.GetComponent<NavMeshAgent>();
                if (agent != null)
                    agents.Add(agent);
            }
        }

        enemyController.SetAgents(agents.ToArray());
    }

    private void UpdateBulletTargetBehaviour()
    {
        bulletTargetBehaviour.SetEnemies(this.spawnedEnemies.FindAll(e => e != null).ToArray());
    }

    private void CleanDeadEnemies() // free memory of dead enemies
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);
            }
        }
    }

}
