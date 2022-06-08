using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesNelio : MonoBehaviour
{

    public EnemyMovement enemyscript;

    // Start is called before the first frame update
    void Start()
    {
        enemyscript.abilities = new List<EnemyAbility>();
        enemyscript.abilities.Add(new Dog());
        enemyscript.abilities.Add(new Zero());

        enemyscript.EnemyStats.Armor = 60;
        enemyscript.EnemyStats.MR = 50;
        enemyscript.EnemyStats.Health = 350;
        enemyscript.EnemyStats.currentHealth = enemyscript.EnemyStats.Health;
        enemyscript.EnemyStats.attack = 40;

    }
}

public class Dog : EnemyAbility
{
    GameObject projectile;
    public Dog()
    {
        type = AttackType.RANGED;
        element = AttackElement.MAGICAL;
        damage = 30;
        abilityName = "Dog";
        accuracy = 50f;
    }

    public override void DoAbility(EnemyMovement enemy)
    {
        enemy.attackmove = true;
        if (projectile == null)
        {
            projectile = EnemyMovement.Instantiate(Resources.Load("prefab/dog"), new Vector3(enemy.enemyPosX, enemy.enemyPosY), Quaternion.identity) as GameObject;
        }

        if (projectile.transform.position.x != enemy.PlayerLoc.PlayerPosX || projectile.transform.position.y != enemy.PlayerLoc.PlayerPosY)
        {
            projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, new Vector3(enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY), 3 * Time.deltaTime);
        }
        else
        {
            EnemyMovement.Destroy(projectile);
            projectile = null;

            missChance = Random.Range(0f, 100f);

            if (missChance <= accuracy)
            {
                damageMultiplier = (100 / (enemy.mapScript.mapTiles[enemy.PlayerLoc.PlayerPosX, enemy.PlayerLoc.PlayerPosY].entity.MR + 100));
                posmitigationdmg = damageMultiplier * this.damage;
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

public class Zero : EnemyAbility
{
    bool charge = false;
    public Zero()
    {
        type = AttackType.MELEE;
        element = AttackElement.MAGICAL;
        damage = 50;
        abilityName = "Zero";
        accuracy = 80f;
    }

    public override void DoAbility(EnemyMovement enemy)
    {
        enemy.attackmove = true;

        if (charge == false)
        {
            if (enemy.transform.position.x != (enemy.PlayerLoc.PlayerPosX + enemy.transform.position.x) / 2 || enemy.transform.position.y != (enemy.PlayerLoc.PlayerPosY + enemy.transform.position.y) / 2)
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
            if (enemy.transform.position.x != enemy.enemyPosX || enemy.transform.position.y != enemy.enemyPosY)
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

