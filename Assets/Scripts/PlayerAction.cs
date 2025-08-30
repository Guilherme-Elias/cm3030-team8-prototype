using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public HealthBar healthBar;

    private GunController gunController;
    private const float MAX_HEALTH = 100;
    private float currentHealth = 0;

    public GameObject Foodaud;

    public AudioSource damage_aud;

    public GameObject GameOverUI;
    public CameraBehaviour Camb;
    public GunController Gunc;

    void Start()
    {
        RestoreHealth(MAX_HEALTH);
        this.healthBar.SetMaxHealth(MAX_HEALTH);
        gunController = FindObjectOfType<GunController>(); // in GunManager 
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
                Debug.Log("Walk");
            }
          
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }


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

    public void RestoreHealth(float amount)
    {
        float futureHealth = this.currentHealth + amount;
        if (futureHealth >= MAX_HEALTH)
        {
            this.currentHealth = MAX_HEALTH;
            this.healthBar.SetHealth(this.currentHealth);
            return;
        }            
        this.currentHealth = futureHealth;
        this.healthBar.SetHealth(this.currentHealth);
    }
    public void RecoverHealth(float amount)
    {
        GameObject obj = Instantiate(Foodaud);
        Destroy(obj, 1);
        float futureHealth = this.currentHealth + amount;
        if (futureHealth >= MAX_HEALTH)
        {
            this.currentHealth = MAX_HEALTH;
            this.healthBar.SetHealth(this.currentHealth);
            return;
        }
        this.currentHealth = futureHealth;
        this.healthBar.SetHealth(this.currentHealth);
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
        this.healthBar.SetHealth(this.currentHealth);
        if (!damage_aud.isPlaying)
        {
            damage_aud.Play();
        }
    }

    public void Die()
    {
        // todo: endgame logic
        // todo: add death sound
        Debug.Log("You have died!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Camb.enabled = false;
        Gunc.gameObject.SetActive(false);
        GameOverUI.SetActive(true);
        Time.timeScale = 0;

    }
}
