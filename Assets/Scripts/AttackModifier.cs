using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackModifier
{
    public int id;

    public abstract void ApplyModifier(GameObject firePoint, GameObject direction, GameObject bulletPrefab, float range);
}
