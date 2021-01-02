using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    
    public FiringUnit firingUnit;
    public GameObject bulletPrefab;
    public float range = 100;
    public int maxHealth = 30;
    public int currentHealth = 30;
    public Transform firePoint;
    public int moveSpeed = 15;
    public float fireRate = 2f;
    public float bulletSpeed = 20f;

    
    void Start()
    {
        firingUnit = GetComponent<FiringUnit>();
    }

    void Update()
    {
        if (currentHealth < 0)
        {
            EventManager.InvokeOnDeath(transform.position);
            Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
    }
}
