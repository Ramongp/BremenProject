using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	// Use this for initialization
	public Material Map;
	public Vector3 startPosition, endPosition;
	public float horizontalSpeed = 0.01F;
	public float verticalSpeed = 0.01F;
	public float CanvasMargin;
	public GameObject CubePL, CubeFake;
	public GameObject[] GBox;
	public Texture[] Faces;
	public Box OrigBox, FinalBox;
	public Quaternion startingRotation;

	void Start () {
		startingRotation = this.gameObject.transform.localRotation;
		CanvasMargin = (Screen.height
			/Camera.main.orthographicSize)/20;
		OrigBox =new Box(new Face ("F", 0,0), new Face ("B", 0,1), new Face ("U", 0,2),
			new Face ("D", 0,3), new Face ("R", 0,4), new Face ("L", 0,5));
		FinalBox = new Box(new Face ("F", 0,0), new Face ("B", 0,1), new Face ("U", 0,2),
			new Face ("D", 0,3), new Face ("R", 0,4), new Face ("L", 0,5));
		

		for (int i = 0; i < Faces.Length; i++) {
			//Box [i].image.TextureWrapMode.Mirror;
			GBox [i].GetComponent<Renderer> ().material.mainTexture = Faces[i];
			Paint (GBox [i].GetComponent<MeshFilter> ().mesh);
		}
		RandomCube (CubeFake, FinalBox);
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
			Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,100));
			HandleTouch(touch.fingerId,cam, touch.phase);
			}

			// Simulate touch events from mouse events
			if (Input.touchCount == 0) {
				if (Input.GetMouseButtonDown(0) ) {
				Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,100));
				HandleTouch(10, cam, TouchPhase.Began);
				}
				if (Input.GetMouseButton(0) ) {
				Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,100));
				HandleTouch(10, cam, TouchPhase.Moved);
				}
				if (Input.GetMouseButtonUp(0) ) {
				Vector3 cam = Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x,Input.mousePosition.y,100));
				HandleTouch(10, cam, TouchPhase.Ended);
				}
			}
		}

		private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) {
			switch (touchPhase) {
		case TouchPhase.Began:
			startPosition = touchPosition;
			Debug.Log ("Sart"+touchPosition.ToString ());
				break;
		case TouchPhase.Moved:
			//Debug.Log ("Move");
			float h2 = horizontalSpeed * Input.GetAxis ("Mouse X") * Mathf.Deg2Rad;
			float v2 = verticalSpeed * Input.GetAxis ("Mouse Y") * Mathf.Deg2Rad;
			//cube.transform.Rotate(v, h, 0);
			CubePL.transform.RotateAround (Vector3.up, -h2);
			CubePL.transform.RotateAround (Vector3.right, +v2);
			break;
		case TouchPhase.Ended:
			endPosition = touchPosition;
			Debug.Log ("End" + touchPosition.ToString ());
			if(Unfold.Fold.Equals(0)&&Unfold.moving.Equals(false)){
				Debug.Log (CanvasMargin.ToString ());
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
					Debug.Log ("Izquierda");
				} else {
					CubePL.GetComponent<Unfold> ().MoveRight ();
					Debug.Log ("Derecha");
				}
			} else {
				if (Mathf.Abs (startPosition.x - endPosition.x) < CanvasMargin) {
					if (startPosition.y > endPosition.y) {
						CubePL.GetComponent<Unfold> ().MoveDown ();
						Debug.Log ("Abajo");
					} else {
						CubePL.GetComponent<Unfold> ().MoveUp ();
						Debug.Log ("Arriba");
					}
				} else {
					if (startPosition.x > endPosition.x) {
						if (startPosition.y > endPosition.y) {
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
						} else {
							CubePL.GetComponent<Unfold> ().MoveUpLeft ();
						}
					} else {
						if (startPosition.y > endPosition.y) {
							CubePL.GetComponent<Unfold> ().MoveUpRight ();
						} else {
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

	void RandomCube(GameObject g, Box b)
	{
		
		Debug.Log ("Orig " + b.Status());
		Debug.Log ("Pasos ");
		int i = Random.Range (1, 4);
		for (int e = 0; e < i; e++) {
			int x=0, y=0, z=0;
			int u = Random.Range (0, 5);
			switch (u) {
			case 0:
				Debug.Log ("Up ");
				b.MoveUp ();
				x += 90;
				break;
			case 1:
				Debug.Log ("Down ");
				b.MoveDown ();
				x -= 90;
				break;
			case 2:
				Debug.Log ("Right ");
				b.MoveRight ();
				y -= 90;
				break;
			case 3:
				Debug.Log ("Left ");
				b.MoveLeft ();
				y += 90;
				break;
			case 4:
				Debug.Log ("TUR ");
				b.MoveUpRight ();
				z -= 90;
				break;
			case 5:
				Debug.Log ("TUL ");
				b.MoveUpLeft ();
				z += 90;
				break;
			}
			g.transform.localRotation = Quaternion.Euler(x,y,z)*startingRotation;
			startingRotation=this.transform.localRotation;
		}
		Debug.Log ("Final " + b.Status());
		FinalBox.Front.localization = 0;FinalBox.Up.localization = 2;FinalBox.Right.localization = 4;
		this.GetComponent<SameCube> ().Compare (OrigBox.Front, OrigBox.Up, OrigBox.Right, FinalBox.Front, FinalBox.Up, FinalBox.Right);
	}
}
