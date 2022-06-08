using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Map : MonoBehaviour
{
    public Tile[,] mapTiles;

    public GameObject Floor;
    public GameObject WallInner;
    public GameObject WallBottom;
    public GameObject WallLeft;
    public GameObject WallRight;
    public GameObject WallTop;
    public GameObject Stairs;
    public GameObject Shop;
    public GameObject InnerWallTopLeft;
    public GameObject InnerWallTopRight;
    public GameObject InnerWallBottomRight;
    public GameObject InnerWallBottomLeft;
    public GameObject OutterWallTopLeft;
    public GameObject OutterWallTopRight;
    public GameObject OutterWallBottomRight;
    public GameObject OutterWallBottomLeft;


    public GameObject Indicator;
    public GameObject Potion;
    public GameObject Mpotion;
    public GameObject playerPrefab;
    public GameObject playerSprite;
    public Player PlayerStats = new Player();


    public GameObject demonPrefab;
    public GameObject nelioPrefab;

    public Menu menu;
    public GameObject Crystals;

    public Gamemaster gamemaster;
    public bool playerSpawned = false;

    public List<SpriteRenderer> lookindicator;





    // Start is called before the first frame update
    public void Start()
    {
        


        GenerateMap();

    }


    public void GenerateMap()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        lookindicator = new List<SpriteRenderer>();

        mapTiles = new Tile[50, 50];

        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
            gamemaster.enemies = new List<EnemyMovement>();

        }

        for(int i = 0; i < tiles.Length; i++)
        {
            Destroy(tiles[i]);
        }

        for(int i = 0; i < lookindicator.Count; i++)
        {
            Destroy(lookindicator[i]);
        }

        SetupSpawns();


        //draw map

        for (int i = 0; i < mapTiles.GetLength(0); i++)
        {
            for (int j = 0; j < mapTiles.GetLength(1); j++)
            {
                if (mapTiles[i, j].type == Tile.TileType.FLOOR)
                {
                    Instantiate(Floor, new Vector3(i, j, 0), Quaternion.identity);
                    lookindicator.Add(Instantiate(Indicator, new Vector3(i, j, 0), Quaternion.identity).GetComponent<SpriteRenderer>());
                }

                else if (mapTiles[i, j].type == Tile.TileType.STAIRS)
                    Instantiate(Stairs, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.SHOP)
                    Instantiate(Shop, new Vector3(i, j, 0), Quaternion.identity);

                else if (i == 0 || i == 49 || j == 0 || j == 49)
                    Instantiate(WallInner, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL &&
                         mapTiles[i, j + 1].type == Tile.TileType.WALL &&
                         mapTiles[i - 1, j].type == Tile.TileType.WALL &&
                         mapTiles[i - 1, j + 1].type != Tile.TileType.WALL)
                    Instantiate(InnerWallBottomRight, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL &&
                         mapTiles[i, j + 1].type == Tile.TileType.WALL &&
                         mapTiles[i + 1, j].type == Tile.TileType.WALL &&
                         mapTiles[i + 1, j + 1].type != Tile.TileType.WALL)
                    Instantiate(InnerWallBottomLeft, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL &&
                         mapTiles[i, j - 1].type == Tile.TileType.WALL &&
                         mapTiles[i - 1, j].type == Tile.TileType.WALL &&
                         mapTiles[i - 1, j - 1].type != Tile.TileType.WALL)
                    Instantiate(InnerWallTopRight, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL &&
                         mapTiles[i, j - 1].type == Tile.TileType.WALL &&
                         mapTiles[i + 1, j].type == Tile.TileType.WALL &&
                         mapTiles[i + 1, j - 1].type != Tile.TileType.WALL)
                    Instantiate(InnerWallTopLeft, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL &&
                         mapTiles[i - 1, j].type != Tile.TileType.WALL &&
                         mapTiles[i, j + 1].type != Tile.TileType.WALL &&
                         mapTiles[i - 1, j + 1].type != Tile.TileType.WALL)
                    Instantiate(OutterWallTopLeft, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL &&
                         mapTiles[i + 1, j].type != Tile.TileType.WALL &&
                         mapTiles[i, j + 1].type != Tile.TileType.WALL &&
                         mapTiles[i + 1, j + 1].type != Tile.TileType.WALL)
                    Instantiate(OutterWallTopRight, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL &&
                         mapTiles[i + 1, j].type != Tile.TileType.WALL &&
                         mapTiles[i, j - 1].type != Tile.TileType.WALL &&
                         mapTiles[i + 1, j - 1].type != Tile.TileType.WALL)
                    Instantiate(OutterWallBottomRight, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL &&
                         mapTiles[i - 1, j].type != Tile.TileType.WALL &&
                         mapTiles[i, j - 1].type != Tile.TileType.WALL &&
                         mapTiles[i - 1, j - 1].type != Tile.TileType.WALL)
                    Instantiate(OutterWallBottomLeft, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL && mapTiles[i, j - 1].type != Tile.TileType.WALL)
                    Instantiate(WallBottom, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL && mapTiles[i, j + 1].type != Tile.TileType.WALL)
                    Instantiate(WallTop, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL && mapTiles[i-1, j].type != Tile.TileType.WALL)
                    Instantiate(WallLeft, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL && mapTiles[i+1, j].type != Tile.TileType.WALL)
                    Instantiate(WallRight, new Vector3(i, j, 0), Quaternion.identity);

                else if (mapTiles[i, j].type == Tile.TileType.WALL)
                    Instantiate(WallInner, new Vector3(i, j, 0), Quaternion.identity);

                if (mapTiles[i, j].item != null)
                {
                    if(mapTiles[i, j].item.id == 1)
                    Instantiate(Potion, new Vector3(i, j, 0), Quaternion.identity);

                    if (mapTiles[i, j].item.id == 2)
                        Instantiate(Crystals, new Vector3(i, j, 0), Quaternion.identity);

                    if (mapTiles[i, j].item.id == 3)
                        Instantiate(Mpotion, new Vector3(i, j, 0), Quaternion.identity);
                }

            }
        }
    }

    public void SetupSpawns()
    {

        List<Vector3Int> FloorTiles = new List<Vector3Int>();
        int randommap = Random.Range(1, 3);
        if (randommap == 1)
        {
            Map1();
        }
        else if (randommap == 2)
        {
            Map2();
        }
        FloorTiles.Clear();
        for (int i = 0; i < mapTiles.GetLength(0); i++)
        {
            for (int j = 0; j < mapTiles.GetLength(1); j++)
            {
                if (mapTiles[i, j].type == Tile.TileType.FLOOR && mapTiles[i, j].func == Tile.TileFunc.ROOM)
                {
                    FloorTiles.Add(new Vector3Int(i, j, 0));
                }
            }
        }


        //Player
        int randomspawn = Random.Range(0, FloorTiles.Count);
        PlayerMovement playerscript;
        if (playerSpawned == false)
        {
            playerSprite = Instantiate(playerPrefab, FloorTiles[randomspawn], Quaternion.identity);
            playerscript = playerSprite.GetComponent<PlayerMovement>();
            mapTiles[FloorTiles[randomspawn].x, FloorTiles[randomspawn].y].entity = PlayerStats;
            FloorTiles.RemoveAt(randomspawn);
            PlayerStats.currentHealth = PlayerStats.Health;
            PlayerStats.currentMana = PlayerStats.PlayerMana;
            playerSpawned = true;

        }
        else
        {
            playerscript = playerSprite.GetComponent<PlayerMovement>();
            mapTiles[playerscript.PlayerPosX, playerscript.PlayerPosY].entity = null;
            mapTiles[FloorTiles[randomspawn].x, FloorTiles[randomspawn].y].entity = PlayerStats;
            Debug.Log(FloorTiles[randommap]);
            playerSprite.transform.position = new Vector3Int(FloorTiles[randomspawn].x, FloorTiles[randomspawn].y, 0);
            playerscript.PlayerPosX = FloorTiles[randomspawn].x;
            playerscript.PlayerPosY = FloorTiles[randomspawn].y;
            FloorTiles.RemoveAt(randomspawn);
        }


        //stairs
        randomspawn = Random.Range(0, FloorTiles.Count);
        mapTiles[FloorTiles[randomspawn].x, FloorTiles[randomspawn].y].type = Tile.TileType.STAIRS;
        FloorTiles.RemoveAt(randomspawn);


        //enemies
        int amount = Random.Range(2, 5);
        for (int i = 0; i < amount; i++)
        {
            randomspawn = Random.Range(0, FloorTiles.Count);
            float enemytype = Random.Range(0f, 100f);

            if (enemytype > 30f)
            {
                Instantiate(demonPrefab, FloorTiles[randomspawn], Quaternion.identity);
                Debug.Log("Test");
            }
            else
            {
                Instantiate(nelioPrefab, FloorTiles[randomspawn], Quaternion.identity);
            }
            FloorTiles.RemoveAt(randomspawn);
        }


        //Health Potion
        amount = Random.Range(2, 7);
        for (int i = 0; i < amount; i++)
        {
            randomspawn = (int)Mathf.Round(Random.Range(0, FloorTiles.Count - 1));
            mapTiles[FloorTiles[randomspawn].x, FloorTiles[randomspawn].y].item = new Item(1, "Health Potion");
            FloorTiles.RemoveAt(randomspawn);
        }

        //Mana Potion
        amount = Random.Range(0, 3);
        for (int i = 0; i < amount; i++)
        {
            randomspawn = (int)Mathf.Round(Random.Range(0, FloorTiles.Count - 1));
            mapTiles[FloorTiles[randomspawn].x, FloorTiles[randomspawn].y].item = new Item(3, "Mana Potion");
            FloorTiles.RemoveAt(randomspawn);
        }

        //Crystals
        amount = Random.Range(2, 7);
        for (int i = 0; i < amount; i++)
        {
            randomspawn = (int)Mathf.Round(Random.Range(0, FloorTiles.Count - 1));
            mapTiles[FloorTiles[randomspawn].x, FloorTiles[randomspawn].y].item = new Item(2, "Crystal");
            FloorTiles.RemoveAt(randomspawn);
        }

        //shop
        randomspawn = Random.Range(0, FloorTiles.Count);
        float chance = Random.Range(0f, 100f);
        if(chance >= 60f)
        {
            mapTiles[FloorTiles[randomspawn].x, FloorTiles[randomspawn].y].type = Tile.TileType.SHOP;
            FloorTiles.RemoveAt(randomspawn);
        }

    }

    public void Map1()
    {
        for (int i = 0; i < mapTiles.GetLength(0); i++)
        {
            for (int j = 0; j < mapTiles.GetLength(1); j++)
            {
                mapTiles[i, j] = new Tile();
                mapTiles[i, j].type = Tile.TileType.WALL;
                mapTiles[i, j].func = Tile.TileFunc.NONE;
            }
        }

        for (int i = 10; i < 17; i++)
        {
            for (int j = 10; j < 17; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 17; i < 25; i++)
        {
            for (int j = 14; j < 15; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 10; i < 11; i++)
        {
            for (int j = 17; j < 23; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 11; i < 14; i++)
        {
            for (int j = 22; j < 23; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 25; i < 32; i++)
        {
            for (int j = 12; j < 26; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 20; i < 25; i++)
        {
            for (int j = 23; j < 24; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 14; i < 20; i++)
        {
            for (int j = 20; j < 27; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 14; i < 15; i++)
        {
            for (int j = 27; j < 35; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 10; i < 20; i++)
        {
            for (int j = 35; j < 40; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 20; i < 29; i++)
        {
            for (int j = 37; j < 38; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 28; i < 29; i++)
        {
            for (int j = 26; j < 38; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }
    }

    public void Map2()
    {
        for (int i = 0; i < mapTiles.GetLength(0); i++)
        {
            for (int j = 0; j < mapTiles.GetLength(1); j++)
            {
                mapTiles[i, j] = new Tile();
                mapTiles[i, j].type = Tile.TileType.WALL;
                mapTiles[i, j].func = Tile.TileFunc.NONE;
            }
        }

        for (int i = 23; i < 30; i++)
        {
            for (int j = 23; j < 30; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 30; i < 34; i++)
        {
            for (int j = 26; j < 27; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 34; i < 41; i++)
        {
            for (int j = 23; j < 30; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 37; i < 38; i++)
        {
            for (int j = 30; j < 38; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 37; i < 38; i++)
        {
            for (int j = 15; j < 23; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 19; i < 23; i++)
        {
            for (int j = 26; j < 27; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 12; i < 19; i++)
        {
            for (int j = 23; j < 30; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 15; i < 16; i++)
        {
            for (int j = 30; j < 38; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 15; i < 16; i++)
        {
            for (int j = 15; j < 23; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }


        for (int i = 26; i < 27; i++)
        {
            for (int j = 30; j < 34; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 23; i < 30; i++)
        {
            for (int j = 34; j < 41; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 16; i < 23; i++)
        {
            for (int j = 37; j < 38; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 30; i < 37; i++)
        {
            for (int j = 37; j < 38; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 26; i < 27; i++)
        {
            for (int j = 19; j < 23; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 23; i < 30; i++)
        {
            for (int j = 12; j < 19; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.ROOM;
            }
        }

        for (int i = 16; i < 23; i++)
        {
            for (int j = 15; j < 16; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }

        for (int i = 30; i < 37; i++)
        {
            for (int j = 15; j < 16; j++)
            {
                mapTiles[i, j].type = Tile.TileType.FLOOR;
                mapTiles[i, j].func = Tile.TileFunc.HALLWAY;
            }
        }
    }
}