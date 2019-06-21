using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject m_reticlePrefab;
    private GameObject m_reticle;
    [SerializeField] private int m_maxAmmo = 10000;
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

    [Header("Audio")]
    public AudioSource m_ShotSound;
    public AudioSource m_EmptySound;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        m_reticle = Instantiate(m_reticlePrefab);
        m_ammunition = new Ammunition(m_maxAmmo);
    }

    private void Update()
    {
        if (UmpireControl.isGameStarted && !m_isGunInitiated)
        {
            m_ammunition.Reload();
            m_isGunInitiated = true;
        }
        
        m_reticle.transform.position = Raycaster.GetHitPoint() + Raycaster.GetHitNormal() * 0.001f;
        m_reticle.transform.rotation = Quaternion.LookRotation(Raycaster.GetHitNormal());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    public void Fire(GameObject target)
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
                    m_ShotSound.Play();
                }
                else
                {
                    m_EmptySound.Play();
                }
            }
            else
                return;
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
