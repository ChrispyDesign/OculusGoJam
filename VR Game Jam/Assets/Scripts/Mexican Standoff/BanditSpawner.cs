using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_banditPrefab;
    [SerializeField] private Vector2 m_fireTimeRange = new Vector2(1, 5);

    [Header("Spawn Variables")]
    [SerializeField] private List<Transform> m_spawnPositions;
    [SerializeField] private int m_numberToSpawn;

    private List<GameObject> m_bandits = new List<GameObject>();
    private List<Transform> m_availableSpawnPositions = new List<Transform>();

    private bool m_isGameStarted = false;
    private bool m_isGameFinished = false;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        // get all available positions
        for (int i = 0; i < m_spawnPositions.Count; i++)
        {
            Transform spawnTransform = m_spawnPositions[i];
            m_availableSpawnPositions.Add(spawnTransform);
        }

        // spawn bandits
        for (int i = 0; i < m_numberToSpawn; i++)
        {
            // pick a random spawn point
            int randomIndex = Random.Range(0, m_availableSpawnPositions.Count);
            Transform spawnTransform = m_availableSpawnPositions[randomIndex];
            m_availableSpawnPositions.RemoveAt(randomIndex); // position no longer available

            // spawn bandit
            GameObject bandit = Instantiate(m_banditPrefab, spawnTransform);
            m_bandits.Add(bandit);

            // inject bandit spawner dependency into bandit
            Bandit banditScript = bandit.GetComponent<Bandit>();
            banditScript.SetBanditSpawner(this);
            banditScript.SetFireTime(Mathf.Lerp(m_fireTimeRange.x, m_fireTimeRange.y, i / (float)m_numberToSpawn));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (UmpireControl.isGameStarted && !m_isGameStarted)
        {
            for (int i = 0; i < m_bandits.Count; i++)
            {
                // show the bandit
                GameObject bandit = m_bandits[i];
                bandit.SetActive(true);

                // start the bandit's countdown
                Bandit banditScript = bandit.GetComponent<Bandit>();
                banditScript.StartFireCountdown();
            }

            m_isGameStarted = true;
        }

        if (m_bandits.Count == 0 && !m_isGameFinished)
        {
            UmpireControl umpire = FindObjectOfType<UmpireControl>();
            umpire.gameSuccess();

            float reactionTime = UmpireControl.reactionTimer;

            if (HighscoreManager.GetHighscore("Mexican Standoff") > reactionTime)
                HighscoreManager.SetHighscore("Mexican Standoff", reactionTime);

            umpire.ShowHighscore(HighscoreManager.GetHighscore("Mexican Standoff"));

            m_isGameFinished = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bandit"></param>
    public void RemoveBandit(GameObject bandit)
    {
        m_bandits.Remove(bandit);
    }
}
