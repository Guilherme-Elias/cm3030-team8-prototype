using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform gunHolderTransform;
    public Transform gunTransform;
    public Animator gunAnimator;
    public Camera mainCamera;

    public AudioSource shootAudioSouce;
    public AudioSource reloadAudioSouce;

    public ParticleSystem muzzleFlash;

    public BulletTargetBehaviour bulletTarget;

    public float shootingRange = 20f;
    private Vector3 gunOffset = new Vector3(.35f, .35f, .2f);

    private const int MAX_AMMO = 32;
    private int currentAmmo = 0;

    private bool reloading = false;

    private void Start()
    {
        // TODO: handle GUN ROTATION ON X and Z axis (gun must follow the camera)
        AttachGunToHolder(gunTransform, gunHolderTransform);
        LoadAmmo();
    }

    /**
     * MAIN CALLBACKS
     */

    public void Shoot()
    {
        if (this.reloading) return;
        if (this.currentAmmo == 0) return;

        StartCoroutine(ShootAnimation());
        shootAudioSouce.Play();
        muzzleFlash.Play();

        // TODO: cast a ray to deal with enemy logic
        this.ShootLogic();
    }

    public void Reload()
    {
        if (this.reloading) return;
        if (this.currentAmmo == MAX_AMMO) return;

        StartCoroutine(ReloadAnimation());
        reloadAudioSouce.Play();
        this.ReloadLogic();
    }

    /**
     * SHOOTING AND RELOADING LOGIC RELATED METHODS
     */

    private void ShootLogic()
    {
        this.CastShootRay();
        this.WasteAmmo();
    }

    private void ReloadLogic()
    {
        this.reloading = true;
        this.LoadAmmo();
    }

    /**
     * ANIMATION RELATED METHODS
     */

    private IEnumerator ShootAnimation() 
    {
        gunAnimator.SetBool("Shooting", true);
        yield return new WaitForSeconds(0.3f);
        gunAnimator.SetBool("Shooting", false);

    }

    private IEnumerator ReloadAnimation()
    {
        gunAnimator.SetBool("Reloading", true);
        yield return new WaitForSeconds(0.3f);
        gunAnimator.SetBool("Reloading", false);
    }

    /**
     * AUXILIAR PRIVATE METHODS
     */

    private void LoadAmmo()
    {
        this.currentAmmo = MAX_AMMO;
        this.reloading = false;
    }

    private void WasteAmmo()
    {
        if (this.currentAmmo == 0) return;
        this.currentAmmo--;
    }

    private void CastShootRay()
    {
        RaycastHit hitInfo;
        
        float impact = 10f;
        bool hitSomething = Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, this.shootingRange);

        if (hitSomething)
        {
            if (hitInfo.collider.CompareTag("Enemy")) // enemy logic
            {
                if (hitInfo.rigidbody != null)
                    bulletTarget.ApplyForce(hitInfo.rigidbody, -hitInfo.normal * impact);
                bulletTarget.TakeDamage(10f, hitInfo.collider.gameObject);
            }


        }
    }

    private void AttachGunToHolder(Transform gun, Transform holder)
    {
        gun.SetParent(holder, false);
        gun.position = holder.position + this.gunOffset;
        gun.rotation = holder.rotation;
    }
}
