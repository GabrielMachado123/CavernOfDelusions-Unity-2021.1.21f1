using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public Transform PfItemWorld;
    public Transform PfCrystalsWorld;

    public Sprite demonSprite;
    public Sprite healthSprite;
    public Sprite manaSprite;
    public Sprite slimeSprite;
    public Sprite inviziSprite;
    public Sprite tpSprite;

}
