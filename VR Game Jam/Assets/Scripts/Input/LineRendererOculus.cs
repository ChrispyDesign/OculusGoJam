using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererOculus : MonoBehaviour
{
    [SerializeField] private Transform m_rightHandAnchor;

    private LineRenderer m_lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = m_rightHandAnchor.position;
        Vector3 direction = m_rightHandAnchor.forward;

        UpdateLineRenderer(origin, direction * 500, new Color(0, 1, 0, 0.5f));

        RaycastHit hit;

        // perform ray cast
        if (Physics.Raycast(origin, direction, out hit))
        {
            UpdateLineRenderer(origin, hit.point, new Color(0, 1, 0, 1));
        }
    }

    private void UpdateLineRenderer(Vector3 origin, Vector3 destination, Color color)
    {
        m_lineRenderer.SetPosition(0, origin);
        m_lineRenderer.SetPosition(1, destination);
        m_lineRenderer.material.color = color;
    }
}
