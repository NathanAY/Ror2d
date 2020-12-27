using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringUnit : MonoBehaviour
{
    public Weapon currentWeapon;
    public bool firing;

    protected bool canShoot = true;
    protected float nextTimeOfFire = 0; //todo move to manager

    protected void Update()
    {
        Attack();
    }

    protected void Attack()
    {
        if (firing && canShoot)
        {
            if (Time.time >= nextTimeOfFire)
            {
                currentWeapon.Shoot();
                nextTimeOfFire = Time.time + 1 / currentWeapon.fireRate;
            }
        }

    }
}
