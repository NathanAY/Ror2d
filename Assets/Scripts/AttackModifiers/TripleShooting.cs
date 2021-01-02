using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShooting : AttackModifier
{
   public TripleShooting()
    {
        id = 1;
    }
    
    public override void ApplyModifier(Transform firePoint, Transform direction, GameObject bulletPrefab, float range, float bulletSpeed)
    {
        var angle = 60;
        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = MonoBehaviour.Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            var bulletMovement = bullet.GetComponent<BulletMovement>();
            bulletMovement.SetupDirection(firePoint.position,
                new Vector3(direction.position.x + angle, direction.position.y, direction.position.z), range);
            bulletMovement.Speed = bulletSpeed;
            angle = angle * -1;
        }
    }
}
