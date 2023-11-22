using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI scoreTextTMP;
    private int score = 0;

    public int Score 
    {  
        get 
        { 
            return score; 
        } 
    }

    void Awake(){
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }
    public void AddScore(int pointsToAdd) {
        score += pointsToAdd;
         scoreTextTMP.text = "Score: " + score;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
