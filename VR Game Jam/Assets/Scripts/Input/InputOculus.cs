using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class InputOculus : MonoBehaviour
{
    [SerializeField] private Transform m_rightHandAnchor;

    private Shooter m_shooter;

    // Start is called before the first frame update
    void Start()
    {
        m_shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = m_rightHandAnchor.position;
        Vector3 direction = m_rightHandAnchor.forward;

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            m_shooter.Fire(origin, direction);
        }
    }
}
