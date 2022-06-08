using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemaster : MonoBehaviour
{
    static public Gamemaster instance;

    private PlayerMovement player;
    public List<EnemyMovement> enemies;
    public Queue<EnemyMovement> attackTurn = new Queue<EnemyMovement>();
    public bool attackTurnBool = false;
    public Queue moveTurn = new Queue();

    // Start is called before the first frame update
    void Start()
    {
        
        enemies = new List<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player.turn == false)
        {
            if(attackTurnBool == false)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i] != null)
                    {
                        enemies[i].turn = true;
                    }

                }
                attackTurnBool = true;
            }
            else
            {
                if(NoMoving())
                {
                    bool check = false;
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i].queuedAttack == true)
                        {
                            check = true;
                        }
                    }

                    if (attackTurn.Count > 0)
                    {
                        if (check == false)
                        {
                            attackTurn.Dequeue().queuedAttack = true;
                        }
                    }
                    else if (check == false)
                    {
                        player.turn = true;
                        attackTurnBool = false;
                    }
                }
            }

        }

    }

    public void AddPlayer(PlayerMovement player)
    {
        this.player = player;
    }

    public void AddEnemy(EnemyMovement enemy)
    {
        enemies.Add(enemy);
    }

    public bool NoMoving()
    {
        if(player.transform.position.x != player.PlayerPosX || player.transform.position.y != player.PlayerPosY)
        {
            return false;
        }

        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].transform.position.x != enemies[i].enemyPosX || enemies[i].transform.position.y != enemies[i].enemyPosY)
            {
                return false;
            }
        }
        return true;
    }
}