using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cube : MonoBehaviour {

	// Use this for initialization
	public Material Map;
	public Vector3 startPosition, endPosition;
	public float horizontalSpeed = 0.01F;
	public float verticalSpeed = 0.01F;
	public float CanvasMargin;
	public GameObject CubePL, OrigCube;
	public GameObject[] GBox;
	public Texture[] Faces;
	public Box OrigBox, FinalBox;
	public Quaternion startingRotation;
	public Texture Fake;
	public Button BHelp, BReset, BUnfold;
	public static bool help; //boolean for the button

	void Start () {
		Unfold.Test = true;
		CanvasMargin = (Screen.height
			/Camera.main.orthographicSize)/20;
		Restart ();

		//CubeFake.transform.rotation = Quaternion.Euler(90 * Random.Range (0, 3), 90 * Random.Range (0, 3), 90 * Random.Range (0, 3));
		//CubeFake.transform.rotation = Quaternion.Euler(-15, 30	,-10)*Quaternion.Euler(90 * Random.Range (0, 3), 90 * Random.Range (0, 3), 90 * Random.Range (0, 3));
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetMouseButton(0)){
			MovePL ();
		}*/
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
					//Debug.Log (CanvasMargin.ToString ());
					Compare ();
			}
			break;
		}
	}
			

	void Paint (Mesh m) { //Pintar antiguo cubo

		Mesh mesh = m;
		Vector2[] UVs = new Vector2[mesh.vertices.Length];

		/*// Front del cubo que hace de cara
		UVs[0] = new Vector2(0.0f, 0.0f);
		UVs[1] = new Vector2(1.0f, 0.0f);
		UVs[2] = new Vector2(0.0f, 1.0f);
		UVs[3] = new Vector2(1.0f,1.0f);*/

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
		/*// Back
		UVs[6]  = new Vector2(0.0f, 0.0f);
		UVs[7]  = new Vector2(1.0f, 0.0f);
		UVs[10] = new Vector2(0.0f, 1.0f);
		UVs[11] = new Vector2(0.0f,0.0f);*/

		/*// Left
		UVs[16] = new Vector2(1.0f, 0.0f);
		UVs[17] = new Vector2(1.0f, 0.0f);
		UVs[18] = new Vector2(1.0f, 0.0f);
		UVs[19] = new Vector2(1.0f,0.0f);
		// Right        
		UVs[20] = new Vector2(1.0f, 1.0f);
		UVs[21] = new Vector2(0.0f, 1.0f);
		UVs[22] = new Vector2(1.0f, 0.0f);
		UVs[23] = new Vector2(0.0f,0.0f);*/
		mesh.uv = UVs;
	}

		public void Compare()
		{
		if ((Mathf.Abs (startPosition.y - endPosition.y) > CanvasMargin) || (Mathf.Abs (startPosition.x - endPosition.x) > CanvasMargin)) {
			if (Mathf.Abs (startPosition.y - endPosition.y) < CanvasMargin) {
				if (startPosition.x > endPosition.x) {
					CubePL.GetComponent<Unfold> ().MoveLeft ();
					//Debug.Log ("Izquierda");
				} else {
					CubePL.GetComponent<Unfold> ().MoveRight ();
					//Debug.Log ("Derecha");
				}
			} else {
				if (Mathf.Abs (startPosition.x - endPosition.x) < CanvasMargin) {
					if (startPosition.y > endPosition.y) {
						CubePL.GetComponent<Unfold> ().MoveDown ();
						//Debug.Log ("Abajo");
					} else {
						CubePL.GetComponent<Unfold> ().MoveUp ();
						//Debug.Log ("Arriba");
					}
				} else {
					if (startPosition.x > endPosition.x) {
						if (startPosition.y > endPosition.y) {
						//	Debug.Log ("Towards-up-left");
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
						} else {
						//	Debug.Log ("Towards-up-left");
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
						}
					} else {
						if (startPosition.y > endPosition.y) {
						//	Debug.Log ("Towards-up-right");
							CubePL.GetComponent<Unfold> ().MoveUpRight ();
						} else {
						//	Debug.Log ("Towards-up-left");
							CubePL.GetComponent<Unfold> ().MoveUpRight ();
						}
					}
				}
			}
		} 
		else {
			CubePL.GetComponent<Unfold> ().SetToStart();
		}
			
		}

	void RandomCube()
	{
		//b.Front = new Face ("X", 0, 0);
		//GBox [0].GetComponent<Renderer> ().material.mainTexture = Fake;

		//Debug.Log ("Orig " + b.Status());
		//int i = Random.Range (1, 4);
		string Pasos = "Pasos ";
		while(RandomSure()){

		Pasos = "Pasos ";
		int i = Random.Range (1, 4);
		for (int e = 0; e < i; e++) {
			int x=0, y=0, z=0;
			int u = Random.Range (0, 5);
			switch (u) {
			case 0:
				Pasos+= "Up ";
				FinalBox.MoveUp ();
				x += 90;
				break;
			case 1:
				Pasos+="Down ";
				FinalBox.MoveDown ();
				x -= 90;
				break;
			case 2:
				Pasos+="Right ";
				FinalBox.MoveRight ();
				y -= 90;
				break;
			case 3:
				Pasos+="Left ";
				FinalBox.MoveLeft ();
				y += 90;
				break;
			case 4:
				Pasos+="Toward-up-Right ";
				FinalBox.MoveUpRight ();
				z -= 90;
				break;
			case 5:
				Pasos+="Toward-up-left ";
				FinalBox.MoveUpLeft ();
				z += 90;
				break;
			}

		//Elegir movs R=3
		/*int x =90,y=90,z=0;
		Pasos+= "Up ";
		b.MoveUp ();
		Pasos+="Left ";
		b.MoveLeft ();*/

			CubePL.transform.localRotation = Quaternion.Euler (x, y, z) * startingRotation;
				startingRotation=CubePL.transform.localRotation;
			//Debug.Log("Transform " + g.transform.rotation.ToString());
			}
		}
		RandomChange ();
		Debug.Log ("Rotaciones" + FinalBox.Front.symbol + " " + FinalBox.Front.orientation + " "+ FinalBox.Up.symbol + " " + FinalBox.Up.orientation + " "+ FinalBox.Right.symbol + " " + FinalBox.Right.orientation);
		Debug.Log(RandomSure().ToString());
		FinalBox.Front.localization = 0;FinalBox.Up.localization = 2;FinalBox.Right.localization = 4;
		Unfold.AfterRandom = CubePL.transform.localRotation;
		Debug.Log (Pasos);
		Debug.Log ("Final " + FinalBox.Status());
		this.GetComponent<SameCube> ().Compare (OrigBox.Front, OrigBox.Up, OrigBox.Right, FinalBox.Front, FinalBox.Up, FinalBox.Right);
	}

	public void Sethelp()
	{
		if (!help) {
			help = true;
			CubePL.GetComponent<Unfold> ().WriteHelp ();
			BHelp.gameObject.SetActive (false);
			BReset.gameObject.SetActive (true);
			BUnfold.gameObject.SetActive (true);
		}
	}

	bool RandomSure()
	{
		return ((OrigBox.Front.localization.Equals(FinalBox.Front.localization) && (OrigBox.Right.localization.Equals (FinalBox.Right.localization)) && (OrigBox.Up.localization.Equals (FinalBox.Up.localization)))
			&&(OrigBox.Front.orientation.Equals(FinalBox.Front.orientation) && (OrigBox.Right.orientation.Equals (FinalBox.Right.orientation)) && (OrigBox.Up.orientation.Equals (FinalBox.Up.orientation))));
	}

	void RandomChange()
	{
		//Escoger la cara que recibe el cambio
		int change = Random.Range (0, 3);
		//int change=4; //comprobar metodo 2quarters
		//int change =2; //mirar que pasa con la orientacion
		int repeatFaces = 0;
		Face SideWithChange;
		int[] position = new int[3];
		Face[] FrontSides = new Face[] { FinalBox.Front, FinalBox.Up, FinalBox.Right };
		Face[] FrontSidesChange = new Face[3];
		for (int i = 0; i < FrontSides.Length; i++) {
			if ((FrontSides [i].localization.Equals (0)) || (FrontSides [i].localization.Equals (2)) || (FrontSides [i].localization.Equals (4))) {
				FrontSidesChange [repeatFaces] = FrontSides [i];
				position [i] = FrontSides [i].localization;
				repeatFaces++;
			}
		}
		int frontface = 0;
			if (repeatFaces.Equals (0)) {
				SideWithChange = FrontSides [0];
			} else {
			int rand = Random.Range (0, repeatFaces - 1);
			frontface = position [rand];
			SideWithChange = FrontSidesChange [rand];
			}


		switch (change){	
		case 0:
			Debug.Log ("No changes");
			break;
		case 1:
			Debug.Log ("Change "+SideWithChange.symbol+" Symbol");
			GBox [SideWithChange.localization].GetComponent<Renderer> ().material.mainTexture = Fake;
			SideWithChange.symbol="X";
			break;
		case 2:
			Debug.Log ("Change Orientation "+ SideWithChange.symbol+" to 1_quarter ");
			PaintRotate1Q (GBox [SideWithChange.localization].GetComponent<MeshFilter> ().mesh);
			SideWithChange.orientation = 1;
			break;
		case 3:
			Debug.Log ("Change Orientation "+SideWithChange.symbol+" to 3_quarters ");
			PaintRotate3Q (GBox [SideWithChange.localization].GetComponent<MeshFilter> ().mesh);
			SideWithChange.orientation = 3;
			break;
		case 4:
			Debug.Log ("Change Orientation "+SideWithChange.symbol+" to 2_quarters ");
			PaintRotate2Q (GBox [SideWithChange.localization].GetComponent<MeshFilter> ().mesh);
			SideWithChange.orientation = 2;
			break;
		}
			
		if (FinalBox.Front.localization.Equals (SideWithChange.localization)) {
			FinalBox.Front = SideWithChange;
		}
		if (FinalBox.Up.localization.Equals (SideWithChange.localization)) {
			FinalBox.Up = SideWithChange;
		}
		if (FinalBox.Right.localization.Equals (SideWithChange.localization)) {
			FinalBox.Right = SideWithChange;
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

	public void Same()
	{
		Timer.end = true;
		if (SameCube.IsSameCube) {
			GameObject.Find ("Time").GetComponent<Timer> ().Animation (true,0);
			Debug.Log ("Correcto Son el mismo cubo ------------");

		} else {
			GameObject.Find ("Time").GetComponent<Timer> ().Animation (false,2);
			Debug.Log ("Fallo No son el mismo cubo -------------");
		}
	}
	public void Different()
	{
		Timer.end = true;
		if (SameCube.IsSameCube) {
			GameObject.Find ("Time").GetComponent<Timer> ().Animation (false,3);
			Debug.Log ("Fallo Son el mismo cubo ------------");
		} else {
			GameObject.Find ("Time").GetComponent<Timer> ().Animation (true,1);
			Debug.Log ("Correcto No son el mismo cubo ------------");
		}
	}

	public void Restart()
	{
		OrigBox =new Box(new Face ("F", 0,0), new Face ("B", 0,1), new Face ("U", 0,2),
			new Face ("D", 0,3), new Face ("R", 0,4), new Face ("L", 0,5));

		FinalBox =new Box(new Face ("F", 0,0), new Face ("B", 0,1), new Face ("U", 0,2),
			new Face ("D", 0,3), new Face ("R", 0,4), new Face ("L", 0,5));
		startingRotation = this.gameObject.transform.localRotation;
		help = false;
		BReset.gameObject.SetActive (false);
		BUnfold.gameObject.SetActive (false);
		BHelp.gameObject.SetActive (true);
		for (int i = 0; i < Faces.Length; i++) {
			//Box [i].image.TextureWrapMode.Mirror;
			GBox [i].GetComponent<Renderer> ().material.mainTexture = Faces[i];
			Paint (GBox [i].GetComponent<MeshFilter> ().mesh);
		}
		RandomCube ();
		Timer.start = true;
	}

	public void ShowFace(GameObject g)
	{
		
	}
}
