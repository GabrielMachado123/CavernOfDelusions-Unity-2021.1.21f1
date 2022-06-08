using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float PlayerMana = 100;
    public float currentMana;
    public int CurrentLevel = 1;
    public float CurrentExp = 0f;
    public float NeededExp;
    public float totalExp;
}