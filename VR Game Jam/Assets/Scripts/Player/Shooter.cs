using UnityEngine;

public class Shooter : MonoBehaviour
{
    /// <summary>
    /// function which performs a "gun" fire. Performs raycast to object and if the object is shootable,
    /// call the shootable object's OnShoot function
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="origin"></param>
    public void Fire(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        // perform ray cast
        if (Physics.Raycast(origin, direction, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            Interactable interactable = hitObject.GetComponent<Interactable>();
            
            if (interactable)
                interactable.OnInteract();
        }
    }
}
