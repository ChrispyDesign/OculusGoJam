using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : Interactable
{
    [Header("Loading")]
    public int levelToLoad;
    [SerializeField] private float m_loadDelay = 0.5f;

    [Header("Other Dependancies")]
    [SerializeField] private SphereCollider m_collider;
    [SerializeField] private Animator m_animator;

    /// <summary>
    /// 
    /// </summary>
    public override void OnInteract()
    {
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

        FindObjectOfType<TransitionManager>().LoadLevel(levelToLoad);
    }
}
