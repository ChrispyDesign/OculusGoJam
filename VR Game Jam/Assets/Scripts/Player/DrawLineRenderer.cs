using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawLineRenderer : MonoBehaviour
{
    private LineRenderer m_lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    public void Draw(Vector3 origin, Vector3 direction)
    {
        m_lineRenderer.SetPosition(0, origin);
        m_lineRenderer.SetPosition(1, direction);

        RaycastHit hit;

        // perform ray cast
        if (Physics.Raycast(origin, direction, out hit))
        {
            SetColour(new Color(0, 1, 0, 1));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    public void SetColour(Color color)
    {
        m_lineRenderer.material.color = color;
    }
}
