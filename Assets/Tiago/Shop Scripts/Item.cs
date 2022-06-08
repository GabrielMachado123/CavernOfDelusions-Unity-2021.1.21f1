using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClass 
{
    public enum ItemType
    {
        DemonCircle,
        HealthPotion,
        ManaPotion,
        SlimeHorde,
        InviziPotion,
        TPorb,
    }


    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType){
            default:
            case ItemType.DemonCircle:  return ItemAssets.Instance.demonSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthSprite;
            case ItemType.ManaPotion:   return ItemAssets.Instance.manaSprite;
            case ItemType.SlimeHorde:   return ItemAssets.Instance.slimeSprite;
            case ItemType.InviziPotion: return ItemAssets.Instance.inviziSprite;
            case ItemType.TPorb:        return ItemAssets.Instance.tpSprite;

        }

    }


    public bool IsStackable()
    {
        switch (itemType)
        {

            case ItemType.DemonCircle: 
            case ItemType.HealthPotion: 
            case ItemType.ManaPotion: 
            case ItemType.SlimeHorde:
            case ItemType.InviziPotion:
            case ItemType.TPorb:
                return true;
            default:
                return false;
        }
    }
}
