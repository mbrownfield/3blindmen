using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour {
    public GameObject XY, XZ, YZ;

	void Update() {
        XY.transform.position = transform.position + new Vector3(0, 0, -1);
        XZ.transform.position = transform.position + new Vector3(0, -1, 0);
        YZ.transform.position = transform.position + new Vector3(-1, 0, 0);
	}
}
