using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class Unfold : MonoBehaviour {

	// Use this for initialization
	public static string Info;
	public bool end, inverse;
	public static int Fold;
	public static float MaxScale, MinScale;
	public int speedRotation, seedMove;
	public Quaternion startingRotation,finalRotation;
	public static Quaternion AfterRandom;
	public float MarginError;
	public static Vector3 UnfoldPosition,FoldPosition;
	public static bool moving, button, Test, restart, ShowExpl;
	public int contWay;
	public string[] way;
	void Start () {
		MaxScale = 1;
		MinScale = 0.7F;
		seedMove = 20;
		//FoldPosition.localPosition = 
		//UnfoldPositon.localPosition = new Vector3 (-10, 0, 10);
		button = false;
		UnfoldPosition = new Vector3 (-7, -10, 30);
		FoldPosition = new Vector3 (0, 0,0);
		MarginError = 0.003f;
		speedRotation = 5;
		//this.transform.rotation = Quaternion.Euler (-30, 60, 0);
		startingRotation = this.gameObject.transform.localRotation;
		finalRotation = startingRotation;
		Fold = 0;
		end = true;
	}
	
	// Update is called once per frame
	void Update () {

		if ((Fold.Equals (0))&& (this.transform.localPosition.z>FoldPosition.z)) {
			//this.gameObject.transform.Translate (FoldPosition.x * Time.deltaTime,FoldPosition.y * Time.deltaTime,FoldPosition.z * Time.deltaTime);
			this.gameObject.transform.localPosition= Vector3.MoveTowards(this.transform.localPosition,FoldPosition,seedMove*Time.deltaTime);
			if (this.transform.localScale.x < MaxScale) {
				this.transform.localScale += new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);
			}
			} 

		if ((Fold.Equals (1)) && (this.transform.localPosition.z<UnfoldPosition.z+MarginError)) {
				//this.gameObject.transform.Translate (UnfoldPositon.x * Time.deltaTime,UnfoldPositon.y * Time.deltaTime,UnfoldPositon.z * Time.deltaTime);
			this.gameObject.transform.localPosition= Vector3.MoveTowards(this.transform.localPosition,UnfoldPosition,seedMove*Time.deltaTime);
			this.gameObject.transform.localRotation = Quaternion.Euler (-80, 0, 0);
			if (this.transform.localScale.x > MinScale) {
				this.transform.localScale -= new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime);
			} 
			}

		if (Cube.help) {
			if (Input.GetKeyDown ("up")) {
				UnfoldBox ();
				//end = false;
				//if (Fold.Equals (0)&& this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 /*&& this.gameObject.GetComponent<Animator>().IsInTransition(0)*/) {

			}

			if (Input.GetKeyDown ("w") && Fold.Equals (0) && moving.Equals (false)) {
				//this.gameObject.transform.Rotate (90, 0, 0);
				MoveUp ();
			}
			if (Input.GetKeyDown ("a") && Fold.Equals (0) && moving.Equals (false)) {
				MoveLeft ();
			}
			if (Input.GetKeyDown ("d") && Fold.Equals (0) && moving.Equals (false)) {
				MoveRight ();
			}
			if (Input.GetKeyDown ("s") && Fold.Equals (0) && moving.Equals (false)) {
				MoveDown ();
			}
			if (Input.GetKeyDown ("e") && Fold.Equals (0) && moving.Equals (false)) {
				MoveUpRight ();
			}
			if (Input.GetKeyDown ("q") && Fold.Equals (0) && moving.Equals (false)) {
				MoveUpLeft ();
			}

			if ((Input.GetKeyDown ("space")) && (Fold.Equals (0)) && (moving.Equals (false)) && ((Mathf.Abs (this.gameObject.transform.localRotation.x - finalRotation.x) > MarginError) ||
				(Mathf.Abs (this.gameObject.transform.localRotation.y - finalRotation.y) > MarginError) || (Mathf.Abs (this.gameObject.transform.localRotation.z - finalRotation.z) > MarginError))) {
				//SetToStart ();

			}
		}


		if (button) {
			
			if ((Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.x )- Mathf.Abs (finalRotation.x)) < MarginError) && (Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.y )- Mathf.Abs (finalRotation.y)) < MarginError) &&
				(Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.z )- Mathf.Abs (finalRotation.z)) < MarginError)) {
				startingRotation = finalRotation;
				moving = false;
				this.gameObject.transform.localRotation = finalRotation;
				button = false;
				if (ShowExpl) {
					if (inverse) {
						ShowWay ();
					} else {
						ShowWayL ();
					}
				}

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

	public void Origin()
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
				if (restart) {
					this.gameObject.GetComponent<Animator> ().SetTrigger ("Restart");
					restart = false;
				} else {
					this.gameObject.GetComponent<Animator> ().SetTrigger ("Close");
				}
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
		Info+="Up_";
		//SaveMove ("Up");
	}

	public void MoveDown()
	{
		RotateSmooth(-45,0,0);
		button = true;
		Info+="Down_";
	}
	public void MoveLeft()
	{
		RotateSmooth(0,45,0);
		button = true;
		Info+="Left_";
	}
	public void MoveRight()
	{
		RotateSmooth(0,-45,0);
		button = true;
		Info+="Right_";
	}
	public void MoveUpLeft()
	{
		RotateSmooth(0,0,45);
		button = true;
		Info+="Toward-up-left_";
	}
	public void MoveUpRight()
	{
		RotateSmooth(0,0,-45);
		button = true;
		Info+="Toward-up-right_";
	}

	public void SetToStart()
	{
		if(((Mathf.Abs(this.gameObject.transform.localRotation.x - finalRotation.x) > MarginError) || (Mathf.Abs (this.gameObject.transform.localRotation.y - finalRotation.y) > MarginError) ||
			(Mathf.Abs (this.gameObject.transform.localRotation.z - finalRotation.z) > MarginError))){
		RotateSmooth(finalRotation.x,finalRotation.y,finalRotation.z);
		button = true;
		}
	}

  
		

		public void SetToAfterRandom()
	{
		if ((Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.x )- Mathf.Abs (finalRotation.x)) < MarginError) && (Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.y )- Mathf.Abs (finalRotation.y)) < MarginError) &&
		(Mathf.Abs (Mathf.Abs (this.gameObject.transform.localRotation.z )- Mathf.Abs (finalRotation.z)) < MarginError)) {

		finalRotation = AfterRandom;
			button = true;
			Debug.Log (AfterRandom.ToString ());
		Info+="Reset_";
		moving = true;
		}
	}

	public void UndoMovement()
	{

			finalRotation = AfterRandom;
			button = true;
			Debug.Log (AfterRandom.ToString ());
			//Info+="Reset_";
			moving = true;
	}

		public void WriteHelp()
	{
		Cube.HelpPressed = true;
		//Info+="Help used,";
		startingRotation = AfterRandom;
		finalRotation = startingRotation;
		AfterRandom = finalRotation;
	}
		public void Restart()
		{
		startingRotation = AfterRandom;
		finalRotation = startingRotation;
		AfterRandom = finalRotation;
		}

	public void CreateWay(bool inverse)
		{
		this.inverse = inverse;
		string expl = "Restart_";
		expl += SameCube.Way;
		way = expl.Split ('_');
		contWay = 0;
		if (inverse) {
			ShowWay ();
		} else {
			ShowWayL ();
		}
		ShowExpl = true;
		Debug.Log ("Camino a seguir " + expl);
		speedRotation = 4;
		}

	public void CreateBonusWay(string bonusPath)
	{
		inverse = false;
		string expl = "Restart_";
		expl += bonusPath;
		way = expl.Split ('_');
		contWay = 0;
		ShowWayL ();
		ShowExpl = true;
		Debug.Log ("Camino a seguir " + expl);
		speedRotation = 4;
	}

		public void ShowWay()
		{
		if(contWay.Equals(way.Length))
		{
			contWay = 0;
		}
		string Move = way [way.Length-1-contWay];
		button = true;
		switch (Move) { //Do the opposite Move
		case "Up":
		RotateSmooth(-90,0,0);
		break;
		case "Down":
		RotateSmooth(90,0,0);
		break;
		case "Left":
		RotateSmooth(0,-90,0);
		break;
		case "Right":
		RotateSmooth(0,90,0);
		break;
		case "Toward-up-right":
		RotateSmooth(0,0,90);
		break;
		case "Toward-up-left":
		RotateSmooth(0,0,-90);
		break;
		case "Restart":
			finalRotation = AfterRandom;
			moving = true;
			break;
		}
		contWay++;

		}
		
	public void ShowWayL()
	{
		speedRotation = 4;
		if(contWay.Equals(way.Length))
		{
			contWay = 0;
		}
		string Move = way [contWay];
		button = true;
		switch (Move) { //Do the Move
		case "Up":
			RotateSmooth(90,0,0);
			break;
		case "Down":
			RotateSmooth(-90,0,0);
			break;
		case "Left":
			RotateSmooth(0,90,0);
			break;
		case "Right":
			RotateSmooth(0,-90,0);
			break;
		case "Toward-up-right":
			RotateSmooth(0,0,-90);
			break;
		case "Toward-up-left":
			RotateSmooth(0,0,90);
			break;
		case "Restart":
			finalRotation = AfterRandom;
			moving = true;
			break;
		}
		contWay++;

	}

	public void SetAfterBonus()
	{
		finalRotation = AfterRandom;
		moving = true;
	}


	public void MoveUp90()
	{
		RotateSmooth(90,0,0);
		button = true;
		Info+="Up_";
		//SaveMove ("Up");
	}

	public void MoveDown90()
	{
		RotateSmooth(-90,0,0);
		button = true;
		Info+="Down_";
	}
	public void MoveLeft90()
	{
		RotateSmooth(0,90,0);
		button = true;
		Info+="Left_";
	}
	public void MoveRight90()
	{
		RotateSmooth(0,-90,0);
		button = true;
		Info+="Right_";
	}
	public void MoveUpLeft90()
	{
		RotateSmooth(0,0,90);
		button = true;
		Info+="Toward-up-left_";
	}
	public void MoveUpRight90()
	{
		RotateSmooth(0,0,-90);
		button = true;
		Info+="Toward-up-right_";
	}

}
