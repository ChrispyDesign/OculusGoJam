using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UmpireControl : MonoBehaviour {

    [Header("Time Settings")]
    public float preDrawTime_min;
    public float preDrawTime_max;
    float preDrawTime;

    public static bool isPlayerReady = false;
    bool timerRunning = false;
    bool isGameOver = false;
    [HideInInspector]
    public static bool isGameStarted = false;

    public static float reactionTimer;


    [Header("Text Settings")]
    public TextMeshPro ready_txt;
    public TextMeshPro wait_txt;
    public TextMeshPro draw_txt;
    public TextMeshPro timer_txt;
    public TextMeshPro highscore_txt;

    [HideInInspector]
    public static bool isObjectiveComplete = false;

    [Header("Audio Settings")]

	// Use this for initialization
	void Awake () {
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
        if (!isPlayerReady)                                 //Set player as ready at start of game
        {
            isPlayerReady = true;
            ready_txt.enabled = false;
            wait_txt.enabled = true;
            //Trigger related functions (e.g. Audio, UI)
        }
    }

    public void onUnholster()
    {
        if (isPlayerReady && !isGameStarted)
        {
            gameFailed();
        }
    }

    void onDrawIntiated()
    {
        wait_txt.enabled = false;
        draw_txt.enabled = true;
        timerRunning = true;
        isGameStarted = true;
        //Trigger related functions (e.g. Audio, UI)
    }

    public void onResetPressed()
    {
        if (isGameOver)
        {
            resetAll();
        }
    }

    void resetAll()
    {
        preDrawTime         = Random.Range(preDrawTime_min, preDrawTime_max); //Generates a random time before "Draw!"
        isPlayerReady       = false;
        timerRunning        = false;
        reactionTimer       = 0.0f;
        ready_txt.enabled   = true;
        wait_txt.enabled    = false;
        draw_txt.enabled    = false;
        timer_txt.enabled   = false;
        highscore_txt.enabled = false;
        isGameOver          = false;
        isObjectiveComplete = false;
        isGameStarted       = false;
    }

    public void gameFailed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameSuccess()
    {
        //game success things
        timerRunning = false;
        draw_txt.enabled = false;
        double reactTimeDisplay = System.Math.Round(reactionTimer, 2);
        timer_txt.text = reactTimeDisplay.ToString();
        timer_txt.enabled = true;
        isGameOver = true;

        //Trigger related functions (e.g. Audio, UI)

    }

    public void ShowHighscore(float highscore)
    {
        highscore_txt.text = "Highscore: " + System.Math.Round(highscore, 2);
        highscore_txt.enabled = true;
    }
}
