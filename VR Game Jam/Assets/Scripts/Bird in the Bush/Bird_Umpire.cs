using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Umpire : MonoBehaviour
{
    float SPWN_leftSideZ;
    float SPWN_rightSideZ;

    float SPWN_YMin;
    float SPWN_YMax;

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
        SPWN_leftSideZ      = -22.0f;
        SPWN_rightSideZ     = 22.0f;
        SPWN_YMin           = -17.0f;
        SPWN_YMax           = 23.0f;
        birdList            = new List<GameObject>();
        flockSpawned        = false;
        totalFlock          = Random.Range(totalBirds_min, totalBirds_max);
        m_umpire            = GetComponent<UmpireControl>();
    }

    void SpawnBird()
    {
        Vector3 bird_startPos = new Vector3(-51.59f, 0.0f, 0.0f);
        Vector3 bird_endPos = new Vector3(-51.59f, 0.0f, 0.0f);

        int LRRand = Random.Range(0, 1);
        if (LRRand == 0) { bird_startPos.z = SPWN_leftSideZ; bird_endPos.z = SPWN_rightSideZ; }
        else { bird_startPos.z = SPWN_rightSideZ; bird_endPos.z = SPWN_leftSideZ; }

        bird_startPos.y = Random.Range(SPWN_YMin, SPWN_YMax);
        bird_endPos.y = Random.Range(SPWN_YMin, SPWN_YMax);

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

