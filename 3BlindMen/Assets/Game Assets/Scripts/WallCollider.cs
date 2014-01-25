using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("COLLIDING");
        if (other.CompareTag("Wall"))
        {
            Debug.LogWarning("COLLIDING WALL");
        }
    }
}
