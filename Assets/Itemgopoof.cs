using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemgopoof : MonoBehaviour
{
    public Map map;
    private int itemPosX;
    private int itemPosY;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("Map").GetComponent<Map>();
        itemPosX = (int)transform.position.x;
        itemPosY = (int)transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(map.mapTiles[itemPosX, itemPosY].item == null)
        {
            Destroy(gameObject);
        }
    }
}
