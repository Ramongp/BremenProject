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
//		Mtext.text = TrainingCube.Tangle.ToString(); //Provisional
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
		if (next.GetComponentInChildren<Text> ().text.Equals (LangTest.LMan.getString ("ErrorTrainingB"))) {
			GameObject.Find("CubePl").GetComponent<Unfold> ().SetToAfterRandom ();
		}
		if(currentOrder.Equals(16))
		{
			Application.LoadLevel ("Map Select Level");	
		}
		next.gameObject.SetActive (false);
		TrainingCube.help = true;
		currentOrder += 1;

	}
	public void Action()
	{

		switch (order) {
		case 0: //le da al botón
			text.text = LangTest.LMan.getString ("Training0");
			TrainingCube.help = false;
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
			MLetrero.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("Down", true);
			Correct.gameObject.SetActive(true);
			text.text = LangTest.LMan.getString ("Training2");
			break;
		case 3:
			Correct.Emit(20);
			Anchor.GetComponent<Animator> ().SetBool ("Down", false);
			Anchor.gameObject.SetActive (false);
			MLetrero.gameObject.SetActive (false);
			TrainingCube.help = false;
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB1");
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
			MLetrero.gameObject.SetActive (true);
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
			TrainingCube.help = false;
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
			MLetrero.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training5");
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("Down", true);
			break;
		case 9:
			Correct.Emit (20);
			Anchor.GetComponent<Animator> ().SetBool ("Down", false);
			Anchor.gameObject.SetActive (false);
			text.text = LangTest.LMan.getString ("Training8");
			MLetrero.gameObject.SetActive (false);
			TrainingCube.help = false;
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB1");
			TrainingCube.MoveNeeded="TowardUpLeft";
			break;
		case 10:
			Correct.gameObject.SetActive(false);
			text.text = LangTest.LMan.getString ("Training9");
			MLetrero.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [2];
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", true);
			break;
		case 11:
			MLetrero.gameObject.SetActive (true);
			Anchor.gameObject.SetActive (true);
			Correct.gameObject.SetActive(true);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", true);
			text.text = LangTest.LMan.getString ("Training5");
			break;
		case 12:
			MLetrero.gameObject.SetActive (true);
			Anchor.gameObject.SetActive (true);
			Correct.Emit (20);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", true);
			text.text = LangTest.LMan.getString ("Training13");
			break;
		case 13:
			Correct.Emit (20);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", false);
			Anchor.gameObject.SetActive (false);
			MLetrero.gameObject.SetActive (false);
			text.text = LangTest.LMan.getString ("Training10");
			TrainingCube.help = false;
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB1");
			break;
		case 14:
			TrainingCube.MoveNeeded = "TowardUpRight";
			text.text = LangTest.LMan.getString ("Training11");;
			Correct.gameObject.SetActive (false);
			MLetrero.gameObject.SetActive (true);	Movement.sprite = MovsSprites [3];
			Anchor.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpRight", true);
			break;
		case 15:
			Correct.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training12");
			MLetrero.gameObject.SetActive (false);
			TrainingCube.help = false;
			next.gameObject.SetActive (true);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpRight", false);
			Anchor.gameObject.SetActive (false);
			TrainingCube.help = false;
			break;
			
		}
	}
	void Error()
	{
		MLetrero.gameObject.SetActive (false);
		TrainingCube.help = false;
		Anchor.gameObject.SetActive (false);
		text.text = LangTest.LMan.getString ("ErrorTraining");
		next.gameObject.SetActive (true);
		next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("ErrorTrainingB");
		//order-=1;
	}
}
