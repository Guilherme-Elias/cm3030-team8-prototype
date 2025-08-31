using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnPoint : MonoBehaviour
{
    [Header("SpawnPoint Settings")]
    public int amountOfEnemiesPerWave = 3;
    public int maxAliveEnemies = 3;
    public float spawnInterval = 5f;
    public GameObject player;
    public GameObject enemyPrefab;

    private float rangeToSpawn = 15f;
    private float nextTimeToSpawn = 0f;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Update()
    {
        this.CleanDeadEnemiesFromMemory();
    }

    public List<GameObject> obtainSpawnPointSpawnedEnemies()
    {
        return this.spawnedEnemies;
    }

    public void SpawnEnemies()
    {
        if (this.IsPlayerTooClose()) return;
        if (!this.IsTimeToSpawn()) return;
        if (this.IsEnemyAmountSaturated()) return;

        int amountOfEnemiesToSpawn = this.CalculateAmountOfEnemiesToSpawn();

        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            this.SpawnNewEnemy();
        }

        // update the timer so this doesn't run every frame
        this.nextTimeToSpawn = Time.time + spawnInterval;
    }

    private bool IsPlayerTooClose()
    {
        Vector3 spawnPointPosition = this.transform.position;
        Vector3 playerPosition = player.transform.position;

        return Vector3.Distance(spawnPointPosition, playerPosition) < rangeToSpawn;
    }

    private bool IsTimeToSpawn()
    {
        return Time.time >= nextTimeToSpawn;
    }

    private bool IsEnemyAmountSaturated()
    {
        if (spawnedEnemies.Count >= maxAliveEnemies) return true;
        return false;
    }

    private int CalculateAmountOfEnemiesToSpawn()
    {
        int enemiesAlive = spawnedEnemies.Count;
        return Mathf.Min(amountOfEnemiesPerWave, maxAliveEnemies - enemiesAlive);
    }

    private Vector3 WhereToSpawn()
    {
        Vector3 spawnPointPosition = this.transform.position;
        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(-5f, 5f), 1f, UnityEngine.Random.Range(-3f, 3));
        return spawnPointPosition + randomOffset;
    }

    private void SpawnNewEnemy()
    {
        if (!enemyPrefab.CompareTag("Enemy")) throw new WrongTagException("Your enemy prefab must have the 'Enemy' tag.");

        Vector3 whereToSpawn = this.WhereToSpawn();
        GameObject newEnemy = Instantiate(enemyPrefab, whereToSpawn, Quaternion.identity);
        spawnedEnemies.Add(newEnemy);
    }

    private void CleanDeadEnemiesFromMemory()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null)
                spawnedEnemies.RemoveAt(i);
        }
    }

    private class WrongTagException : Exception
    {
        public WrongTagException() { }
        public WrongTagException(string message) : base(message) { }
    }
}