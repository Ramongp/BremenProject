using UnityEngine;
using System.Collections;

public class Unfold : MonoBehaviour {

	// Use this for initialization
	public bool end;
	public static int Fold;
	public int speedRotation, seedMove;
	public Quaternion startingRotation,finalRotation;
	public float MarginError;
	public Vector3 UnfoldPosition= new Vector3 (-10, 0, 10),FoldPosition = new Vector3 (0, 0,0);
	public static bool moving, button;
	void Start () {
		seedMove = 20;
		//FoldPosition.localPosition = 
		//UnfoldPositon.localPosition = new Vector3 (-10, 0, 10);
		button = false;
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
			} 

		if ((Fold.Equals (1)) && (this.transform.localPosition.z<UnfoldPosition.z+MarginError)) {
				//this.gameObject.transform.Translate (UnfoldPositon.x * Time.deltaTime,UnfoldPositon.y * Time.deltaTime,UnfoldPositon.z * Time.deltaTime);
			this.gameObject.transform.localPosition= Vector3.MoveTowards(this.transform.localPosition,UnfoldPosition,seedMove*Time.deltaTime);
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
		RotateSmooth(90,0,0);
		button = true;	
	}

	public void MoveDown()
	{
		RotateSmooth(-90,0,0);
		button = true;
	}
	public void MoveLeft()
	{
		RotateSmooth(0,90,0);
		button = true;
	}
	public void MoveRight()
	{
		RotateSmooth(0,-90,0);
		button = true;
	}
	public void MoveUpLeft()
	{
		RotateSmooth(0,0,90);
		button = true;
	}
	public void MoveUpRight()
	{
		RotateSmooth(0,0,-90);
		button = true;
	}

	public void SetToStart()
	{
		if(((Mathf.Abs(this.gameObject.transform.localRotation.x - finalRotation.x) > MarginError) || (Mathf.Abs (this.gameObject.transform.localRotation.y - finalRotation.y) > MarginError) || (Mathf.Abs (this.gameObject.transform.localRotation.z - finalRotation.z) > MarginError))){
		RotateSmooth(finalRotation.x,finalRotation.y,finalRotation.z);
		button = true;
		}
	}

}
