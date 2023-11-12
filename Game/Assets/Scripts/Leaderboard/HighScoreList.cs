using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreList
{
    // Leaderboard list for all the high scores
    private List<HighScoreElement> highScoresList;

    public List<HighScoreElement> HighScoresList 
    {
        get
        { 
            return this.highScoresList; 
        }
        set 
        { 
            this.highScoresList = value; 
        }
    }

    // Method for adding score elements to the leaderboard
    public void AddToList(HighScoreElement element)
    {
        // If the leaderboard is empty, add the score to it
        if (highScoresList.Count == 0)
        {
            highScoresList.Add(element);
            return;
        }
        else
        {
            // If the leaderboard is not empty, go through each element in it and add the new score
            // to the correct spot, so the leaderboard retains its order
            int counter = 0;
            foreach (HighScoreElement el in HighScoresList)
            {
                if (el.Score < element.Score)
                {
                    HighScoresList.Insert(counter, element);
                    break;
                }
                counter++;
                if (highScoresList.Count == counter)
                {
                    highScoresList.Add(element);
                    break;
                }
            }
        }
        // If the leaderboard has 10 entries, remove the last one
        if (HighScoresList.Count > 10)
        {
            highScoresList.RemoveAt(10);
        }
    }
}
