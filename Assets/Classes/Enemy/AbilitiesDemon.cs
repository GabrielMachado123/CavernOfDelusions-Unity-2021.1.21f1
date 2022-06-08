using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesDemon : MonoBehaviour
{

    public EnemyMovement enemyscript;

    // Start is called before the first frame update
    void Start()
    {
        enemyscript.abilities = new List<EnemyAbility>();
        enemyscript.abilities.Add(new Fireball());
        enemyscript.abilities.Add(new HellClaw());

        enemyscript.EnemyStats.Armor = 20;
        enemyscript.EnemyStats.MR = 15;
        enemyscript.EnemyStats.Health = 100;
        enemyscript.EnemyStats.currentHealth = enemyscript.EnemyStats.Health;
        enemyscript.EnemyStats.attack = 30;

    }
}

public class Fireball : EnemyAbility
{
    GameObject projectile;
    public Fireball()
    {
        type = AttackType.RANGED;
        element = AttackElement.MAGICAL;
        damage = 20;
        abilityName = "Fireball";
        accuracy = 60f;
}

    public override void DoAbility(EnemyMovement enemy)
    {
        enemy.attackmove = true;
        if(projectile == null)
        {
            projectile = EnemyMovement.Instantiate(Resources.Load("prefab/DemonFireball"), new Vector3(enemy.enemyPosX, enemy.enemyPosY), Quaternion.identity) as GameObject;
        }

        if(projectile.transform.position.x != enemy.PlayerLoc.PlayerPosX || projectile.transform.position.y != enemy.PlayerLoc.PlayerPosY)
        {
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, new Vector3(enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY), 3 * Time.deltaTime);
        }
        else
        {
            EnemyMovement.Destroy(projectile);
            projectile = null;

            missChance = Random.Range(0f, 100f);

            if(missChance <= accuracy)
            {
                damageMultiplier = (100 / (enemy.mapScript.mapTiles[enemy.PlayerLoc.PlayerPosX  , enemy.PlayerLoc.PlayerPosY  ].entity.MR + 100));
                posmitigationdmg = damageMultiplier * this.damage;
                Debug.Log(posmitigationdmg);
                Debug.Log(this.damage);
                Debug.Log(damageMultiplier);
                enemy.mapScript.mapTiles[enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY].entity.currentHealth = enemy.mapScript.mapTiles[enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY].entity.currentHealth - posmitigationdmg;
                GameObject DamageDoneSprite = EnemyMovement.Instantiate(enemy.PlayerLoc.DamageDonePrefab, new Vector3(enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY + 0.5f, 0), Quaternion.identity);
                DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;
            }
            else
            {
                enemy.PlayerLoc.MissSprite = EnemyMovement.Instantiate(enemy.PlayerLoc.MissPrefab, new Vector3(enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY + 0.5f, 0), Quaternion.identity);
            }
            enemy.queuedAttack = false;
            enemy.attackChoice = -1;
            enemy.attackmove = false;
        }
    }
}

public class HellClaw : EnemyAbility
{
    bool charge = false;
    public HellClaw()
    {
        type = AttackType.MELEE;
        element = AttackElement.MAGICAL;
        damage = 25;
        abilityName = "Hell Claw";
        accuracy = 80f;
    }

    public override void DoAbility(EnemyMovement enemy)
    {
        enemy.attackmove = true;

        if (charge == false)
        {
            if(enemy.transform.position.x != (enemy.PlayerLoc.PlayerPosX + enemy.transform.position.x) / 2 || enemy.transform.position.y != (enemy.PlayerLoc.PlayerPosY + enemy.transform.position.y) / 2)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, new Vector3((enemy.PlayerLoc.PlayerPosX + enemy.transform.position.x) / 2, (enemy.PlayerLoc.PlayerPosY + enemy.transform.position.y) / 2), 4 * Time.deltaTime);
            }
            else
            {
                charge = true;
                missChance = Random.Range(0f, 100f);

                if (missChance <= accuracy)
                {
                    damageMultiplier = (100 / (enemy.mapScript.mapTiles[enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY].entity.Armor + 100));
                    posmitigationdmg = damageMultiplier * this.damage;
                    enemy.mapScript.mapTiles[enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY].entity.currentHealth = enemy.mapScript.mapTiles[enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY].entity.currentHealth - posmitigationdmg;
                    GameObject DamageDoneSprite = EnemyMovement.Instantiate(enemy.PlayerLoc.DamageDonePrefab, new Vector3(enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY + 0.5f, 0), Quaternion.identity);
                    DamageDoneSprite.GetComponent<damageFeedback>().damage = posmitigationdmg;
                }
                else
                {
                    enemy.PlayerLoc.MissSprite = EnemyMovement.Instantiate(enemy.PlayerLoc.MissPrefab, new Vector3(enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY + 0.5f, 0), Quaternion.identity);
                }
            }
        }
        else
        {
            if(enemy.transform.position.x != enemy.enemyPosX || enemy.transform.position.y != enemy.enemyPosY)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, new Vector3(enemy.enemyPosX, enemy.enemyPosY), 4 * Time.deltaTime);
            }
            else
            {
                enemy.queuedAttack = false;
                charge = false;
                enemy.attackmove = false;
                enemy.attackChoice = -1;
            }
        }
    }
}

