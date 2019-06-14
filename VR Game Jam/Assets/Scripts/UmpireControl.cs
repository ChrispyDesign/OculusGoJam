using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UmpireControl : MonoBehaviour {

    public float preDrawTime_min;
    public float preDrawTime_max;
    float preDrawTime;

    public static bool isPlayerReady;
    bool timerRunning;
    bool isGameOver;
    [HideInInspector]
    public static bool isGameStarted;

    float reactionTimer;

    public Text ready_txt;
    public Text draw_txt;

    [HideInInspector]
    public static bool isObjectiveComplete;

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
            draw_txt.text = "DRAW!";
            if (preDrawTime <= 0.0f)                        //When "Draw!" occurs, start the timer
            {
                onDrawIntiated();
            }
        }
        else if (timerRunning)                              //Adds to reaction timer
        {
            reactionTimer += Time.deltaTime;
        }
    }

    public void onHolster()
    {
        if (!isPlayerReady)                                 //Set player as ready at start of game
        {
            isPlayerReady = true;
            ready_txt.enabled = false;
            //Trigger related functions (e.g. Audio, UI)
        }
        if (isObjectiveComplete)                            //
        {
            gameSuccess();
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
        draw_txt.enabled    = false;
        isGameOver          = false;
        ready_txt.enabled   = true;
        isObjectiveComplete = false;
    }

    public void gameFailed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameSuccess()
    {
        //game success things
        timerRunning = false;
        draw_txt.text = reactionTimer.ToString();
        isGameOver = true;

        //Trigger related functions (e.g. Audio, UI)

    }
}
