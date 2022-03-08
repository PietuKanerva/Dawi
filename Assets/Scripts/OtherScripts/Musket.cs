using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musket : MonoBehaviour
{
    [Header("Musket Stats")]
    public static float reloadSpeed = 3f;
    public static float damage = 50f;
    public float range = 100f;
    bool canShoot = true;

    [Header("Visual Effects")]
    public Camera cam;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    public ParticleSystem flint;
    public AudioSource audioSource;

    //Simple shoot function. Uses raycast. ReloadSpeed could be increased with upgrades or talents or somesuch.

    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(WaitingTime());
            muzzleflash.Play();
            flint.Play();
            audioSource.Play();
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }
    }
    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(reloadSpeed);
        canShoot = true;
    }
}
