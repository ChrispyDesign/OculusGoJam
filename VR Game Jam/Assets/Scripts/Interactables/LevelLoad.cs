using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : Interactable
{
    public int levelToLoad;

    public override void OnInteract()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
