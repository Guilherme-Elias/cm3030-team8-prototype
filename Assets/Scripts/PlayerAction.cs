using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAction : MonoBehaviour
{
    private GunController gunController;
    private const float MAX_HEALTH = 100;
    private float currentHealth = 0;

    void Start()
    {
        RestoreHealth(MAX_HEALTH);
        gunController = FindObjectOfType<GunController>(); // in GunManager 
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            this.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.Reload();
        }
    }

    private void Shoot()
    {
        gunController.Shoot();
    }

    private void Reload()
    {
        gunController.Reload();
    }

    private void RestoreHealth(float amount)
    {
        float futureHealth = this.currentHealth + amount;
        if (futureHealth >= MAX_HEALTH)
        {
            this.currentHealth = MAX_HEALTH;
            return;
        }            
        this.currentHealth = futureHealth;
    }

    public void TakeDamage(float amount)
    {
        // todo: add damage sound

        float futureHealth = this.currentHealth - amount;
        if (futureHealth <= 0)
        {
            Die();
        }

        this.currentHealth = futureHealth;
    }

    public void Die()
    {
        // todo: endgame logic
        // todo: add death sound
        Debug.Log("You have died!");
        
    }
}
