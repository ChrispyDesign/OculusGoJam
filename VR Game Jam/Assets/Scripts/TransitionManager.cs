using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [Header("Fading")]
    [SerializeField] private Material m_gameOverFade;
    [SerializeField] private float m_fadeTime = 0.5f;
    private bool m_isFadeOver = false;

    [Header("Other Dependencies")]
    [SerializeField] private GameObject m_holsterZone;
    [SerializeField] private float m_holsterDelay = 0.5f;

    #region getters

    public bool GetIsFadeOver() { return m_isFadeOver; }

    #endregion

    private void Awake()
    {
        StartCoroutine(PerformFadeIn());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator PerformFadeIn()
    {
        float timer = 0;
        m_holsterZone.SetActive(false);

        // wait
        while (timer < m_fadeTime)
        {
            timer += Time.deltaTime;
            float percentage = 1 - (timer / m_fadeTime);

            // perform fade in
            m_gameOverFade.color = new Color(0, 0, 0, percentage);

            yield return null;
        }

        yield return new WaitForSeconds(m_holsterDelay);
        m_holsterZone.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildID"></param>
    public void LoadLevel(int buildID)
    {
        StartCoroutine(PerformFadeOut(buildID));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildID"></param>
    /// <returns></returns>
    private IEnumerator PerformFadeOut(int buildID)
    {
        float timer = 0;
        m_holsterZone.SetActive(false);

        // wait
        while (timer < m_fadeTime)
        {
            timer += Time.deltaTime;
            float percentage = timer / m_fadeTime;

            // perform fade out
            m_gameOverFade.color = new Color(0, 0, 0, percentage);

            yield return null;
        }

        // load relevant scene
        SceneManager.LoadScene(buildID);
    }
}
