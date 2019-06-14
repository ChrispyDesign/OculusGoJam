using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int m_maxAmmo = 6;
    private Ammunition m_ammunition;
    private bool m_isGunInitiated = false;

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

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        m_ammunition = new Ammunition(m_maxAmmo);
    }

    private void Update()
    {
        if (UmpireControl.isGameStarted && !m_isGunInitiated)
        {
            m_ammunition.Reload();
            m_isGunInitiated = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="inputVR"></param>
    public void Fire(GameObject target, InputOculus inputVR = null)
    {
        Interactable interactable = target.GetComponent<Interactable>();

        if (interactable)
        {
            if (interactable.IsShootableAnytime()) // menu interactables
            {
                interactable.OnInteract(); // shoot but don't decrease ammo
            }
            else if (UmpireControl.isGameStarted) // game interactables
            {
                if (m_ammunition.CanFire()) // does the player have ammo?
                {
                    interactable.OnInteract(); // shoot
                    m_ammunition.Fire(); // and decrease ammo
                }
            }
            else
                return;

            if (inputVR)
                inputVR.StartVibrate(m_fireTime, m_fireFrequency, m_fireAmplitude);
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
