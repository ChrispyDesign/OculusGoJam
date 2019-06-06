using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int m_maxAmmo = 6;
    private Ammunition m_ammunition;

    // fire vibration variables
    [Header("Fire Vibration")]
    [SerializeField] private float m_fireTime = 0.5f;
    [Range(0, 1)]
    [SerializeField] private float m_fireFrequency = 0.5f;
    [Range(0, 1)]
    [SerializeField] private float m_fireAmplitude = 0.5f;

    [Header("Reload Vibration")]
    [SerializeField] private float m_reloadTime = 0.5f;
    [Range(0, 1)]
    [SerializeField] private float m_reloadFrequency = 0.5f;
    [Range(0, 1)]
    [SerializeField] private float m_reloadAmplitude = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_ammunition = new Ammunition(m_maxAmmo);
    }

    public void Fire(GameObject target, InputOculus inputVR = null)
    {
        if (m_ammunition.CanFire())
        {
            Debug.Log("Fire!");
            m_ammunition.Fire();

            if (inputVR)
                inputVR.StartVibrate(m_fireTime, m_fireFrequency, m_fireAmplitude);

            Interactable interactable = target.GetComponent<Interactable>();
            if (interactable)
                interactable.OnInteract();
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }
}
