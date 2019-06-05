using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseCheck : MonoBehaviour
{

    UmpireControl umpire;

    // Start is called before the first frame update
    void Start()
    {
        umpire = GameObject.Find("GameUmpire").GetComponent<UmpireControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            umpire.onReadyPressed();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            umpire.onShootPressed();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            umpire.onResetPressed();
        }
    }
}
