using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Opencloseshop : MonoBehaviour
{

    [SerializeField] private GameObject CanvasShop;
    [SerializeField] private TMP_Text CrystalTxt;


    public ShopManager Currency;

    void Awake()
    {

        CrystalTxt.text = "Crystals: " + Currency.crystals.ToString();

        //Change the Vector3 Position, to correspond to the matrix

        //CrystalsWorld.SpawmnCrystalWorld(new Vector3(1, -3));
    }

    //This is the collider to add items and currency. Change to matrix.
    //private void OnTriggerEnter2D(Collider2D collider)
    //{

    //    if (crytalworld != null)
    //    {
    //        Currency.crystals++;
    //        CrystalTxt.text = "Crytals: " + Currency.crystals.ToString();
    //        Currency.CheckPurchaseable();
    //        crytalworld.DestroySelf();
    //    }

    //}

    void Update()
    {
        //Disable Player Movement & Open Shop
        if (Input.GetKeyDown(KeyCode.Space))
            CanvasShop.SetActive(!CanvasShop.activeSelf);
        if (Input.GetKeyDown(KeyCode.L))
            {
                Currency.crystals++;
                CrystalTxt.text = "Crytals: " + Currency.crystals.ToString();
                Currency.CheckPurchaseable();
                //crytalworld.DestroySelf();
            }

    }

    private void FixedUpdate()
    {
        if (CanvasShop.activeSelf)
            return;
    }
}
