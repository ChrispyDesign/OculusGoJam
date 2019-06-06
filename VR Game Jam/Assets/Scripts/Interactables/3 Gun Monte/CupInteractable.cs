using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInteractable : Interactable
{
    public override void OnInteract()
    {
        Destroy(gameObject);
    }
}
