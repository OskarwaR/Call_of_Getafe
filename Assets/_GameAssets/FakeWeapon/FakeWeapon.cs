using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FakeWeapon : MonoBehaviour
{
    [Header("Positions")]
    public Transform shootPoint;
    public Transform bulletShellPoint;
    [Header("Prefabs")]
    public GameObject prefabBullet;
    public GameObject prefabBulletShell;
    [Header("Sounds")]
    public AudioClip shootSound;
    public AudioClip jammingSound;
    [Header("Weapon status")]
    [Range(0, 100)]
    public float shootForce;
    [Range(0, 100)]
    public float bulletShellForce;
    [Range(0,5)]
    public float delayBetweenShoots = 0.5f;

    private AudioSource audioSource;
    private bool canShoot = true;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (canShoot)
            {
                Shoot();
            } else
            {
                PlayJammingSound();
            }
        }
    }

    private void Shoot()
    {
        canShoot = false;
        ThrowBullet();
        ThrowBulletShell();
        PlayShootSound();
        Invoke("RestoreStatus", delayBetweenShoots);
    }

    private void ThrowBullet()
    {
        GameObject bullet = Instantiate(prefabBullet, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb==null)
        {
            Debug.LogError("El proyectil debe tener un componente Rigidbody");
            return;
        }
        rb.AddForce(shootPoint.forward * shootForce);
    }

    private void ThrowBulletShell()
    {
        GameObject bulletShell = Instantiate(prefabBulletShell, bulletShellPoint.position, bulletShellPoint.rotation);
        Rigidbody rb = bulletShell.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("El casquillo debe tener un componente Rigidbody");
            return;
        }
        rb.AddForce(bulletShellPoint.forward * bulletShellForce);
    }

    private void RestoreStatus()
    {
        canShoot = true;
    }

    private void PlayJammingSound()
    {
        audioSource.PlayOneShot(jammingSound);
    }
    private void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }
}
