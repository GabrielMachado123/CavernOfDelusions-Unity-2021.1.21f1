using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PetFake : MonoBehaviour
{

    public TextMeshPro DamageDone;
    public string pet;
    // Start is called before the first frame update
    void Start()
    {
        DamageDone = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
        DamageDone.text = pet.ToString();
    }
}
