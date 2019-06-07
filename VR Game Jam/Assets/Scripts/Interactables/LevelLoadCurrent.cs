﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadCurrent : Interactable
{
    public int levelToLoad;

    public override void OnInteract()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}