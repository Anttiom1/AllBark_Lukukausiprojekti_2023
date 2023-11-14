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
        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //F1 defines how many decimal will be shown
        string formatedTime = timer.ToString("F1");
        timerText.text = formatedTime;
        if (timer < 0)
        {
            Debug.Log("test");
            float score = 0;
            HighScore.Instance.ShowInputQuery(score);
        }

    }
}
