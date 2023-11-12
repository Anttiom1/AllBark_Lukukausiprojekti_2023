using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class HighScoreElement
{
    // Name and score for one element of the leaderboard
    private string name;
    private float score;

    public string Name { get { return name; } }

    public float Score { get { return score; } }

    public HighScoreElement(string name, float score)
    {
        this.name = name;
        this.score = score;
    }
}
