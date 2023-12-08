using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{

    [SerializeField]
    private GameObject highScoreTable;

    [SerializeField]
    private GameObject highScoreElement;

    [SerializeField]
    private GameObject highScoreBoard;

    [SerializeField]
    private GameObject highScoreInput;

    List<HighScoreElement> list = new List<HighScoreElement>();

    HighScoreList highScoreStore;

    // Instance is set as this class in Start
    private static HighScore instance;

    public static HighScore Instance 
    { 
        get
        { 
            return instance; 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        highScoreStore = ReadScore(); // Get whatever the previously saved leaderboard was and update the current one
        UpdateScoreBoard(highScoreStore.HighScoresList);
    }

    // Show and Hide methods for the leaderboard
    public void Show()
    {
        highScoreBoard.SetActive(true);
    }

    public void Hide()
    {
        highScoreBoard.SetActive(false);
    }

    // Method to update the leaderboard element
    void UpdateScoreBoard(List<HighScoreElement> list)
    {
        foreach (Transform child in highScoreTable.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (HighScoreElement elem in list)
        {
            GameObject tmp = GameObject.Instantiate(highScoreElement, highScoreTable.transform);
            tmp.SetActive(true);
            tmp.GetComponent<Text>().text = elem.Name + " " + elem.Score.ToString();
        }
    }

    // Method to save the leaderboard, adding a new element to it, saving it to a file, and updating it
    public void Save(string name, float score)
    {
        highScoreStore.AddToList(new HighScoreElement(name, score));
        SaveScoreBoard(highScoreStore);
        highScoreInput.SetActive(false);
        UpdateScoreBoard(highScoreStore.HighScoresList);
    }

    public void ShowInputQuery(float score) 
    {
        //Debug.Log("TestHighScore");
        Debug.Log(score);
        highScoreInput.GetComponent<HighScoreInput>().Score = score;
        highScoreInput.SetActive(true);
    }

    // Saving the leaderboard into a file
    public void SaveScoreBoard(HighScoreList sb)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/s003.save", FileMode.OpenOrCreate);
        bf .Serialize(file, sb);
        file.Close();
    }

    // Method to either get the leaderboard from the saved file, or create a new list with dummy data if it doesn't exist
    HighScoreList ReadScore()
    {
        HighScoreList sb = null;
        if (File.Exists(Application.persistentDataPath + "/s003.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/s003.save", FileMode.Open);
            sb = (HighScoreList)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            sb = new HighScoreList();
            sb.HighScoresList = new List<HighScoreElement>();
            sb.AddToList(new HighScoreElement("AAA", 1));
            SaveScoreBoard(sb);
        }
        return sb;


    }
}
