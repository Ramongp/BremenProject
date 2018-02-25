using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	// Use this for initialization
	public Material Map;
	public GameObject cube, CubePL;
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
	}
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetMouseButton(0)){
			MovePL ();
		}*/
			
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
	//Obsoleto
	/*void Move(){
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
	}*/

}
