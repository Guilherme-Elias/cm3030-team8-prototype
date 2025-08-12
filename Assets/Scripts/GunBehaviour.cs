using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 10f;

    public Camera camera;
    public GameObject holder;
    public GameObject gun;
    public ParticleSystem shootingFlash;
    //public GameObject bulletImpactEffect;

    private PlayerAction playerAction;    

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
        Shoot();
    }

    private void Shoot()
    {
        shootingFlash.Play();

        RaycastHit hitInfo;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, this.range))
        {
            TargetBehaviour target = hitInfo.transform.GetComponent<TargetBehaviour>();
            
            // applies damage if a target got hit
            if (target != null)
            {
                target.TakeDamage(this.damage);
            }

            // applies a force to the target's body
            if (hitInfo.rigidbody != null)
            {
                hitInfo.rigidbody.AddForce(-hitInfo.normal * this.impactForce);
            }

            // instantiate the impact effect on the hit point
            //var go = Instantiate(bulletImpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            //Destroy(go, 1);
        }
    }

    void OnDestroy()
    {
        playerAction.shoot -= OnHolderShoot;    
    }
}
