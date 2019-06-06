using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : Interactable
{
    public override void OnInteract()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
