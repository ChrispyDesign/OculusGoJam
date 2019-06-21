using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadCurrent : Interactable
{
    [SerializeField] private float m_loadDelay = 0.5f;
    private Animator m_animator;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

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

        yield return new WaitForSeconds(m_loadDelay);

        FindObjectOfType<TransitionManager>().LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
