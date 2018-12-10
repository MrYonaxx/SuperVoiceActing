using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeux : MonoBehaviour {

	int wait_time = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		wait_time -= 1;
		if (wait_time == 0) {
			GetComponent<SpriteRenderer> ().enabled = true;
		}
		if (wait_time == -4) {
			GetComponent<SpriteRenderer> ().enabled = false;
			wait_time = Random.Range (90, 600);
		}
	}
}
