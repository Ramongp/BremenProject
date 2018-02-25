using UnityEngine;
using System.Collections;

public class Unfold : MonoBehaviour {

	// Use this for initialization
	public bool end;
	public int Fold;
	public int speedRotation;
	public Quaternion startingRotation,finalRotation;
	public float MarginError;
	public bool moving, button;
	void Start () {
		button = false;
		MarginError = 0.003f;
		speedRotation = 10;
		startingRotation = this.gameObject.transform.rotation;
		finalRotation = startingRotation;
		Fold = 0;
		end = true;
		Debug.Log (end.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up")) {
			//end = false;
			//if (Fold.Equals (0)&& this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1 /*&& this.gameObject.GetComponent<Animator>().IsInTransition(0)*/) {
			if (this.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("New State")&& Fold.Equals(0)){
				this.gameObject.GetComponent<Animator> ().SetTrigger ("Open");
				Fold = 1;
			} else {
				if (this.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("Unfold")&& Fold.Equals(1)) {
					this.gameObject.GetComponent<Animator> ().SetTrigger ("Close");
					Fold = 0;
				}
			}
		}

		if (Input.GetKeyDown ("w")&& Fold.Equals(0)&&moving.Equals(false)) {
			//this.gameObject.transform.Rotate (90, 0, 0);
			RotateSmooth(90,0,0);
			button = true;
		}
		if (Input.GetKeyDown ("a")&& Fold.Equals(0)&&moving.Equals(false)) {
			RotateSmooth(0,90,0);
			button = true;
		}
		if (Input.GetKeyDown ("d")&& Fold.Equals(0)&&moving.Equals(false)) {
			RotateSmooth(0,-90,0);
			button = true;
		}
		if (Input.GetKeyDown ("s")&& Fold.Equals(0)&&moving.Equals(false)) {
			RotateSmooth(-90,0,0);
			button = true;
		}
		if (Input.GetKeyDown ("e")&& Fold.Equals(0)&&moving.Equals(false)) {
			RotateSmooth(0,0,-90);
			button = true;
		}
		if (Input.GetKeyDown ("q")&& Fold.Equals(0)&&moving.Equals(false)) {
			RotateSmooth(0,0,90);
			button = true;
		}

		if (Input.GetKeyDown ("space")&& Fold.Equals(0)&&moving.Equals(false)) {
			RotateSmooth(finalRotation.x,finalRotation.y,finalRotation.z);
			button = true;
		}


		if (button) {
			if ((Mathf.Abs (this.gameObject.transform.rotation.x - finalRotation.x) < MarginError) && (Mathf.Abs (this.gameObject.transform.rotation.y - finalRotation.y) < MarginError) && (Mathf.Abs (this.gameObject.transform.rotation.z - finalRotation.z) < MarginError)) {
				moving = false;
				this.gameObject.transform.rotation = finalRotation;
				startingRotation = finalRotation;
				button = false;
			} else {
				//	Debug.Log ("GameObject" + this.gameObject.transform.rotation.x.ToString() + "Final" + finalRotation.x.ToString()+"Resultado"+Mathf.Abs(this.gameObject.transform.rotation.x-finalRotation.x).ToString());
				this.transform.rotation = Quaternion.Lerp (this.transform.rotation, finalRotation, Time.deltaTime * speedRotation);

			}
		}

	
	}

	public void RotateSmooth(float x, float y, float z){
		 finalRotation = Quaternion.Euler (x, y, z) * startingRotation;
		moving = true;

	}

	public void Origin()
	{
		RotateSmooth(finalRotation.x,finalRotation.y,finalRotation.z);
		button = true;
	}
}
