 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public Entry[] entries;
    private List<Entry> remainingEntries;
    private Entry currentEntry;

    // Strings
    private string jumbledWord;
    private string playerGuess;

    private bool isHintVisable = false;

    // Numerics
    private int playerScore = -1;
    public float addedTime = 10f;
    private int hints = 1;
    private int hintCounter = 0;

    private GameTimer gameTimer;

    // Texts
    [SerializeField]
    private InputField playerInput;
    [SerializeField]
    private Text jumbleWordText;
    [SerializeField]
    private Text playerScoreText;
    [SerializeField]
    private Text hintText;
    [SerializeField]
    private Text remainingHintText;

    // Start is called before the first frame update
    void Start()
    {
        if (remainingEntries == null)
        {
            remainingEntries = entries.ToList<Entry>();
        }

        gameTimer = gameObject.GetComponent<GameTimer>();

        // Begin first round 
        StartNextRound();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();
        ManageUI();

        if (remainingEntries.Count == 0)
        {
            CorrectlyGuessedAllWords();
        }
    }

    void ManageUI()
    {
        playerScoreText.text = playerScore.ToString() + "/" + entries.Length;
        remainingHintText.text = "HINTS: " + hints.ToString();
    }

    void CheckPlayerInput()
    {
        playerGuess = playerInput.text;
        if (playerGuess == currentEntry.word)
        {
            AudioManager.audioManager.PlaySound("Correct Guess");
            StartNextRound();
            playerInput.text = "";  
        }
    }

    void GetRandomEntry()
    {
        int randomEntryIndex = Random.Range(0, remainingEntries.Count);
        currentEntry = remainingEntries[randomEntryIndex];
    }

    void SetJumbledWord()
    {
        char[] myChar = currentEntry.word.ToCharArray(); // Convert string to char array

        // Jumble char array 
        for (int i = myChar.Length - 1; i > 0; i--)
         {
             int rnd = Random.Range(0, i);
             char temp = myChar[i];

            if (temp != ' ' && myChar[rnd] != ' ')
            {
                myChar[i] = myChar[rnd];
                myChar[rnd] = temp;
            }
         }
         
        jumbledWord = new string(myChar);   // Convert char array to string 
        jumbleWordText.text = jumbledWord;   // Display jumbled word to screen
        
    }

    void StartNextRound()
    {
        // Remove entry from list (if answered correctly)
        remainingEntries.Remove(currentEntry);

        gameTimer.InreaseTime(addedTime);
        playerScore++;
        
        UpdateDataControl();
        HideHint();

        // Obtain new entry to jumble
        GetRandomEntry();
        SetJumbledWord();
    }

    public void ShowHint()
    {
        if (hints > 0 && isHintVisable == false)
        {
            isHintVisable = true;
            hintText.text = currentEntry.hint; 
            hints--;
        }
    }

    void HideHint()
    {
        isHintVisable = false;
        hintText.text = " ";
        hintCounter++;

        if (hintCounter == 4)
        {
            hints++;
            hintCounter = 0;
        }
    }

    public void GameOver()
    {
        DataControl.dataControl.correctWord = currentEntry.word;
        DataControl.dataControl.Save();

        AudioManager.audioManager.PlaySound("Game Over");
        SceneManager.LoadScene("Game Over", LoadSceneMode.Single);
    }

    void CorrectlyGuessedAllWords()
    {
        AudioManager.audioManager.PlaySound("Game Win");
        SceneManager.LoadScene("Game Win", LoadSceneMode.Single);
    }

    void UpdateDataControl()
    {
        DataControl.dataControl.currentScore = playerScore;
        
        if (playerScore > DataControl.dataControl.highScore)
        {
            DataControl.dataControl.highScore = playerScore;
        }

        DataControl.dataControl.Save();
    }
}
