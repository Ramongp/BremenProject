using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrainingCube : MonoBehaviour {

	public Vector3 startPosition, endPosition, OrigPos;
	public float horizontalSpeed = 10F,verticalSpeed = 10F, OrigAngle,angle;
	public float CanvasMargin, AxisX, AxisY, RotMargin =1F;
	public GameObject CubePL;
	public GameObject[] GBox;
	public Texture[] Faces;
	public Box OrigBox;
	public static string MoveNeeded;
	public int Quadrant;
	public Quaternion startingRotation;
	//public Button BHelp, BReset, BUnfold;
	public static bool help, Assigned; //boolean for the button
	//Rotation testing
	public static float Tangle,Zangle, AxisRot,prevAngle, restAngle,RefAngle; //AxisRot 0=z,1=x,2=y
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
		if ((Input.touchCount.Equals (2))&& help) {
			HandleTouch2 (Input.GetTouch (0), Input.GetTouch (1));
				} 
		else {
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
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {

		switch (touchPhase) {
		case TouchPhase.Began:
			startPosition = touchPosition;
			//	Debug.Log ("Sart"+touchPosition.ToString ());
			break;
		case TouchPhase.Moved:
			if (help) {
				float h2 = horizontalSpeed * Input.GetAxis ("Mouse X");
				float v2 = verticalSpeed * Input.GetAxis ("Mouse Y") ;
				//CubePL.transform.RotateAround (Vector3.up, -h2);
				CubePL.transform.Rotate (Vector3.up, -h2, Space.World);
				CubePL.transform.Rotate (Vector3.right, +v2, Space.World);
			}
			break;
		case TouchPhase.Ended:
			Assigned = false;
			endPosition = touchPosition;
			//Debug.Log ("End" + touchPosition.ToString ());
			if (Unfold.Fold.Equals (0) && Unfold.moving.Equals (false) && (help)) {
				Compare ();
			}
			break;
		}
	}

	private void HandleTouch2(Touch p1, Touch p2) {

		if (((p1.phase.Equals (TouchPhase.Moved)) && (p2.phase.Equals (TouchPhase.Moved)))
			||((p1.phase.Equals (TouchPhase.Stationary)) && (p2.phase.Equals (TouchPhase.Moved)))
			||((p1.phase.Equals (TouchPhase.Moved)) && (p2.phase.Equals (TouchPhase.Stationary)))){
			if (help) {
				Vector3 diff3 = p2.position - p1.position;
				angle = Mathf.Atan2 (diff3.y, diff3.x);
				angle = (Mathf.Rad2Deg * angle);
				if ((prevAngle>0)&&(angle < 0)){
					if (prevAngle > 90) {
						Quadrant += 360;
					}
					prevAngle*=-1;
				}
				if ((prevAngle<0)&&(angle > 0)){
					if (prevAngle < -90) {
						Quadrant -= 360;
					}
					prevAngle*=-1;
				}

				restAngle = angle - prevAngle;
				prevAngle= angle;
				//angle = (Mathf.Rad2Deg * angle);
				//Tangle = angle;
				RefAngle = angle + Quadrant;
			//	CubePL.transform.rotation= Quaternion.Euler (CubePL.transform.rotation.x, CubePL.transform.rotation.y, CubePL.transform.rotation.z+angle);
				//CubePL.transform.RotateAround (Vector3.forward, angle*Mathf.Deg2Rad*Time.deltaTime);
				CubePL.transform.Rotate (Vector3.forward, (Mathf.Deg2Rad* restAngle)/Time.deltaTime, Space.World);
			
			}
			return;
		}
		if((p1.phase.Equals(TouchPhase.Ended))||(p2.phase.Equals(TouchPhase.Ended))){
			Assigned = false;

			if (RefAngle > OrigAngle+RotMargin) {
				CubePL.GetComponent<Unfold> ().MoveUpLeft();
				if (MoveNeeded.Equals ("TowardUpLeft")) {
					Training.currentOrder++;
					Unfold.AfterRandom = CubePL.GetComponent<Unfold> ().finalRotation;
				} else {
					Training.currentOrder--;

				}	
			} else {
				if (RefAngle < OrigAngle-RotMargin) {
				CubePL.GetComponent<Unfold> ().MoveUpRight();
				Training.currentOrder--;
				}
			}
			return;
		}


			/*if (p1.position.y < p2.position.y) {
				Touch temp = p1;
				p1 = p2;
				p2 = temp;
			}*/

		//Conseguir angulo de referencia 
		if (!Assigned) {
			Assigned = true;
			OrigPos = p1.position;
			Vector3 diff = p2.position - p1.position;
			angle = Mathf.Atan2 (diff.y, diff.x);
			Zangle = CubePL.transform.rotation.z;
			Quadrant = 0;
			RefAngle = (Mathf.Rad2Deg * angle);
			OrigAngle = RefAngle;
			prevAngle = OrigAngle;
			Tangle = OrigAngle;
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
				} /*else {
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
				}*/
			}
		} 
		else {
			CubePL.GetComponent<Unfold> ().SetToStart();
		}

	}

}
