using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{

    Vector3 m_destination;
    float m_speed;

    void Start()
    {
        m_destination = new Vector3(0.0f, 0.0f, 0.0f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDestination (Vector3 target)
    {
        m_destination = target;   
    }

    public void setSpeed (float speed)
    {
        m_speed = speed;
    }
}
