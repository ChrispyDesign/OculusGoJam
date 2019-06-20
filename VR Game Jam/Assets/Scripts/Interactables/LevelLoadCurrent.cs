using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadCurrent : Interactable
{
    public override void OnInteract()
    {
        FindObjectOfType<TransitionManager>().LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
