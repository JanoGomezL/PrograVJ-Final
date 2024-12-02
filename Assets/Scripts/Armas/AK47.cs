using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Weapon
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("AK-47 disparó.");
    }
}

