using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum EndCondition
{
    WrongTarget,
    TimeOut,
    Unholster
}

public class UmpireControl : MonoBehaviour {

    public float preDrawTime_min;
    public float preDrawTime_max;
    float preDrawTime;

    public static bool isPlayerReady = false;
    bool timerRunning = false;
    bool isGameOver = false;
    [HideInInspector]
    public static bool isGameStarted = false;

    public static float reactionTimer;

    [HideInInspector]
    public static bool isObjectiveComplete = false;

    [SerializeField] private TransitionManager m_transitionManager;

    // UI elements
    [Header("UI Elements")]
    [SerializeField] private TextMeshPro m_gameStateText;
    [SerializeField] private TextMeshPro m_gameOverText;
    [SerializeField] private TextMeshPro m_scoreText;
    [SerializeField] private TextMeshPro m_highscoreText;
    
    // game state messages
    [Header("Game State")]
    [SerializeField] private string m_readyMessage = "Ready?";
    [SerializeField] private string m_steadyMessage = "Steady...";
    [SerializeField] private string m_drawMessage = "Draw!";

    // game over messages
    [Header("Game Over")]
    [SerializeField] private float m_gameOverWaitTime = 3.0f;
    [SerializeField] private string m_wrongTargetMessage = "Wrong Target!";
    [SerializeField] private string m_timeOutMessage = "Time Out!";
    [SerializeField] private string m_unholsterMessage = "Be Patient!";
    public AudioSource m_SuccessSound;
    public AudioSource m_FailureSound;

    // Use this for initialization
    void Awake ()
    {
        resetAll();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isPlayerReady && !timerRunning && !isGameOver)            //When Player ready, countdown timer to "Draw!"
        {
            preDrawTime -= Time.deltaTime;

            if (preDrawTime <= 0.0f)                        //When "Draw!" occurs, start the timer
            {
                onDrawIntiated();
            }
        }
        else if (timerRunning)                              //Adds to reaction timer
        {
            reactionTimer += Time.deltaTime;
        }
        if (isObjectiveComplete)                            //
        {
            gameSuccess();
            isObjectiveComplete = false;
        }
    }

    public void onHolster()
    {
        if (!isPlayerReady && !isGameStarted && !isGameOver) //Set player as ready at start of game
        {
            isPlayerReady = true;

            // update UI to steady/wait
            ShowGameState(m_steadyMessage);

            //Trigger related functions (e.g. Audio, UI)

        }
    }

    public void onUnholster()
    {
        if (isPlayerReady && !isGameStarted)
        {
            gameFailed(EndCondition.Unholster);
        }
    }

    void onDrawIntiated()
    {
        // update UI to draw
        ShowGameState(m_drawMessage);

        timerRunning = true;
        isGameStarted = true;

        //Trigger related functions (e.g. Audio, UI)

    }

    void resetAll()
    {
        preDrawTime         = Random.Range(preDrawTime_min, preDrawTime_max); //Generates a random time before "Draw!"
        isPlayerReady       = false;
        timerRunning        = false;
        reactionTimer       = 0.0f;

        // show ready UI element
        ShowGameState(m_readyMessage);

        isGameOver          = false;
        isObjectiveComplete = false;
        isGameStarted       = false;
    }

    public void gameSuccess()
    {
        //game success things
        timerRunning = false;

        m_SuccessSound.Play();

        // game is over, show the score
        isGameOver = true;
        ShowScore();

        //Trigger related functions (e.g. Audio, UI)

    }

    /// <summary>
    /// game over function which displays a fail message and starts scene reload
    /// </summary>
    /// <param name="endCondition"></param>
    public void gameFailed(EndCondition endCondition = EndCondition.WrongTarget)
    {
        // player is no longer ready
        isPlayerReady = false;
        isGameOver = true;

        m_FailureSound.Play();

        // different message depending on fail condition
        switch (endCondition)
        {
            case EndCondition.WrongTarget:
                ShowGameOver(m_wrongTargetMessage);
                break;

            case EndCondition.TimeOut:
                ShowGameOver(m_timeOutMessage);
                break;

            case EndCondition.Unholster:
                ShowGameOver(m_unholsterMessage);
                break;
        }

        // wait an arbritrary amount of time before reloading scene
        StartCoroutine(PerformFadeOut());
    }

    /// <summary>
    /// coroutine which waits an arbritrary amount of time before resetting the scene
    /// </summary>
    private IEnumerator PerformFadeOut()
    {
        yield return new WaitForSeconds(m_gameOverWaitTime);

        // reload scene
        m_transitionManager.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    private void ShowGameState(string text)
    {
        // disable other UI elements
        m_gameOverText.enabled = false;
        m_scoreText.enabled = false;
        m_highscoreText.enabled = false;

        // show game state UI element
        m_gameStateText.text = text;
        m_gameStateText.enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    private void ShowGameOver(string text)
    {
        // disable other UI elements
        m_gameStateText.enabled = false;
        m_scoreText.enabled = false;
        m_highscoreText.enabled = false;

        // show game over UI element
        m_gameOverText.text = text;
        m_gameOverText.enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    private void ShowScore()
    {
        // disable other UI elements
        m_gameStateText.enabled = false;
        m_gameOverText.enabled = false;

        // update score UI element
        double reactTimeDisplay = System.Math.Round(reactionTimer, 2);
        m_scoreText.text = reactTimeDisplay.ToString();
        m_scoreText.enabled = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="highscore"></param>
    public void ShowHighscore(float highscore)
    {
        // disable other UI elements
        m_gameStateText.enabled = false;
        m_gameOverText.enabled = false;

        // show high score UI element
        m_highscoreText.text = "Highscore: " + System.Math.Round(highscore, 2);
        m_highscoreText.enabled = true;
    }
}
