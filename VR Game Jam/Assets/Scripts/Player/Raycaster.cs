using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private DrawLineRenderer m_lineRenderer;
    [SerializeField] private Transform m_rightHandAnchor;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        m_lineRenderer = GetComponent<DrawLineRenderer>();
    }

    /// <summary>
    /// function which performs a raycast
    /// </summary>
    /// <param name="origin">origin point of raycast</param>
    /// <param name="direction">direction to perform raycast in</param>
    /// <returns>null if an object was hit, otherwise returns the hit object</returns>
    public GameObject Raycast(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        // perform raycast
        if (Physics.Raycast(origin, direction, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            m_lineRenderer.Draw(m_rightHandAnchor.position, direction);

            if (hitObject)
            {
                m_lineRenderer.Draw(m_rightHandAnchor.position, hit.point);
                return hitObject; // object was hit
            }
        }

        return null; // no object was hit
    }

    /// <summary>
    /// function which performs a raycast
    /// </summary>
    /// <param name="ray">the ray to cast along</param>
    /// <returns>null if an object was hit, otherwise returns the hit object</returns>
    public GameObject Raycast(Ray ray)
    {
        RaycastHit hit;

        // perform raycast
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            m_lineRenderer.Draw(m_rightHandAnchor.position, ray.direction);

            if (hitObject)
            {
                m_lineRenderer.Draw(m_rightHandAnchor.position, hit.point);
                return hitObject; // object was hit
            }
        }

        return null; // no object was hit
    }
}
