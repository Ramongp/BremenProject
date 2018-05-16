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
	public static bool help, CanUnfold; //boolean for the button, Bolean if the ches can be unfold
	public bool passed, WaitUnfold;
	// Use this for initialization
	public Image[] Arrows;
	public Sprite[] Movs;
	public int moveCont, AxX,AxY,AxZ;
	public int[] CurrentSides;
	public int LockSide, LockOrientation;
	public static int Test;
	public static bool NoTime, WrongPath;
	void Start () {
		CurrentSides = new int[6];
		Test = 0;
		Unfold.AfterRandom = CubePL.transform.localRotation;
		//Set ();
		SetPreLock();
	}

	// Update is called once per frame
	void Update () {

		if (WaitUnfold && Unfold.moving.Equals (false)) { 
			WaitUnfold = false;
			help = true;
		}

		// Handle native touch events

		if ((Input.touchCount.Equals (2)) && help) {
			HandleTouch2 (Input.GetTouch (0), Input.GetTouch (1));
		} else {
			foreach (Touch touch in Input.touches) {
				Vector3 cam = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100);
				HandleTouch (touch.fingerId, cam, touch.phase);

			}

			// Simulate touch events from mouse events
			if (Input.touchCount == 0) {
				if (Input.GetMouseButtonDown (0)) {
					Vector3 cam = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100);
					HandleTouch (10, cam, TouchPhase.Began);
				}
				if (Input.GetMouseButton (0)) {
					Vector3 cam = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100);
					HandleTouch (10, cam, TouchPhase.Moved);
				}
				if (Input.GetMouseButtonUp (0)) {
					Vector3 cam = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 100);
					HandleTouch (10, cam, TouchPhase.Ended);
				}
			}
		}
			
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
		if(Unfold.moving.Equals(false)){
			bool firstMove=false;
			if (CanUnfold) {  //Si es el primer movimiento permite rotar el cubo en el espacio del boton
				firstMove = true;
			}
			switch (touchPhase) {
			case TouchPhase.Began:
					startPosition = touchPosition;
					endPosition = startPosition;
				break;
			case TouchPhase.Moved:
				if (help) {
					float h2 = horizontalSpeed * Input.GetAxis ("Mouse X");
					float v2 = verticalSpeed * Input.GetAxis ("Mouse Y");
					if (Mathf.Abs( h2 )> Mathf.Abs( v2)) {
						CubePL.transform.Rotate (Vector3.up, -h2, Space.World);
					} else {
						CubePL.transform.Rotate (Vector3.right, +v2, Space.World);
					}
					if ((Mathf.Abs (startPosition.y - touchPosition.y) > CanvasMargin) || (Mathf.Abs (startPosition.x - touchPosition.x) > CanvasMargin)) {
						CanUnfold = false; //Va a hacer un move
					} else {
						if (firstMove) {
							CanUnfold = true;  //No hará un move
						}
					}

				}
				break;
			case TouchPhase.Ended:
				endPosition = touchPosition;
				if (Unfold.Fold.Equals (0) && (help)) {
					Compare ();
				}
				break;
			}
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
		CanUnfold = false; //Ha hecho un move
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
				RefAngle = angle + Quadrant;
				CubePL.transform.Rotate (Vector3.forward, (Mathf.Deg2Rad* restAngle)/Time.deltaTime, Space.World);

			}
			return;
		}
		if((p1.phase.Equals(TouchPhase.Ended))||(p2.phase.Equals(TouchPhase.Ended))){
			Assigned = false;
			if (RefAngle > OrigAngle) {
				CubePL.GetComponent<Unfold> ().MoveUpLeft90 ();
					OrigBox.MoveUpLeft ();
					Arrows [moveCont].gameObject.SetActive (true);
					Arrows [moveCont].sprite = Movs [5];
					moveCont++;
			} else {
					CubePL.GetComponent<Unfold> ().MoveUpRight90 ();
						OrigBox.MoveUpRight ();
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [4];
						moveCont++;
			}
			if ((OrigBox.Front.symbol.Equals ("Lock")) && (OrigBox.Front.orientation.Equals (0)) && !passed) {
				passed = true;
				Debug.Log ("Movimiento Logrado");
				GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().Pass ();
			} 
			else {
				if (moveCont.Equals (3)) {
					GameObject.Find ("LockTimeCode").GetComponent<LockTimer>().NeedToReset(OrigBox.Front.symbol.Equals ("Lock"));
				}
			}
			return;
		}
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
	void PaintRotate3Q (Mesh m) { //orientacion igual a tres-cuartos

		Mesh mesh = m;
		Vector2[] UVs = new Vector2[mesh.vertices.Length];



		// Bottom del cubo que hace de cara (plane) //Estan al reves para hacer mirror de la textura
		UVs[13] = new Vector2(0.0f, 1.0f); //top right
		UVs[12] = new Vector2(1.0f,1.0f);//btm right
		UVs[14] = new Vector2(0.0f, 0.0f);	//btm left
		UVs[15] = new Vector2(1.0f, 0.0f);//top left


		// Top del cubo que hace de cara (plane) 
		UVs[8] = new Vector2(0.0f, 1.0f); //top l
		//UVs[4] = new Vector2(1.0f, 1.0f);//top r
		UVs[4] = new Vector2(1.0f, 1.0f);//top r
		UVs[9] = new Vector2(0.0f, 0.0f);//btm l
		UVs[5] = new Vector2(1.0f,0.0f);//btm r

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
				CanUnfold = false; //Ha hecho un move
				if (Mathf.Abs (startPosition.y - endPosition.y) < Mathf.Abs (startPosition.x - endPosition.x)) {
					if (startPosition.x > endPosition.x) {
						CubePL.GetComponent<Unfold> ().MoveLeft90 ();
						OrigBox.MoveLeft ();
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [3];
						moveCont++;
					} else {
						CubePL.GetComponent<Unfold> ().MoveRight90 ();
						OrigBox.MoveRight ();
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [2];
						moveCont++;
					}
				} else {
					if (startPosition.y > endPosition.y) {
						CubePL.GetComponent<Unfold> ().MoveDown90 ();
						OrigBox.MoveDown ();
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [1];
						moveCont++;
					} else {
						CubePL.GetComponent<Unfold> ().MoveUp90 ();
						OrigBox.MoveUp ();
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [0];
						moveCont++;
					}
						
				}
				if ((OrigBox.Front.symbol.Equals ("Lock")) && (OrigBox.Front.orientation.Equals (0)) && !passed) {
					passed = true;
					Debug.Log ("Movimiento Logrado");
					GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().Pass ();
				} else {
					if (moveCont.Equals (3)) {
						GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().NeedToReset (OrigBox.Front.symbol.Equals ("Lock"));
					}
				}
			} else {
				CubePL.GetComponent<Unfold> ().SetToStart ();
			}
	}
	void CreateTest(){
		switch (Test) {
		case 0:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("Lock", 0, 2),
				new Face ("D", 0, 3), new Face ("R", 0, 4), new Face ("L", 0, 5));
			CurrentSides = new int[]{ 1, 2, 12, 4, 5, 6 };
			LockSide = 2;
			LockOrientation = 0;
			move = "Down_";
			break;
		case 1:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("U", 0, 2),
				new Face ("D", 0, 3), new Face ("Lock", 1, 4), new Face ("L", 0, 5));
			CurrentSides = new int[]{ 7, 8, 9, 4, 12, 5 };
			LockSide = 4;
			LockOrientation = 1;
			move = "Down_Left_";
			break;
		case 2:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("Lock", 2, 2),
				new Face ("D", 0, 3), new Face ("R", 0, 4), new Face ("L", 0, 5));
			CurrentSides = new int[]{ 1, 3, 12, 4, 8, 7 };
			LockSide = 2;
			LockOrientation = 2;
			move = "Left_Left_Down_";
			break;
		case 3:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("U", 0, 2),
				new Face ("D", 0, 3), new Face ("R", 0, 4), new Face ("Lock", 0, 5));
			CurrentSides = new int[]{ 1, 2, 9, 11, 3,12  };
			LockSide = 5;
			LockOrientation = 0;
			move = "Right_";
			break;
		case 4:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("Lock", 0, 1), new Face ("U", 0, 2),
			new Face ("D", 0, 3), new Face ("R", 0, 4), new Face ("L", 0, 5));
			CurrentSides = new int[]{ 7, 12, 8, 4, 5, 6 };
			LockSide = 1;
			LockOrientation = 0;
			move = "Left_Left_";
			break;
		case 5:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("U", 0, 2),
				new Face ("Lock", 0, 3), new Face ("R", 0, 4), new Face ("L", 0, 5));
			CurrentSides = new int[]{ 5, 10, 8, 12, 3, 6 };
			LockSide = 3;
			LockOrientation = 0;
			move = "Up_";
			break;
		case 6:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("Lock", 1, 1), new Face ("U", 0, 2),
				new Face ("D", 0, 3), new Face ("R", 0, 4), new Face ("L", 0, 5));
			CurrentSides = new int[]{ 9, 12, 8, 1, 3, 6 };
			LockSide = 1;
			LockOrientation = 1;
			move = "Left_Left_Toward-up-left_";
			break;
		case 7:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("U", 0, 2),
				new Face ("D", 0, 3), new Face ("R", 0, 4), new Face ("Lock", 2, 5));
			CurrentSides = new int[]{ 5, 10, 8, 6, 7, 12 };
			LockSide = 5;
			LockOrientation = 2;
			move = "Right_Toward-up-left_Toward-up-left_";
			break;
		case 8:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("U", 0, 2),
				new Face ("Lock", 2, 3), new Face ("R", 0, 4), new Face ("L", 0, 5));
			CurrentSides = new int[]{ 2, 5, 4, 12, 3, 1 };
			LockSide = 3;
			LockOrientation = 2;
			move = "Up_Toward-up-left_Toward-up-left_";
			break;
		case 9:
			OrigBox = new Box (new Face ("F", 0, 0), new Face ("Lock", 2, 1), new Face ("U", 0, 2),
				new Face ("L", 2, 3), new Face ("R", 0, 4), new Face ("L", 0, 5));
			CurrentSides = new int[]{ 4, 12, 1, 8, 10, 6 };
			LockSide = 1;
			LockOrientation = 2;
			move = "Dwon_Down_";
			break;

		}
		for (int i = 0; i < GBox.Length; i++) {
			GBox [i].GetComponent<Renderer> ().material.mainTexture = Faces [CurrentSides[i]];
			Paint (GBox [i].GetComponent<MeshFilter> ().mesh);
		}
		switch (LockOrientation) {
		case 1:
			PaintRotate1Q (GBox [LockSide].GetComponent<MeshFilter> ().mesh);
			break;
		case 2:
			PaintRotate2Q (GBox [LockSide].GetComponent<MeshFilter> ().mesh);
			break;
		case 3:
			PaintRotate3Q (GBox [LockSide].GetComponent<MeshFilter> ().mesh);
			break;
		}

		Test++;
	}


	public void ShowWay (bool Pulsado)
	{
		if(help || NoTime || WrongPath){
		HideArrows ();
		CanUnfold = false;
		LockCube.help = false;
		CubePL.GetComponent<Unfold> ().CreateBonusWay (move);
		string tempmov = move;
		string[] listmov = tempmov.Split ('_');
		for (int i = 0; i < listmov.Length-1; i++) {
			//Debug.Log (listmov.Length.ToString() + " " + i.ToString());
			Arrows [i].gameObject.SetActive (true);
			Arrows [i].sprite = Movs [MovetoInt (listmov [i])];
		}
		if (Pulsado) {
			GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().GiveUp ();
		}
	}
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
		if (help || WrongPath) {
			GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().Message.gameObject.SetActive (false);
			CanUnfold = true;
			WrongPath = false;
			help = true;
			HideArrows ();
			CubePL.GetComponent<Unfold> ().SetToAfterRandom ();
			AxX = 0;
			AxY = 0;
			AxZ = 0;
			Test--;
			CreateTest ();
		}
		}



	public void Set()
	{
			WrongPath=false;
			NoTime = false;
			CanUnfold = true;
			passed = false;
			HideArrows ();
			moveCont = 0;
			CubePL.transform.localRotation = Unfold.AfterRandom;
			horizontalSpeed = 0.5F;
			verticalSpeed = 0.5F;
			Unfold.Test = false;
			startingRotation = this.gameObject.transform.localRotation;
			CanvasMargin = (Screen.height/20);
			CreateTest ();
			//Debug.Log (move);
			GameObject.Find ("LockTimeCode").GetComponent<LockTimer> ().Set ();
			CubePL.GetComponent<Unfold> ().finalRotation = Unfold.AfterRandom;
			CubePL.GetComponent<Unfold> ().SetToAfterRandom ();
			help = true;
	}

	public void SetPreLock() //Colocar cosas para explicar test
	{
		passed = false;
		HideArrows ();
		moveCont = 0;
		CubePL.transform.localRotation = Unfold.AfterRandom;
		horizontalSpeed = 0.5F;
		verticalSpeed = 0.5F;
		Unfold.Test = false;
		CurrentSides = new int[] {12,0,1,3,6,10};
		for (int i = 0; i < GBox.Length; i++) {
			//Box [i].image.TextureWrapMode.Mirror;
			GBox [i].GetComponent<Renderer> ().material.mainTexture = Faces [CurrentSides[i]];
			Paint (GBox [i].GetComponent<MeshFilter> ().mesh);
		}
		startingRotation = this.gameObject.transform.localRotation;
		//Debug.Log (move);
		CubePL.GetComponent<Unfold> ().finalRotation = Unfold.AfterRandom;
		//CubePL.GetComponent<Unfold> ().SetToAfterRandom ();
	}
		

	public void UnfoldButton()
	{
		if (CanUnfold && Unfold.moving.Equals(false)) {
			Unfold.MaxScale = 0.25F;
			Unfold.MinScale = 0.18F;
			Unfold.UnfoldPosition = new Vector3 (-3, -2.5F, 10);
			CubePL.GetComponent<Unfold> ().UnfoldBox ();
			if (Unfold.Fold.Equals(1)) {
				help=false;
			}
			else{
				WaitUnfold = true;
			}
		
		}

	}



						//Antigua version Compare
						/*if (startPosition.x > endPosition.x) {
					CubePL.GetComponent<Unfold> ().MoveLeft ();
					AxY--;
					if (AxY.Equals (-2)) {
						OrigBox.MoveLeft();
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
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [2];
						AxY = 0;
						moveCont++;
					}


				}
			} else {
					if (startPosition.y > endPosition.y) {
						CubePL.GetComponent<Unfold> ().MoveDown ();
						AxX++;
						if (AxX.Equals (2)) {
							OrigBox.MoveDown ();
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
					}
						
			}

				//Antigua version Handle Touch
				if (RefAngle > OrigAngle+RotMargin) {
				CubePL.GetComponent<Unfold> ().MoveUpLeft ();
				AxZ++;
				if (AxZ.Equals (2)) {
					OrigBox.MoveUpLeft ();
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
						/*if (moveCont.Equals (3)){
							HideArrows ();
						}
						Arrows [moveCont].gameObject.SetActive (true);
						Arrows [moveCont].sprite = Movs [4];
						AxZ = 0;
						moveCont++;
						}
						}
						}
						*/
}