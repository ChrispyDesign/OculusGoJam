﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupRotator : MonoBehaviour
{
    [Header("Cup Creation")]
    [SerializeField] private GameObject m_cupPrefab;
    [SerializeField] private int m_cupCount;
    [SerializeField] private Transform m_cupAnchorPoint;
    [SerializeField] private Vector2 m_cupSpacing;

    [Header("Cup Reveal")]
    [SerializeField] private float m_cupRevealTime = 3;

    [Header("Cup Rotation")]
    [SerializeField] private float m_rotateTime = 1;
    [SerializeField] private float m_rotateFrequency;
    [SerializeField] private float m_pauseTime;
    [SerializeField] private Vector2 m_pauseOnRotationRange = new Vector2(5, 10);
    private int m_currentNumberOfRotations = 0;
    private int m_pauseOnRotation;

    [Header("Umpire")]
    [SerializeField] private UmpireControl m_umpire;

    private List<GameObject> m_cups = new List<GameObject>();
    private List<GameObject> m_availableCups = new List<GameObject>();
    private GameObject m_desiredCup;
    private bool m_isRotating = false;
    private bool m_isGameStarted = false;

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        for (int i = 0; i < m_cupCount; i++)
        {
            GameObject cup = Instantiate(m_cupPrefab, transform);
            m_cups.Add(cup);
            m_availableCups.Add(m_cups[i]);
            cup.GetComponent<CupInteractable>().SetUmpire(m_umpire);

            Vector3 position = m_cupAnchorPoint.position;
            position.z += m_cupSpacing.y * (i - (m_cupCount / 2));
            position.x += m_cupSpacing.x * (i - (m_cupCount / 2));
            cup.transform.position = position;
        }

        SetDesiredCup();
        ShowDesiredCup();
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (UmpireControl.isPlayerReady && !m_isGameStarted)
        {
            HideDesiredCup();
            StartCoroutine(StartRotation());
            m_isGameStarted = true;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetDesiredCup()
    {
        m_desiredCup = m_cups[Random.Range(0, m_cups.Count)];
        CupInteractable cup = m_desiredCup.GetComponent<CupInteractable>();
    }

    /// <summary>
    /// 
    /// </summary>
    private void ShowDesiredCup()
    {
        CupInteractable cup = m_desiredCup.GetComponent<CupInteractable>();

        cup.SetAsDesiredCup();
        cup.Reveal();
    }

    /// <summary>
    /// 
    /// </summary>
    private void HideDesiredCup()
    {
        CupInteractable cup = m_desiredCup.GetComponent<CupInteractable>();
        cup.Hide();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartRotation()
    {
        GameObject cup1 = GetAvailableCup();
        GameObject cup2 = GetAvailableCup();

        if (cup1 != null && cup2 != null)
        {
            StartCoroutine(RotateCups(cup1, cup2));
            m_currentNumberOfRotations++;
            m_pauseOnRotation = Random.Range((int)m_pauseOnRotationRange.x, (int)m_pauseOnRotationRange.y);
        }

        if (m_currentNumberOfRotations == m_pauseOnRotation)
        {
            m_currentNumberOfRotations = 0;
            m_pauseOnRotation = Random.Range((int)m_pauseOnRotationRange.x, (int)m_pauseOnRotationRange.y);
            yield return new WaitForSeconds(m_rotateFrequency + m_pauseTime);
        }
        else
            yield return new WaitForSeconds(m_rotateFrequency);

        if (!UmpireControl.isGameStarted)
            StartCoroutine(StartRotation());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cup1"></param>
    /// <param name="cup2"></param>
    /// <returns></returns>
    private IEnumerator RotateCups(GameObject cup1, GameObject cup2)
    {
        Vector3 midPoint = (cup1.transform.position + cup2.transform.position) * 0.5f;

        GameObject parent = new GameObject();
        parent.transform.SetParent(transform);
        parent.transform.position += midPoint;
        cup1.transform.SetParent(parent.transform);
        cup2.transform.SetParent(parent.transform);
        
        float timer = 0;
        while (timer <= m_rotateTime)
        {
            timer += Time.deltaTime;

            parent.transform.eulerAngles = new Vector3(0, Mathf.Lerp(0, 180, timer / m_rotateTime), 0);
        
            yield return null;
        }

        cup1.transform.SetParent(transform);
        cup2.transform.SetParent(transform);
        Destroy(parent);

        ReturnCupToPool(cup1);
        ReturnCupToPool(cup2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private GameObject GetAvailableCup()
    {
        if (m_availableCups.Count <= 0)
            return null;

        GameObject randomCup = m_availableCups[Random.Range(0, m_availableCups.Count)];
        m_availableCups.Remove(randomCup);
        return randomCup;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cup"></param>
    private void ReturnCupToPool(GameObject cup)
    {
        m_availableCups.Add(cup);
    }
}
