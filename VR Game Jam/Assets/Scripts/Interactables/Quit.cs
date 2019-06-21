using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : Interactable
{
    [Header("Quitting")]
    [SerializeField] private float m_quitDelay = 0.5f;

    [Header("Other Dependencies")]
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

        yield return new WaitForSeconds(m_quitDelay);

        Application.Quit(); ;
    }
}
