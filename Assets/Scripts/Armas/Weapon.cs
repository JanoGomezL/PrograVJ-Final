using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int ammo;
    public int maxAmmo;
    public float fireRate;
    public float range;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioClip shootSound;

    protected float nextFireTime;

    public virtual void Shoot()
    {
        if (Time.time >= nextFireTime && ammo > 0)
        {
            Vector3 shootDirection;

            // Intentar disparar hacia el punto que está centrado en la cámara
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, range))
            {
                // Dirección hacia el punto de impacto
                shootDirection = (hit.point - firePoint.position).normalized;
            }
            else
            {
                // Si no hay colisión, disparar hacia adelante relativo a la cámara
                shootDirection = Camera.main.transform.forward;
            }

            // Crear el proyectil
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

            // Aplicar velocidad al proyectil
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = shootDirection * 20f; // Ajusta la velocidad según lo necesario
            }

            // Reducir munición
            ammo--;

            // Reproducir sonido del disparo
            if (shootSound != null)
            {
                AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
            }

            // Actualizar el tiempo para el siguiente disparo
            nextFireTime = Time.time + fireRate;

            Debug.Log("Disparo realizado hacia: " + shootDirection);
        }
        else if (ammo <= 0)
        {
            Debug.Log("Sin munición en " + weaponName);
        }
    }

    public virtual void Reload()
    {
        ammo = maxAmmo;
        Debug.Log("Recargando " + weaponName);
    }
}
