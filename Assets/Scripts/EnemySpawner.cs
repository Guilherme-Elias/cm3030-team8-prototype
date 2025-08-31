using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [Header("Required Managers")]
    public BulletTargetBehaviour bulletTargetBehaviour;

    private SpawnPoint[] spawnPoints;
    private List<GameObject> globalSpawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
        if (spawnPoints.Length == 0) throw new NoSpawnPointsException("You passed no spawn points to the Enemy Spawner!");
    }

    private void Update()
    {
        this.SpawnWave();
        this.MakeModelsEven();

        if (globalSpawnedEnemies.Count > 0) 
            this.UpdateBulletTargetBehaviour();
    }

    private void SpawnWave()
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            spawnPoint.SpawnEnemies();
        }
    }

    private void MakeModelsEven()
    {
        // Clear the list first to avoid adding duplicates every frame!
        this.globalSpawnedEnemies.Clear();

        foreach (SpawnPoint spawnPoint in spawnPoints)
        {
            List<GameObject> spawnPointEnemies = spawnPoint.obtainSpawnPointSpawnedEnemies();
            foreach (GameObject spawnPointEnemy in spawnPointEnemies)
            {
                this.globalSpawnedEnemies.Add(spawnPointEnemy);
            }
        }
    }

    private void UpdateBulletTargetBehaviour()
    {
        bulletTargetBehaviour.SetEnemies(this.globalSpawnedEnemies.FindAll(e => e != null).ToArray());
    }

    /**
     * EXCEPTIONS USED IN THIS CLASS
     */

    private class NoSpawnPointsException : Exception
    {
        public NoSpawnPointsException() { }
        public NoSpawnPointsException(string message) : base(message) { }
    }
}
