using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InvItem
{

    public int id;
    public string name;
    public int quantity;

    public InvItem(int id, string name, int quantity)
    {
        this.id = id;
        this.name = name;
        this.quantity = quantity;
    }
}

public class Ability
{
    public int id;
    public string name;
    public int cost;

    public Ability(int id, string name, int cost)
    {
        this.id = id;
        this.name = name;
        this.cost = cost;
    }
}

public class Menu : MonoBehaviour
{
    public PlayerMovement player;
    public enum MenuType { ATTACK, ITEM };
    public MenuType selection;
    public int row = 0;

    public SpriteRenderer attack;
    public SpriteRenderer item;
    private Color selected = new Color(0.3122997f, 0.5275972f, 0.5471698f);
    private Color notSelected = new Color(0.1686275f, 0.2980392f, 0.3098039f);

    private GameObject abilities;
    private GameObject items;
    public bool update = false;

    public List<InvItem> itemRow;
    public List<Ability> ability;

    // Start is called before the first frame update
    void Start()
    {
        selection = MenuType.ATTACK;
        attack = gameObject.transform.Find("Attack Box").Find("Box").GetComponent<SpriteRenderer>();
        item = gameObject.transform.Find("Item Box").Find("Box").GetComponent<SpriteRenderer>();
        itemRow = new List<InvItem>();
        ability = new List<Ability>();

        abilities = GameObject.Find("Abilities");
        items = GameObject.Find("Items");

        GameObject.Find("Map").GetComponent<Map>().menu = gameObject.GetComponent<Menu>();

      
    }

    // Update is called once per frame
    void Update()
    {
        if(update == false)
        {
            UpdateInventory();
            UpdateAbilities();
            update = true;
        }

        //Button inputs
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            player.menuOpen = false;
            gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            if(selection == MenuType.ATTACK)
            {
                selection = MenuType.ITEM;
            }
            else
            {
                selection = MenuType.ATTACK;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (row > 0)
            {
                --row;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (row < 8)
            {
                ++row;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (selection == MenuType.ATTACK)
            {
                if (ability[row].id == 1)
                {
                    player.HeatUpBlood();
                }
                else if (ability[row].id == 2)
                {
                    player.HeavyFists();
                }
                else if (ability[row].id == 3)
                {
                    player.Meditate();
                }
                else if (ability[row].id == 4)
                {
                    player.BloodyFists();
                }
                else if (ability[row].id == 5)
                {
                    player.FireSpark();
                }
                else if (ability[row].id == 6)
                {
                    player.GroundSlam();
                }
                else if (ability[row].id == 7)
                {
                    player.StoneSkin();
                }
                else if (ability[row].id == 8)
                {
                    player.UnstableBeam();
                }
                else if (ability[row].id == 9)
                {
                    player.BreakArmor();
                }
                else if (ability[row].id == 10)
                {
                    player.BloodBullets();
                }
                else if (ability[row].id == 11)
                {
                    player.Judgement();
                }
                else if (ability[row].id == 12)
                {
                    player.ConsumeSoul();
                }
            }
            else if (selection == MenuType.ITEM)
            {
                if (itemRow[row].id == 1)
                {
                    itemRow[row].quantity--;
                    player.HealthPotion();
                    player.turn = false;
                }

                if (itemRow[row].id == 3)
                {
                    itemRow[row].quantity--;
                    player.ManaPotion();
                    player.turn = false;
                }

                if (itemRow[row].quantity <= 0)
                {
                    itemRow.RemoveAt(row);
                }
                UpdateInventory();
            }

            player.menuOpen = false;
            gameObject.SetActive(false);
        }






        //attack
        if (selection == MenuType.ATTACK)
        {
            attack.color = selected;
            abilities.SetActive(true);
            items.SetActive(false);

            if (ability.Count > 0)
            {
                if (row >= ability.Count)
                {
                    row = ability.Count - 1;
                }

                for (int i = 0; i < ability.Count; i++)
                {
                    if (row == i)
                    {
                        gameObject.transform.Find("Abilities").GetChild(i).Find("Thing").Find("Box").GetComponent<SpriteRenderer>().color = selected;
                        gameObject.transform.Find("Abilities").GetChild(i).Find("Cost").Find("Box").GetComponent<SpriteRenderer>().color = selected;
                    }
                    else
                    {
                        gameObject.transform.Find("Abilities").GetChild(i).Find("Thing").Find("Box").GetComponent<SpriteRenderer>().color = notSelected;
                        gameObject.transform.Find("Abilities").GetChild(i).Find("Cost").Find("Box").GetComponent<SpriteRenderer>().color = notSelected;
                    }
                }
            }
        }
        else
        {
            attack.color = notSelected;
        }

        //item
        if(selection == MenuType.ITEM)
        {
            item.color = selected;
            items.SetActive(true);
            abilities.SetActive(false);

            if(itemRow.Count > 0)
            {
                if (row >= itemRow.Count)
                {
                    row = itemRow.Count - 1;
                }

                for (int i = 0; i < itemRow.Count; i++)
                {
                    if (row == i)
                    {
                        gameObject.transform.Find("Items").GetChild(i).Find("Thing").Find("Box").GetComponent<SpriteRenderer>().color = selected;
                        gameObject.transform.Find("Items").GetChild(i).Find("Cost").Find("Box").GetComponent<SpriteRenderer>().color = selected;
                    }
                    else
                    {
                        gameObject.transform.Find("Items").GetChild(i).Find("Thing").Find("Box").GetComponent<SpriteRenderer>().color = notSelected;
                        gameObject.transform.Find("Items").GetChild(i).Find("Cost").Find("Box").GetComponent<SpriteRenderer>().color = notSelected;
                    }
                }
            }

        }
        else
        {
            item.color = notSelected;
        }


        
    }


    public void UpdateInventory()
    {
        for (int i = 0; i < 8; i++)
        {
            gameObject.transform.Find("Items").GetChild(i).Find("Thing").Find("Name").GetComponent<TextMeshProUGUI>().text = null;
            gameObject.transform.Find("Items").GetChild(i).Find("Cost").Find("Quantity").GetComponent<TextMeshProUGUI>().text = null;
            gameObject.transform.Find("Items").GetChild(i).Find("Thing").Find("Box").GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("Items").GetChild(i).Find("Thing").Find("Square").GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("Items").GetChild(i).Find("Cost").Find("Box").GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("Items").GetChild(i).Find("Cost").Find("Square").GetComponent<SpriteRenderer>().enabled = false;
        }


        if(itemRow.Count > 0)
        {
            for (int i = 0; i < itemRow.Count; i++)
            {
                gameObject.transform.Find("Items").GetChild(i).Find("Thing").Find("Name").GetComponent<TextMeshProUGUI>().text = itemRow[i].name;
                gameObject.transform.Find("Items").GetChild(i).Find("Cost").Find("Quantity").GetComponent<TextMeshProUGUI>().text = itemRow[i].quantity.ToString();
                gameObject.transform.Find("Items").GetChild(i).Find("Thing").Find("Box").GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("Items").GetChild(i).Find("Thing").Find("Square").GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("Items").GetChild(i).Find("Cost").Find("Box").GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("Items").GetChild(i).Find("Cost").Find("Square").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void UpdateAbilities()
    {
        for (int i = 0; i < 8; i++)
        {
            gameObject.transform.Find("Abilities").GetChild(i).Find("Thing").Find("Name").GetComponent<TextMeshProUGUI>().text = null;
            gameObject.transform.Find("Abilities").GetChild(i).Find("Cost").Find("Cost").GetComponent<TextMeshProUGUI>().text = null;
            gameObject.transform.Find("Abilities").GetChild(i).Find("Thing").Find("Box").GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("Abilities").GetChild(i).Find("Thing").Find("Square").GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("Abilities").GetChild(i).Find("Cost").Find("Box").GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.Find("Abilities").GetChild(i).Find("Cost").Find("Square").GetComponent<SpriteRenderer>().enabled = false;
        }


        if (ability.Count > 0)
        {
            for (int i = 0; i < ability.Count; i++)
            {
                gameObject.transform.Find("Abilities").GetChild(i).Find("Thing").Find("Name").GetComponent<TextMeshProUGUI>().text = ability[i].name;
                gameObject.transform.Find("Abilities").GetChild(i).Find("Cost").Find("Cost").GetComponent<TextMeshProUGUI>().text = ability[i].cost.ToString();
                gameObject.transform.Find("Abilities").GetChild(i).Find("Thing").Find("Box").GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("Abilities").GetChild(i).Find("Thing").Find("Square").GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("Abilities").GetChild(i).Find("Cost").Find("Box").GetComponent<SpriteRenderer>().enabled = true;
                gameObject.transform.Find("Abilities").GetChild(i).Find("Cost").Find("Square").GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
}
