using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (!(GameObject.FindGameObjectWithTag("GUIGameOver").GetComponent("GameOverGUI") as GameOverGUI).GameOver)
        {
            if (other.CompareTag("Wall"))
            {
                (GameObject.FindGameObjectWithTag("GUITimer").GetComponent("TimerGUI") as TimerGUI).remainingTime = -1;
                (GameObject.FindGameObjectWithTag("GUITimer").GetComponent("TimerGUI") as TimerGUI).hitAWall = true;
            }
            if (other.CompareTag("Ending"))
            {
                (GameObject.FindGameObjectWithTag("GUIGameOver").GetComponent("GameOverGUI") as GameOverGUI).ShowVictory();
                (GameObject.FindGameObjectWithTag("Player").GetComponent("PlayerController") as PlayerController).SetLose();
                (GameObject.FindGameObjectWithTag("GUITimer").GetComponent("TimerGUI") as TimerGUI).Stop();
            }
        }
    }
}
