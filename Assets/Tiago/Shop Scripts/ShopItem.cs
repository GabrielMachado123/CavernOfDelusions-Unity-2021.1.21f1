using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;



[CreateAssetMenu(fileName = "shopMenu", menuName = "Scriptale objects/New Shop Item", order = 1)]
public class ShopItem : ScriptableObject
{
    public string title;
    public string description;
    public int basecost;
    public Sprite sprite;
}
