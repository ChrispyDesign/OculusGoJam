using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolsterZone : Interactable
{
    [SerializeField] private UmpireControl m_umpire;

    /// <summary>
    /// 
    /// </summary>
    public override void OnInteract()
    {
        return;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnHover()
    {
        m_umpire.onHolster();
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnUnhover()
    {
        m_umpire.onUnholster();
    }
}
