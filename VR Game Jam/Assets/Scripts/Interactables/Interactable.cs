using UnityEngine;

/// <summary>
/// base class for interactable objects
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    public abstract void OnInteract();
    public virtual void OnHover() { }
    public virtual void OnUnhover() { }
}
