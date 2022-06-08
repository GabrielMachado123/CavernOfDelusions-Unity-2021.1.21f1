using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToGame : MonoBehaviour
{
    public GameObject lMenu;
   
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }


    public void GoToGAME()
    {
        SceneManager.LoadScene(1);
    }
    public void ToLogin()
    {
        lMenu.SetActive(true);
    }
   
}
