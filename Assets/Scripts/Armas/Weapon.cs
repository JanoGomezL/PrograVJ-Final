using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;        // Nombre del arma
    public float fireRate;           // Tiempo entre disparos
    public float damage;             // Daño por disparo
    public float range;              // Alcance del disparo
    public int ammo;                 // Munición actual
    public int maxAmmo;              // Máxima munición
    public GameObject bulletPrefab;  // Prefab del proyectil
    public Transform firePoint;      // Punto de salida del disparo
    public AudioClip shootSound;     // Sonido del disparo

    protected float nextFireTime = 0f;

    public virtual void Shoot()
    {
        if (Time.time >= nextFireTime && ammo > 0)
        {
            // Crear proyectil
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Reducir munición
            ammo--;

            // Reproducir sonido
            if (shootSound != null)
            {
                AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
            }

            // Actualizar tiempo del próximo disparo
            nextFireTime = Time.time + fireRate;
        }
        else if (ammo <= 0)
        {
            Debug.Log("Sin munición en " + weaponName);
        }
    }

    public virtual void Reload()
    {
        ammo = maxAmmo;
        Debug.Log(weaponName + " recargada.");
    }
}
