using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject m_featherParticlePrefab;
    private Vector3 m_currentPosition;

    Vector3 m_destination;

    float m_speed;
    public float speed_min;
    public float speed_max;
    [Range(0, 1)]
    public float wanderAmt;
    public float wanderRadius;
    public float headingDist;

    Vector3 m_currentVelocity;

    Bird_Umpire brd_ump;


    void Awake()
    {
        m_destination = new Vector3(0.0f, 0.0f, 0.0f);
        m_speed = Random.Range(speed_min, speed_max);
        brd_ump = GameObject.Find("GameUmpire").GetComponent<Bird_Umpire>();
    }
    void Update()
    {
        m_currentVelocity += getTotalSteeringForce() * Time.deltaTime;
        transform.position += m_currentVelocity * Time.deltaTime;

        if (System.Math.Abs(transform.position.z - m_destination.z) < 0.5f)
        {
            brd_ump.onBirdSurvive();
        }

        m_currentPosition = transform.position;
    }

    private void OnDestroy()
    {
        GameObject featherParticle = Instantiate(m_featherParticlePrefab);
        featherParticle.transform.position = m_currentPosition;
    }

    private Vector3 getTotalSteeringForce()
    {
        Vector3 total_force = getSeekSteerForce() + (getWanderSteerForce() * wanderAmt);
        Vector3.Normalize(total_force);
        total_force *= m_speed;
        total_force.x = 0;
        return total_force;
    }

    private Vector3 getWanderSteerForce()
    {
        Vector3 target = new Vector3(0.0f, 0.0f, 0.0f);
        target.z = Random.Range(0.0f, 2.0f);
        target.y = Random.Range(0.0f, 2.0f);

        Vector3.Normalize(target);
        target *= wanderRadius;

        target += m_currentVelocity * headingDist;

        return calculateSeekForce(target);
    }

    private Vector3 getSeekSteerForce()
    {
        return calculateSeekForce(m_destination);
    }

    private Vector3 calculateSeekForce(Vector3 dest)
    {
        Vector3 seekVec = dest - transform.position;
        Vector3.Normalize(seekVec);
        seekVec *= m_speed;
        Vector3 seekForce = seekVec - m_currentVelocity;
        seekForce.x = 0;
        return seekForce;
    }

    public void setDestination(Vector3 target)
    {
        m_destination = target;
    }
}

