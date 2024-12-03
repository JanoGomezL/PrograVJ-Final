using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Pistola disparó.");
    }
}

