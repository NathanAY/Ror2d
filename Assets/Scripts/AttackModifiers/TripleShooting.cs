using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShooting : AttackModifier
{
   public TripleShooting()
    {
        id = 1;
    }
    
    public override void ApplyModifier(GameObject firePoint, GameObject direction, GameObject bulletPrefab, float range)
    {
        var angle = 30;
        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = MonoBehaviour.Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
            bullet.GetComponent<BulletMovement>().SetupDirection(firePoint.transform.position, new Vector3(direction.transform.position.x + angle, direction.transform.position.y, direction.transform.position.z), range);
            angle -= 60;
        }
    }
}
