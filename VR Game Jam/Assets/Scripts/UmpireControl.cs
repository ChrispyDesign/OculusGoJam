using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UmpireControl : MonoBehaviour {

    public float preDrawTime_min;
    public float preDrawTime_max;
    float preDrawTime;

    bool isPlayerReady;
    bool timerRunning;
    bool isGameOver;
    [HideInInspector]
    public static bool isGameStarted;

    float reactionTimer;

    public Text ready_txt;
    public Text draw_txt;
    public Text again_txt;

    public GameObject holster_OBJ;

	// Use this for initialization
	void Start () {
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

        if (!isPlayerReady && Input.GetKeyDown(KeyCode.R))
        {
            onReadyPressed();
        }
    }



    public void onReadyPressed()
    {
        if (!isPlayerReady)
        {
            isPlayerReady = true;
            ready_txt.enabled = false;
            //Trigger related functions (e.g. Audio, UI)
        }
    }

    void onDrawIntiated()
    {
        draw_txt.enabled = true;
        timerRunning = true;
        isGameStarted = true;
        //Trigger related functions (e.g. Audio, UI)
    }

    public void onShootPressed()
    {
        if (timerRunning)
        {
            timerRunning = false;
            draw_txt.text = reactionTimer.ToString();
            isGameOver = true;
            again_txt.enabled = true;
            //Trigger related functions (e.g. Audio, UI)
        }

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
        again_txt.enabled   = false;
        ready_txt.enabled   = true;
    }
}
