using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInteractable : Interactable
{
    private bool m_isDesiredCup = false;

    #region getters

    public bool GetIsDesiredCup() { return m_isDesiredCup; }

    #endregion

    #region setters

    public void SetAsDesiredCup() { m_isDesiredCup = true; }

    #endregion

    public override void OnInteract()
    {
        if (m_isDesiredCup)
            Debug.Log("Nice!");
        else
            Debug.Log("Game over!");

        Destroy(gameObject);
    }
}
