using System;
using System.Collections;
using System.Collections.Generic;
// using Packages.Rider.Editor;
using UnityEngine;

public class SurpriseMF : MonoBehaviour, OnEnemyDeathTrigger
{
    public GameObject explosionEffect;
    public LayerMask layerMask;
    public int force = 300;

    public void Awake()
    {
        layerMask = LayerMask.GetMask("Enemy");
    }
    
    public void OnEnemyDeathTrigger(GameObject whoKill, Vector3 whereDied)
    {
        Player player = whoKill.GetComponent<Player>();
        List<Item> items = player.Inventory.GetItemList();
        foreach (Item item in items)
        {
            if (item.itemType == Item.ItemType.SurpriseMF)
            {
                int radius = 6 + (item.amount - 1) * 3;
                int damage = player.damage * 2 + item.amount * player.damage;
                CreateExplosion(whereDied, radius, damage);
            }
        }
    }

    private void CreateExplosion(Vector3 position, int radius, int damage)
    {
        CreateEffect(position, radius);
        Collider2D[] objects = Physics2D.OverlapCircleAll(position, radius, layerMask);
        foreach (Collider2D o in objects)
        {
            Vector2 direction = o.transform.position - position;
            o.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
            o.GetComponent<IDamageable>().Damage(damage);
        }
    }

    private void CreateEffect(Vector3 position, int radius)
    {
        ParticleSystem.ShapeModule shape = explosionEffect.GetComponent<ParticleSystem>().shape;
        shape.angle = radius;
        shape.radius = radius / 2;
        Instantiate(explosionEffect, position, Quaternion.identity).GetComponent<ParticleSystem>();
    }
}
