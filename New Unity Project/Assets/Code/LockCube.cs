using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockCube : MonoBehaviour {

	public Vector3 startPosition, endPosition;
	public float horizontalSpeed = 10F,verticalSpeed = 10F,RotMargin =1F;
	public float angle, restAngle, prevAngle, RefAngle, OrigAngle;
	public int Quadrant;
	public bool Assigned;
	public float CanvasMargin;
	public GameObject CubePL;
	public GameObject[] GBox;
	public Texture[] Faces;
	public Box OrigBox;
	public string move;
	public static string MoveNeeded;
	public Quaternion startingRotation;
	//public Button BHelp, BReset, BUnfold;
	public static bool help; //boolean for the button
	public bool passed;
	// Use this for initialization
	public Image[] Arrows;
	public Sprite[] Movs;
	public int moveCont, AxX,AxY,AxZ;
	public static int Test;
	void Start () {
		Test = 0;
		Unfold.AfterRandom = CubePL.transform.localRotation;
		Set ();
	}

	// Update is called once per frame
	void Update () {
		// Handle native touch events
		if ((Input.touchCount.Equals (2)) && help) {
			HandleTouch2 (Input.GetTouch (0), Input.GetTouch (1));
		} else {
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
				float v2 = verticalSpeed * Input.GetAxis ("Mouse Y");
				CubePL.transform.Rotate (Vector3.up, -h2, Space.World);
				CubePL.transform.Rotate (Vector3.right, +v2, Space.World);
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
				CubePL.GetComponent<Unfold> ().MoveUpLeft ();
				AxZ++;
				if (AxZ.Equals (2)) {
					OrigBox.MoveUpLeft ();
					if (moveCont.Equals (3)){
						HideArrows ();
					}
					Arrows [moveCont].gameObject.SetActive (true);
					Arrows [moveCont].sprite = Movs [5];
					AxZ = 0;
					moveCont++;
				}
			} else {
				if (RefAngle < OrigAngle-RotMargin) {
					CubePL.GetComponent<Unfold> ().MoveUpRight ();
					AxZ--;
					if (AxZ.Equals (-2)) {
						OrigBox.MoveUpRight ();
						if (moveCont.Equals (3)){
							HideArrows ();
						}
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [4];
						AxZ = 0;
						moveCont++;
					}
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
			Vector3 diff = p2.position - p1.position;
			angle = Mathf.Atan2 (diff.y, diff.x);
			Quadrant = 0;
			RefAngle = (Mathf.Rad2Deg * angle);
			OrigAngle = RefAngle;
			prevAngle = OrigAngle;
		}

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

	void PaintRotate1Q (Mesh m) { //orientacion igual a tres-cuartos

		Mesh mesh = m;
		Vector2[] UVs = new Vector2[mesh.vertices.Length];



		// Bottom del cubo que hace de cara (plane) //Estan al reves para hacer mirror de la textura
		UVs[15] = new Vector2(0.0f, 1.0f); //top right
		UVs[14] = new Vector2(1.0f,1.0f);//btm right
		UVs[12] = new Vector2(0.0f, 0.0f);	//btm left
		UVs[13] = new Vector2(1.0f, 0.0f);//top left


		// Top del cubo que hace de cara (plane) 
		UVs[5] = new Vector2(0.0f, 1.0f); //top l
		//UVs[4] = new Vector2(1.0f, 1.0f);//top r
		UVs[9] = new Vector2(1.0f, 1.0f);//top r
		UVs[4] = new Vector2(0.0f, 0.0f);//btm l
		UVs[8] = new Vector2(1.0f,0.0f);//btm r

		mesh.uv = UVs;
	}
	public void Compare()
	{
		if ((Mathf.Abs (startPosition.y - endPosition.y) > CanvasMargin) || (Mathf.Abs (startPosition.x - endPosition.x) > CanvasMargin)) {
			if (Mathf.Abs (startPosition.y - endPosition.y) < CanvasMargin) {
				if (startPosition.x > endPosition.x) {
					CubePL.GetComponent<Unfold> ().MoveLeft ();
					AxY--;
					if (AxY.Equals (-2)) {
						OrigBox.MoveLeft();
						if (moveCont.Equals (3)){
							HideArrows ();
						}
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [3];
						AxY = 0;
						moveCont++;
					}
					//Debug.Log ("Izquierda");
				} else {
					CubePL.GetComponent<Unfold> ().MoveRight ();
					AxY++;
					if (AxY.Equals (2)) {
						OrigBox.MoveRight ();
						if (moveCont.Equals (3)){
							HideArrows ();
						}
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [2];
						AxY = 0;
						moveCont++;
					}


				}
			} else {
				if (Mathf.Abs (startPosition.x - endPosition.x) < CanvasMargin) {
					if (startPosition.y > endPosition.y) {
						CubePL.GetComponent<Unfold> ().MoveDown ();
						AxX++;
						if (AxX.Equals (2)) {
							OrigBox.MoveDown ();
							if (moveCont.Equals (3)){
								HideArrows ();
							}
							Arrows [moveCont].gameObject.SetActive (true);
							Arrows [moveCont].sprite = Movs [1];
							AxX = 0;
							moveCont++;
						}
					}

					else {
						CubePL.GetComponent<Unfold> ().MoveUp ();
						AxX--;
						if (AxX.Equals (-2)) {
							OrigBox.MoveUp ();
							if (moveCont.Equals (3)){
								HideArrows ();
							}
							Arrows [moveCont].gameObject.SetActive (true);
							Arrows [moveCont].sprite = Movs [0];
							AxX = 0;
							moveCont++;
						}
						//Debug.Log ("Arriba");
					}
				}/* else { //Para testear
					if (startPosition.x > endPosition.x) {
						if (startPosition.y > endPosition.y) {
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
							AxZ++;
							if (AxZ.Equals (2)) {
								OrigBox.MoveUpLeft ();
								if (moveCont.Equals (3)){
									HideArrows ();
								}
								Arrows [moveCont].gameObject.SetActive (true);
								Arrows [moveCont].sprite = Movs [5];
								AxZ = 0;
								moveCont++;
							}


						} else {
							//	Debug.Log ("Towards-up-left");
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
							AxZ++;
							if (AxZ.Equals (2)) {
								OrigBox.MoveUpLeft ();
								if (moveCont.Equals (3)){
									HideArrows ();
								}
								Arrows [moveCont].gameObject.SetActive (true);
								Arrows [moveCont].sprite = Movs [5];
								AxZ = 0;
								moveCont++;
							}
						}
					}else {
						if (startPosition.y > endPosition.y) {
							CubePL.GetComponent<Unfold> ().MoveUpRight ();
							AxZ--;
							if (AxZ.Equals (-2)) {
								OrigBox.MoveUpRight ();
								if (moveCont.Equals (3)){
									HideArrows ();
								}
								Arrows [moveCont].gameObject.SetActive (true);
								Arrows [moveCont].sprite = Movs [4];
								AxZ = 0;
								moveCont++;
							}

						} else {
							CubePL.GetComponent<Unfold> ().MoveUpRight ();
							AxZ--;
							if (AxZ.Equals (-2)) {
								OrigBox.MoveUpRight ();
								if (moveCont.Equals (3)){
									HideArrows ();
								}
								Arrows [moveCont].gameObject.SetActive (true);
								Arrows [moveCont].sprite = Movs [4];
								AxZ = 0;
								moveCont++;
							}


						}
					}
				} //*/
			}
		} 
		else {
			CubePL.GetComponent<Unfold> ().SetToStart();
		}
		if ((OrigBox.Front.symbol.Equals ("Lock")) && (OrigBox.Front.orientation.Equals (0))&& !passed) {
			passed = true;
				Debug.Log ("Movimiento Logrado");
				GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().Pass ();
		} 
	}
	void CreateTest(){
		switch (Test) {
		case 0:
			OrigBox =new Box(new Face ("F", 0,0), new Face ("B", 0,1), new Face ("Lock", 0,2),
				new Face ("D", 0,3), new Face ("R", 0,4), new Face ("L", 0,5));
			GBox [2].GetComponent<Renderer> ().material.mainTexture = Faces[1];
			Paint (GBox [2].GetComponent<MeshFilter> ().mesh);
			GBox [1].GetComponent<Renderer> ().material.mainTexture = Faces[2];
			Paint (GBox [1].GetComponent<MeshFilter> ().mesh);
			break;
		case 1:
			OrigBox =new Box(new Face ("F", 0,0), new Face ("B", 0,1), new Face ("U", 0,2),
				new Face ("D", 0,3), new Face ("Lock", 1,4), new Face ("L", 0,5));
			GBox [4].GetComponent<Renderer> ().material.mainTexture = Faces[1];
			PaintRotate1Q (GBox [4].GetComponent<MeshFilter> ().mesh);
			GBox [1].GetComponent<Renderer> ().material.mainTexture = Faces[4];
			Paint (GBox [1].GetComponent<MeshFilter> ().mesh);
			break;
		case 2:
			OrigBox =new Box(new Face ("F", 0,0), new Face ("B", 0,1), new Face ("Lock", 2,2),
				new Face ("D", 0,3), new Face ("R", 0,4), new Face ("L", 0,5));
			GBox [2].GetComponent<Renderer> ().material.mainTexture = Faces[1];
			PaintRotate2Q (GBox [2].GetComponent<MeshFilter> ().mesh);
			GBox [1].GetComponent<Renderer> ().material.mainTexture = Faces[2];
			Paint  (GBox [1].GetComponent<MeshFilter> ().mesh);
			break;
		}
		Test++;
	}


	public void ShowWay ()
	{
		LockCube.help = false;
		CubePL.GetComponent<Unfold> ().CreateWay (false);
		string tempmov = move;
		string[] listmov = tempmov.Split ('_');
		for (int i = 0; i < listmov.Length-1; i++) {
			Debug.Log (listmov.Length.ToString() + " " + i.ToString());
			Arrows [i].gameObject.SetActive (true);
			Arrows [i].sprite = Movs [MovetoInt (listmov [i])];
		}
		GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().GiveUp ();
	}
	public int  MovetoInt(string move)
	{
		switch (move) {
		case "Up":
			return 0;

		case "Down":
			return 1;

		case "Left":
			return 3;

		case "Right":
			return 2;

		case "Toward-up-right":
			return 4;

		case "Toward-up-left":
			return 5;

		default:
			return 0;
		}
	}

	public void HideArrows ()
	{
		moveCont = 0;
		Arrows [0].gameObject.SetActive (false);
		Arrows [1].gameObject.SetActive (false);
		Arrows [2].gameObject.SetActive (false);
	}

	public	void ResetLock()
		{
		HideArrows();
		CubePL.GetComponent<Unfold> ().SetToAfterRandom ();
		AxX = 0;AxY=0;AxZ = 0;
		Test--;
		CreateTest ();
		}



	public void Set()
	{
		if (!Points.exchange) {
			passed = false;
			HideArrows ();
			moveCont = 0;
			CubePL.transform.localRotation = Unfold.AfterRandom;
			horizontalSpeed = 0.5F;
			verticalSpeed = 0.5F;
			Unfold.Test = false;
			for (int i = 0; i < Faces.Length; i++) {
				//Box [i].image.TextureWrapMode.Mirror;
				GBox [i].GetComponent<Renderer> ().material.mainTexture = Faces [i];
				Paint (GBox [i].GetComponent<MeshFilter> ().mesh);
			}
			startingRotation = this.gameObject.transform.localRotation;
			CanvasMargin = (Screen.height
				/ Camera.main.orthographicSize) / 40;
			CreateTest ();
			help = true;
			move = GameObject.Find ("Main Camera").GetComponent<SameCube> ().LockTest (OrigBox.Front, OrigBox.Up, OrigBox.Right);
			Debug.Log (move);
			GameObject.Find ("Reward").GetComponent<Points> ().Set ();
			GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().Set ();
			CubePL.GetComponent<Unfold> ().finalRotation = Unfold.AfterRandom;
			CubePL.GetComponent<Unfold> ().SetToAfterRandom ();

		}
	}
}