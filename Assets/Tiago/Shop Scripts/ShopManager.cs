using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int crystals;
    public TMP_Text crystalUI;
    public ShopItem[] shopItemsSO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseButtons;

    void Start()
    {
        LoadPanels();
    }

    void Update()
    {

    }

    public void CheckPurchaseable()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (crystals >= shopItemsSO[i].basecost)
                myPurchaseButtons[i].interactable = true;
            else
                myPurchaseButtons[i].interactable = false;
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (crystals >= shopItemsSO[btnNo].basecost)
        {
            crystals = crystals - shopItemsSO[btnNo].basecost;
            crystalUI.text = "Crytals: " + crystals.ToString();
            CheckPurchaseable();
            //Unlocks the item
        }
    }



    public void LoadPanels() 
    { 
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanels[i].titleText.text = shopItemsSO[i].title;
            shopPanels[i].descriptionText.text = shopItemsSO[i].description;
            shopPanels[i].cosText.text = "Crytals " + shopItemsSO[i].basecost.ToString();
            shopPanels[i].image.sprite = shopItemsSO[i].sprite;
        }
    }
    
        


}
