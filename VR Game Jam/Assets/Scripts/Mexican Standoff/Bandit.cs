using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct RingStage
{
    public Color colour;
    [Range(0, 1)]
    public float percentage;
}

public class Bandit : Interactable
{
    private BanditSpawner m_banditSpawner;

    [SerializeField] Vector2 m_randomFireRange = new Vector2(5, 10);
    [SerializeField] List<RingStage> m_ringStages;
    private float m_randomFireTime;
    
    [SerializeField] GameObject m_fireRing;

    #region setters

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
        m_randomFireTime = Random.Range(m_randomFireRange.x, m_randomFireRange.y);
        Vector3 newSize = ResizeRing();
        
        float timer = 0;
        while (timer < m_randomFireTime)
        {
            float percentage = 1 - (timer / m_randomFireTime);
            Vector3 newNewSizeForRealThisTime = percentage * newSize;

            m_fireRing.transform.localScale = newNewSizeForRealThisTime;
            m_fireRing.GetComponent<SpriteRenderer>().color = DetermineRingColour(percentage);

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
        float ringSizePercent = m_randomFireTime / (m_randomFireRange.y - m_randomFireRange.x);

        Vector3 newSize = ringSizePercent * m_fireRing.transform.localScale;
        m_fireRing.transform.localScale = newSize;

        return newSize;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="percentage"></param>
    /// <returns></returns>
    private Color DetermineRingColour(float percentage)
    {
        RingStage bestMatch = m_ringStages[0];

        for (int i = 0; i < m_ringStages.Count; i++)
        {
            RingStage ringStage = m_ringStages[i];

            if (ringStage.percentage > percentage)
            {
                float percentageDifference = ringStage.percentage - percentage;

                if (percentageDifference < bestMatch.percentage - percentage)
                    bestMatch = ringStage;
            }
        }

        return bestMatch.colour;
    }
}
