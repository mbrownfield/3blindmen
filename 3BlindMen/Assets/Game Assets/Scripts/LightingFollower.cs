using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightingFollower : MonoBehaviour {
    public GameObject lights;
	// Update is called once per frame
	void Update () {
            lights.transform.position = transform.position;
	}
}
