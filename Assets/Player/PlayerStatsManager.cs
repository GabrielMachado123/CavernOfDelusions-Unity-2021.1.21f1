using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    private Map mapScript;
    private PlayerMovement pm;

    public GameObject LevelUpPrefab;
    public GameObject LevelUpSprite;

   public int healthPerLevel = 10;
  public float armorPerLevel = 1.5f;
    public int magicResPerLevel = 1;
    public int attackPerLevel = 5;


    public int HpIncrease = 0;
    public int DamgeIncrease = 0;
    public int ArmorIncrease = 0;
    public int MrIncrease = 0;


    void Start()
    {
        mapScript = GameObject.Find("Map").GetComponent<Map>();
        

        mapScript.PlayerStats.Health = 90 + (mapScript.PlayerStats.CurrentLevel * healthPerLevel);

        mapScript.PlayerStats.Armor = 30 + (mapScript.PlayerStats.CurrentLevel * armorPerLevel);

        mapScript.PlayerStats.MR = 20 + (mapScript.PlayerStats.CurrentLevel * magicResPerLevel);

        mapScript.PlayerStats.attack = 40 + (mapScript.PlayerStats.CurrentLevel * attackPerLevel);

    }


    private void Update()
    {
        if(Input.GetKeyDown("u"))
        {
            mapScript.PlayerStats.CurrentLevel++;
            levelUpStats();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        expManager();
      
      
      
        mapScript.PlayerStats.attack = 40 + (mapScript.PlayerStats.CurrentLevel * attackPerLevel) + mapScript.PlayerStats.attackBuff + DamgeIncrease;

        mapScript.PlayerStats.Armor = 30 + (mapScript.PlayerStats.CurrentLevel * armorPerLevel) + mapScript.PlayerStats.armorBuff + ArmorIncrease;

        mapScript.PlayerStats.MR = 20 + (mapScript.PlayerStats.CurrentLevel * magicResPerLevel) + mapScript.PlayerStats.mrBuff + MrIncrease;

        if (mapScript.PlayerStats.currentMana > mapScript.PlayerStats.PlayerMana)
        {
            mapScript.PlayerStats.currentMana = mapScript.PlayerStats.PlayerMana;
        }

        if(mapScript.PlayerStats.turnsAttBuff <= 0)
        {
            mapScript.PlayerStats.attackBuff = 0;
           
        }


        if (mapScript.PlayerStats.turnsArmorBuff <= 0)
        {
            mapScript.PlayerStats.armorBuff = 0;

        }


        if (mapScript.PlayerStats.turnsMrBuff <= 0)
        {
            mapScript.PlayerStats.mrBuff = 0;

        }

    }

    public void levelUpStats()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        LevelUpSprite = Instantiate(LevelUpPrefab, new Vector3(pm.PlayerPosX, pm.PlayerPosY + 0.75f, 0), Quaternion.identity);

        mapScript.PlayerStats.Health += healthPerLevel;

        mapScript.PlayerStats.currentHealth = mapScript.PlayerStats.Health;
        
        mapScript.PlayerStats.currentMana = mapScript.PlayerStats.PlayerMana;

    }



    public void expManager()
    {

        mapScript.PlayerStats.totalExp = ((mapScript.PlayerStats.CurrentLevel * 100) * 1.25f);
        mapScript.PlayerStats.NeededExp = (mapScript.PlayerStats.totalExp - mapScript.PlayerStats.CurrentExp);
        if(mapScript.PlayerStats.NeededExp <= 0)
        {
            
            mapScript.PlayerStats.CurrentLevel++;
            levelUpStats();
            mapScript.PlayerStats.CurrentExp = 0;
            Debug.Log("level"+mapScript.PlayerStats.CurrentLevel);
        }
    }

}

