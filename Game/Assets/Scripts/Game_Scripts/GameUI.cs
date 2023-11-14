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
    private bool timerActive = false;

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
        timer = 5;
        timerActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //F1 defines how many decimal will be shown
        string formatedTime = timer.ToString("F1");
        timerText.text = formatedTime;
        if (timer < 0 && timerActive == true)
        {
            timerActive = false;
            Debug.Log("timer test");
            FinishTimer();
            
        }
    }

    void FinishTimer()
    {
        float score = 0;
        HighScore.Instance.ShowInputQuery(score);
    }
}
