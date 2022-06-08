using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class damageFeedback : MonoBehaviour
{
    public TextMeshPro DamageDone;
    public float damage;

    public 
    // Start is called before the first frame update
    void Start()
    {
        DamageDone = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

        damage = Mathf.Round(damage);
       DamageDone.text = damage.ToString();
        
    }
}
