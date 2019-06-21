using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Negative : Interactable
{
    public override void OnInteract()
    {
        Debug.Log("Oh no!");
        GameObject.Find("GameUmpire").GetComponent<UmpireControl>().gameFailed();

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("hide");
    }
}
