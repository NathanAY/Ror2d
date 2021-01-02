using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public float Speed { get; set;} = 20;
    private float range;
    private Vector3 shootDir;
    private float proccedRange = 0;
    public int damage = 10;
    public GameObject destroyEffect;
    
    public void SetupDirection(Vector3 startingPoint, Vector3 targetPoint, float range)
    {
        this.range = range;
        transform.position = startingPoint;
        shootDir = (targetPoint - startingPoint).normalized;
        transform.eulerAngles = new Vector3(0, 0, CalculateEulerAngles(shootDir));
    }
    
    void Update()
    {
        transform.position += shootDir * Speed * Time.deltaTime;
        proccedRange += Speed * Time.deltaTime;
        CheckToDestroy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damage);
        }
        else
        {
            ItemWorld item = other.gameObject.GetComponent<ItemWorld>();
            if (item != null)
            {
                return;
            }
        }
        DestroyProjectile();
    }
    
    private float CalculateEulerAngles(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void CheckToDestroy()
    {
        if (proccedRange > range)
        {
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        if (destroyEffect != null)
        {
            GameObject explosion =  Instantiate(destroyEffect, transform.position, Quaternion.identity).gameObject;
            Destroy(explosion, 1);
        }
        Destroy(gameObject);
    }
}
