using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class HighScoreInput : MonoBehaviour
{
    // List of letters shown on screen
    [SerializeField]
    List<GameObject> listOfLetters = new List<GameObject>();

    // Text of the score
    [SerializeField]
    Text scoreText;

    // Float to keep track of score
    private float score;

    public float Score {  get { return score; } set {  score = value; } }

    // Int to keep track of which letter is currently selected
    private int selectedLetter = 0;

    private Color defaultColor;

    private CustomController controller;
    private int xbuttonThreshold;
    private int zbuttonThreshold;


    // OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        selectedLetter = 0;
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;
        listOfLetters[1].GetComponent<Animator>().enabled = false;
        listOfLetters[2].GetComponent<Animator>().enabled = false;
        scoreText.text = score.ToString();
    }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        defaultColor = listOfLetters[selectedLetter].GetComponent<Text>().color;
        
    }

    private void Start()
    {
        controller = FindAnyObjectByType<CustomController>();
        xbuttonThreshold = controller.XQuart;
        zbuttonThreshold = controller.ZQuart;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Calling the corresponding method to change which of the three letters is selected, and
        // which letter of the alphabet is set
        // Inputs are temporary, later to be changed to use the custom controller imputs
        /*if (controller.ZQuart > zbuttonThreshold+10 )
        {
            PrevLetter();
            zbuttonThreshold = controller.ZQuart;
        }*/
        // Key input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevLetter();
        }
        /*
        if (controller.ZQuart < zbuttonThreshold -10)
        {
            NextLetter();
            zbuttonThreshold = controller.ZQuart;
        }*/
        // Key input
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextLetter();
        }
        /*
        if (controller.XQuart > xbuttonThreshold+10)
        {
            NextAlphabet();
            xbuttonThreshold = controller.XQuart;
        }*/
        // Key input
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NextAlphabet();
        }
        /*
        if (controller.XQuart < xbuttonThreshold-10)
        {
            PrevAlphabet();
            xbuttonThreshold = controller.XQuart;
            
        }*/
        // Key input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PrevAlphabet();
        }

        // Saving the set name when string is pulled
        if (controller.Counter == 999)
        {
            //Score = GameManager.instance.Score;
            HighScore.Instance.Save(listOfLetters[0].GetComponent<Text>().text
                + listOfLetters[1].GetComponent<Text>().text +
                listOfLetters[2].GetComponent<Text>().text, score);
            SceneManager.LoadScene(0); // Load back to the main menu scene

        }
        // Key input for submitting the high score input with Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HighScore.Instance.Save(listOfLetters[0].GetComponent<Text>().text
                + listOfLetters[1].GetComponent<Text>().text +
                listOfLetters[2].GetComponent<Text>().text, score);
            SceneManager.LoadScene(0); // Load back to the main menu scene
        }

    }

    // Methods for changing which letter of the alphabet is set
    void NextAlphabet()
    {
        char c = listOfLetters[selectedLetter].GetComponent<Text>().text.ToCharArray()[0];
        c++;
        if (c > (int)'Z') c = 'A';
        listOfLetters[selectedLetter].GetComponent<Text>().text = c.ToString();
    }

    void PrevAlphabet()
    {
        char c = listOfLetters[selectedLetter].GetComponent<Text>().text.ToCharArray()[0];
        c--;
        if (c < (int)'A') c = 'Z';
        listOfLetters[selectedLetter].GetComponent<Text>().text = c.ToString();
    }

    // Methods for changing which letter is currently being highlighted
    void NextLetter()
    {
        listOfLetters[selectedLetter].GetComponent<Text>().color = defaultColor;
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = false;
        selectedLetter++;
        if (selectedLetter > 2) selectedLetter = 0;
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;
    }

    void PrevLetter()
    {
        listOfLetters[selectedLetter].GetComponent<Text>().color = defaultColor;
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = false;
        selectedLetter--;
        if (selectedLetter < 0) selectedLetter = 2;
        listOfLetters[selectedLetter].GetComponent<Animator>().enabled = true;
    }
}
