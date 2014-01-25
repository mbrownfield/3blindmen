using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightingFollower : MonoBehaviour {
    public GameObject light;
	// Update is called once per frame
	void Update () {
            light.transform.position = transform.position;
	}
}
