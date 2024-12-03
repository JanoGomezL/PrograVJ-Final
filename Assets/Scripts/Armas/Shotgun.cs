using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : Weapon
{
    public int pellets = 8; // Número de balas disparadas al mismo tiempo

    public override void Shoot()
    {
        if (Time.time >= nextFireTime && ammo > 0)
        {
            for (int i = 0; i < pellets; i++)
            {
                Quaternion spread = Quaternion.Euler(
                    firePoint.eulerAngles.x,
                    firePoint.eulerAngles.y + Random.Range(-5, 5),
                    firePoint.eulerAngles.z
                );

                Instantiate(bulletPrefab, firePoint.position, spread);
            }

            ammo--;
            nextFireTime = Time.time + fireRate;
            Debug.Log("Disparo múltiple con la escopeta.");
        }
        else if (ammo <= 0)
        {
            Debug.Log("Sin munición en la escopeta.");
        }
    }
}

