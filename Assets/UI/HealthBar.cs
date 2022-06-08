using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image barImage;
    private Map mapSript;
    
    private void Awake()
    {
        barImage = transform.Find("bar").GetComponent<Image>();
        mapSript = GameObject.Find("Map").GetComponent<Map>();
     

    }


    private void Update()
    {
        barImage.fillAmount = GetHealth();
    }

    public float GetHealth()
    {
        return mapSript.PlayerStats.currentHealth / mapSript.PlayerStats.Health;
    }
}
