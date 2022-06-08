using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUpSkillManager : MonoBehaviour
{
    public ActivateSpellMenu Ac;

    private Map mapScript;

    private PlayerStatsManager Ps;

    public GameObject Description1;
    public GameObject spellName1;
    public GameObject button1;

    public Button b1;
    public TextMeshProUGUI sn1;
    public TextMeshProUGUI D1;

    public GameObject Description2;
    public GameObject spellName2;
    public GameObject button2;

    public Button b2;
    public TextMeshProUGUI sn2;
    public TextMeshProUGUI D2;


    public GameObject Description3;
    public GameObject spellName3;
    public GameObject button3;

    public Button b3;
    public TextMeshProUGUI sn3;
    public TextMeshProUGUI D3;

    public GameObject objmenu;
    public Menu menu;

    public Image icon1;
    public Image icon2;
    public Image icon3;

    public int Whereami = 0;

    public Sprite meditate;
    public Sprite HeavyFists;
    public Sprite HeatUpBlood;

    public Sprite GroundSlam;
    public Sprite BloodyFists;
    public Sprite FireSpark;

    public Sprite StoneSkin;
    public Sprite Combustion;
    public Sprite BreakArmor;

    public Sprite TankUp;
    public Sprite Balance;
    public Sprite ManUp;

    public Sprite Consume;
    public Sprite Judgement;
    public Sprite BloodBullets;
    public void Button1()
    {
       if(Whereami == 5)
        {
            menu.ability.Add(new Ability(1, "Heat-Up blood", 10));
          
        }
       else if (Whereami == 10)
        {
            menu.ability.Add(new Ability(4, "Bloody Fists", 15));

        }
        else if (Whereami == 15)
        {
            menu.ability.Add(new Ability(7, "StoneSkin", 15));
        }
        else if (Whereami == 20)
        {
            
            Ps.ArmorIncrease += 60;
            Ps.MrIncrease += 60;
          
          
        }
        else if (Whereami == 25)
        {
            menu.ability.Add(new Ability(10, "Blood Bullets", 0));
        }
        menu.UpdateAbilities();
        gameObject.SetActive(false);
    }

    public void Button2()
    {
        if (Whereami == 5)
        {
            menu.ability.Add(new Ability(2, "Heavy Fists", 5));
        }
        else if (Whereami == 10)
        {
            menu.ability.Add(new Ability(5, "Fire Spark", 10));
        }
        else if (Whereami == 15)
        {
            menu.ability.Add(new Ability(8, "Combustion", 30));
        }
        else if (Whereami == 20)
        {
            
            Ps.DamgeIncrease += 30;
            Ps.ArmorIncrease += 20;
            Ps.MrIncrease += 20;
            mapScript.PlayerStats.currentHealth = mapScript.PlayerStats.Health;
        }
        else if (Whereami == 25)
        {
            menu.ability.Add(new Ability(11, "Judgement", 15));
        }
        menu.UpdateAbilities();
        gameObject.SetActive(false);
    }


    public void Button3()
    {
        if (Whereami == 5)
        {
            menu.ability.Add(new Ability(3, "Meditation", 15));
        }
        else if (Whereami == 10)
        {
            menu.ability.Add(new Ability(6, "Ground Slam", 20));
        }
        else if(Whereami == 15)
        {
            menu.ability.Add(new Ability(9, "BreakAmor", 15));
        }
        else if (Whereami == 20)
        {
            Ps.MrIncrease -= 25;
            Ps.ArmorIncrease -= 25;
            
            Ps.DamgeIncrease += 100;
           
        }
        else if (Whereami == 25)
        {
            menu.ability.Add(new Ability(12, "ConsumeSoul", 30));
        }

        menu.UpdateAbilities();
        gameObject.SetActive(false);
    }

    void Start()
    {

      
       

        mapScript = GameObject.Find("Map").GetComponent<Map>();

        menu = objmenu.GetComponent<Menu>();

        Ac = GameObject.FindGameObjectWithTag("PM").GetComponent<ActivateSpellMenu>();

        Ps = GameObject.FindGameObjectWithTag("PM").GetComponent<PlayerStatsManager>();

        b1 = button1.GetComponent<Button>();
        sn1 = spellName1.GetComponent<TextMeshProUGUI>();
        D1 = Description1.GetComponent<TextMeshProUGUI>();

        b2 = button2.GetComponent<Button>();
        sn2 = spellName2.GetComponent<TextMeshProUGUI>();
        D2 = Description2.GetComponent<TextMeshProUGUI>();


        b3 = button3.GetComponent<Button>();
        sn3 = spellName3.GetComponent<TextMeshProUGUI>();
        D3 = Description3.GetComponent<TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
         if(Ac.level25)
        {
            sn1.text = "Blood Bullets";
            D1.text = "Cost 30% of your current health to deal 3.5X player's attack damage + cost as magical damage(ranged ability)";
            icon1.GetComponent<Image>().sprite = BloodBullets;

            sn2.text = "Judgement";
            D2.text = "melle ability that deals 2X of player's resistences + 80% of player's attack as damage by the cost of 15 mana";
            icon2.GetComponent<Image>().sprite = Judgement;
            sn3.text = "Cosume Soul";
            D3.text = "Steal for 4 turns the resistences and attack of one enemy by the cost of 30 mana(melle ability)";
            icon3.GetComponent<Image>().sprite = Consume;

            Whereami = 25;
        }
        else if (Ac.level20)
        {
            sn1.text = "Tank Up";
            D1.text = "gain 60 points for both resistences";
            icon1.GetComponent<Image>().sprite = TankUp;

            sn2.text = "Balanced hero";
            D2.text = "Increase your attack power by 30 and both resistences by 20";
            icon2.GetComponent<Image>().sprite = Balance;

            sn3.text = "Man Up";
            D3.text = "Lose 25 of both resistences to gain 100 attack power";
            icon3.GetComponent<Image>().sprite = ManUp;

            Whereami = 20;
        }
        else if(Ac.level15)
        {
            sn1.text = "Stone Skin";
            D1.text = "for 15 mana increase your resistences by 25 plus 25% of your total resistences";
            icon1.GetComponent<Image>().sprite = StoneSkin;

            sn2.text = "Combustion";
            D2.text = "for 30 mana deals 550% of player,s attack as magical damage to an enemy as a ranged attack but has a 50% chance of missing";
            icon2.GetComponent<Image>().sprite = Combustion;

            sn3.text = "Break Armor";
            D3.text = "for 15 mana deals 100% of player,s attack as physical damage to an enemy in a melle range and reduce it's armor by half";
            icon3.GetComponent<Image>().sprite = BreakArmor;

            Whereami = 15;
        }
        else if (Ac.level10)
        {
            sn1.text = "Bloody Fists";
            D1.text = "for 15 mana deals 200% of attack as physical damage to an enemy in a melle range and heal the player for the damage done";
            icon1.GetComponent<Image>().sprite = BloodyFists;

            sn2.text = "Fire Spark";
            D2.text = "for 10 mana deals 210% of attack as magical damage to an enemy as a ranged attack";
            icon2.GetComponent<Image>().sprite = FireSpark;

            sn3.text = "Ground Slam";
            D3.text = "for 20 mana deals 90% of attack as physical damage to all tiles around the player and stun the enemies around";
            icon3.GetComponent<Image>().sprite = GroundSlam;

            Whereami = 10;
       
        }
        else
        {
           
            sn1.text = "Heat-Up blood";
            D1.text = "for 10 mana heals 10 + 20% of  maximum health";
            icon1.GetComponent<Image>().sprite = HeatUpBlood;


            sn2.text = "Heavy fists";
            D2.text = "for 5 mana deals 150% of attack as physical damage to an enemy in a melle range and reduce it's armor by 5";
            icon2.GetComponent<Image>().sprite = HeavyFists;

            sn3.text = "Meditation";
            D3.text = "for 15 mana increases attack for 30% for 5 turns(not stackable)";
            icon3.GetComponent<Image>().sprite = meditate;

            Whereami = 5;

        }





    }
}
