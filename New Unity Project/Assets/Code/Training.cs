using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Training : MonoBehaviour {

	// Use this for initialization
	public Text text, Mtext;
	public Image message;
	public Button next;
	public static int order,currentOrder;
	public ParticleSystem Correct;
	public Image Anchor, Movement, MLetrero;
	public Sprite[] MovsSprites;
	void Start () {
		MLetrero.gameObject.SetActive (false);
		Movement.gameObject.SetActive (false);
		Anchor.gameObject.SetActive (false);
		order = 0;
		currentOrder = order;
		text.text = LangTest.LMan.getString ("StartTraining");
		next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB0");
		Correct.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (currentOrder>order) {
			Action ();
			order = currentOrder;
		}
		if (currentOrder < order){
			order--;
			currentOrder = order;
			Error ();
		}

	}
	public void Next()
	{
		if (next.GetComponentInChildren<Text> ().text.Equals ("Try Again")) {
			GameObject.Find("CubePl").GetComponent<Unfold> ().SetToAfterRandom ();
			TrainingCube.help = true;
		}
		next.gameObject.SetActive (false);
		currentOrder += 1;

	}
	public void Action()
	{

		switch (order) {
		case 0: //le da al botón
			text.text = LangTest.LMan.getString ("Training0");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB1");
			break;
		case 1: //le da al botón
			TrainingCube.MoveNeeded = "Down";
			TrainingCube.help = true;
			MLetrero.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Mtext.text=LangTest.LMan.getString ("MSign");
			Movement.sprite = MovsSprites [0];
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("Down", true);
			text.text = LangTest.LMan.getString ("Training1");
			break;

		case 2:
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("Down", true);
			Correct.gameObject.SetActive(true);
			text.text = LangTest.LMan.getString ("Training2");
			break;
		case 3:
			Correct.Emit(20);
			Anchor.GetComponent<Animator> ().SetBool ("Down", false);
			Anchor.gameObject.SetActive (false);
			MLetrero.gameObject.SetActive (false);
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = "Ok";
			text.text = LangTest.LMan.getString ("Training3");
			break;
		case 4:
			Correct.gameObject.SetActive(false);
			TrainingCube.MoveNeeded="Right";
			MLetrero.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [1];
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("Right", true);
			text.text = LangTest.LMan.getString ("Training4");
			break;
		case 5:
			Correct.gameObject.SetActive(true);
			text.text = LangTest.LMan.getString ("Training5");
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("Right", true);
			break;
		case 6:
			TrainingCube.MoveNeeded="Down";
			Correct.Emit (20);
			Anchor.GetComponent<Animator> ().SetBool ("Right", false);
			Anchor.gameObject.SetActive (false);
			text.text = LangTest.LMan.getString ("Training6");
			MLetrero.gameObject.SetActive (false);
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB1");
			break;
		case 7:
			Correct.gameObject.SetActive(false);
			MLetrero.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [0];
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("Down", true);
			text.text = LangTest.LMan.getString ("Training7");

			break;
		case 8:
			Correct.gameObject.SetActive(true);
			text.text = "Great, another one";
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("Down", true);
			break;
		case 9:
			Correct.Emit (20);
			Anchor.GetComponent<Animator> ().SetBool ("Down", false);
			Anchor.gameObject.SetActive (false);
			text.text = "Almos there, are you ready for the last movement?";
			MLetrero.gameObject.SetActive (false);
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB1");
			TrainingCube.MoveNeeded="TowardUpLeft";
			break;
		case 10:
			Correct.gameObject.SetActive(false);
			text.text = "This one is a little tricky, slide your finger from the right bottom to the top left";
			MLetrero.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [2];
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", true);
			break;
		case 11:
			Anchor.gameObject.SetActive (true);
			Correct.gameObject.SetActive(true);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", true);
			text.text = "Great, another one";
			break;
		case 12:
			Anchor.gameObject.SetActive (true);
			Correct.Emit (20);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", true);
			text.text = "The last one";
			break;
		case 13:
			Correct.Emit (20);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", false);
			Anchor.gameObject.SetActive (false);
			MLetrero.gameObject.SetActive (false);
			text.text = "Ups, too many movements, Luckily you can do the opposite of the movement";
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB1");
			break;
		case 14:
			TrainingCube.MoveNeeded = "TowardUpRight";
			text.text = "So every movement has their own opposite";
			Correct.gameObject.SetActive (false);
			MLetrero.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [3];
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpRight", true);
			break;
		case 15:
			Correct.gameObject.SetActive (true);
			text.text = "Now I can open it";
			MLetrero.gameObject.SetActive (false);
			next.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpRight", false);
			Anchor.gameObject.SetActive (false);
			TrainingCube.help = false;
			Points.animation = true;
			break;
		}
	}
	void Error()
	{
		TrainingCube.help = false;
		Anchor.gameObject.SetActive (false);
		text.text = "Sorry, but that was not the movement that we need";
		next.gameObject.SetActive (true);
		next.GetComponentInChildren<Text> ().text = "Try Again";
		//order-=1;
	}
}
