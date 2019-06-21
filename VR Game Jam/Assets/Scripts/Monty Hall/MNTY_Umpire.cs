using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MNTY_Umpire : MonoBehaviour
{
    List<GameObject> doorList;

    [Range(0,5)]
    public int LVL1_opponents;
    [Range(0, 5)]
    public int LVL1_bystanders;
    [Range(0, 5)]
    public int LVL2_opponents;
    [Range(0, 5)]
    public int LVL2_bystanders;
    [Range(0, 5)]
    public int LVL3_opponents;
    [Range(0, 5)]
    public int LVL3_bystanders;

    Vector3 offset;

    public GameObject prefab_SuccessTarget;
    public GameObject prefab_FailureTarget;

    [Range(1, 3)]
    public int testDifficulty;

   UmpireControl m_umpire;

    bool isDrawSignalSent;

    [HideInInspector]
    public static List<GameObject> m_opponents;

    int m_totalDoors;

    void Start()
    {
        m_totalDoors = 5;
        m_umpire = GetComponent<UmpireControl>();
        m_opponents = new List<GameObject>();
        doorList = new List<GameObject>();
        for (int i = 1; i < m_totalDoors + 1; i++)
        {
            string doorName = "MontyDoor" + i.ToString();

            GameObject doorOBJ = GameObject.Find(doorName);
            doorList.Add(doorOBJ);
        }
        //offset = new Vector3(1.55f, 0.0f, 1.1f);
        setupMNTY(testDifficulty);
        isDrawSignalSent = false;
    }

    public void setupMNTY(int difficulty) //1 for easy up to 3 for hard
    {
        int opponents = 0;
        int bystanders = 0;
        if      (difficulty == 1)       { opponents = LVL1_opponents;   bystanders = LVL1_bystanders; }
        else if (difficulty == 2)       { opponents = LVL2_opponents;   bystanders = LVL2_bystanders; }
        else if (difficulty == 3)       { opponents = LVL3_opponents;   bystanders = LVL3_bystanders; }
        else                            { Debug.Log("Error Invalid Difficulty set."); }

        runDoorSetup(opponents,bystanders);
    }

    void runDoorSetup(int opponents, int bystanders)
    {
        List<GameObject> temp_doors = new List<GameObject>(doorList);

        for (int o = 0; o < opponents; o++)
        {
            int doorListSelect = UnityEngine.Random.Range(0, temp_doors.Count - 1);
            GameObject selectedDoor = temp_doors[doorListSelect];
            selectedDoor.GetComponent<MNTY_Door>().setWillBeOpenedTrue();
            GameObject go = Instantiate(prefab_SuccessTarget);
            //go.transform.SetParent(selectedDoor.transform);
            Vector3 target_pos = selectedDoor.transform.GetChild(1).position;
            go.transform.position = target_pos;
            go.GetComponent<Animator>().SetTrigger("reveal");
            m_opponents.Add(go);

            temp_doors.Remove(selectedDoor);
        }

        for (int b = 0; b < bystanders; b++)
        {
            int doorListSelect = UnityEngine.Random.Range(0, temp_doors.Count - 1);
            GameObject selectedDoor = temp_doors[doorListSelect];
            selectedDoor.GetComponent<MNTY_Door>().setWillBeOpenedTrue();
            GameObject go = Instantiate(prefab_FailureTarget);
            //go.transform.SetParent(selectedDoor.transform);
            Vector3 target_pos = selectedDoor.transform.GetChild(1).position;
            go.transform.position = target_pos;
            go.GetComponent<Animator>().SetTrigger("reveal");

            temp_doors.Remove(selectedDoor);
        }

        //foreach (GameObject leftover in temp_doors)
        //{
        //    leftover.GetComponent<MNTY_Door>().setWillBeOpened(false);
        //}

    }

    public void RemoveBandit(GameObject bandit)
    {
        m_opponents.Remove(bandit);
    }

    void Update()
    {
        if (UmpireControl.isGameStarted && !isDrawSignalSent)
        {
            onDrawSignal();
            isDrawSignalSent = true;
        }

        if (m_opponents.Count == 0)
        {
            //m_umpire.isObjectiveComplete = true;
            m_umpire.gameSuccess();
        }
    }
    
    public void onDrawSignal()
    {
        foreach (GameObject door in doorList)
        {
            door.GetComponent<MNTY_Door>().onDrawSignal();
        }
    }

    public void onOpponentShot(GameObject go)
    {
        m_opponents.Remove(go);
    }
}
