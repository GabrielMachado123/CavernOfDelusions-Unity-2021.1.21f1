using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public SpriteRenderer Item1;
    public SpriteRenderer Item2;
    public int select = 0;
    public Menu menuscript;
    public PlayerMovement player;
    public bool initialization = false;

    void Start()
    {
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
        else if(initialization == false)
        {
            Debug.Log(player);
            player.shop = gameObject;
            gameObject.SetActive(false);
            initialization = true;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) && (select == 0))
        {
            select = 1;
        }

        if (Input.GetKeyDown(KeyCode.W) && (select - 1 > 0)) 
        {
            select--;
        }

        if (Input.GetKeyDown(KeyCode.S) && (select + 1 < 3))
        {
            select++;
        }

        if (select == 1)
        {
            Item1.color = new Color(0.3122997f, 0.5275972f, 0.5471698f);
        }

        else
        {
            Item1.color = new Color(0.1686275f, 0.2980392f, 0.3098039f);
        }

        if (select == 2)
        {
            Item2.color = new Color(0.3122997f, 0.5275972f, 0.5471698f);
        }

        else
        {
            Item2.color = new Color(0.1686275f, 0.2980392f, 0.3098039f);
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (select == 1)
            {
                Debug.Log(player);
                if (player.crystals >= 2)
                {
                    if (ItemSearch(select))
                    {
                        for (int i = 0; i < menuscript.itemRow.Count; i++)
                        {
                            if (menuscript.itemRow[i].id == select)
                            {
                                menuscript.itemRow[i].quantity++;
                                break;
                            }
                        }
                    }
                    else
                    {
                        menuscript.itemRow.Add(new InvItem(1, "Health Potion", 1));
                    }

                    menuscript.UpdateInventory();
                    player.crystals = player.crystals - 2;
                    player.crystaldisplay.text = player.crystals.ToString();
                }
            }
            else if (select == 2)
            {
                if (player.crystals >= 5)
                {
                    if (ItemSearch(select))
                    {
                        for (int i = 0; i < menuscript.itemRow.Count; i++)
                        {
                            if (menuscript.itemRow[i].id == select)
                            {
                                menuscript.itemRow[i].quantity++;
                                break;
                            }
                        }
                    }
                    else
                    {
                        menuscript.itemRow.Add(new InvItem(3, "Mana Potion", 1));
                    }

                    menuscript.UpdateInventory();
                    player.crystals = player.crystals - 5;
                    player.crystaldisplay.text = player.crystals.ToString();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            gameObject.SetActive(false);
            player.shopopen = false;
        }
        
    }

    public bool ItemSearch(int id)
    {
        for (int i = 0; i < menuscript.itemRow.Count; i ++)
        {
            if (menuscript.itemRow[i].id == id)
            {
                return true;
            }
        }

        return false;
    }
}
