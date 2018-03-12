using UnityEngine;
using System.Collections;
using System.IO;

public class Unfold : MonoBehaviour {

	// Use this for initialization
	public bool end;
	public static int Fold;
	public int speedRotation, seedMove;
	public Quaternion startingRotation,finalRotation;
	public float MarginError;
	public Vector3 UnfoldPosition,FoldPosition;
	public static bool moving, button;
	void Start () {

		WriteTest ();
		seedMove = 20;
		//FoldPosition.localPosition = 
		//UnfoldPositon.localPosition = new Vector3 (-10, 0, 10);
		button = false;
		UnfoldPosition = new Vector3 (-5, -10, 20);
		FoldPosition = new Vector3 (0, 0,0);
		MarginError = 0.003f;
		speedRotation = 10;
		//this.transform.rotation = Quaternion.Euler (-30, 60, 0);
		startingRotation = this.gameObject.transform.localRotation;
		finalRotation = startingRotation;
		Fold = 0;
		end = true;
		Debug.Log (end.ToString ());
	}
	
	// Update is called once per frame
	void Update () {

		if ((Fold.Equals (0))&& (this.transform.localPosition.z>FoldPosition.z)) {
			//this.gameObject.transform.Translate (FoldPosition.x * Time.deltaTime,FoldPosition.y * Time.deltaTime,FoldPosition.z * Time.deltaTime);
			this.gameObject.transform.localPosition= Vector3.MoveTowards(this.transform.localPosition,FoldPosition,seedMove*Time.deltaTime);
			if (this.transform.localScale.x < 1) {
				this.transform.localScale += new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);
			}
			} 

		if ((Fold.Equals (1)) && (this.transform.localPosition.z<UnfoldPosition.z+MarginError)) {
				//this.gameObject.transform.Translate (UnfoldPositon.x * Time.deltaTime,UnfoldPositon.y * Time.deltaTime,UnfoldPositon.z * Time.deltaTime);
			this.gameObject.transform.localPosition= Vector3.MoveTowards(this.transform.localPosition,UnfoldPosition,seedMove*Time.deltaTime);
			if (this.transform.localScale.x > 0.7) {
				this.transform.localScale -= new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);
			}
			}


		if (Input.GetKeyDown ("up")) {
			UnfoldBox ();
			//end = false;
			//if (Fold.Equals (0)&& this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 /*&& this.gameObject.GetComponent<Animator>().IsInTransition(0)*/) {

		}

		if (Input.GetKeyDown ("w")&& Fold.Equals(0)&&moving.Equals(false)) {
			//this.gameObject.transform.Rotate (90, 0, 0);
			MoveUp();
		}
		if (Input.GetKeyDown ("a")&& Fold.Equals(0)&&moving.Equals(false)) {
			MoveLeft();
		}
		if (Input.GetKeyDown ("d")&& Fold.Equals(0)&&moving.Equals(false)) {
			MoveRight();
		}
		if (Input.GetKeyDown ("s")&& Fold.Equals(0)&&moving.Equals(false)) {
			MoveDown();
		}
		if (Input.GetKeyDown ("e")&& Fold.Equals(0)&&moving.Equals(false)) {
			MoveUpRight();
		}
		if (Input.GetKeyDown ("q")&& Fold.Equals(0)&&moving.Equals(false)) {
			MoveUpLeft();
		}

		if ((Input.GetKeyDown ("space"))&& (Fold.Equals(0))&&(moving.Equals(false))&&((Mathf.Abs(this.gameObject.transform.localRotation.x - finalRotation.x) > MarginError) || (Mathf.Abs (this.gameObject.transform.localRotation.y - finalRotation.y) > MarginError) || (Mathf.Abs (this.gameObject.transform.localRotation.z - finalRotation.z) > MarginError)))  {
			SetToStart ();

		}


		if (button) {
			if ((Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.x )- Mathf.Abs (finalRotation.x)) < MarginError) && (Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.y )- Mathf.Abs (finalRotation.y)) < MarginError) && (Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.z )- Mathf.Abs (finalRotation.z)) < MarginError)) {
				moving = false;
				this.gameObject.transform.localRotation = finalRotation;
				startingRotation = finalRotation;
				button = false;
			} else {
				//	Debug.Log ("GameObject" + this.gameObject.transform.rotation.x.ToString() + "Final" + finalRotation.x.ToString()+"Resultado"+Mathf.Abs(this.gameObject.transform.rotation.x-finalRotation.x).ToString());
				this.transform.localRotation = Quaternion.Lerp (this.transform.localRotation, finalRotation, Time.deltaTime * speedRotation);

			}
		}

	
	}

	public void RotateSmooth(float x, float y, float z){
		//this.gameObject.transform.localRotation= startingRotation;
		finalRotation = Quaternion.Euler (x, y, z) * startingRotation;
		//Debug.Log ("Final rotation "+ finalRotation.ToString ());
		moving = true;

	}

	void Origin()
	{
		RotateSmooth(finalRotation.x,finalRotation.y,finalRotation.z);
		button = true;
	}

	public void UnfoldBox(){
		if (this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("New State")&& Fold.Equals(0)){
			this.gameObject.GetComponent<Animator> ().SetTrigger ("Open");
			Fold = 1;
		} else {
			if (this.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Unfold")&& Fold.Equals(1)) {
				this.gameObject.GetComponent<Animator> ().SetTrigger ("Close");
				RotateSmooth(finalRotation.x,finalRotation.y,finalRotation.z);
				button = true;
				Fold = 0;
			}
		}
	}

	public void MoveUp()
	{
		RotateSmooth(45,0,0);
		button = true;
		SaveMove ("Up");
	}

	public void MoveDown()
	{
		RotateSmooth(-45,0,0);
		button = true;
		SaveMove ("Down");
	}
	public void MoveLeft()
	{
		RotateSmooth(0,45,0);
		button = true;
		SaveMove ("Left");
	}
	public void MoveRight()
	{
		RotateSmooth(0,-45,0);
		button = true;
		SaveMove ("Right");
	}
	public void MoveUpLeft()
	{
		RotateSmooth(0,0,45);
		button = true;
		SaveMove ("UpLeft");
	}
	public void MoveUpRight()
	{
		RotateSmooth(0,0,-45);
		button = true;
		SaveMove ("UpRight");
	}

	public void SetToStart()
	{
		if(((Mathf.Abs(this.gameObject.transform.localRotation.x - finalRotation.x) > MarginError) || (Mathf.Abs (this.gameObject.transform.localRotation.y - finalRotation.y) > MarginError) || (Mathf.Abs (this.gameObject.transform.localRotation.z - finalRotation.z) > MarginError))){
		RotateSmooth(finalRotation.x,finalRotation.y,finalRotation.z);
		button = true;
		}
	}

	private string getPath ()
	{
		#if UNITY_EDITOR
		return Application.dataPath + "/CSV/" + "Moves.csv";
		#elif UNITY_ANDROID
		return Application.persistentDataPath+"Saved_Inventory.csv";
		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/"+"Saved_Inventory.csv";
		#else
		return Application.dataPath +"/"+"Saved_Inventory.csv";
		#endif
		}    
		void SaveMove (string move)
	{
	
		string filePath = getPath ();
		string delimiter = ",";  

		//This is the writer, it writes to the filepath
		//StreamWriter writer = new StreamWriter (filePath);

		//This is writing the line of the type, name, damage... etc... (I set these)
		//writer.Write(move);
		File.AppendAllText (filePath, move+delimiter);
		//This loops through everything in the inventory and sets the file to these.
		//writer.Flush ();
		//This closes the file
	//	writer.Close ();
	}
		void WriteTest ()
		{

		string filePath = getPath ();
		string delimiter = ",";  
		string test = "Test 1";
		//This is the writer, it writes to the filepath
		//StreamWriter writer = new StreamWriter (filePath);

		//This is writing the line of the type, name, damage... etc... (I set these)
		//writer.Write(move);
		File.AppendAllText (filePath, test+delimiter);
		//This loops through everything in the inventory and sets the file to these.
		//writer.Flush ();
		//This closes the file
		//	writer.Close ();
		}
}
