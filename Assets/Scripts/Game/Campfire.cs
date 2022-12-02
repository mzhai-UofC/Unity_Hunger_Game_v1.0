using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Campfire : MonoBehaviour
{
    [SerializeField] new Light light;
    [SerializeField] Text FireLeftTime;
    private float maxTime = 20;    
    private float currentTime = 20;
   

    void Update()
    {
        if (currentTime < 0)
        {
            currentTime = 0;
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

        light.intensity = Mathf.Clamp(currentTime / maxTime, 0, 1) * 10f;
        if (light.intensity <= 0)
        {
            light.transform.gameObject.SetActive(false);
        }
        String currentime = currentTime.ToString();
        FireLeftTime.text = currentime;
    }

    public void AddTime()
    {
        currentTime += 15;
        light.transform.gameObject.SetActive(true);
    }
}
