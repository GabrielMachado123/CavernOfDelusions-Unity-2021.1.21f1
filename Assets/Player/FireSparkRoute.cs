using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSparkRoute : MonoBehaviour
{
    public Vector3math v;
    public Vector3 target;
    public GameObject explosionPrefab;
    public GameObject explosionSprite;

    public PlayerMovement pm;
    public Map mapScript;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        mapScript = GameObject.Find("Map").GetComponent<Map>();
    }

    // Update is called once per frame
    void Update()
    {
       
        v = mathFunctions.CheckDistance(new Vector3math(pm.PlayerPosX, pm.PlayerPosY, 0), target);
        if (v.x > 0)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        if (v.y < 0)
            gameObject.GetComponent<SpriteRenderer>().flipY = true;

        transform.position = Vector2.MoveTowards(transform.position, target, 9*Time.deltaTime);
      if(transform.position == target)
        {
            explosionSprite = Instantiate(explosionPrefab, target, Quaternion.identity);
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
            pm.turn = false;
            Destroy(gameObject);
        }
    }
}
