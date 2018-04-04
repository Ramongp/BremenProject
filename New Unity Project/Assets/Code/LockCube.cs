using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockCube : MonoBehaviour {

	public Vector3 startPosition, endPosition;
	public float horizontalSpeed = 10F;
	public float verticalSpeed = 10F;
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
	// Use this for initialization
	public Image[] Arrows;
	public Sprite[] Movs;
	public int moveCont, AxX,AxY,AxZ, Test;
	void Start () {
		Test = 2;
		moveCont = 0;
		Arrows [0].gameObject.SetActive (false);
		Arrows [1].gameObject.SetActive (false);
		Arrows [2].gameObject.SetActive (false);
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
		CreateTest (Test);
		help = true;
		move=GameObject.Find("Main Camera").GetComponent<SameCube> ().LockTest (OrigBox.Front, OrigBox.Up, OrigBox.Right);
		Debug.Log (move);
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
				if (Mathf.Abs (startPosition.x - endPosition.x) < CanvasMargin) {
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
							Arrows [moveCont].gameObject.SetActive (true);
							Arrows [moveCont].sprite = Movs [0];
							AxX = 0;
							moveCont++;
						}
						//Debug.Log ("Arriba");
					}
				} else {
					if (startPosition.x > endPosition.x) {
						if (startPosition.y > endPosition.y) {
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
							//	Debug.Log ("Towards-up-left");
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
							OrigBox.MoveUpLeft ();
							AxZ++;
							if (AxZ.Equals (2)) {
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
								Arrows [moveCont].gameObject.SetActive (true);
								Arrows [moveCont].sprite = Movs [4];
								AxZ = 0;
								moveCont++;
							}


						}
					}
				}
			}
		} 
		else {
			CubePL.GetComponent<Unfold> ().SetToStart();
		}

		if((OrigBox.Front.symbol.Equals("Lock"))&&(OrigBox.Front.orientation.Equals(0)))
			{
			Debug.Log ("Movimiento Logrado");
		}

	}
	void CreateTest(int test){
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
	}


	public void ShowWay ()
	{
		string tempmov = move;
		string[] listmov = tempmov.Split (',');
		for (int i = 0; i < listmov.Length-1; i++) {
			Debug.Log (listmov.Length.ToString() + " " + i.ToString());
			Arrows [i].gameObject.SetActive (true);
			Arrows [i].sprite = Movs [MovetoInt (listmov [i])];
		}
	}
	public int  MovetoInt(string move)
	{
		switch (move) {
		case "Up":
			return 0;
			break;
		case "Down":
			return 1;
			break;
		case "Left":
			return 3;
			break;
		case "Right":
			return 2;
			break;
		case "Toward-up-right":
			return 4;
			break;
		case "Toward-up-left":
			return 5;
			break;
		default:
			return 0;
			break;
		}
	}
}