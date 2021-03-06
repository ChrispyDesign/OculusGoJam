﻿using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private LineRenderer m_lineRenderer;
    [SerializeField] private Transform m_lineRendererAnchor;

    private Vector3 m_direction;
    private static Vector3 m_hitNormal;
    private static Vector3 m_hitPoint;

    private GameObject m_hoveredObject;
    private GameObject m_unhoveredObject;

    #region getters

    public static Vector3 GetHitNormal() { return m_hitNormal; }
    public static Vector3 GetHitPoint() { return m_hitPoint; }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        m_lineRenderer.SetPosition(0, m_lineRendererAnchor.position);
        m_lineRenderer.SetPosition(1, m_direction);
    }

    /// <summary>
    /// function which performs a raycast
    /// </summary>
    /// <param name="origin">origin point of raycast</param>
    /// <param name="direction">direction to perform raycast in</param>
    /// <returns>null if an object was hit, otherwise returns the hit object</returns>
    public GameObject Raycast(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;

        // perform raycast
        if (Physics.Raycast(origin, direction, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject)
            {
                if (m_hoveredObject != hitObject)
                    Hover(hitObject);

                m_hitNormal = hit.normal;
                m_hitPoint = hit.point;
                m_direction = m_hitPoint;
                return hitObject; // object was hit
            }
        }
        else
        {
            m_direction = direction - origin;
        }

        return null; // no object was hit
    }

    /// <summary>
    /// function which performs a raycast
    /// </summary>
    /// <param name="ray">the ray to cast along</param>
    /// <returns>null if an object was hit, otherwise returns the hit object</returns>
    public GameObject Raycast(Ray ray)
    {
        RaycastHit hit;

        // perform raycast
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject)
            {
                if (m_hoveredObject != hitObject)
                    Hover(hitObject);

                m_hitNormal = hit.normal;
                m_hitPoint = hit.point;
                m_direction = m_hitPoint;
                return hitObject; // object was hit
            }
        }
        else
        {
            m_direction = ray.GetPoint(100);
        }

        return null; // no object was hit
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hitObject"></param>
    private void Hover(GameObject hitObject)
    {
        if (m_unhoveredObject != m_hoveredObject)
        {
            m_unhoveredObject = m_hoveredObject; // store unhovered object

            if (m_unhoveredObject != null)
            {
                HolsterZone unhoveredHolster = m_unhoveredObject.GetComponent<HolsterZone>();
                if (unhoveredHolster)
                    unhoveredHolster.OnUnhover();
            }
        }

        m_hoveredObject = hitObject; // store hovered object

        if (m_hoveredObject)
        {
            HolsterZone hoveredHolster = m_hoveredObject.GetComponent<HolsterZone>();
            if (hoveredHolster)
                hoveredHolster.OnHover();
        }
    }
}
