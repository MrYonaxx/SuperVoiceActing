using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovingDecor : MonoBehaviour {

	float original_view_x;
	float original_view_y;

	float actual_view_x;
	float actual_view_y;

	[SerializeField]
	float speed = 2;
	[SerializeField]
	float amplitude = 20;

	// Use this for initialization
	void Start () {
		original_view_x = this.transform.position.x;
		original_view_y = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		var tmp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		actual_view_x = 0;
		actual_view_y = 0;
		print (actual_view_y);

		actual_view_x += (tmp.x);
		actual_view_x /= amplitude;
		actual_view_x += original_view_x;

		actual_view_y += (tmp.y);
		actual_view_y /= amplitude;
		actual_view_y += original_view_y;
		transform.position -= new Vector3 ((transform.position.x - actual_view_x) * Time.deltaTime * speed, 
											(transform.position.y - actual_view_y) * Time.deltaTime * speed, 
											0);
	}
}
