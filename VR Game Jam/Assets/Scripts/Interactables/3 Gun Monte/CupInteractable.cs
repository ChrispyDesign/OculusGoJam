using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInteractable : Interactable
{
    private MeshRenderer m_meshRenderer;
    private CapsuleCollider m_capsuleCollider;

    private bool m_isDesiredCup = false;
    private UmpireControl m_umpire;

    #region getters

    public bool GetIsDesiredCup() { return m_isDesiredCup; }

    #endregion

    #region setters

    public void SetAsDesiredCup() { m_isDesiredCup = true; }
    public void SetUmpire(UmpireControl umpire) { m_umpire = umpire; }

    #endregion

    private void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public override void OnInteract()
    {
        if (m_isDesiredCup)
        {
            m_umpire.gameSuccess();

            m_meshRenderer.enabled = false;
            m_capsuleCollider.enabled = false;
        }
        else
        {
            m_umpire.gameFailed();
        }
    }
}
