using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNTY_Door : MonoBehaviour
{
    public bool willBeOpened;
    public bool isOpening;

    Vector3 start_rot =  new Vector3(0.0f,0.0f,0.0f);
    Vector3 end_rot = new Vector3(0.0f, 120.0f, 0.0f);

    float timer;
    public float doorOpenTime;

    // Start is called before the first frame update
    void Awake()
    {
        willBeOpened = false;
        isOpening = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpening)
        {
            timer += Time.deltaTime;
            float percentDone = timer / doorOpenTime;
            Quaternion current_rot = Quaternion.Euler(Vector3.Lerp(start_rot, end_rot, percentDone));
            transform.rotation = current_rot;
            if (percentDone > 0.999f)
            {
                isOpening = false;
                timer = 0.0f;
            }
        }
    }

    public void setWillBeOpenedTrue()
    {
        willBeOpened = true;
    }

    public void onDrawSignal()
    {
        if (willBeOpened) { isOpening = true; /*runDoorOpen();*/ }
    }
}
