using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerInfo
{
    public string username;
    public string passwd;
    public int playerId;

    public PlayerInfo(string user, string pw, int d)
    {
        username = user;
        passwd = pw;
        playerId = d;
    }
}

[System.Serializable]
public class FoodInfo
{
    public int quantity;
    

    public FoodInfo(int q)
    {
        quantity = q;
       
    }
}

[System.Serializable]
public class FList
{
    public FoodInfo[] players;
}



[System.Serializable]
public class InputInfo
{
    public string username;
    public string passwd;

    public InputInfo(string i, string s)
    {
        username = i;
        passwd = s;
    }
}

[System.Serializable]
public class aInfo
{
    public int playerId;
    

    public aInfo(int i)
    {
        playerId = i;
       
    }
}

[System.Serializable]
public class RegisterPlayerInfo
{
    public string username;
    public string passwd;
    

    public RegisterPlayerInfo(string u, string p)
    {
        username = u;
        passwd = p;
        
    }
}

[System.Serializable]
public class PlayerList
{
    public PlayerInfo[] players;
}


public class Servidor : MonoBehaviour
{
    public GameObject userInput;
    public GameObject passInput;
    public GameObject ipInput;
    public string usernameValue;
    public string passValue;
    public int Id;
    public float Timer = 0;
    public float HAHA = 3;
    public bool sent = false;
    public int happy;

    public string BaseAPI = "http://192.168.1.80:3434/";
    IEnumerator GetPlayersRequest(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Request ERROR!!");
        }
        else
        {
     
        }


        PlayerList playerList = JsonUtility.FromJson<PlayerList>(
            webRequest.downloadHandler.text);

        foreach (PlayerInfo player in playerList.players)
        {
          
        }
    }


    IEnumerator PostRequest(string url, string jsondata)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        byte[] jsonConverted = new System.Text.UTF8Encoding().
                                    GetBytes(jsondata);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonConverted);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Request ERROR!!");
        }
        else
        {
            PlayerList playerList = JsonUtility.FromJson<PlayerList>(
          webRequest.downloadHandler.text);
            foreach (PlayerInfo player in playerList.players)
            {
                usernameValue = (player.username);
                passValue = (player.passwd);
                Id = (player.playerId);
             
            }
          
        }

    }
    IEnumerator PostRequest2(string url, string jsondata)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        byte[] jsonConverted = new System.Text.UTF8Encoding().GetBytes(jsondata);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonConverted);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Request Error!!");
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
          
        }

    }

    IEnumerator PostRequest3(string url, string jsondata)
    {
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        byte[] jsonConverted = new System.Text.UTF8Encoding().GetBytes(jsondata);
        webRequest.uploadHandler = new UploadHandlerRaw(jsonConverted);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Request Error!!");
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            FList playerList = JsonUtility.FromJson<FList>(webRequest.downloadHandler.text);
            foreach (FoodInfo player in playerList.players)
            {
                happy = (player.quantity);
            }
            Debug.Log(webRequest.downloadHandler.text);
        }

    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //RegisterPlayerInfo info = new RegisterPlayerInfo("nelio", "god");
        //string json = JsonUtility.ToJson(info);
        //Debug.Log(json);
        //StartCoroutine(GetPlayersRequest(BaseAPI + "player/list"));
        //StartCoroutine(PostRequest(BaseAPI + "players/new", json));


    }
     void Update()
    {
        
        if (sent)
        {

            Debug.Log(Timer);
            Debug.Log(HAHA);
            Debug.Log(Timer < HAHA);
            if (Timer < HAHA)
            {
                Timer = Timer + 1f * Time.deltaTime;
               
            }
            else
            {
                
                string ip;

                ip = ipInput.GetComponent<TMP_InputField>().text;
                BaseAPI = "http://" + ip + ":3434/";
                aInfo inpt = new aInfo(Id);
                string json2 = JsonUtility.ToJson(inpt);
                Debug.Log(Id);
                StartCoroutine(PostRequest3(BaseAPI + "player/quantity",json2));

                if (userInput.GetComponent<TMP_InputField>().text == usernameValue && passInput.GetComponent<TMP_InputField>().text == passValue)
                {
                    SceneManager.LoadScene(1);

                };
                Timer = 0f;
                sent = false;
            }
        }
    }

    public void Login()
    {
        string ip;

        ip = ipInput.GetComponent<TMP_InputField>().text;
        BaseAPI = "http://"+ip+":3434/";
        if (userInput.GetComponent<TMP_InputField>().text != null && passInput.GetComponent<TMP_InputField>().text != null)
        {
            InputInfo inpt = new InputInfo(userInput.GetComponent<TMP_InputField>().text, passInput.GetComponent<TMP_InputField>().text);
            string json2 = JsonUtility.ToJson(inpt);
            Debug.Log(json2);
            StartCoroutine(PostRequest(BaseAPI + "player/login", json2));
            sent = true;
         
              
        }




    }

    public void Register()
    {
        InputInfo inpt = new InputInfo(userInput.GetComponent<TMP_InputField>().text, passInput.GetComponent<TMP_InputField>().text);
        string json2 = JsonUtility.ToJson(inpt);
        Debug.Log(json2);
        StartCoroutine(PostRequest2(BaseAPI + "players/new", json2));
             
    }

}