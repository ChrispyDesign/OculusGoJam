using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class InputPC : MonoBehaviour
{
    [SerializeField] private Camera m_camera;

    private Shooter m_shooter;

    // Start is called before the first frame update
    void Start()
    {
        m_shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_shooter.Fire(transform.position, m_camera.WorldToScreenPoint(Input.mousePosition));
        }
    }
}
