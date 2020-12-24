using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringUnit : MovingUnit
{
    public Weapon currentWeapon;
    public bool firing;

    protected List<AttackModifier> attackModifiers = new List<AttackModifier> { new TripleShooting() };
    protected bool canShoot = true;
    protected float nextTimeOfFire = 0; //todo move to manager

    protected void Update()
    {
        base.Update();
        Attack();
    }

    protected void Attack()
    {
        if (firing && canShoot)
        {
            if (Time.time >= nextTimeOfFire)
            {
                currentWeapon.Shoot(attackModifiers);
                nextTimeOfFire = Time.time + 1 / currentWeapon.fireRate;
            }
        }

    }
}
