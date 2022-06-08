using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCamera : MonoBehaviour
{
    private Transform player;
    private Map mapScript;


    // Start is called before the first frame update
    void Start()
    {
        mapScript = GameObject.Find("Map").GetComponent<Map>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mapScript.playerSpawned == true)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
        }
}
