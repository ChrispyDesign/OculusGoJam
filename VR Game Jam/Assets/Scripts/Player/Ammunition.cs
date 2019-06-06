using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition
{
    private int m_maxAmmo;
    private int m_currentAmmo;

    #region getters

    public int GetMaxAmmo() { return m_maxAmmo; }
    public int GetCurrentAmmo() { return m_currentAmmo; }
    public bool CanFire() { return m_currentAmmo > 0; }

    #endregion

    public Ammunition(int maxAmmo)
    {
        m_maxAmmo = maxAmmo;
        m_currentAmmo = m_maxAmmo;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Fire()
    {
        m_currentAmmo -= 1;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Reload()
    {
        m_currentAmmo = m_maxAmmo;
    }
}
