using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Spinner : Interactable
{
    [Header("Spinning")]
    [SerializeField] private float m_spinDelay = 0.5f;
    private bool m_canSpin = true;

    [Header("Other Dependancies")]
    [SerializeField] private Animator m_animator;

    /// <summary>
    /// 
    /// </summary>
    public override void OnInteract()
    {
        if (m_canSpin)
            StartCoroutine(Spin());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spin()
    {
        m_animator.SetTrigger("spin");
        m_canSpin = false;

        yield return new WaitForSeconds(m_spinDelay);

        m_canSpin = true;
    }
}
