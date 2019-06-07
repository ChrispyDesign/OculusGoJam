using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positive : Interactable
{
    public override void OnInteract()
    {
        Debug.Log("Yay!");
        FindObjectOfType<MNTY_Umpire>().onOpponentShot(gameObject);
        Destroy(gameObject);
    }
}
