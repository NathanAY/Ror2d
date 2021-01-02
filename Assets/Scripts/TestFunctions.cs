using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFunctions : MonoBehaviour
{

    public GameObject test;
    public int param1 = 30;
    
    public void TestLeft()
    {
        Inventory inventory = Player.Instance.Inventory;
        inventory.AddItem(new Item(){itemType = Item.ItemType.SurpriseMF, amount = 1});
    }

    public void TestRight()
    {
        test.GetComponent<Rigidbody2D>().AddForce(new Vector2(param1, 0));
    }
    
    public void TestUp()
    {
        test.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, param1));
    }
    
    public void TestDown()
    {
        test.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 0 - param1));
    }
}
