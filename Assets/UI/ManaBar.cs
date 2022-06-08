using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
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
        barImage.fillAmount = GetMana();
    }

    public float GetMana()
    {
        return mapSript.PlayerStats.currentMana / mapSript.PlayerStats.PlayerMana;
    }
}
