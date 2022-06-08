using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    public int PlayerPosX;
    public int PlayerPosY;
    public int directionX = 1;//right = 0, up = 1, left = 2, down = 3;
    public int directionY = 0;
    private bool directionChange = false;
    private Map mapScript;
    public bool turn = true;

    public bool shot = false;

    private float dist;
    private Gamemaster gamemaster;

    public GameObject DamageHealPrefab;
    public GameObject DamageHealSprite;

    public GameObject DamageDonePrefab;
    public GameObject DamageDoneSprite;

    public GameObject petPrefab;
    public GameObject petSprite;


    public GameObject explosionPrefab;
    public GameObject explosionSprite;

    public GameObject MissPrefab;
    public GameObject MissSprite;

    public GameObject AttBuffPrefab;
    public GameObject AttBuffSprite;

    public GameObject DefBuffPrefab;
    public GameObject DefBuffSprite;

    public GameObject ShinePrefab;
    public GameObject ShineSprite;

    public GameObject MeditatePrefab;
    public GameObject MeditateSprite;

    public GameObject SmokePrefab;
    public GameObject SmokeSprite;

    public GameObject HealPrefab;
    public GameObject HealSprite;

    public GameObject FireSparkPrefab;
    public GameObject FireSparkSprite;

    public GameObject UnstableBeamPrefab;
    public GameObject UnstableBeamSprite;

    public Animator anim;
    public Vector3 FireSparkTarget;

    public int floor = 0;

    public Sprite on;
    public Sprite off;
    public int crystals = 0;
    public TextMeshProUGUI crystaldisplay;
    public bool shoptrigger = false;
    public int numberOfstates = 0;

    public bool shopopen = false;
    public GameObject shop;

    //menu stuff
    public GameObject menu;
    public bool menuOpen = false;
    float Timer = 0f;
    float Limit = 0.7f;

   
    void Start()
    {
        PlayerPosX = (int)transform.position.x;
        PlayerPosY = (int)transform.position.y;

        mapScript = GameObject.Find("Map").GetComponent<Map>();
        gamemaster = GameObject.Find("Master").GetComponent<Gamemaster>();

        gamemaster.AddPlayer(this);


        anim = GetComponent<Animator>();

        menu = GameObject.Find("Menu");
        menu.GetComponent<Menu>().player = gameObject.GetComponent<PlayerMovement>();
        menu.SetActive(false);
        crystaldisplay = GameObject.Find("Canvas").transform.Find("crystaldisplay").GetComponent<TextMeshProUGUI>();


    }


    void Update()
    {
        if (!menuOpen && !shopopen)
        {

            pMovement();
            SetDirection();
            basicAttack();
            OpenMenu();
          
        }
        
        if(transform.position.x == PlayerPosX && transform.position.y == PlayerPosY)
        {
            anim.SetInteger("WalkDirection", 0);
        }


        if (shot)
        {
            //Animação
          
            if (Timer < Limit)
            {
                Timer = Timer + 1f * Time.deltaTime;
                Debug.Log(Timer);
            }
            else
            {
                //O que quiseres
                shot = false;
                anim.SetInteger("ShootDirection", 0);
                Debug.Log("HEY");
                Timer = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
     

        


        if (mapScript.PlayerStats.currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }

    public bool itemSearch()
    {
        for(int i = 0; i < menu.GetComponent<Menu>().itemRow.Count; i++)
        {
            if (menu.GetComponent<Menu>().itemRow[i].id == mapScript.mapTiles[PlayerPosX, PlayerPosY].item.id)
                return true;
        }
        return false;
    }

    public bool PetFakeConection()
    {
        for (int i = 0; i < menu.GetComponent<Menu>().itemRow.Count; i++)
        {
            if (menu.GetComponent<Menu>().itemRow[i].id == 1)
            {
                return true;
            }
        }
        return false;
    }

    public void pMovement()
    {
        if (directionChange == false)
        {

            if (turn == true && transform.position.x == PlayerPosX && transform.position.y == PlayerPosY)
            {
                if (mapScript.mapTiles[PlayerPosX, PlayerPosY].item != null)
                {
                    if (mapScript.mapTiles[PlayerPosX, PlayerPosY].item.id == 2)
                    {
                        mapScript.mapTiles[PlayerPosX, PlayerPosY].item = null;
                        crystals++;
                        crystaldisplay.text = crystals.ToString();
                    }
                    else
                    {
                        if (menu.GetComponent<Menu>().itemRow.Count < 1)
                        {
                            menu.GetComponent<Menu>().itemRow.Add(new InvItem(mapScript.mapTiles[PlayerPosX, PlayerPosY].item.id, mapScript.mapTiles[PlayerPosX, PlayerPosY].item.name, 1));
                            mapScript.mapTiles[PlayerPosX, PlayerPosY].item = null;
                            menu.GetComponent<Menu>().UpdateInventory();
                        }
                        else
                        {
                            if (itemSearch())
                            {
                                for (int i = 0; i < menu.GetComponent<Menu>().itemRow.Count; i++)
                                {
                                    if (menu.GetComponent<Menu>().itemRow[i].id == mapScript.mapTiles[PlayerPosX, PlayerPosY].item.id)
                                    {
                                        menu.GetComponent<Menu>().itemRow[i].quantity++;
                                        mapScript.mapTiles[PlayerPosX, PlayerPosY].item = null;
                                        menu.GetComponent<Menu>().UpdateInventory();
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                menu.GetComponent<Menu>().itemRow.Add(new InvItem(mapScript.mapTiles[PlayerPosX, PlayerPosY].item.id, mapScript.mapTiles[PlayerPosX, PlayerPosY].item.name, 1));
                                mapScript.mapTiles[PlayerPosX, PlayerPosY].item = null;
                                menu.GetComponent<Menu>().UpdateInventory();
                            }
                        }
                    }

                }

                if (mapScript.mapTiles[PlayerPosX, PlayerPosY].type == Tile.TileType.STAIRS)
                {
                    mapScript.GenerateMap();
                    floor++;
                    float random = Random.Range(0f, 100f);
                    if(random > 50f)
                    {
                        if (PetFakeConection()) 
                        {
                            for (int i = 0; i < menu.GetComponent<Menu>().itemRow.Count; i++)
                            {
                                if (menu.GetComponent<Menu>().itemRow[i].id == 1)
                                {
                                    menu.GetComponent<Menu>().itemRow[i].quantity++;                                  
                                    menu.GetComponent<Menu>().UpdateInventory();
                                    petSprite = Instantiate(petPrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
                                    petSprite.GetComponent<PetFake>().pet = "your pet got a potion!";
                                    break;
                                }
                             
                            }
                        }
                        else
                        {
                            menu.GetComponent<Menu>().itemRow.Add(new InvItem(1, "Health Potion", 1));
                            menu.GetComponent<Menu>().UpdateInventory();
                            petSprite = Instantiate(petPrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
                            petSprite.GetComponent<PetFake>().pet = "your pet got a potion!";

                        }
                    }
                }

                if (mapScript.mapTiles[PlayerPosX, PlayerPosY].type == Tile.TileType.SHOP && shoptrigger == false)
                {
                    shop.SetActive(true);
                    shopopen = true;
                    shoptrigger = true;
                }
                else if (mapScript.mapTiles[PlayerPosX, PlayerPosY].type != Tile.TileType.SHOP && shoptrigger == true)
                {
                    shoptrigger = false;
                }

                if (floor >= 5)
                {
                    SceneManager.LoadScene(3);
                }

          

                if (Input.GetKey("d") && PlayerPosX < mapScript.mapTiles.GetLength(0) && mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].type != Tile.TileType.WALL && mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity == null)
                {
                    anim.SetInteger("WalkDirection", 1);
                    if (mapScript.PlayerStats.turnsAttBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsAttBuff;
                    }

                    if (mapScript.PlayerStats.turnsArmorBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsArmorBuff;
                    }

                    if (mapScript.PlayerStats.turnsMrBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsMrBuff;
                    }

                    mapScript.mapTiles[PlayerPosX, PlayerPosY].entity = null;
                    PlayerPosX++;
                    mapScript.mapTiles[PlayerPosX, PlayerPosY].entity = mapScript.PlayerStats;
                    turn = false;
                    dist = Vector3.Distance(transform.position, new Vector3(PlayerPosX, PlayerPosY, 0));
                  
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    directionX = 1;
                    directionY = 0;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    anim.SetBool("LookUp", false);

                }
                if (Input.GetKey("a") && PlayerPosX >= 0 && mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].type != Tile.TileType.WALL && mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity == null)
                {
                    anim.SetInteger("WalkDirection", 1);
                    if (mapScript.PlayerStats.turnsAttBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsAttBuff;
                    }

                    if (mapScript.PlayerStats.turnsArmorBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsArmorBuff;
                    }

                    if (mapScript.PlayerStats.turnsMrBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsMrBuff;
                    }
                    mapScript.mapTiles[PlayerPosX, PlayerPosY].entity = null;
                    PlayerPosX--;
                    mapScript.mapTiles[PlayerPosX, PlayerPosY].entity = mapScript.PlayerStats;
                    turn = false;
                    dist = Vector3.Distance(transform.position, new Vector3(PlayerPosX, PlayerPosY, 0));
                    
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    directionX = -1;
                    directionY = 0;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    anim.SetBool("LookUp", false);
                }
                if (Input.GetKey("w") && PlayerPosY < mapScript.mapTiles.GetLength(1) && mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].type != Tile.TileType.WALL && mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity == null)
                {
                    anim.SetInteger("WalkDirection", 2);
                    if (mapScript.PlayerStats.turnsAttBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsAttBuff;
                    }

                    if (mapScript.PlayerStats.turnsArmorBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsArmorBuff;
                    }

                    if (mapScript.PlayerStats.turnsMrBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsMrBuff;
                    }
                    mapScript.mapTiles[PlayerPosX, PlayerPosY].entity = null;
                    PlayerPosY++;
                    mapScript.mapTiles[PlayerPosX, PlayerPosY].entity = mapScript.PlayerStats;
                    turn = false;
                    dist = Vector3.Distance(transform.position, new Vector3(PlayerPosX, PlayerPosY, 0));
                    
                    directionX = 0;
                    directionY = 1;
                    anim.SetBool("LookUp", true);
                }
                if (Input.GetKey("s") && PlayerPosY >= 0 && mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].type != Tile.TileType.WALL && mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity == null)
                {
                    anim.SetInteger("WalkDirection", 3);
                    if (mapScript.PlayerStats.turnsAttBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsAttBuff;
                    }

                    if (mapScript.PlayerStats.turnsArmorBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsArmorBuff;
                    }

                    if (mapScript.PlayerStats.turnsMrBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsMrBuff;
                    }
                    mapScript.mapTiles[PlayerPosX, PlayerPosY].entity = null;
                    PlayerPosY--;
                    mapScript.mapTiles[PlayerPosX, PlayerPosY].entity = mapScript.PlayerStats;
                    turn = false;
                    dist = Vector3.Distance(transform.position, new Vector3(PlayerPosX, PlayerPosY, 0));
                 
                    directionX = 0;
                    directionY = -1;
                    anim.SetBool("LookUp", false);
                }

                if (Input.GetKeyDown("x"))
                {
                    if (mapScript.PlayerStats.turnsAttBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsAttBuff;
                    }

                    if (mapScript.PlayerStats.turnsArmorBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsArmorBuff;
                    }

                    if (mapScript.PlayerStats.turnsMrBuff > 0)
                    {
                        --mapScript.PlayerStats.turnsMrBuff;
                    }
                    turn = false;
                }

            }
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerPosX, PlayerPosY, 0), dist * 3 * Time.deltaTime);
    }

    public void SetDirection()
    {
        if (mapScript.lookindicator.Count > 0)
        {
            if (Input.GetKey("c"))
            {
                directionChange = true;

                for (int c = 0; c < mapScript.lookindicator.Count; c++)
                {
                    mapScript.lookindicator[c].enabled = true;
                    mapScript.lookindicator[c].sprite = off;

                }

                int i = 0;
                while (mapScript.mapTiles[PlayerPosX + i * directionX, PlayerPosY + i * directionY].type != Tile.TileType.WALL)
                {
                    for (int j = 0; j < mapScript.lookindicator.Count; j++)
                    {
                        if (mapScript.lookindicator[j].transform.position.x == PlayerPosX + i * directionX && mapScript.lookindicator[j].transform.position.y == PlayerPosY + i * directionY)
                        {
                            mapScript.lookindicator[j].sprite = on;
                        }
                    }
                    i++;
                }

                if (Input.GetKey("w") && Input.GetKey("d"))
                {
                    directionX = 1;
                    directionY = 1;
                    anim.SetBool("LookUp", true);
                }
                else if (Input.GetKey("w") && Input.GetKey("a"))
                {
                    directionX = -1;
                    directionY = 1;
                    anim.SetBool("LookUp", true);
                }
                else if (Input.GetKey("s") && Input.GetKey("a"))
                {
                    directionX = -1;
                    directionY = -1;
                    anim.SetBool("LookUp", false);
                }
                else if (Input.GetKey("s") && Input.GetKey("d"))
                {
                    directionX = 1;
                    directionY = -1;
                    anim.SetBool("LookUp", false);
                }
                else if (Input.GetKey("d"))
                {
                    directionX = 1;
                    directionY = 0;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    anim.SetBool("LookUp", false);

                }
                else if (Input.GetKey("w"))
                {
                    directionX = 0;
                    directionY = 1;
                    anim.SetBool("LookUp", true);
                }
                else if (Input.GetKey("a"))
                {
                    directionX = -1;
                    directionY = 0;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    anim.SetBool("LookUp", false);
                }
                else if (Input.GetKey("s"))
                {
                    directionX = 0;
                    directionY = -1;
                    anim.SetBool("LookUp", false);
                }
            }
            else
            {
                directionChange = false;
                for (int i = 0; i < mapScript.lookindicator.Count; i++)
                {
                    mapScript.lookindicator[i].enabled = false;
                }
            }

        }
    }
    public void basicAttack()
    {
        float damage = mapScript.PlayerStats.attack;
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 80f;

        if (Input.GetKeyDown(KeyCode.Keypad7) && turn == true)
        {

            if (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity != null)
            {
                anim.SetInteger("ShootDirection", 2);
                shot = true;
                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor >= 0)
                    {


                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor + 100));
                        posmitigationdmg = damageMultiplier * damage;
                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor)));
                        posmitigationdmg = damageMultiplier * damage;
                    }

                    mapScript.PlayerStats.currentMana += 5;

                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY+ directionY].entity.currentHealth - posmitigationdmg;


                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;
                }
                else
                {
                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                }
            }
            turn = false;
            if (mapScript.PlayerStats.turnsAttBuff > 0)
            {
                --mapScript.PlayerStats.turnsAttBuff;
            }
            if (mapScript.PlayerStats.turnsArmorBuff > 0)
            {
                --mapScript.PlayerStats.turnsArmorBuff;
            }

            if (mapScript.PlayerStats.turnsMrBuff > 0)
            {
                --mapScript.PlayerStats.turnsMrBuff;
            }
           
        }

    }


    public void HeavyFists()
    {

        float damage = mapScript.PlayerStats.attack;
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 80f;
        int ManaCost = 5;
        float skilldmg = damage * 1.5f;
        int armorReduction = 5;



        if (mapScript.PlayerStats.currentMana >= ManaCost)
        {

            if (turn == true)
            {
                anim.SetInteger("ShootDirection", 2);
                shot = true;
                mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;

                missChance = Random.Range(0f, 100f);

                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor >= 0)
                    {



                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor + 100));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }


                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor - armorReduction;
                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;
                }
                else
                {
                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                }
                turn = false;
                if (mapScript.PlayerStats.turnsAttBuff > 0)
                {
                    --mapScript.PlayerStats.turnsAttBuff;
                }
                if (mapScript.PlayerStats.turnsArmorBuff > 0)
                {
                    --mapScript.PlayerStats.turnsArmorBuff;
                }

                if (mapScript.PlayerStats.turnsMrBuff > 0)
                {
                    --mapScript.PlayerStats.turnsMrBuff;
                }
            }
        }
    }

    public void HeatUpBlood()
    {

        float baseHeal = 10;
        int ManaCost = 5;
        float totalHeal = baseHeal + (mapScript.PlayerStats.Health * 0.2f);

        if (mapScript.PlayerStats.currentMana >= ManaCost && mapScript.PlayerStats.currentHealth < mapScript.PlayerStats.Health && turn == true)
        {
            anim.SetInteger("ShootDirection", 1);
            shot = true;
            mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;

            mapScript.PlayerStats.currentHealth = mapScript.PlayerStats.currentHealth + totalHeal;

            DamageHealSprite = Instantiate(DamageHealPrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
            DamageHealSprite.GetComponent<damageFeedback>().damage = totalHeal;

            HealSprite = Instantiate(HealPrefab, new Vector3(PlayerPosX, PlayerPosY , 0), Quaternion.identity);

            if (mapScript.PlayerStats.currentHealth > mapScript.PlayerStats.Health)
            {
                mapScript.PlayerStats.currentHealth = mapScript.PlayerStats.Health;
            }
            turn = false;
            if (mapScript.PlayerStats.turnsAttBuff > 0)
            {
                --mapScript.PlayerStats.turnsAttBuff;
            }
            if (mapScript.PlayerStats.turnsArmorBuff > 0)
            {
                --mapScript.PlayerStats.turnsArmorBuff;
            }

            if (mapScript.PlayerStats.turnsMrBuff > 0)
            {
                --mapScript.PlayerStats.turnsMrBuff;
            }
        }

    }


    public void BloodyFists()
    {

        float damage = mapScript.PlayerStats.attack;
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 80f;
        int ManaCost = 15;
        float skilldmg = damage * 2f;



        if (mapScript.PlayerStats.currentMana >= ManaCost)
        {
            if (turn == true)
            {
                anim.SetInteger("ShootDirection", 2);
                shot = true;
                mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;

                missChance = Random.Range(0f, 100f);

                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor >= 0)
                    {



                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor + 100));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }
                    mapScript.PlayerStats.currentHealth = mapScript.PlayerStats.currentHealth + posmitigationdmg;

                    DamageHealSprite = Instantiate(DamageHealPrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
                    DamageHealSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;

                    if (mapScript.PlayerStats.currentHealth > mapScript.PlayerStats.Health)
                    {
                        mapScript.PlayerStats.currentHealth = mapScript.PlayerStats.Health;
                    }

                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;

                }
                else
                {
                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                }
                turn = false;
               
                if (mapScript.PlayerStats.turnsAttBuff > 0)
                {
                    --mapScript.PlayerStats.turnsAttBuff;
                }
                if (mapScript.PlayerStats.turnsArmorBuff > 0)
                {
                    --mapScript.PlayerStats.turnsArmorBuff;
                }

                if (mapScript.PlayerStats.turnsMrBuff > 0)
                {
                    --mapScript.PlayerStats.turnsMrBuff;
                }
            }
        }
    }


    public void FireSpark()
    {

        float damage = mapScript.PlayerStats.attack;
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 70f;
        int ManaCost = 10;
        float skilldmg = damage * 2.1f;
        int j = 1;


        if (mapScript.PlayerStats.currentMana >= ManaCost)
        {
            if (turn == true)
            {
                anim.SetInteger("ShootDirection", 1);
                shot = true;
                mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;



                while (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity == null && mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].type == Tile.TileType.FLOOR)
                {
                    j++;
                }

                if (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity != null)
                {

                    missChance = Random.Range(0f, 100f);
                    if (missChance <= accuracy)
                    {
                        if (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR >= 0)
                        {
                            damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR + 100));

                            posmitigationdmg = damageMultiplier * skilldmg;

                        }
                        else
                        {
                            damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR)));
                            posmitigationdmg = damageMultiplier * skilldmg;
                        }


                        FireSparkSprite = Instantiate(FireSparkPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);



                        FireSparkSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);


                        mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.currentHealth - posmitigationdmg;

                        DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY + 0.5f, 0), Quaternion.identity);
                        DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;


                    }
                    else
                    {

                        FireSparkSprite = Instantiate(FireSparkPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);
                        FireSparkSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);
                        MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY + 0.5f, 0), Quaternion.identity);
                    }
                }
                else
                {
                    FireSparkSprite = Instantiate(FireSparkPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);
                    FireSparkSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);
                }

            }
        }
    }

    public void GroundSlam()
    {


        float damage = mapScript.PlayerStats.attack;
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 80f;
        int ManaCost = 20;
        float skilldmg = damage * 0.9f;



        if (mapScript.PlayerStats.currentMana >= ManaCost && turn == true)
        {
            anim.SetInteger("ShootDirection", 1);
            shot = true;
            turn = false;
            mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;
            if (mapScript.PlayerStats.turnsAttBuff > 0)
            {
                --mapScript.PlayerStats.turnsAttBuff;
            }
            if (mapScript.PlayerStats.turnsArmorBuff > 0)
            {
                --mapScript.PlayerStats.turnsArmorBuff;
            }

            if (mapScript.PlayerStats.turnsMrBuff > 0)
            {
                --mapScript.PlayerStats.turnsMrBuff;
            }

            if (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity != null)
            {

                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity.Armor >= 0)
                    {
                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity.Armor + 100));

                        posmitigationdmg = damageMultiplier * skilldmg;

                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity.stun = 4;

                    mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + 1, PlayerPosY + 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;



                    Debug.Log(mapScript.mapTiles[PlayerPosX + 1, PlayerPosY].entity.currentHealth);
                }
                else
                {

                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + 1, PlayerPosY + 0.5f, 0), Quaternion.identity);
                }
            }

            if (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity != null)
            {

                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity.Armor >= 0)
                    {
                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity.Armor + 100));

                        posmitigationdmg = damageMultiplier * skilldmg;

                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity.stun = 4;

                    mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity.currentHealth = mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX - 1, PlayerPosY + 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;



                    Debug.Log(mapScript.mapTiles[PlayerPosX - 1, PlayerPosY].entity.currentHealth);
                }
                else
                {

                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX - 1, PlayerPosY + 0.5f, 0), Quaternion.identity);
                }
            }

            if (mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity != null)
            {

                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity.Armor >= 0)
                    {
                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity.Armor + 100));

                        posmitigationdmg = damageMultiplier * skilldmg;

                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity.stun = 4;

                    mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity.currentHealth = mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX, PlayerPosY + 1.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;

                    Debug.Log(mapScript.mapTiles[PlayerPosX, PlayerPosY + 1].entity.currentHealth);
                }
                else
                {

                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX, PlayerPosY + 1.5f, 0), Quaternion.identity);
                    Debug.Log("miss");
                }
            }

            if (mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity != null)
            {

                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity.Armor >= 0)
                    {
                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity.Armor + 100));

                        posmitigationdmg = damageMultiplier * skilldmg;

                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity.stun = 4;

                    mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity.currentHealth = mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX, PlayerPosY - 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;

                    Debug.Log(mapScript.mapTiles[PlayerPosX, PlayerPosY - 1].entity.currentHealth);
                }
                else
                {

                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX, PlayerPosY - 0.5f, 0), Quaternion.identity);
                    Debug.Log("miss");
                }
            }


            if (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY + 1].entity != null)
            {

                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY + 1].entity.Armor >= 0)
                    {
                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY + 1].entity.Armor + 100));

                        posmitigationdmg = damageMultiplier * skilldmg;

                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + 1, PlayerPosY + 1].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    mapScript.mapTiles[PlayerPosX + 1, PlayerPosY + 1].entity.stun = 4;

                    mapScript.mapTiles[PlayerPosX + 1, PlayerPosY + 1].entity.currentHealth = mapScript.mapTiles[PlayerPosX + 1, PlayerPosY + 1].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + 1, PlayerPosY + 1.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;

                    Debug.Log(mapScript.mapTiles[PlayerPosX + 1, PlayerPosY + 1].entity.currentHealth);
                }
                else
                {

                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + 1, PlayerPosY + 1.5f, 0), Quaternion.identity);
                    Debug.Log("miss");
                }
            }


            if (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY + 1].entity != null)
            {

                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY + 1].entity.Armor >= 0)
                    {
                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY + 1].entity.Armor + 100));

                        posmitigationdmg = damageMultiplier * skilldmg;

                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX - 1, PlayerPosY + 1].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    mapScript.mapTiles[PlayerPosX - 1, PlayerPosY + 1].entity.stun = 4;

                    mapScript.mapTiles[PlayerPosX - 1, PlayerPosY + 1].entity.currentHealth = mapScript.mapTiles[PlayerPosX - 1, PlayerPosY + 1].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX - 1, PlayerPosY + 1.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;

                    Debug.Log(mapScript.mapTiles[PlayerPosX - 1, PlayerPosY + 1].entity.currentHealth);
                }
                else
                {

                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX - 1, PlayerPosY + 1.5f, 0), Quaternion.identity);
                    Debug.Log("miss");
                }
            }


            if (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY - 1].entity != null)
            {

                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY - 1].entity.Armor >= 0)
                    {
                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX - 1, PlayerPosY - 1].entity.Armor + 100));

                        posmitigationdmg = damageMultiplier * skilldmg;

                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX - 1, PlayerPosY - 1].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    mapScript.mapTiles[PlayerPosX - 1, PlayerPosY - 1].entity.stun = 4;

                    mapScript.mapTiles[PlayerPosX - 1, PlayerPosY - 1].entity.currentHealth = mapScript.mapTiles[PlayerPosX - 1, PlayerPosY - 1].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX - 1, PlayerPosY - 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;

                    Debug.Log(mapScript.mapTiles[PlayerPosX - 1, PlayerPosY - 1].entity.currentHealth);
                }
                else
                {

                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX - 1, PlayerPosY - 0.5f, 0), Quaternion.identity);
                    Debug.Log("miss");
                }
            }

            if (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY - 1].entity != null)
            {

                missChance = Random.Range(0f, 100f);
                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY - 1].entity.Armor >= 0)
                    {
                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + 1, PlayerPosY - 1].entity.Armor + 100));

                        posmitigationdmg = damageMultiplier * skilldmg;

                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + 1, PlayerPosY - 1].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    mapScript.mapTiles[PlayerPosX + 1, PlayerPosY - 1].entity.stun = 4;

                    mapScript.mapTiles[PlayerPosX + 1, PlayerPosY - 1].entity.currentHealth = mapScript.mapTiles[PlayerPosX + 1, PlayerPosY - 1].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + 1, PlayerPosY - 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;

                    Debug.Log(mapScript.mapTiles[PlayerPosX + 1, PlayerPosY - 1].entity.currentHealth);
                }
                else
                {

                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + 1, PlayerPosY - 0.5f, 0), Quaternion.identity);
                    Debug.Log("miss");
                }
            }



            SmokeSprite = Instantiate(SmokePrefab, new Vector3(PlayerPosX + 1, PlayerPosY, 0), Quaternion.identity);

            SmokeSprite = Instantiate(SmokePrefab, new Vector3(PlayerPosX - 1, PlayerPosY, 0), Quaternion.identity);

           SmokeSprite = Instantiate(SmokePrefab, new Vector3(PlayerPosX, PlayerPosY + 1, 0), Quaternion.identity);

            SmokeSprite = Instantiate(SmokePrefab, new Vector3(PlayerPosX, PlayerPosY - 1, 0), Quaternion.identity);

            SmokeSprite = Instantiate(SmokePrefab, new Vector3(PlayerPosX + 1, PlayerPosY + 1, 0), Quaternion.identity);

            SmokeSprite = Instantiate(SmokePrefab, new Vector3(PlayerPosX - 1, PlayerPosY + 1, 0), Quaternion.identity);

            SmokeSprite = Instantiate(SmokePrefab, new Vector3(PlayerPosX - 1, PlayerPosY - 1, 0), Quaternion.identity);

            SmokeSprite = Instantiate(SmokePrefab, new Vector3(PlayerPosX + 1, PlayerPosY - 1, 0), Quaternion.identity);
        }
    }

    public void Meditate()
    {
        float damage = mapScript.PlayerStats.attack;
        float damageBuff;
        int ManaCost = 15;

        if ((mapScript.PlayerStats.currentMana >= ManaCost && turn == true) )
        {
            anim.SetInteger("ShootDirection", 1);
            shot = true;
            turn = false;
            mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;

            damageBuff = (damage) * (0.3f);
            mapScript.PlayerStats.attackBuff += damageBuff;
            mapScript.PlayerStats.turnsAttBuff = 5;
            if (numberOfstates == 0)
            {
                AttBuffSprite = Instantiate(AttBuffPrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
            }
            else if(numberOfstates == 1)
            {
                AttBuffSprite = Instantiate(AttBuffPrefab, new Vector3(PlayerPosX - 0.25f, PlayerPosY + 0.5f, 0), Quaternion.identity);
            }

            MeditateSprite = Instantiate(MeditatePrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
        }

    }

    public void StoneSkin()
    {
        int ManaCost = 15;
        float armorBuff;
        float mrBuff;

        if ((mapScript.PlayerStats.currentMana >= ManaCost && turn == true))
        {
            anim.SetInteger("ShootDirection", 1);
            shot = true;
            turn = false;
            mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;

            armorBuff = 25 + (mapScript.PlayerStats.Armor) * (0.25f);
            mapScript.PlayerStats.armorBuff += armorBuff;
            Debug.Log(mapScript.PlayerStats.armorBuff);
            mrBuff = 25 + (mapScript.PlayerStats.MR) * (0.25f);
            mapScript.PlayerStats.mrBuff += mrBuff;

            mapScript.PlayerStats.turnsMrBuff += 8;
            mapScript.PlayerStats.turnsArmorBuff += 8;
            if (numberOfstates == 0)
            {
                DefBuffSprite = Instantiate(DefBuffPrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
            }
            else if (numberOfstates == 1)
            {
                DefBuffSprite = Instantiate(DefBuffPrefab, new Vector3(PlayerPosX + 0.25f, PlayerPosY + 0.5f, 0), Quaternion.identity);
            }

            ShineSprite = Instantiate(ShinePrefab, new Vector3(PlayerPosX, PlayerPosY, 0), Quaternion.identity);

        }
    }

    public void UnstableBeam()
    {

        float damage = mapScript.PlayerStats.attack;
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 50f;
        int ManaCost = 20;
        float skilldmg = damage * 5.5f;
        int j = 1;


        if (mapScript.PlayerStats.currentMana >= ManaCost)
        {
            if (turn == true)
            {
                anim.SetInteger("ShootDirection", 1);
                shot = true;
                
                mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;



                while (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity == null && mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].type == Tile.TileType.FLOOR)
                {
                    j++;
                }

                if (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity != null)
                {

                    missChance = Random.Range(0f, 100f);
                    if (missChance <= accuracy)
                    {
                        if (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR >= 0)
                        {
                            damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR + 100));

                            posmitigationdmg = damageMultiplier * skilldmg;

                        }
                        else
                        {
                            damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR)));
                            posmitigationdmg = damageMultiplier * skilldmg;
                        }


                        UnstableBeamSprite = Instantiate(UnstableBeamPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);



                        UnstableBeamSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);
                      
                     

                        mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.currentHealth - posmitigationdmg;

                        DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY + 0.5f, 0), Quaternion.identity);
                        DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;


                    }
                    else
                    {

                        UnstableBeamSprite = Instantiate(UnstableBeamPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);
                        UnstableBeamSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);
                        MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY + 0.5f, 0), Quaternion.identity);
                    }
                }
                else
                {
                    UnstableBeamSprite = Instantiate(UnstableBeamPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);
                    UnstableBeamSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);
                }

            }
        }
    }

    public void BreakArmor()
    {

        float damage = mapScript.PlayerStats.attack;
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 80f;
        int ManaCost = 15;
        float skilldmg = damage;
       



        if (mapScript.PlayerStats.currentMana >= ManaCost)
        {

            if (turn == true)
            {
                mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;
                anim.SetInteger("ShootDirection", 2);
                shot = true;
                missChance = Random.Range(0f, 100f);

                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor >= 0)
                    {



                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor + 100));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

                    Debug.Log(mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor);
                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor / 2;
                    Debug.Log(mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor);
                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;
                }
                else
                {
                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                }
                turn = false;
                if (mapScript.PlayerStats.turnsAttBuff > 0)
                {
                    --mapScript.PlayerStats.turnsAttBuff;
                }
                if (mapScript.PlayerStats.turnsArmorBuff > 0)
                {
                    --mapScript.PlayerStats.turnsArmorBuff;
                }

                if (mapScript.PlayerStats.turnsMrBuff > 0)
                {
                    --mapScript.PlayerStats.turnsMrBuff;
                }
            }
        }
    }


    public void ConsumeSoul()
    {

       
        float missChance;
        float accuracy = 80f;
        int ManaCost = 50;
        float armorBuff;
        float mrBuff;
        float damageBuff;


        if (mapScript.PlayerStats.currentMana >= ManaCost)
        {

            if (turn == true)
            {
                anim.SetInteger("ShootDirection", 1);
                shot = true;
                mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;

                missChance = Random.Range(0f, 100f);

                if (missChance <= accuracy)
                {

                    damageBuff = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.attack;
                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.attack = 0;
                    mapScript.PlayerStats.attackBuff += damageBuff;
                    mapScript.PlayerStats.turnsAttBuff = 4;
                    if (numberOfstates == 0)
                    {
                        AttBuffSprite = Instantiate(AttBuffPrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
                    }
                    else if (numberOfstates == 1)
                    {
                        AttBuffSprite = Instantiate(AttBuffPrefab, new Vector3(PlayerPosX - 0.25f, PlayerPosY + 0.5f, 0), Quaternion.identity);
                    }

                    armorBuff = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor;
                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor = 0;
                    mrBuff = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.MR;
                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.MR = 0;
                                 
                    mapScript.PlayerStats.armorBuff += armorBuff;
                    mapScript.PlayerStats.mrBuff += mrBuff;

                    mapScript.PlayerStats.turnsMrBuff += 4;
                    mapScript.PlayerStats.turnsArmorBuff += 4;
                    if (numberOfstates == 0)
                    {
                        DefBuffSprite = Instantiate(DefBuffPrefab, new Vector3(PlayerPosX, PlayerPosY + 0.5f, 0), Quaternion.identity);
                    }
                    else if (numberOfstates == 1)
                    {
                        DefBuffSprite = Instantiate(DefBuffPrefab, new Vector3(PlayerPosX + 0.25f, PlayerPosY + 0.5f, 0), Quaternion.identity);
                    }

                }
                else
                {
                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                }
                turn = false;
                if (mapScript.PlayerStats.turnsAttBuff > 0)
                {
                    --mapScript.PlayerStats.turnsAttBuff;
                }
                if (mapScript.PlayerStats.turnsArmorBuff > 0)
                {
                    --mapScript.PlayerStats.turnsArmorBuff;
                }

                if (mapScript.PlayerStats.turnsMrBuff > 0)
                {
                    --mapScript.PlayerStats.turnsMrBuff;
                }
            }
        }
    }



    public void Judgement()
    {

        float damage = (mapScript.PlayerStats.attack * 0.8f) + 2f * (mapScript.PlayerStats.Armor + mapScript.PlayerStats.MR);
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 80f;
        int ManaCost = 20;
        float skilldmg = damage;




        if (mapScript.PlayerStats.currentMana >= ManaCost )
        {

            if (turn == true)
            {
                anim.SetInteger("ShootDirection", 2);
                shot = true;
                mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana - ManaCost;

                missChance = Random.Range(0f, 100f);

                if (missChance <= accuracy)
                {
                    if (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor >= 0)
                    {



                        damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor + 100));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }
                    else
                    {
                        damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.Armor)));
                        posmitigationdmg = damageMultiplier * skilldmg;
                    }

              
                    mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + directionX, PlayerPosY + directionY].entity.currentHealth - posmitigationdmg;

                    DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;
                }
                else
                {
                    MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY + 0.5f, 0), Quaternion.identity);
                }
                turn = false;
                if (mapScript.PlayerStats.turnsAttBuff > 0)
                {
                    --mapScript.PlayerStats.turnsAttBuff;
                }
                if (mapScript.PlayerStats.turnsArmorBuff > 0)
                {
                    --mapScript.PlayerStats.turnsArmorBuff;
                }

                if (mapScript.PlayerStats.turnsMrBuff > 0)
                {
                    --mapScript.PlayerStats.turnsMrBuff;
                }
            }
        }
    }


    public void BloodBullets()
    {
        float damage = mapScript.PlayerStats.attack;
        float posmitigationdmg;
        float damageMultiplier;
        float missChance;
        float accuracy = 80f;
        float cost = (mapScript.PlayerStats.currentHealth * 0.3f);
        float skilldmg = damage * 3.5f + cost;
        int j = 1;


        if (mapScript.PlayerStats.currentHealth > 1 )
        {
            if (turn == true)
            {
                anim.SetInteger("ShootDirection", 1);
                shot = true;
                mapScript.PlayerStats.currentHealth = (mapScript.PlayerStats.currentHealth - cost);



                while (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity == null && mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].type == Tile.TileType.FLOOR)
                {
                    j++;
                }

                if (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity != null)
                {

                    missChance = Random.Range(0f, 100f);
                    if (missChance <= accuracy)
                    {
                        if (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR >= 0)
                        {
                            damageMultiplier = (100 / (mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR + 100));

                            posmitigationdmg = damageMultiplier * skilldmg;

                        }
                        else
                        {
                            damageMultiplier = (2 - (100 / (100 - mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.MR)));
                            posmitigationdmg = damageMultiplier * skilldmg;
                        }


                        UnstableBeamSprite = Instantiate(UnstableBeamPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);



                        UnstableBeamSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);
                        UnstableBeamSprite.GetComponent<SpriteRenderer>().color = Color.red;


                        mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.currentHealth = mapScript.mapTiles[PlayerPosX + j * directionX, PlayerPosY + j * directionY].entity.currentHealth - posmitigationdmg;

                        DamageDoneSprite = Instantiate(DamageDonePrefab, new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY + 0.5f, 0), Quaternion.identity);
                        DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;


                    }
                    else
                    {

                        UnstableBeamSprite = Instantiate(UnstableBeamPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);
                        UnstableBeamSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);
                        UnstableBeamSprite.GetComponent<SpriteRenderer>().color = Color.red;
                        MissSprite = Instantiate(MissPrefab, new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY + 0.5f, 0), Quaternion.identity);
                    }
                }
                else
                {
                    UnstableBeamSprite = Instantiate(UnstableBeamPrefab, new Vector3(PlayerPosX + directionX, PlayerPosY + directionY, 0), Quaternion.identity);
                    UnstableBeamSprite.GetComponent<SpriteRenderer>().color = Color.red;
                    UnstableBeamSprite.GetComponent<FireSparkRoute>().target = new Vector3(PlayerPosX + j * directionX, PlayerPosY + j * directionY, 0f);
                }

            }
        }
    }

    public void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            menu.SetActive(true);
            menuOpen = true;
        }
    }

    public void HealthPotion()
    {
        if(mapScript.mapTiles[PlayerPosX, PlayerPosY].entity.currentHealth + 50 < mapScript.mapTiles[PlayerPosX, PlayerPosY].entity.Health)
        {
            mapScript.mapTiles[PlayerPosX, PlayerPosY].entity.currentHealth = mapScript.mapTiles[PlayerPosX, PlayerPosY].entity.currentHealth + 50;
        }
        else
        {
            mapScript.mapTiles[PlayerPosX, PlayerPosY].entity.currentHealth = mapScript.mapTiles[PlayerPosX, PlayerPosY].entity.Health;
        }
    }

    public void ManaPotion()
    {
        if (mapScript.PlayerStats.currentMana + 50 < 100)
        {
            mapScript.PlayerStats.currentMana = mapScript.PlayerStats.currentMana + 50;
        }
        else
        {
            mapScript.PlayerStats.currentMana = 100;
        }
    }
}
