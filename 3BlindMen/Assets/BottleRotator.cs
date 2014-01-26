using UnityEngine;
using System.Collections;

public class BottleRotator : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 2, 0), Space.World);
	}
}
