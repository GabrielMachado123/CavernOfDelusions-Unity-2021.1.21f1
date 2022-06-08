using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum TileType { FLOOR, WALL, STAIRS, SHOP };
    public enum TileFunc { ROOM, HALLWAY, NONE };

    public TileType type;
    public Item item;
    public Entity entity;
    public TileFunc func;
}
public class Entity
{
    public float Health = 1f;
    public float Armor;
    public float MR;
    public float attack;
    public float currentHealth;
    public int stun = 0;
    public float attackBuff = 0;
    public int turnsAttBuff = 0;
    public float armorBuff = 0;
    public int turnsArmorBuff = 0;
    public float mrBuff = 0;
    public float turnsMrBuff = 0;
}



public class Item : Tile
{
    public int id;
    public string name;

    public Item(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

