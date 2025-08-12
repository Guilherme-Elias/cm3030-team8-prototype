using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 10f;
    public float fireRate = 10f;
    public float nextTimeToFire = 0f;
    public int maxAmmo = 50;
    private int currentAmmo;
    public float realoadTime = 1f;
    private bool isReloading = false;

    public new Camera camera;
    public GameObject holder;
    public GameObject gun;
    public ParticleSystem shootingFlash;
    public Animator animator;
    //public GameObject bulletImpactEffect;

    private PlayerAction playerAction;    

    void Start()
    {
        currentAmmo = maxAmmo;
        playerAction = FindObjectOfType<PlayerAction>();
        playerAction.shoot += OnHolderShoot;

        holder = GameObject.Find("Player");
        this.transform.SetParent(holder.transform);

        gun = GameObject.Find("Gun");
    }

    void OnHolderShoot()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            Debug.Log("Reloading initiated");
            StartCoroutine(Reload());
            return;
        }

        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        shootingFlash.Play();
        currentAmmo--;

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

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(realoadTime); // reloading delay
        animator.SetBool("Reloading", false);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void OnDestroy()
    {
        playerAction.shoot -= OnHolderShoot;    
    }
}
