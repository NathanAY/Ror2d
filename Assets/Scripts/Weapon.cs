using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public Sprite currentWeaponSprite;
    public GameObject bulletPrefab;
    public float fireRate = 1;
    public int damage = 20;
    public float range = 100;

    public void Shoot(List<AttackModifier> attackModifiers)
    {
        GameObject firePoint = GameObject.Find("FirePoint");
        GameObject direction = GameObject.Find("Dir");
        foreach (var attackModifier in attackModifiers)
        {
            attackModifier.ApplyModifier(firePoint, direction, bulletPrefab, range);
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletMovement>().SetupDirection(firePoint.transform.position, direction.transform.position, range);
    }
}
