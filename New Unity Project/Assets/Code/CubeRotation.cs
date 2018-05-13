using UnityEngine;
using System.Collections;

public class CubeRotation : MonoBehaviour {

	public GameObject Cube;
	public float horizontalSpeed=10,verticalSpeed=10;
	public Vector3 startPosition,endPosition;
	public float MasAngleFB,MasAngleRL, MasAngleUD;
	// Use this for initialization
	void Start () {
		MasAngleFB = MasAngleRL = MasAngleUD=0;
	}
	
	// Update is called once per frame
	void Update () {
			foreach (Touch touch in Input.touches) {
				Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100));
				HandleTouch (touch.fingerId, cam, touch.phase);
			}

			// Simulate touch events from mouse events
			if (Input.touchCount == 0) {
				if (Input.GetMouseButtonDown (0)) {
					Vector3 cam =new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100);
					HandleTouch (10, cam, TouchPhase.Began);
				}
				if (Input.GetMouseButton (0)) {
					//Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100));
					Vector3 cam =new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100);
					HandleTouch (10, cam, TouchPhase.Moved);
				}
				if (Input.GetMouseButtonUp (0)) {
					//Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100));
					Vector3 cam =new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100);
					HandleTouch (10, cam, TouchPhase.Ended);
				}
			}
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
		//BReset.GetComponentInChildren<Text>().text= touchPosition.x+" x "+touchPosition.y+ " y ";
		switch (touchPhase) {
		case TouchPhase.Began:
			startPosition = touchPosition;
		//	endPosition = startPosition;
			//	Debug.Log ("Sart"+touchPosition.ToString ());
			break;
		case TouchPhase.Moved:
			float h2 = touchPosition.x - startPosition.x;
			float v2 = touchPosition.y - startPosition.y;
			if (Mathf.Abs (h2) > Mathf.Abs (v2)) {
				float angLeftRight = h2 * 90 / Screen.width;
				Cube.transform.rotation = Quaternion.Euler (Cube.transform.localRotation.x,Cube.transform.localRotation.y-angLeftRight,Cube.transform.localRotation.x);
			} else {
				float angUpDown = v2 * 90 / Screen.height;
				Cube.transform.rotation = Quaternion.Euler (Cube.transform.localRotation.x+angUpDown, Cube.transform.localRotation.y,Cube.transform.localRotation.z);
			}
					//Cube.transform.Rotate (Vector3.up, -h2, Space.World);

					

			/*if (Unfold.Fold.Equals (0) && Unfold.moving.Equals (false) && (help)) {
				if (Mathf.Abs (endPosition.y - touchPosition.y) < Mathf.Abs (endPosition.x - touchPosition.x)) {
							//BReset.GetComponentInChildren<Text>().text= "Heigh" +Screen.currentResolution +" "+(startPosition.x - endPosition.x).ToString();
					CubePL.transform.Rotate (Vector3.up, (endPosition.x - touchPosition.x), Space.World);
						} else {
							//	BReset.GetComponentInChildren<Text>().text= "Heigh" +Screen.height.ToString()+" "+(startPosition.y - endPosition.y).ToString();
					CubePL.transform.Rotate (Vector3.right, -(endPosition.y - touchPosition.y), Space.World);
								}
							}
			endPosition = touchPosition;*/
			break;
		case TouchPhase.Ended:
		//	endPosition = touchPosition;
			//Debug.Log ("End" + touchPosition.ToString ());
			break;
		}
	}
}
