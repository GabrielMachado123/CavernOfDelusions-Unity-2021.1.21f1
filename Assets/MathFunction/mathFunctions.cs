using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mathFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3math CheckDistance(Vector3math v, Vector3 u)
    {
        Vector3math c;

        c = new Vector3math(v.x - u.x, v.y - u.y, v.z - u.z);
            
        return c;
    }

    public static Vector3math CheckDistance2(Vector3math v, Vector3math u)
    {
        Vector3math c;

        c = new Vector3math(v.x - u.x, v.y - u.y, v.z - u.z);

        return c;
    }

    public static Vector3math MoveTowards2(Vector3math origin, Vector3math destiny, float speed)
    {
        Vector3math direction = Normalize(CheckDistance2(destiny, origin));
        origin = new Vector3math(origin.x + direction.x * speed * Time.deltaTime, origin.y + direction.y * speed * Time.deltaTime, origin.z + direction.z * speed * Time.deltaTime);
        return origin;
    }

    public static Vector3math Normalize(Vector3math vec)
    {
        return new Vector3math(vec.x / Magnitude(vec), vec.y/Magnitude(vec), vec.z / Magnitude(vec));
    }

    public static float Magnitude(Vector3math vec)
    {
        return Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
    }

    
}

public class Vector3math
{
    public float x;
    public float y;
    public float z;

    public Vector3math(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

