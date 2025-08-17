using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletTargetBehaviour : MonoBehaviour
{
    public GameObject[] enemies; // must be passed in the Inspector
    private Dictionary<GameObject, float> healthMap = new Dictionary<GameObject, float>();

    private void Start()
    {
        if (enemies.Length == 0) throw new EmptyEnemiesException("No enemies were passed!");

        float initialHealth = 50f;
        foreach (var enemy in enemies)
        {
            healthMap.Add(enemy, initialHealth);
        }
    }

    public void TakeDamage(float amount, GameObject target)
    {
        if (!this.EnemyInMap(target)) throw new EnemyDoesNotExist("Target is not a known enemy!");
        this.DecreaseHealthOf(target, amount);
    }

    public void ApplyForce(Rigidbody target, Vector3 magnitude)
    {
        target.AddForce(magnitude);
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

}
