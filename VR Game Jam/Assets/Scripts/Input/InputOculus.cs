using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Raycaster))]
public class InputOculus : MonoBehaviour
{
    [SerializeField] private Gun m_gun;
    [SerializeField] private Transform m_rightHandAnchor;

    private Raycaster m_raycaster;

    private IEnumerator m_vibrateRoutine;

    // Start is called before the first frame update
    void Start()
    {
        m_raycaster = GetComponent<Raycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = m_rightHandAnchor.position;
        Vector3 direction = m_rightHandAnchor.forward;
        GameObject hitObject = m_raycaster.Raycast(origin, direction);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {

            if (m_gun)
                m_gun.Fire(hitObject, this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    /// <param name="frequency"></param>
    /// <param name="amplitude"></param>
    public void StartVibrate(float time, float frequency, float amplitude)
    {
        if (m_vibrateRoutine != null)
            StopCoroutine(m_vibrateRoutine);

        StartCoroutine(m_vibrateRoutine = Vibrate(time, frequency, amplitude));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    /// <param name="frequency"></param>
    /// <param name="amplitude"></param>
    /// <returns></returns>
    private IEnumerator Vibrate(float time, float frequency, float amplitude)
    {
        OVRInput.SetControllerVibration(frequency, amplitude);

        yield return new WaitForSeconds(time);

        OVRInput.SetControllerVibration(0, 0);
    }
}
