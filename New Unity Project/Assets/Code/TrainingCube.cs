using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrainingCube : MonoBehaviour {

	public Vector3 startPosition, endPosition;
	public float horizontalSpeed = 10F;
	public float verticalSpeed = 10F;
	public float CanvasMargin;
	public GameObject CubePL;
	public GameObject[] GBox;
	public Texture[] Faces;
	public Box OrigBox;
	public static string MoveNeeded;
	public Quaternion startingRotation;
	//public Button BHelp, BReset, BUnfold;
	public static bool help; //boolean for the button
	// Use this for initialization
	void Start () {
		Unfold.AfterRandom=CubePL.transform.localRotation;
		horizontalSpeed = 0.5F;
		verticalSpeed = 0.5F;
		Unfold.Test = false;
		for (int i = 0; i < Faces.Length; i++) {
			//Box [i].image.TextureWrapMode.Mirror;
			GBox [i].GetComponent<Renderer> ().material.mainTexture = Faces[i];
			Paint (GBox [i].GetComponent<MeshFilter> ().mesh);
		}
		startingRotation = this.gameObject.transform.localRotation;
		CanvasMargin = (Screen.height
			/Camera.main.orthographicSize)/20;
	}
	
	// Update is called once per frame
	void Update () {
		// Handle native touch events
		foreach (Touch touch in Input.touches) {
			Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100));
			HandleTouch (touch.fingerId, cam, touch.phase);
		}

		// Simulate touch events from mouse events
		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown (0)) {
				Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100));
				HandleTouch (10, cam, TouchPhase.Began);
			}
			if (Input.GetMouseButton (0)) {
				Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100));
				HandleTouch (10, cam, TouchPhase.Moved);
			}
			if (Input.GetMouseButtonUp (0)) {
				Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100));
				HandleTouch (10, cam, TouchPhase.Ended);
			}
		}
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {

		switch (touchPhase) {
		case TouchPhase.Began:
			startPosition = touchPosition;
			//	Debug.Log ("Sart"+touchPosition.ToString ());
			break;
		case TouchPhase.Moved:
			if (help) {
				float h2 = horizontalSpeed * Input.GetAxis ("Mouse X") * Mathf.Deg2Rad;
				float v2 = verticalSpeed * Input.GetAxis ("Mouse Y") * Mathf.Deg2Rad;
				CubePL.transform.RotateAround (Vector3.up, -h2);
				CubePL.transform.RotateAround (Vector3.right, +v2);
			}
			break;
		case TouchPhase.Ended:
			endPosition = touchPosition;
			//Debug.Log ("End" + touchPosition.ToString ());
			if (Unfold.Fold.Equals (0) && Unfold.moving.Equals (false) && (help)) {
				Compare ();
			}
			break;
		}
	}
	void Paint (Mesh m) { //Pintar antiguo cubo

		Mesh mesh = m;
		Vector2[] UVs = new Vector2[mesh.vertices.Length];


		// Bottom del cubo que hace de cara (plane) //Estan al reves para hacer mirror de la textura
		UVs[12] = new Vector2(0.0f, 1.0f); //top right
		UVs[13] = new Vector2(0.0f,0.0f);//btm right
		UVs[14] = new Vector2(1.0f, 0.0f);	//btm left
		UVs[15] = new Vector2(1.0f, 1.0f);//top left


		// Top del cubo que hace de cara (plane)
		UVs[4] = new Vector2(0.0f, 1.0f); //top l
		UVs[5] = new Vector2(1.0f, 1.0f);//top r
		UVs[8] = new Vector2(0.0f, 0.0f);//btm l
		UVs[9] = new Vector2(1.0f,0.0f);//btm r

		mesh.uv = UVs;
	}

	void PaintRotate2Q (Mesh m) { //orientacion igual a tres-cuartos

		Mesh mesh = m;
		Vector2[] UVs = new Vector2[mesh.vertices.Length];



		// Bottom del cubo que hace de cara (plane) //Estan al reves para hacer mirror de la textura
		UVs[14] = new Vector2(0.0f, 1.0f); //top right
		UVs[15] = new Vector2(0.3f,1.0f);//btm right
		UVs[13] = new Vector2(0.0f, 0.0f);	//btm left
		UVs[12] = new Vector2(1.0f, 0.0f);//top left


		// Top del cubo que hace de cara (plane) 
		UVs[9] = new Vector2(0.0f, 1.0f); //top l
		//UVs[4] = new Vector2(1.0f, 1.0f);//top r
		UVs[8] = new Vector2(1.0f, 1.0f);//top r
		UVs[5] = new Vector2(0.0f, 0.0f);//btm l
		UVs[4] = new Vector2(1.0f,0.0f);//btm r

		mesh.uv = UVs;
	}
	public void Compare()
	{
		if ((Mathf.Abs (startPosition.y - endPosition.y) > CanvasMargin) || (Mathf.Abs (startPosition.x - endPosition.x) > CanvasMargin)) {
			if (Mathf.Abs (startPosition.y - endPosition.y) < CanvasMargin) {
				if (startPosition.x > endPosition.x) {
					CubePL.GetComponent<Unfold> ().MoveLeft ();
					Training.currentOrder--;

					//Debug.Log ("Izquierda");
				} else {
					CubePL.GetComponent<Unfold> ().MoveRight ();
					if (MoveNeeded.Equals ("Right")) {
							Training.currentOrder++;
							Unfold.AfterRandom = CubePL.GetComponent<Unfold>().finalRotation;
					}
						else{
						Training.currentOrder--;

						}

				
				}
			} else {
				if (Mathf.Abs (startPosition.x - endPosition.x) < CanvasMargin) {
					if (startPosition.y > endPosition.y) {
						CubePL.GetComponent<Unfold> ().MoveDown ();
						if (MoveNeeded.Equals ("Down")) {
								GBox [1].GetComponent<Animator> ().SetBool ("Training", true);
								Training.currentOrder++;
								Unfold.AfterRandom = CubePL.GetComponent<Unfold>().finalRotation;
						}
								else{
							

							Training.currentOrder--;
								}

						}

					else {
					CubePL.GetComponent<Unfold> ().MoveUp ();
					Training.currentOrder--;
						//Debug.Log ("Arriba");
					}
				} else {
					if (startPosition.x > endPosition.x) {
						if (startPosition.y > endPosition.y) {
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
							if (MoveNeeded.Equals ("TowardUpLeft")) {
								Training.currentOrder++;
								Unfold.AfterRandom = CubePL.GetComponent<Unfold> ().finalRotation;
							} else {
								Training.currentOrder--;

							}


						} else {
							//	Debug.Log ("Towards-up-left");
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
							if (MoveNeeded.Equals ("TowardUpLeft")) {
								Training.currentOrder++;
								Unfold.AfterRandom = CubePL.GetComponent<Unfold> ().finalRotation;
							} else {
								Training.currentOrder--;

							}
						}
					}else {
						if (startPosition.y > endPosition.y) {
							CubePL.GetComponent<Unfold> ().MoveUpRight ();
							if (MoveNeeded.Equals ("TowardUpRight")) {
								Training.currentOrder++;
								Unfold.AfterRandom = CubePL.GetComponent<Unfold> ().finalRotation;
							} else {
								Training.currentOrder--;

							}

						} else {
							CubePL.GetComponent<Unfold> ().MoveUpRight ();
							if (MoveNeeded.Equals ("TowardUpRight")) {
								Training.currentOrder++;
								Unfold.AfterRandom = CubePL.GetComponent<Unfold> ().finalRotation;
							} else {
								Training.currentOrder--;

							}

						
						}
					}
				}
			}
		} 
		else {
			CubePL.GetComponent<Unfold> ().SetToStart();
		}

	}

}
