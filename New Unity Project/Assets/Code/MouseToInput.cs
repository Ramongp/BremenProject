using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseToInput : MonoBehaviour {

	public GameObject Cube;
	public float horizontalSpeed = 2.0F;
	public float verticalSpeed = 2.0F;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Handle native touch events
		foreach (Touch touch in Input.touches) {
			HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
		}

		// Simulate touch events from mouse events
		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown(0) ) {
				HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
			}
			if (Input.GetMouseButton(0) ) {
				HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
			}
			if (Input.GetMouseButtonUp(0) ) {
				HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
			}
		}
	}
	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
		switch (touchPhase) {
		case TouchPhase.Began:
			// TODO
			break;
		case TouchPhase.Moved:
			float h2 = horizontalSpeed  * Input.GetAxis("Mouse X")*Mathf.Deg2Rad;
			float v2 = verticalSpeed* Input.GetAxis("Mouse Y")*Mathf.Deg2Rad;
			//cube.transform.Rotate(v, h, 0);
			Cube.transform.RotateAround(Vector3.up,-h2);
			Cube.transform.RotateAround(Vector3.right,+v2);
			break;
		case TouchPhase.Ended:
			// TODO
			break;
		}
	}
}
