using UnityEngine;

/// <summary>
/// base class for interactable objects
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] private bool m_isShootableAnytime = false;

    #region getters

    public bool IsShootableAnytime() { return m_isShootableAnytime; }

    #endregion

    public abstract void OnInteract();
    public virtual void OnHover() { }
    public virtual void OnUnhover() { }
}