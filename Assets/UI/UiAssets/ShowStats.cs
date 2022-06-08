using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class  ShowStats : MonoBehaviour
{
    Map mapScript;
    public TextMeshProUGUI armor;
    public TextMeshProUGUI Mr;
    public TextMeshProUGUI att;
    void Start()
    {
        mapScript = GameObject.Find("Map").GetComponent<Map>();

    }

    // Update is called once per frame
    void Update()
    {
        armor.text = "" + mapScript.PlayerStats.Armor;
        Mr.text = "" + mapScript.PlayerStats.MR;
        att.text = "" + mapScript.PlayerStats.attack;
    }
}
