using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	// Use this for initialization
	public Material Map;
	public Vector3 startPosition, endPosition;
	public float horizontalSpeed = 0.01F;
	public float verticalSpeed = 0.01F;
	public float CanvasMargin = 20;
	public GameObject cube, CubePL, CubeFake;
	public GameObject[] GBox;
	public Texture[] Faces;
	public Face[] Box;

	void Start () {
		Box = new Face[6] {new Face ("S", 9, Faces [0],0), new Face ("S", 9, Faces [1],1), new Face ("S", 9, Faces [2],2),
			new Face ("S", 9, Faces [3],3), new Face ("S", 9, Faces [4],4), new Face ("S", 9, Faces [5],5)};

		for (int i = 0; i < Box.Length; i++) {
			//Box [i].image.TextureWrapMode.Mirror;
			GBox [i].GetComponent<Renderer> ().material.mainTexture = Box[i].image;
			Paint (GBox [i].GetComponent<MeshFilter> ().mesh);
		}

		//CubeFake.transform.rotation = Quaternion.Euler(90 * Random.Range (0, 3), 90 * Random.Range (0, 3), 90 * Random.Range (0, 3));
		CubeFake.transform.rotation = Quaternion.Euler(-15, 30	,-10)*Quaternion.Euler(90 * Random.Range (0, 3), 90 * Random.Range (0, 3), 90 * Random.Range (0, 3));
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetMouseButton(0)){
			MovePL ();
		}*/
			// Handle native touch events
			foreach (Touch touch in Input.touches) {
				HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
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
	void Move(){
			float h = horizontalSpeed * Input.GetAxis("Mouse X")*Mathf.Deg2Rad;
		float v = verticalSpeed * Input.GetAxis("Mouse Y")*Mathf.Deg2Rad;
			//cube.transform.Rotate(v, h, 0);
		}
	void MovePL(){
		float h2 = horizontalSpeed * Input.GetAxis("Mouse X")*Mathf.Deg2Rad;
		float v2 = verticalSpeed * Input.GetAxis("Mouse Y")*Mathf.Deg2Rad;
		//cube.transform.Rotate(v, h, 0);
		CubePL.transform.RotateAround(Vector3.up,-h2);
		CubePL.transform.RotateAround(Vector3.right,+v2);
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
}
