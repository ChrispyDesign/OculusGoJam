using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Interactable
{
    [SerializeField] Vector2 m_randomFireRange = new Vector2(5, 10);
    private float m_randomFireTime;
    
    private bool m_wasShot = false;

    /// <summary>
    /// 
    /// </summary>
    public override void OnInteract()
    {
        m_wasShot = true;
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
        yield return new WaitForSeconds(m_randomFireTime);

        if (!m_wasShot)
            FindObjectOfType<UmpireControl>().gameFailed();
    }
}
