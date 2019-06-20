using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScene : MonoBehaviour
{
    public string m_switchGroup;
    public string m_switchSetting;

    public AK.Wwise.Event   m_soundEvent;

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetSwitch(m_switchGroup, m_switchSetting, gameObject);
        m_soundEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
