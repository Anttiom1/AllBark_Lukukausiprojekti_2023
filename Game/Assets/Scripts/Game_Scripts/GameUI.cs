using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI timerText;
    
    private float timer;

    public float Timer 
    {  
        get 
        { 
            return timer; 
        }
    }

    float charge = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = 60;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //F1 defines how many decimal will be shown
        string formatedTime = timer.ToString("F1");
        timerText.text = formatedTime;

    }
}
