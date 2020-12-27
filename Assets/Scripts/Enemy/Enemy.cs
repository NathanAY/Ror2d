using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    
    public FiringUnit firingUnit;
    public GameObject bulletPrefab;
    public float range = 100;
    public int health = 10;
    public int currentHealth = 10;
    public Transform firePoint;

    
    void Start()
    {
        firingUnit = GetComponent<FiringUnit>();
    }

    void Update()
    {
        if (currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("sdf");
    }
}
