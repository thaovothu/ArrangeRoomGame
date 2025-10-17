using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySetting : MonoBehaviour
{
    [SerializeField] private Light morningLight;
    [SerializeField] private Light noonLight;

    void Start()
    {
        morningLight.enabled = true;
        noonLight.enabled = false;

    }

    public void SetMorning()
    {
        morningLight.enabled = true;
        noonLight.enabled = false;
  
    }
    public void SetNoon()
    {
        morningLight.enabled = false;
        noonLight.enabled = true;
   
    }
    public void SetEvening()
    {
        morningLight.enabled = false;
        noonLight.enabled = false;
     
    }
}
    
