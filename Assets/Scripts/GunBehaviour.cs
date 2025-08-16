//using System.Collections;
//using UnityEngine;

//public class GunBehaviour : MonoBehaviour
//{
//    public GameObject gun;
//    public GameObject player;
//    public Camera camera;

//    private float damage = 10f;
//    private float range = 100f;
//    private float impactForce = 10f;
//    private float fireRate = 10f;
//    private float nextTimeToFire = 0f;
//    private int maxAmmo = 32;
//    private int currentAmmo;
//    private float realoadTime = 1f;
//    private bool isReloading = false;

//    private ParticleSystem shootingFlash;
//    private Animator animator;
//    private PlayerAction playerAction;    

//    void Start()
//    {
//        this.shootingFlash = gun.GetComponent<ParticleSystem>();
//        this.animator = gun.GetComponent<Animator>();

//        currentAmmo = maxAmmo;
//        playerAction = FindObjectOfType<PlayerAction>();
//        playerAction.shoot += OnHolderShoot;

//        this.transform.SetParent(holder.transform);

//        gun = GameObject.Find("Gun");
//    }

//    void OnHolderShoot()
//    {
//        if (isReloading)
//            return;

//        if (currentAmmo <= 0)
//        {
//            Debug.Log("Reloading initiated");
//            StartCoroutine(Reload());
//            return;
//        }

//        if (Time.time >= nextTimeToFire)
//        {
//            nextTimeToFire = Time.time + 1f/fireRate;
//            Shoot();
//        }
//    }

//    private void Shoot()
//    {
//        shootingFlash.Play();
//        currentAmmo--;

//        RaycastHit hitInfo;
//        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, this.range))
//        {
//            BulletTargetBehaviour target = hitInfo.transform.GetComponent<BulletTargetBehaviour>();
            
//            // applies damage if a target got hit
//            if (target != null)
//            {
//                target.TakeDamage(this.damage);
//            }

//            // applies a force to the target's body
//            if (hitInfo.rigidbody != null)
//            {
//                hitInfo.rigidbody.AddForce(-hitInfo.normal * this.impactForce);
//            }

//            // instantiate the impact effect on the hit point
//            //var go = Instantiate(bulletImpactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
//            //Destroy(go, 1);
//        }
//    }

//    IEnumerator Reload()
//    {
//        isReloading = true;
//        animator.SetBool("Reloading", true);
//        yield return new WaitForSeconds(realoadTime); // reloading delay
//        animator.SetBool("Reloading", false);
//        currentAmmo = maxAmmo;
//        isReloading = false;
//    }

//    void OnDestroy()
//    {
//        playerAction.shoot -= OnHolderShoot;    
//    }
//}
