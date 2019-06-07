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

    // reload vibration variables
    [Header("Reload Vibration")]
    [SerializeField] private float m_reloadTime = 0.5f;
    [Range(0, 1)]
    [SerializeField] private float m_reloadFrequency = 0.5f;
    [Range(0, 1)]
    [SerializeField] private float m_reloadAmplitude = 0.5f;

    [HideInInspector]
    public GameObject m_holster;
    [HideInInspector]
    public UmpireControl m_umpire;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        m_ammunition = new Ammunition(m_maxAmmo);
        m_holster = GameObject.Find("Holster");
        m_umpire = GameObject.Find("GameUmpire").GetComponent<UmpireControl>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="inputVR"></param>
    public void Fire(GameObject target, InputOculus inputVR = null)
    {
        if (m_ammunition.CanFire())
        {
            // can fire
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
            // cant fire - out of ammo - add a gun click sound
            Debug.Log("Out of ammo!");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reload()
    {
        m_ammunition.Reload();
    }
}
