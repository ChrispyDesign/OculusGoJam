using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Interactable
{
    private BanditSpawner m_banditSpawner;

    private float m_fireTime;

    [SerializeField] GameObject m_fireRing;
    [SerializeField] float m_fireRingScalar = 1.0f;
    [SerializeField] Gradient m_gradient;

    #region setters

    public void SetFireTime(float fireTime) { m_fireTime = fireTime; }
    public void SetBanditSpawner(BanditSpawner banditSpawner) { m_banditSpawner = banditSpawner; }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public override void OnInteract()
    {
        m_banditSpawner.RemoveBandit(gameObject);
        Destroy(gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    public void StartFireCountdown()
    {
        StartCoroutine(FireCountdown());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator FireCountdown()
    {
        m_fireRing.SetActive(true);
        Vector3 newSize = ResizeRing();

        float timer = 0;
        while (timer < m_fireTime)
        {
            float percentage = 1 - (timer / m_fireTime);
            Vector3 newNewSizeForRealThisTime = percentage * newSize;

            m_fireRing.transform.localScale = newNewSizeForRealThisTime;
            m_fireRing.GetComponent<SpriteRenderer>().color = m_gradient.Evaluate(percentage);

            timer += Time.deltaTime;
            yield return null;
        }

        FindObjectOfType<UmpireControl>().gameFailed();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private Vector3 ResizeRing()
    {
        Vector3 newSize = m_fireTime * m_fireRing.transform.localScale * m_fireRingScalar;
        m_fireRing.transform.localScale = newSize;

        return newSize;
    }
}
