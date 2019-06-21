using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadCurrent : Interactable
{
    [Header("Loading")]
    [SerializeField] private float m_loadDelay = 0.5f;

    [Header("Other Dependancies")]
    [SerializeField] private SphereCollider m_collider;
    [SerializeField] private Animator m_animator;
    public AudioSource m_ShotSound;
    
    /// <summary>
    /// 
    /// </summary>
    public override void OnInteract()
    {
        m_ShotSound.Play();
        StartCoroutine(Spin());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spin()
    {
        m_animator.SetTrigger("spin");
        m_collider.enabled = false;

        yield return new WaitForSeconds(m_loadDelay);

        FindObjectOfType<TransitionManager>().LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
