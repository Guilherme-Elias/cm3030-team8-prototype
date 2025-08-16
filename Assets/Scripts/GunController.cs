using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform gunHolderTransform;
    public Transform gunTransform;
    public Animator gunAnimator;

    public AudioSource shootAudioSouce;
    public AudioSource reloadAudioSouce;

    public ParticleSystem muzzleFlash;

    private Vector3 gunOffset = new Vector3(.35f, .35f, .2f);

    private void Start()
    {         
        AttachGunToHolder(gunTransform, gunHolderTransform);
    }

    private void AttachGunToHolder(Transform gun, Transform holder)
    {
        gun.SetParent(holder, false);
        gun.position = holder.position + this.gunOffset;
        gun.rotation = holder.rotation;
    }

    public void Shoot()
    {
        // play shoot animation
        StartCoroutine(ShootAnimation());

        // play shooting sound
        shootAudioSouce.Play();

        // TODO: play muzzle flash
        muzzleFlash.Play();

        // TODO: cast a ray to deal with enemy logic
        
    }

    public void Reload()
    {
        // play reload animation
        StartCoroutine(ReloadAnimation());

        // play reload sound
        reloadAudioSouce.Play();
        
        // reload the gun logic
    }


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
}
