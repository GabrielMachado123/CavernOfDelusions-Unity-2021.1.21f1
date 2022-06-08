using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Entity
{
    public float health;
    public string direction;
}


public class EnemyMovement : MonoBehaviour
{
    public int enemyPosX;
    public int enemyPosY;
    public Map mapScript;
    public Enemy EnemyStats = new Enemy();
    public Gamemaster gamemaster;
    private List<Vector2> doorPositions;

    public PlayerMovement PlayerLoc;

    //turn variables
    public bool turn = false;
    public float dist;
    //Scan variables
    private bool scan = false;
    private int doortargetX;
    private int doortargetY;

    private int roomstartX;
    private int roomstartY;
    private int roomendX;
    private int roomendY;

    //hallway variables
    private int hwdirectionX;
    private int hwdirectionY;

    public List<EnemyAbility> abilities;
    public bool queuedAttack = false;
    public int attackChoice = -1;
    public bool attackmove = false;


    private void Start()
    {
        enemyPosX = (int)transform.position.x;
        enemyPosY = (int)transform.position.y;
        mapScript = GameObject.Find("Map").GetComponent<Map>();
        gamemaster = GameObject.Find("Master").GetComponent<Gamemaster>();
        doorPositions = new List<Vector2>();



        mapScript.mapTiles[enemyPosX, enemyPosY].entity = EnemyStats;

        gamemaster.AddEnemy(this);
    }

    private void Update()
    {
        if(EnemyStats.currentHealth <= 0)
        {
            mapScript.PlayerStats.CurrentExp += 100;
            mapScript.mapTiles[enemyPosX, enemyPosY].entity = null;
            EnemyStats = null;
            gamemaster.enemies.Remove(this);
            Destroy(gameObject);
            return;
        }
    
        if(queuedAttack == false)
        {
            if(turn == true)
            {
                if (mapScript.mapTiles[enemyPosX, enemyPosY].entity.stun > 0)
                {
                    --mapScript.mapTiles[enemyPosX, enemyPosY].entity.stun;
                    turn = false;
                }
                else
                {
                    TerrifyinglyBigAIPleaseHelpMe();
                }
            }
        }
        else
        {
            EnemyAttack();
        }
        if(attackmove == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(enemyPosX, enemyPosY, 0), dist * 3 * Time.deltaTime);
        }
    }


    public void EnemyMoveAI(int TargetX, int TargetY, Vector3 distance)
    {
        if (enemyPosX > TargetX && enemyPosY > TargetY)
        {
            if (CheckDiagonalMovement(-1, -1))
            {
                MoveEnemy(-1, -1);
            }
            else if (distance.x > distance.y)
            {
                if (CheckCardinalMovement(-1, 0))
                {
                    MoveEnemy(-1, 0);
                }
                else if (CheckCardinalMovement(0, -1))
                {
                    MoveEnemy(0, -1);
                }
            }
            else if (distance.x < distance.y)
            {
                if (CheckCardinalMovement(0, -1))
                {
                    MoveEnemy(0, -1);
                }
                else if (CheckCardinalMovement(-1, 0))
                {
                    MoveEnemy(-1, 0);
                }
            }
            else if (distance.x == distance.y)
            {
                if (CheckCardinalMovement(-1, 0))
                {
                    MoveEnemy(-1, 0);
                }
                else if (CheckCardinalMovement(0, -1))
                {
                    MoveEnemy(0, -1);
                }
            }

        }
        else if (enemyPosX > TargetX && enemyPosY < TargetY)
        {
            if (CheckDiagonalMovement(-1, 1))
            {
                MoveEnemy(-1, 1);
            }
            else if (distance.x > distance.y)
            {
                if (CheckCardinalMovement(-1, 0))
                {
                    MoveEnemy(-1, 0);
                }
                else if (CheckCardinalMovement(0, 1))
                {
                    MoveEnemy(0, 1);
                }
            }
            else if (distance.x < distance.y)
            {
                if (CheckCardinalMovement(0, 1))
                {
                    MoveEnemy(0, 1);
                }
                else if (CheckCardinalMovement(-1, 0))
                {
                    MoveEnemy(-1, 0);
                }
            }
            else if (distance.x == distance.y)
            {
                if (CheckCardinalMovement(-1, 0))
                {
                    MoveEnemy(-1, 0);
                }
                else if (CheckCardinalMovement(0, 1))
                {
                    MoveEnemy(0, 1);
                }
            }

        }
        else if (enemyPosX < TargetX && enemyPosY > TargetY)
        {
            if (CheckDiagonalMovement(1, -1))
            {
                MoveEnemy(1, -1);
            }
            else if (distance.x > distance.y)
            {
                if (CheckCardinalMovement(1, 0))
                {
                    MoveEnemy(1, 0);
                }
                else if (CheckCardinalMovement(0, -1))
                {
                    MoveEnemy(0, -1);
                }
            }
            else if (distance.x < distance.y)
            {
                if (CheckCardinalMovement(0, -1))
                {
                    MoveEnemy(0, -1);
                }
                else if (CheckCardinalMovement(1, 0))
                {
                    MoveEnemy(1, 0);
                }
            }
            else if (distance.x == distance.y)
            {
                if (CheckCardinalMovement(1, 0))
                {
                    MoveEnemy(1, 0);
                }
                else if (CheckCardinalMovement(0, -1))
                {
                    MoveEnemy(0, -1);
                }
            }

        }
        else if (enemyPosX < TargetX && enemyPosY < TargetY)
        {
            if (CheckDiagonalMovement(1, 1))
            {
                MoveEnemy(1, 1);
            }
            else if (distance.x > distance.y)
            {
                if (CheckCardinalMovement(1, 0))
                {
                    MoveEnemy(1, 0);
                }
                else if (CheckCardinalMovement(0, 1))
                {
                    MoveEnemy(0, 1);
                }
            }
            else if (distance.x < distance.y)
            {
                if (CheckCardinalMovement(0, 1))
                {
                    MoveEnemy(0, 1);
                }
                else if (CheckCardinalMovement(1, 0))
                {
                    MoveEnemy(1, 0);
                }
            }
            else if (distance.x == distance.y)
            {
                if (CheckCardinalMovement(1, 0))
                {
                    MoveEnemy(1, 0);
                }
                else if (CheckCardinalMovement(0, 1))
                {
                    MoveEnemy(0, 1);
                }
            }

        }
        else if (enemyPosX > TargetX && enemyPosY == TargetY)
        {
            if (CheckCardinalMovement(-1, 0))
            {
                MoveEnemy(-1, 0);
            }
            else if (CheckDiagonalMovement(-1, -1))
            {
                MoveEnemy(-1, -1);
            }
            else if (CheckDiagonalMovement(-1, 1))
            {
                MoveEnemy(-1, 1);
            }

        }
        else if (enemyPosX < TargetX && enemyPosY == TargetY)
        {
            if (CheckCardinalMovement(1, 0))
            {
                MoveEnemy(1, 0);
            }
            else if (CheckDiagonalMovement(1, 1))
            {
                MoveEnemy(1, 1);
            }
            else if (CheckDiagonalMovement(1, -1))
            {
                MoveEnemy(1, -1);
            }
        }
        else if (enemyPosX == TargetX && enemyPosY > TargetY)
        {
            if (CheckCardinalMovement(0, -1))
            {
                MoveEnemy(0, -1);
            }
            else if (CheckDiagonalMovement(1, -1))
            {
                MoveEnemy(1, -1);
            }
            else if (CheckDiagonalMovement(-1, -1))
            {
                MoveEnemy(-1, -1);
            }

        }
        else if (enemyPosX == TargetX && enemyPosY < TargetY)
        {
            if (CheckCardinalMovement(0, 1))
            {
                MoveEnemy(0, 1);
            }
            else if (CheckDiagonalMovement(-1, 1))
            {
                MoveEnemy(-1, 1);
            }
            else if (CheckDiagonalMovement(1, 1))
            {
                MoveEnemy(1, 1);
            }
        }
    }

    public void MoveEnemy(int x, int y)
    {
        mapScript.mapTiles[enemyPosX, enemyPosY].entity = null;
        enemyPosX = enemyPosX + x;
        enemyPosY = enemyPosY + y;
        mapScript.mapTiles[enemyPosX, enemyPosY].entity = EnemyStats;
    }

    public bool CheckDiagonalMovement(int x, int y)
    {
        return (mapScript.mapTiles[enemyPosX + x, enemyPosY + y].type != Tile.TileType.WALL && mapScript.mapTiles[enemyPosX + x, enemyPosY + y].entity == null) &&
               (mapScript.mapTiles[enemyPosX + x, enemyPosY].type != Tile.TileType.WALL && mapScript.mapTiles[enemyPosX, enemyPosY + y].type != Tile.TileType.WALL);
    }

    public bool CheckCardinalMovement(int x, int y)
    {
        return mapScript.mapTiles[enemyPosX + x, enemyPosY + y].type != Tile.TileType.WALL && mapScript.mapTiles[enemyPosX + x, enemyPosY + y].entity == null;
    }


    public bool CheckPlayerForRanged()
    {

        for (int x = 0; x < abilities.Count; x++)
        {
            if (abilities[x].type == EnemyAbility.AttackType.RANGED)
            {
                int i = 1;
                int j = 1;
                while (mapScript.mapTiles[enemyPosX + i, enemyPosY + j].type != Tile.TileType.WALL)
                {
                    if (enemyPosX + i == PlayerLoc.PlayerPosX && enemyPosY + j == PlayerLoc.PlayerPosY)
                    {
                        return true;
                    }
                    i++;
                    j++;
                }

                i = -1;
                j = 1;
                while (mapScript.mapTiles[enemyPosX + i, enemyPosY + j].type != Tile.TileType.WALL)
                {
                    if (enemyPosX + i == PlayerLoc.PlayerPosX && enemyPosY + j == PlayerLoc.PlayerPosY)
                    {
                        return true;
                    }
                    i--;
                    j++;
                }

                i = 1;
                j = -1;
                while (mapScript.mapTiles[enemyPosX + i, enemyPosY + j].type != Tile.TileType.WALL)
                {
                    if (enemyPosX + i == PlayerLoc.PlayerPosX && enemyPosY + j == PlayerLoc.PlayerPosY)
                    {
                        return true;
                    }
                    i++;
                    j--;
                }

                i = -1;
                j = -1;
                while (mapScript.mapTiles[enemyPosX + i, enemyPosY + j].type != Tile.TileType.WALL)
                {
                    if (enemyPosX + i == PlayerLoc.PlayerPosX && enemyPosY + j == PlayerLoc.PlayerPosY)
                    {
                        return true;
                    }
                    i--;
                    j--;
                }

                i = 1;
                while (mapScript.mapTiles[enemyPosX + i, enemyPosY].type != Tile.TileType.WALL)
                {
                    if (enemyPosX + i == PlayerLoc.PlayerPosX && enemyPosY == PlayerLoc.PlayerPosY)
                    {
                        return true;
                    }
                    i++;
                }

                i = -1;
                while (mapScript.mapTiles[enemyPosX + i, enemyPosY].type != Tile.TileType.WALL)
                {

                    if (enemyPosX + i == PlayerLoc.PlayerPosX && enemyPosY == PlayerLoc.PlayerPosY)
                    {
                        return true;
                    }
                    i--;
                }

                i = -1;
                while (mapScript.mapTiles[enemyPosX, enemyPosY + i].type != Tile.TileType.WALL)
                {
                    if (enemyPosX == PlayerLoc.PlayerPosX && enemyPosY + i == PlayerLoc.PlayerPosY)
                    {
                        return true;
                    }
                    i--;
                }

                i = -1;
                while (mapScript.mapTiles[enemyPosX, enemyPosY + i].type != Tile.TileType.WALL)
                {
                    if (enemyPosX == PlayerLoc.PlayerPosX && enemyPosY + i == PlayerLoc.PlayerPosY)
                    {
                        return true;
                    }
                    i--;
                }

            }
            return false;
        }
        return false;

    }

    public void TerrifyinglyBigAIPleaseHelpMe()
    {
        if (PlayerLoc == null)
        {
            PlayerLoc = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        }

        StartAI();
        AINormal();
        turn = false;
    }

    public void StartAI()
    {
        //If enemy is in hallway (start)
        if (scan == true && mapScript.mapTiles[enemyPosX, enemyPosY].func == Tile.TileFunc.HALLWAY)
        {
            scan = false;
            roomendX = 0;
            roomendY = 0;
            roomstartX = 0;
            roomstartY = 0;
            doorPositions.Clear();

            if (mapScript.mapTiles[enemyPosX + 1, enemyPosY].func == Tile.TileFunc.HALLWAY && mapScript.mapTiles[enemyPosX + 1, enemyPosY].type != Tile.TileType.WALL)
            {
                hwdirectionX = 1;
                hwdirectionY = 0;
            }
            else if (mapScript.mapTiles[enemyPosX - 1, enemyPosY].func == Tile.TileFunc.HALLWAY && mapScript.mapTiles[enemyPosX - 1, enemyPosY].type != Tile.TileType.WALL)
            {
                hwdirectionX = -1;
                hwdirectionY = 0;
            }
            else if (mapScript.mapTiles[enemyPosX, enemyPosY + 1].func == Tile.TileFunc.HALLWAY && mapScript.mapTiles[enemyPosX, enemyPosY + 1].type != Tile.TileType.WALL)
            {
                hwdirectionX = 0;
                hwdirectionY = 1;
            }
            else if (mapScript.mapTiles[enemyPosX, enemyPosY - 1].func == Tile.TileFunc.HALLWAY && mapScript.mapTiles[enemyPosX, enemyPosY - 1].type != Tile.TileType.WALL)
            {
                hwdirectionX = 0;
                hwdirectionY = -1;
            }

        }

        //if enemy is in a room (start)
        if (scan == false && mapScript.mapTiles[enemyPosX, enemyPosY].func == Tile.TileFunc.ROOM)
        {
            roomstartX = enemyPosX;
            roomstartY = enemyPosY;

            while (mapScript.mapTiles[roomstartX - 1, enemyPosY].func == Tile.TileFunc.ROOM)
            {
                roomstartX--;
            }
            while (mapScript.mapTiles[enemyPosX, roomstartY - 1].func == Tile.TileFunc.ROOM)
            {
                roomstartY--;
            }
            roomendX = roomstartX;
            roomendY = roomstartY;

            while (mapScript.mapTiles[roomendX + 1, roomstartY].func == Tile.TileFunc.ROOM)
            {
                roomendX++;
            }
            while (mapScript.mapTiles[roomstartX, roomendY + 1].func == Tile.TileFunc.ROOM)
            {
                roomendY++;
            }
            roomstartX--;
            roomstartY--;
            roomendX++;
            roomendY++;

            for (int i = roomstartX; i <= roomendX; i++)
            {
                for (int j = roomstartY; j <= roomendY; j++)
                {
                    if (mapScript.mapTiles[i, j].func == Tile.TileFunc.HALLWAY)
                    {
                        doorPositions.Add(new Vector2(i, j));
                    }
                }
            }

            //closest door delete and new door
            if (doorPositions.Count > 1)
            {
                float closestdoor = Vector3.Distance(doorPositions[0], new Vector3(enemyPosX, enemyPosY));
                int delete = 0;
                for (int i = 0; i < doorPositions.Count; i++)
                {
                    if (closestdoor > Vector3.Distance(doorPositions[i], new Vector3(enemyPosX, enemyPosY)))
                    {
                        closestdoor = Vector3.Distance(doorPositions[i], new Vector3(enemyPosX, enemyPosY));
                        delete = i;
                    }
                }

                doorPositions.RemoveAt(delete);

                int random = (int)Mathf.Round(Random.Range(0, doorPositions.Count));
                doortargetX = (int)doorPositions[random].x;
                doortargetY = (int)doorPositions[random].y;
            }
            else
            {
                doortargetX = (int)doorPositions[0].x;
                doortargetY = (int)doorPositions[0].y;
            }
            scan = true;
        }
    }

    public void AINormal()
    {
        //if enemy is in a room
        if (mapScript.mapTiles[enemyPosX, enemyPosY].func == Tile.TileFunc.ROOM)
        {

            if (PlayerLoc.PlayerPosX >= roomstartX && PlayerLoc.PlayerPosX <= roomendX && PlayerLoc.PlayerPosY >= roomstartY && PlayerLoc.PlayerPosY <= roomendY)
            {
                Vector3 distance = new Vector3(Mathf.Abs(PlayerLoc.PlayerPosX - enemyPosX), Mathf.Abs(PlayerLoc.PlayerPosY - enemyPosY));
                if (distance.x > 1 || distance.y > 1)
                {
                    if (CheckPlayerForRanged())
                    {
                        float idk = Random.Range(0, 100);
                        if (idk >= 50)
                        {
                            QueueAttack();
                        }
                        else
                        {
                            EnemyMoveAI(PlayerLoc.PlayerPosX, PlayerLoc.PlayerPosY, distance);
                        }
                    }
                    else
                    {
                        EnemyMoveAI(PlayerLoc.PlayerPosX, PlayerLoc.PlayerPosY, distance);
                    }
                }
                else
                {
                    QueueAttack();
                }

                if (mapScript.mapTiles[PlayerLoc.PlayerPosX, PlayerLoc.PlayerPosY].func == Tile.TileFunc.HALLWAY)
                {
                    doortargetX = PlayerLoc.PlayerPosX;
                    doortargetY = PlayerLoc.PlayerPosY;
                }
            }
            else
            {
                Vector3 distance = new Vector3(Mathf.Abs(doortargetX - enemyPosX), Mathf.Abs(doortargetY - enemyPosY));
                EnemyMoveAI(doortargetX, doortargetY, distance);
            }
            dist = Vector3.Distance(transform.position, new Vector3(enemyPosX, enemyPosY, 0));

        }
        //if enemy is in a hallway
        else if (mapScript.mapTiles[enemyPosX, enemyPosY].func == Tile.TileFunc.HALLWAY)
        {

            if (PlayerLoc.PlayerPosX >= enemyPosX - 3 && PlayerLoc.PlayerPosX <= enemyPosX + 3 &&
               PlayerLoc.PlayerPosY >= enemyPosY - 3 && PlayerLoc.PlayerPosY <= enemyPosY + 3)
            {


                Vector3 distance = new Vector3(Mathf.Abs(PlayerLoc.PlayerPosX - enemyPosX), Mathf.Abs(PlayerLoc.PlayerPosY - enemyPosY));
                if (distance.x > 1 || distance.y > 1)
                    if (CheckPlayerForRanged())
                    {
                        float idk = Random.Range(0, 100);
                        if (idk >= 50)
                        {
                            QueueAttack();
                        }
                        else
                        {
                            EnemyMoveAI(PlayerLoc.PlayerPosX, PlayerLoc.PlayerPosY, distance);
                        }
                    }
                    else
                    {
                        EnemyMoveAI(PlayerLoc.PlayerPosX, PlayerLoc.PlayerPosY, distance);
                    }
                else

                    //enemyattack
                    QueueAttack();
            }
            else
            {
                if (CheckCardinalMovement(hwdirectionX, hwdirectionY))
                {
                    MoveEnemy(hwdirectionX, hwdirectionY);
                }
                else
                {
                    if (mapScript.mapTiles[enemyPosX + hwdirectionX, enemyPosY + hwdirectionY].entity != null)
                    {
                        hwdirectionX = -hwdirectionX;
                        hwdirectionY = -hwdirectionY;
                    }
                    else if (mapScript.mapTiles[enemyPosX + hwdirectionX, enemyPosY + hwdirectionY].type != Tile.TileType.FLOOR)
                    {
                        if (mapScript.mapTiles[enemyPosX + 1, enemyPosY].type != Tile.TileType.WALL && enemyPosX + 1 != enemyPosX - hwdirectionX)
                        {
                            hwdirectionX = 1;
                            hwdirectionY = 0;
                        }
                        else if (mapScript.mapTiles[enemyPosX - 1, enemyPosY].type != Tile.TileType.WALL && enemyPosX - 1 != enemyPosX - hwdirectionX)
                        {
                            hwdirectionX = -1;
                            hwdirectionY = 0;
                        }
                        else if (mapScript.mapTiles[enemyPosX, enemyPosY + 1].type != Tile.TileType.WALL && enemyPosY + 1 != enemyPosY - hwdirectionY)
                        {
                            hwdirectionX = 0;
                            hwdirectionY = 1;
                        }
                        else if (mapScript.mapTiles[enemyPosX, enemyPosY - 1].type != Tile.TileType.WALL && enemyPosY - 1 != enemyPosY - hwdirectionY)
                        {
                            hwdirectionX = 0;
                            hwdirectionY = -1;
                        }
                    }

                    MoveEnemy(hwdirectionX, hwdirectionY);
                }
            }

        }
    }

    public void EnemyAttack()
    {
        if(attackChoice == -1)
        {
            Vector3 distance = new Vector3(Mathf.Abs(PlayerLoc.PlayerPosX - enemyPosX), Mathf.Abs(PlayerLoc.PlayerPosY - enemyPosY));
            if (distance.x > 1 || distance.y > 1)
            {
                List<int> ranged = new List<int>();
                for(int i = 0; i < abilities.Count; i++)
                {
                    if (abilities[i].type == EnemyAbility.AttackType.RANGED)
                    {
                        ranged.Add(i);
                    }
                }
                attackChoice = (int)Mathf.Round(Random.Range(0, ranged.Count));
                attackChoice = ranged[attackChoice];

            }
            else
            {
                attackChoice = (int)Mathf.Round(Random.Range(0, abilities.Count));
            }
        }
        else
        {
            abilities[attackChoice].DoAbility(this);
        }
    }

    public void QueueAttack()
    {
        gamemaster.attackTurn.Enqueue(this);
    }

}
  
public abstract class EnemyAbility
{
    

    public enum AttackType { RANGED, MELEE };
    public enum AttackElement { MAGICAL, PHYSICAL };

    public AttackType type;
    public AttackElement element;
    public int damage;
    public string abilityName;
    public float posmitigationdmg;
    public float damageMultiplier;
    public float missChance;
    public float accuracy;

    public abstract void DoAbility(EnemyMovement enemy);
}