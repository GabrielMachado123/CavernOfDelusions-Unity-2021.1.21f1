using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeppAttBuff : MonoBehaviour
{
    public Map mapScript;
    public Transform pm;
    public PlayerMovement PScript;

    void Start()
    {
        mapScript = GameObject.Find("Map").GetComponent<Map>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        PScript.numberOfstates++;
    }

   
    void Update()
    {
        if(PScript.numberOfstates == 1)
        transform.position = new Vector3(pm.position.x, pm.position.y + 0.65f, transform.position.z);
        else if(PScript.numberOfstates == 2)
        transform.position = new Vector3(pm.position.x - 0.25f, pm.position.y + 0.65f, transform.position.z);

        if (mapScript.PlayerStats.turnsAttBuff <= 0)
        {
            PScript.numberOfstates--;
            Destroy(gameObject);
        }
        
    }
}
