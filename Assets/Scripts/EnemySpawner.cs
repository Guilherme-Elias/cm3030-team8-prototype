using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public int enemiesPerWave = 3;
    public float spawnInterval = 5f;
    public int maxAliveEnemies = 3;
    public GameObject enemyPrefab; // must have a NavMeshAgent component
    public Transform target;
    public float spawnZoneExclusion = 10f; // distance to check that no player is in before considering it as active
    public string spawnTag = "EnemySpawnPoint";

    [Header("Required Managers")]
    public EnemyController enemyController;
    public BulletTargetBehaviour bulletTargetBehaviour;

    private Transform[] spawnLocations;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private float nextTimeToSpawn = 0f;

    private void Awake()
    {
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag(spawnTag);
        if (spawnPointObjects.Length > 0)
        {
            spawnLocations = new Transform[spawnPointObjects.Length];
            for (int i = 0; i < spawnPointObjects.Length; i++)
            {
                spawnLocations[i] = spawnPointObjects[i].transform;
            }
        }
        else
        {
            Debug.LogError($"No game objects found with the tag '{spawnTag}'. Enemies will not spawn.");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Time.time >= this.nextTimeToSpawn)
        {
            this.SpawnWave();
            this.nextTimeToSpawn = Time.time + spawnInterval;
        }

        this.CleanDeadEnemies();
    }

    private Vector3 GetBestSpawnPosition()
    {
        Vector3 playerPosition = target.position;
        List<Vector3> furthestPoints = new List<Vector3>();

        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Vector3 spawnPos = spawnLocations[i].position;

            if (Vector3.Distance(playerPosition, spawnPos) > this.spawnZoneExclusion)
            {
                furthestPoints.Add(spawnPos);
            }
        }

        Vector3 randomPos = furthestPoints[Random.Range(0,  furthestPoints.Count)];

        return randomPos;
    }

    private void SpawnWave()
    {
        int aliveEnemies = this.spawnedEnemies.FindAll(e => e != null).Count;
        if (aliveEnemies >= maxAliveEnemies) return;

        int toSpawn = Mathf.Min(enemiesPerWave, maxAliveEnemies - aliveEnemies);

        Vector3 spawnPos = this.GetBestSpawnPosition();

        for (int i = 0; i < toSpawn; i++)
        {
            this.SpawnNewEnemy(spawnPos);
        }

        this.UpdateEnemyController();
        this.UpdateBulletTargetBehaviour();
    }

    private void SpawnNewEnemy(Vector3 spawnPos)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-3f, 3)); // not overlap positions
        Vector3 spawnPosition = spawnPos + randomOffset;

        Quaternion spawnRotation = Quaternion.identity;

        GameObject newEnemy = Instantiate(enemyPrefab, randomOffset, spawnRotation);

        if (!newEnemy.CompareTag("Enemy"))
            newEnemy.tag = "Enemy";

        spawnedEnemies.Add(newEnemy);
    }

    private void UpdateEnemyController()
    {
        /*
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
        */
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
