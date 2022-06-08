using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsWorld : MonoBehaviour
{
    public static CrystalsWorld SpawmnCrystalWorld(Vector3 position)
    {
        Transform transform = Instantiate(ItemAssets.Instance.PfCrystalsWorld, position, Quaternion.identity);

        CrystalsWorld crystalsWorld = transform.GetComponent<CrystalsWorld>();


        return crystalsWorld;

    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
