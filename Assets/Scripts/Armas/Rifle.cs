using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Rifle automático disparó.");
    }
}
