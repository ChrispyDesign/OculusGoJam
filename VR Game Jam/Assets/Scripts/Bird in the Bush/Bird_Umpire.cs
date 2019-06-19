using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Umpire : MonoBehaviour
{
    public GameObject m_spawnLeftGuide;
    public GameObject m_spawnRightGuide;

    float SPWNLeft_YMin, SPWNLeft_YMax, SPWNRight_YMin, SPWNRight_YMax;

    public int totalBirds_min;
    public int totalBirds_max;
    int totalFlock;

    public GameObject prfb_bird;

    List<GameObject> birdList;

    bool flockSpawned;

    UmpireControl m_umpire;

    // Start is called before the first frame update
    void Start()
    {
        SPWNLeft_YMin   = m_spawnLeftGuide.GetComponent<Collider>().bounds.min.y;
        SPWNLeft_YMax   = m_spawnLeftGuide.GetComponent<Collider>().bounds.max.y;
        SPWNRight_YMin  = m_spawnRightGuide.GetComponent<Collider>().bounds.min.y;
        SPWNRight_YMax   = m_spawnRightGuide.GetComponent<Collider>().bounds.max.y;

        birdList            = new List<GameObject>();
        flockSpawned        = false;
        totalFlock          = Random.Range(totalBirds_min, totalBirds_max+1);
        m_umpire            = GetComponent<UmpireControl>();
    }

    void SpawnBird()
    {
        Vector3 bird_startPos = new Vector3(-51.59f, 0.0f, 0.0f);
        Vector3 bird_endPos = new Vector3(-51.59f, 0.0f, 0.0f);


        float SPWN_leftSideZ = m_spawnLeftGuide.transform.position.z;
        float SPWN_rightSideZ = m_spawnRightGuide.transform.position.z;

        float SPWNStart_YMin, SPWNStart_YMax, SPWNEnd_YMin, SPWNEnd_YMax;

        int LRRand = Random.Range(0, 2);
        if (LRRand == 0)
        {
            bird_startPos.z = SPWN_leftSideZ;   bird_endPos.z = SPWN_rightSideZ;
            SPWNStart_YMin = SPWNLeft_YMin;     SPWNStart_YMax = SPWNLeft_YMax;
            SPWNEnd_YMin = SPWNRight_YMin;      SPWNEnd_YMax = SPWNRight_YMax;
        }
        else
        {
            bird_startPos.z = SPWN_rightSideZ;  bird_endPos.z = SPWN_leftSideZ;
            SPWNStart_YMin = SPWNRight_YMin;    SPWNStart_YMax = SPWNRight_YMax;
            SPWNEnd_YMin = SPWNLeft_YMin;       SPWNEnd_YMax = SPWNLeft_YMax;
        }

        

        bird_startPos.y = Random.Range(SPWNStart_YMin, SPWNStart_YMax);
        bird_endPos.y = Random.Range(SPWNEnd_YMin, SPWNEnd_YMax);

        GameObject go = Instantiate(prfb_bird, bird_startPos, Quaternion.identity);

        go.GetComponent<BirdBehaviour>().setDestination(bird_endPos);

        birdList.Add(go);
    }

    void spawnFlock()
    {
        for (int i = 0; i < totalFlock; i++)
        {
            SpawnBird();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UmpireControl.isGameStarted && !flockSpawned)
        {
            spawnFlock();
            flockSpawned = true;
        }
        if (UmpireControl.isGameStarted && birdList.Count == 0)
        {
            m_umpire.gameSuccess();

            float reactionTime = UmpireControl.reactionTimer;

            if (HighscoreManager.GetHighscore("Bird in the Bush") > reactionTime)
                HighscoreManager.SetHighscore("Bird in the Bush", reactionTime);

            m_umpire.ShowHighscore(HighscoreManager.GetHighscore("Bird in the Bush"));
        }
    }


    public void onOpponentShot(GameObject go)
    {
        birdList.Remove(go);
    }

    public void onBirdSurvive()
    {
        foreach (GameObject bird in birdList)
        {
            Destroy(bird);
        }
        m_umpire.gameFailed();
    }
}

