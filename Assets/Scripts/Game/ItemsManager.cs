using System;
using UnityEngine;


public class ItemsManager : MonoBehaviour
{
    public static ItemsManager instance;
    
    public Item[] items;

    private void Awake()
    {
        instance = this;
        EventManager.OnDeath += EventManagerOnOnDeath;
    }

    public Item GetRandomItem()
    {
        Item i = items[UnityEngine.Random.Range(0, items.Length)];
        return new Item() {itemType = i.itemType, amount =  1};
    }

    public void CreateRandomItem(Vector3 position)
    {
        ItemWorld.SpawnItemWorld(position, GetRandomItem());
    }
    
    private void EventManagerOnOnDeath(Vector3 position)
    {
        CreateRandomItem(position);
    }
}