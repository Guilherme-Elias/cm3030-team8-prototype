using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletTargetBehaviour : MonoBehaviour
{
    private GameObject[] enemies;
    private Dictionary<GameObject, float> healthMap = new Dictionary<GameObject, float>();

    //private void Start()
    //{
    //    SetEnemies(enemies);
    //    PopulateHealthMap(enemies);
    //}

    /**
     * CLASS INTERFACE
     */

    public void TakeDamage(float amount, GameObject target)
    {
        if (!this.EnemyInMap(target)) throw new EnemyDoesNotExist("Target is not a known enemy!");
        this.DecreaseHealthOf(target, amount);
    }

    public void ApplyForce(Rigidbody target, Vector3 magnitude)
    {
        target.AddForce(magnitude);
    }

    /**
     * SETTERS
     */

    public void SetEnemies(GameObject[] enemies) // see EnemySpawner
    {
        if (enemies.Length == 0) throw new EmptyEnemiesException("No enemies were passed!");
        if (this.AnyWithWrongTag(enemies)) throw new WrongEnemyTag("All enemies must have the 'Enemy' Tag!");
        this.enemies = enemies;
        this.PopulateHealthMap(enemies);
        Debug.Log("Health Map: ");
        Debug.Log(this.healthMap);
    }

    /**
     * PRIVATE UTILITIES
     */

    private void PopulateHealthMap(GameObject[] enemies)
    {
        float initialHealth = 50f;
        foreach (var enemy in enemies)
        {
            if (!healthMap.ContainsKey(enemy))
                healthMap.Add(enemy, initialHealth);
        }
    }

    private void Die(GameObject deadEnemy)
    {
        // removing from world
        Destroy(deadEnemy);

        // removing from DS
        healthMap.Remove(deadEnemy);
    }

    private bool EnemyInMap(GameObject enemy)
    {
        return healthMap.ContainsKey(enemy);
    }

    private float ObtainHealthOf(GameObject enemy)
    {
        return healthMap[enemy];
    }

    private void DecreaseHealthOf(GameObject enemy, float amount)
    {
        float currentHealth = this.ObtainHealthOf(enemy);
        float futureHealth = currentHealth - amount;
        if (futureHealth <= 0) Die(enemy);
        this.healthMap[enemy] = futureHealth;
    }

    private bool AnyWithWrongTag(GameObject[] enemies)
    {
        foreach (var enemy in enemies)
        {
            if (!enemy.CompareTag("Enemy")) return true;
        }
        return false;
    }

    /**
     * EXCEPTIONS USED IN THIS CLASS
     */

    public class EmptyEnemiesException : Exception
    {
        public EmptyEnemiesException(){ }
        public EmptyEnemiesException(string message) : base(message) { }
    }

    public class EnemyDoesNotExist : Exception
    {
        public EnemyDoesNotExist() { }
        public EnemyDoesNotExist(string message) : base(message) { }
    }

    public class WrongEnemyTag : Exception 
    {
        public WrongEnemyTag() { }
        public WrongEnemyTag(string message) : base(message) { }
    }
}
