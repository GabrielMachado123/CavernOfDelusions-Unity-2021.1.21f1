using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BasicStatsUI : MonoBehaviour
{
    Map mapScript;
    public Text health;
    public Text mana;
    public int healthValue;
 
    void Start()
    {
        mapScript = GameObject.Find("Map").GetComponent<Map>();
       
    }

    // Update is called once per frame
    void Update()
    {
        healthValue = Mathf.RoundToInt(mapScript.PlayerStats.currentHealth);
        health.text = "Health: " + healthValue;
        mana.text = "Mana: " + mapScript.PlayerStats.currentMana;
    }
}
