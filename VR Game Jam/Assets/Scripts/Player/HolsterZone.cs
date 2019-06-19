using UnityEngine;

public class HolsterZone : Interactable
{
    [SerializeField] private UmpireControl m_umpire;

    /// <summary>
    /// do nothing on interaction
    /// </summary>
    public override void OnInteract()
    {
        return;
    }

    /// <summary>
    /// handle hovering
    /// </summary>
    public override void OnHover()
    {
        m_umpire.onHolster();
    }

    /// <summary>
    /// handle unhovering
    /// </summary>
    public override void OnUnhover()
    {
        m_umpire.onUnholster();
    }
}
