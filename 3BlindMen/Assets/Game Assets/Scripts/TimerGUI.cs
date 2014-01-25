using UnityEngine;
using System.Collections;

public class TimerGUI : MonoBehaviour
{
    float remainingTime;

    void Update()
    {

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
    }

    void Start()
    {
        remainingTime = 60 * 5;
    }

    void OnGUI()
    {
        if ((Mathf.Floor(remainingTime % 60)) > 9)
        {
            GUI.Label(new Rect(10, 10, 100, 20),
                (Mathf.Floor(remainingTime / 60).ToString() + ":" +
                (Mathf.Floor(remainingTime % 60)).ToString()));
        }
        else
        {
            GUI.Label(new Rect(10, 10, 100, 20),
                (Mathf.Floor(remainingTime / 60).ToString() + ":0" +
                (Mathf.Floor(remainingTime % 60)).ToString()));
        }
    }
}
