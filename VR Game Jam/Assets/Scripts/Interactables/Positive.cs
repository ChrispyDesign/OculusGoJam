using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positive : Interactable
{
    public override void OnInteract()
    {
        Debug.Log("Yay!");
        GameObject.Find("GameUmpire").GetComponent<UmpireControl>().onOpponentShot(gameObject);
        Destroy(gameObject);
    }
}
