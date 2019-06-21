using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positive : Interactable
{
    public override void OnInteract()
    {
        Debug.Log("Yay!");

        var MNT_Ump = FindObjectOfType<MNTY_Umpire>();
        if (MNT_Ump)
        {
            MNT_Ump.onOpponentShot(gameObject);
            MNT_Ump.RemoveBandit(gameObject);

            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("hide");
        }

        var BRD_Ump = FindObjectOfType<Bird_Umpire>();
        if (BRD_Ump)
        {
            BRD_Ump.onOpponentShot(gameObject);
            Destroy(gameObject);
        }
        
    }
}
