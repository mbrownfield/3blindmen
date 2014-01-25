using UnityEngine;
using System.Collections;

public class TimerGUI : MonoBehaviour
{
    public float remainingTime;
    const int TIME = 5;
    bool tick = false;
    public bool hitAWall = false;

    void Update()
    {
        if (tick)
        {
            remainingTime -= Time.deltaTime;
        }
        if (remainingTime > 0)
        {
            if ((Mathf.Floor(remainingTime % 60)) > 9)
            {
                guiText.text = 
                    (Mathf.Floor(remainingTime / 60).ToString() + ":" +
                    (Mathf.Floor(remainingTime % 60)).ToString());
            }
            else
            {
                guiText.text = 
                    (Mathf.Floor(remainingTime / 60).ToString() + ":0" +
                    (Mathf.Floor(remainingTime % 60)).ToString());
            }
        }
        else
        {
            guiText.text = "";
            (GameObject.FindGameObjectWithTag("GUIGameOver").GetComponent("GameOverGUI") as GameOverGUI).ShowGameOver(hitAWall);
            (GameObject.FindGameObjectWithTag("Player").GetComponent("PlayerController") as PlayerController).SetLose();
        }
        if (remainingTime < -3)
        {
            remainingTime = TIME;
            (GameObject.FindGameObjectWithTag("GUIGameOver").GetComponent("GameOverGUI") as GameOverGUI).Restart();
            (GameObject.FindGameObjectWithTag("Player").GetComponent("PlayerController") as PlayerController).Restart();
        }
    }

    public void Start()
    {
        remainingTime = TIME;
        tick = true;
    }

    public void Stop()
    {
        tick = false;
    }

    internal void HideTimer()
    {
        guiText.text = "";
    }
}
