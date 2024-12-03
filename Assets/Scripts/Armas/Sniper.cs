using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public override void Shoot()
    {
        base.Shoot();
        Debug.Log("Disparo preciso con el francotirador.");
    }
}

