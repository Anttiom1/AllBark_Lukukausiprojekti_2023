using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // The idea is to use this scene as a way of inputting the name and saving the score that a player got in a way where
        // the actual game scene isn't loaded while the HighScoreInput is visible, but it doesn't quite work yet so not implemented.
        float playerScore = GameManager.instance.Score;
        HighScore.Instance.ShowInputQuery(playerScore);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
