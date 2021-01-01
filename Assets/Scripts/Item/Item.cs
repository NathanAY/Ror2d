using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public enum ItemType
    {
        SoldiersSyringe,
        SurpriseMF,
    }

    public ItemType itemType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.SoldiersSyringe: return ItemAssets.Instance.soldiersSyringeSprite;
            case ItemType.SurpriseMF: return ItemAssets.Instance.surpriseMfSprite ;
        }
    }

    public Color GetColor()
    {
        switch (itemType)
        {
            default:
            case ItemType.SoldiersSyringe: return new Color(1, 1, 1);
            case ItemType.SurpriseMF: return new Color(0, 1, 0);
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            case ItemType.SoldiersSyringe:
            case ItemType.SurpriseMF:
                return true;
            default:
                return true;
        }
    }
}