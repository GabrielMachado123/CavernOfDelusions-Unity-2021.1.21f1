using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpellMenu : MonoBehaviour
{
    public GameObject SpellMenu;
    private Map mapScript;

    public bool level5 = false;
    public bool level10 = false;
    public bool level15 = false;
    public bool level20 = false;
    public bool level25 = false;

    // Start is called before the first frame update
    void Start()
    {
        mapScript = GameObject.Find("Map").GetComponent<Map>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (mapScript.PlayerStats.CurrentLevel >= 2 && level5 == false )
        {
            level5 = true;
            SpellMenu.SetActive(true);
        }else if (mapScript.PlayerStats.CurrentLevel >= 3 && level10 == false)
        {
            
            level10 = true;
            SpellMenu.SetActive(true);
        }
        else if (mapScript.PlayerStats.CurrentLevel >= 4 && level15 == false)
        {

            level15 = true;
            SpellMenu.SetActive(true);
        }
        else if (mapScript.PlayerStats.CurrentLevel >= 5 && level20 == false)
        {

            level20 = true;
            SpellMenu.SetActive(true);
        }
        else if (mapScript.PlayerStats.CurrentLevel >= 6 && level25 == false)
        {

            level25 = true;
            SpellMenu.SetActive(true);
        }




    }
}
