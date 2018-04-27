using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cube : MonoBehaviour {

	// Use this for initialization
	public Material Map;
	public Vector3 startPosition, endPosition;
	public float horizontalSpeed = 10F,verticalSpeed = 10F,RotMargin =1F;
	public float angle, restAngle, prevAngle, RefAngle, OrigAngle;
	public int Quadrant;
	public bool Assigned;
	public float CanvasMargin;
	public GameObject CubePL, OrigCube;
	public GameObject[] GBox, OGbox;
	public Texture[] Faces;
	public Box OrigBox, FinalBox;
	public Quaternion startingRotation;
	public Texture Fake;
	public RandomBox[] QuestionsCube;
	public Button BHelp, BReset, BUnfold, SameDiff, BSame,BDiff;
	public static int change, Test;
	public static bool help; //boolean for the button

	void Start () {
		QuestionsCube = new RandomBox[] {new RandomBox (new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("U", 4, 2),
						new Face ("D", 0, 3), new Face ("R", 0, 4), new Face ("L", 0, 5)), 0),
						new RandomBox (new Box (new Face ("F", 0, 0), new Face ("B", 0, 1), new Face ("U", 4, 2),
						new Face ("D", 0, 3), new Face ("R", 0, 4), new Face ("L", 0, 5)), 0)
		};
		BUnfold.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("UnfoldB");
		BReset.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("ResetB");
		BHelp.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("HelpB");
		BSame.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("SameB");
		BDiff.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("DiffB");
		Test = 0;
		Unfold.Test = true;
		CanvasMargin = (Screen.height
			/Camera.main.orthographicSize)/40;
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
				//	Debug.Log ("Towards-up-left");
				CubePL.GetComponent<Unfold> ().MoveUpLeft ();
			} else {
				if (RefAngle < OrigAngle-RotMargin) {
					//	Debug.Log ("Towards-up-left");
					CubePL.GetComponent<Unfold> ().MoveUpRight ();
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
				} else { //Para testear
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
				}  //*/
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
		Debug.Log ("Rotaciones " + FinalBox.Front.symbol + " " + FinalBox.Front.orientation + " "+ FinalBox.Up.symbol + " " + FinalBox.Up.orientation + " "+ FinalBox.Right.symbol + " " + FinalBox.Right.orientation);
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
		//int change=4; //comprobar metodo 2quarters
		//int change =2; //mirar que pasa con la orientacion
		int repeatFaces = 0, facesWithOri=0;
		Face SideWithChange;
		int[] position = new int[3];
		Face[] FrontSides = new Face[] { FinalBox.Front, FinalBox.Up, FinalBox.Right };
		Face[] FrontSidesChange = new Face[3];
		for (int i = 0; i < FrontSides.Length; i++) {
			if ((FrontSides [i].localization.Equals (0)) || (FrontSides [i].localization.Equals (2)) || (FrontSides [i].localization.Equals (4))) {
				if (!(FrontSides [i].orientation.Equals (4))) {
					facesWithOri++;
					FrontSidesChange [repeatFaces] = FrontSides [i];
					position [i] = FrontSides [i].localization;
					repeatFaces++;
				}

			}
		}
		change = Random.Range (1, 3);
		if ((repeatFaces>1)&&(facesWithOri>1)) { // Only when there are two or more visibles sides from the original cube one symbol can be changed, otherwise if the three symbols are different it has to be the same cube, and also one face needs to have visible orientation
			change = Random.Range (0, 3);
		}
		//int frontface = 0;
			if (repeatFaces.Equals (0)) {
				SideWithChange = FrontSides [0];
			} else {
			int rand = Random.Range (0, repeatFaces - 1);
			//frontface = position [rand];
			SideWithChange = FrontSidesChange [rand];
			}
		string expChange="";

		switch (change){
		case 0:
			expChange = "Change " + Timer.TradLocaton (SideWithChange.localization) + " Symbol";
			Debug.Log ("Change " + SideWithChange.symbol + " Symbol");
			GBox [SideWithChange.localization].GetComponent<Renderer> ().material.mainTexture = Fake;
			SideWithChange.symbol = "X";
			SideWithChange.orientation = 0;
			break;
		case 1:
			expChange = "No changes";
			Debug.Log ("No changes");
			break;
		case 2:
			expChange = "Change Orientation " + Timer.TradLocaton (SideWithChange.localization) + " + 1_quarter ";
			Debug.Log ("Change Orientation " + SideWithChange.symbol + " + 1_quarter ");
			PaintRotate1Q (GBox [SideWithChange.localization].GetComponent<MeshFilter> ().mesh);
			switch (SideWithChange.orientation) {
			case 0:
				SideWithChange.orientation = 1;
				break;
			case 1:
				SideWithChange.orientation = 2;
				break;
			case 2:
				SideWithChange.orientation = 3;
				break;
			case 3:
				SideWithChange.orientation = 0;
				break;
			case 4:
				Debug.Log ("Pasa por 4");
				break;
			}

			break;
		case 3:
			expChange = "Change Orientation " + Timer.TradLocaton (SideWithChange.localization) + " + 3_quarters ";
			Debug.Log ("Change Orientation "+SideWithChange.symbol+" + 3_quarters ");
			PaintRotate3Q (GBox [SideWithChange.localization].GetComponent<MeshFilter> ().mesh);
			switch (SideWithChange.orientation) {
			case 0:
				SideWithChange.orientation = 3;
				break;
			case 1:
				SideWithChange.orientation = 0;
				break;
			case 2:
				SideWithChange.orientation = 1;
				break;
			case 3:
				SideWithChange.orientation = 2;
				break;
			case 4:
				Debug.Log ("Pasa por 4");
				break;
			}
			break;
		case 4:
			expChange = "Change Orientation " + Timer.TradLocaton (SideWithChange.localization) + " + 2_quarters ";
			Debug.Log ("Change Orientation "+SideWithChange.symbol+" + 2_quarters ");
			PaintRotate2Q (GBox [SideWithChange.localization].GetComponent<MeshFilter> ().mesh);
			switch (SideWithChange.orientation) {
			case 0:
				SideWithChange.orientation = 2;
				break;
			case 1:
				SideWithChange.orientation = 3;
				break;
			case 2:
				SideWithChange.orientation = 0;
				break;
			case 3:
				SideWithChange.orientation = 1;
				break;
			case 4:
				Debug.Log ("Pasa por 4");
				break;
			}
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

		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (expChange);
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
		Hide ();
		Timer.end = true;
		if (SameCube.IsSameCube) {
			GameObject.Find ("Time").GetComponent<Timer> ().Animation (true,0);
			Debug.Log ("Correcto Son el mismo cubo ------------");

		} else {
			Timer.TimeLeft = 0;
			GameObject.Find ("Time").GetComponent<Timer> ().Animation (false,2);
			Debug.Log ("Fallo No son el mismo cubo -------------");
		}
	}
	public void Different()
	{
		Hide ();
		Timer.end = true;
		if (SameCube.IsSameCube) {
			Timer.TimeLeft = 0;
			GameObject.Find ("Time").GetComponent<Timer> ().Animation (false,3);
			Debug.Log ("Fallo Son el mismo cubo ------------");
		} else {
			GameObject.Find ("Time").GetComponent<Timer> ().Animation (true,1);
			Debug.Log ("Correcto No son el mismo cubo ------------");
		}
	}
	public void Hide()
	{
		help = true;
		SameDiff.gameObject.SetActive (false);
		BHelp.gameObject.SetActive (false);
		BReset.gameObject.SetActive (false);
		BUnfold.gameObject.SetActive (false);

	}

	public void Restart()
	{
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (Test.ToString ());
		if (LangTest.Comments) {
			BHelp.gameObject.SetActive (true);
		} else {
			BHelp.gameObject.SetActive (false);
		}
		SameDiff.gameObject.SetActive (true);
		GameObject.Find ("CubePl Sin Codigo (L)").GetComponent<Animator> ().SetBool ("Unfold", false);
		OrigBox =new Box(new Face ("Hook", 0,0), new Face ("Skull", 0,1), new Face ("Lifesv", 4,2),
			new Face ("Spy", 0,3), new Face ("Rum", 0,4), new Face ("Bomb", 0,5));

		FinalBox = new Box(new Face ("Hook", 0,0), new Face ("Skull", 0,1), new Face ("Lifesv", 4,2),
			new Face ("Spy", 0,3), new Face ("Rum", 0,4), new Face ("Bomb", 0,5));
		startingRotation = this.gameObject.transform.localRotation;
		help = false;
		BReset.gameObject.SetActive (false);
		BUnfold.gameObject.SetActive (false);
		for (int i = 0; i < GBox.Length; i++) {
			//Box [i].image.TextureWrapMode.Mirror;
			GBox [i].GetComponent<Renderer> ().material.mainTexture = Faces[i];
			OGbox [i].GetComponent<Renderer> ().material.mainTexture = Faces[i];
			GBox [i].GetComponent<Animator> ().SetBool ("Highlight", false);
			OGbox [i].GetComponent<Animator> ().SetBool ("Highlight", false);
			Paint (GBox [i].GetComponent<MeshFilter> ().mesh);
		}
		RandomCube ();
		CubePL.GetComponent<Unfold> ().finalRotation = startingRotation;
		Timer.start = true;
		CubePL.GetComponent<Unfold> ().speedRotation = 10;
		if (Unfold.Fold.Equals (1)) {
			Unfold.restart = true;
			CubePL.GetComponent<Unfold> ().UnfoldBox ();
			CubePL.GetComponent<Unfold> ().Restart ();

		}

		Test++;
	}

	public int TradSymbol(string s)
	{
		switch (s) {
		case "Hook":
			return 0;
			break;
		case "Skull":
			return 1;
			break;
		case "Lifesv":
			return 2;
			break;
		case "Spy":
			return 3;
			break;
		case "Rum":
			return 4;
			break;
		case "Bomb":
			return 5;
			break;
		case "Sword":
			return 6;
			break;
		case "Fish":
			return 7;
			break;
		case "Rose":
			return 8;
			break;
		case "Parrot":
			return 9;
			break;
		case "Barrel":
			return 10;
			break;
		 default:
			return 10;
			break;
		}
	}

}
