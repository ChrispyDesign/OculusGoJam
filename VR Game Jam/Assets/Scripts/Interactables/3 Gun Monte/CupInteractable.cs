using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInteractable : Interactable
{
    private bool m_isDesiredCup = false;
    private UmpireControl m_umpire;

    #region getters

    public bool GetIsDesiredCup() { return m_isDesiredCup; }

    #endregion

    #region setters

    public void SetAsDesiredCup() { m_isDesiredCup = true; }
    public void SetUmpire(UmpireControl umpire) { m_umpire = umpire; }

    #endregion

    public override void OnInteract()
    {
        if (m_isDesiredCup)
        {
            Debug.Log("Nice!");
            m_umpire.gameSuccess();
        }
        else
        {
            Debug.Log("Game over!");
            m_umpire.gameFailed();
        }
    }
}
