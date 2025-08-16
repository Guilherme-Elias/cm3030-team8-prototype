using System;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private GunController gunController;

    void Start()
    {
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
}
