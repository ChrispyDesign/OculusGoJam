using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class InputPC : MonoBehaviour
{
    [SerializeField] private Gun m_gun;
    [SerializeField] private Camera m_camera;

    private Raycaster m_raycaster;


    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        m_raycaster = GetComponent<Raycaster>();
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
        GameObject hitObject = m_raycaster.Raycast(ray);

        if (Input.GetMouseButtonDown(0))
        {
            if (hitObject && m_gun)
                m_gun.Fire(hitObject);
        }
    }
}
