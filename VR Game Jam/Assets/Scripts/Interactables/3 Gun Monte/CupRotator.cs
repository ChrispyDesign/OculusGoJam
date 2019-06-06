using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupRotator : MonoBehaviour
{
    [SerializeField] private GameObject m_cupPrefab;
    [SerializeField] private int m_cupCount;
    [SerializeField] private Transform m_cupAnchorPoint;
    [SerializeField] private Vector2 m_cupSpacing;

    [SerializeField] private float m_rotateSpeed;
    [SerializeField] private float m_rotateFrequency;

    private List<GameObject> m_cups = new List<GameObject>();
    private bool m_isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_cupCount; i++)
        {
            GameObject cup = Instantiate(m_cupPrefab, transform);
            m_cups.Add(cup);

            Vector3 position = m_cupAnchorPoint.position;
            position.z += m_cupSpacing.y * (i - (m_cupCount / 2));
            position.x += m_cupSpacing.x * (i - (m_cupCount / 2));
            cup.transform.position = position;
        }

        StartCoroutine(StartRotation());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator StartRotation()
    {
        StartCoroutine(Rotate());

        yield return new WaitForSeconds(m_rotateFrequency);

        StartCoroutine(StartRotation());
    }

    private IEnumerator Rotate()
    {
        GameObject randomCup1 = m_cups[Random.Range(0, m_cups.Count)];
        GameObject randomCup2 = m_cups[Random.Range(0, m_cups.Count)];

        while (randomCup2 == randomCup1)
            randomCup2 = m_cups[Random.Range(0, m_cups.Count)];

        Vector3 midPoint = (randomCup1.transform.position + randomCup2.transform.position) * 0.5f;
        midPoint.z = m_cupAnchorPoint.position.z;

        float timer = 0;
        float rotateTime = m_rotateSpeed * Time.deltaTime * 180;
        m_isRotating = true;

        while (timer <= rotateTime)
        {
            rotateTime = m_rotateSpeed * Time.deltaTime * 180;
            
            randomCup1.transform.RotateAround(midPoint, Vector3.up, m_rotateSpeed * Time.deltaTime);
            randomCup2.transform.RotateAround(midPoint, Vector3.up, m_rotateSpeed * Time.deltaTime);

            timer += Time.deltaTime;

            yield return null;
        }

        m_isRotating = false;
    }
}
