using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{

    private PlayerAction playerAction;    
    public GameObject holder;
    public GameObject gun;
    public GameObject bulletPrefab;
    private Vector3 bulletOffset = new Vector3(0f, 0f, 1f);

    void Start()
    {
        playerAction = FindObjectOfType<PlayerAction>();
        playerAction.shoot += OnHolderShoot;

        holder = GameObject.Find("Player");
        this.transform.SetParent(holder.transform);

        gun = GameObject.Find("Gun");
    }

    void OnHolderShoot()
    {
        SpawnBullet();        
    }

    void SpawnBullet()
    {
        Vector3 spawnPosition = bulletOffset;
        Quaternion spawnRotation = transform.rotation;

        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, spawnRotation);
    }

    void OnDestroy()
    {
        playerAction.shoot -= OnHolderShoot;    
    }
}
