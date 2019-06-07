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
        
        if (Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = m_raycaster.Raycast(ray);

            if (hitObject && m_gun)
                m_gun.Fire(hitObject);
        }

        if (m_raycaster.Raycast(ray) == m_gun.m_holster)
        {
            m_gun.m_umpire.onHolster();
        }
    }
}
