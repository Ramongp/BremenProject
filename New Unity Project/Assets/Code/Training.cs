using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Training : MonoBehaviour {

	// Use this for initialization
	public Text text, Mtext;
	public Image message;
	public Button next, PathButton;
	public static int order,currentOrder;
	public ParticleSystem Correct;
	public Image Anchor, Movement, MLetrero;
	public Sprite[] MovsSprites;
	public Image[] Arrows;
	public static bool ShowSide;
	void Start () {
		ShowSide = false;
		TrainingCube.help = false;
		Mtext.text=LangTest.LMan.getString ("MSign");
		PathButton.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("Path");
		PathButton.gameObject.SetActive (false); 
		GameObject.Find("Sides").GetComponent<CanvasGroup>().alpha=0;
		GameObject.Find("ArrowsPanel").GetComponent<CanvasGroup>().alpha=0;
		GameObject.Find ("MostrarLados").GetComponent<CanvasGroup> ().alpha = 0;
		GameObject.Find("UnfoldChest").GetComponent<CanvasGroup>().interactable=false;
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
			GameObject.Find("CubePl").GetComponent<Unfold> ().UndoMovement ();
		}
		if(currentOrder.Equals(22))
		{
			SceneManager.LoadScene ("Map Select Level");	
		}
		next.gameObject.SetActive (false);
		currentOrder += 1;

	}
	public void Action()
	{

		switch (order) {
		case 0: //Explica
			text.text = LangTest.LMan.getString ("Training0");
			TrainingCube.help = false;
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;
		case 1: //Ensena caras
			TrainingCube.help = false;
			GameObject.Find ("Sides").GetComponent<CanvasGroup> ().alpha = 1;
			GameObject.Find ("FrontSide").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Front");
			GameObject.Find ("RightSide").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Right");
			GameObject.Find ("UpSide").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Up");
			text.text = LangTest.LMan.getString ("Training1");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;

		case 2://Tapa las caras explica
			TrainingCube.help = false;
			GameObject.Find ("Sides").GetComponent<CanvasGroup> ().alpha = 0;
			text.text = LangTest.LMan.getString ("Training2");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB0");
			break;
		case 3: //Primer movimiento
			TrainingCube.MoveNeeded = "Down";
			MLetrero.gameObject.SetActive (true);
			Anchor.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [0];
			Anchor.GetComponent<Animator> ().SetBool ("Down", true);
			text.text = LangTest.LMan.getString ("Training3");
			TrainingCube.help = true;
			break;
		case 4: //Explica cambio tras Down
			TrainingCube.help = false;
			MLetrero.gameObject.SetActive (false);
			Anchor.GetComponent<Animator> ().SetBool ("Down", false);
			Anchor.gameObject.SetActive (false);
			text.text = LangTest.LMan.getString ("Training4");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;
		case 5://Mov Up
			TrainingCube.MoveNeeded="Up";
			MLetrero.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training5");
			Anchor.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [1];
			Anchor.GetComponent<Animator> ().SetBool ("Down", false);
			Anchor.GetComponent<Animator> ().SetBool ("Up", true);
			TrainingCube.help = true;
			break;
		case 6:// Mov Left
			TrainingCube.MoveNeeded="Left";
			MLetrero.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training6");
			Anchor.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [3];
			Anchor.GetComponent<Animator> ().SetBool ("Up", false);
			Anchor.GetComponent<Animator> ().SetBool ("Left", true);
			TrainingCube.help = true;
			break;
		case 7: //Mov Right
			TrainingCube.MoveNeeded="Right";
			MLetrero.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training7");
			Anchor.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [2];
			Anchor.GetComponent<Animator> ().SetBool ("Left", false);
			Anchor.GetComponent<Animator> ().SetBool ("Right", true);
			TrainingCube.help = true;
			break;
		case 8: //Explica los dos last moves
			TrainingCube.help = false;
			MLetrero.gameObject.SetActive (false);
			Anchor.GetComponent<Animator> ().SetBool ("Right", false);
			Anchor.gameObject.SetActive (false);
			text.text = LangTest.LMan.getString ("Training8");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;
		case 9: //Mov towardupleft
			TrainingCube.MoveNeeded = "TowardUpLeft";
			MLetrero.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training9");
			Anchor.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [5];
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", true);
			TrainingCube.help = true;
			break;
		case 10: //explica tras rotacion
			TrainingCube.help = false;
			MLetrero.gameObject.SetActive (false);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpLeft", false);
			Anchor.gameObject.SetActive (false);
			text.text = LangTest.LMan.getString ("Training10");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;
		case 11: // TowardsUpRight
			TrainingCube.MoveNeeded = "TowardUpRight";
			MLetrero.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training11");
			Anchor.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [4];
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpRight", true);
			TrainingCube.help = true;
			break;
		case 12: //Pregunta desplegar
			TrainingCube.help = false;
			MLetrero.gameObject.SetActive (false);
			Anchor.GetComponent<Animator> ().SetBool ("TowardUpRight", false);
			Anchor.gameObject.SetActive (false);
			text.text = LangTest.LMan.getString ("Training12");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;
		case 13://Explica Unfold
			TrainingCube.help = false;
			text.text = LangTest.LMan.getString ("Training13");
			GameObject.Find("UnfoldChest").GetComponent<CanvasGroup>().interactable=true;
			break;
		case 14://Explica Caras desplegadas
			TrainingCube.help = false;
			text.text = LangTest.LMan.getString ("Training14");
			GameObject.Find ("UnFront").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Front");
			GameObject.Find ("UnRight").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Right");
			GameObject.Find ("UnLeft").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Left");
			GameObject.Find ("UnUp").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Up");
			GameObject.Find ("UnBack").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Back");
			GameObject.Find ("UnDown").GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Down");
			GameObject.Find("UnfoldChest").GetComponent<CanvasGroup>().interactable=false;
			next.gameObject.SetActive (true);
			GameObject.Find ("MostrarLados").GetComponent<CanvasGroup> ().alpha = 1;
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;
		case 15: //Explica plegar
			TrainingCube.help = false;
			GameObject.Find ("MostrarLados").GetComponent<CanvasGroup> ().alpha = 0;
			GameObject.Find("UnfoldChest").GetComponent<CanvasGroup>().interactable=true;
			text.fontSize=22;
			text.text = LangTest.LMan.getString ("Training15");
			break;
		case 16: //Explica camino
			TrainingCube.help = false;
			text.fontSize=25;
			GameObject.Find("UnfoldChest").GetComponent<CanvasGroup>().interactable=false;
			PathButton.gameObject.SetActive (true);
			Arrows [0].gameObject.SetActive (false);
			Arrows [1].gameObject.SetActive (false);
			Arrows [2].gameObject.SetActive (false);
			GameObject.Find("ArrowsPanel").GetComponent<CanvasGroup>().alpha=1;
			text.text = LangTest.LMan.getString ("Training16");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			TrainingCube.help = false;
			break;
		case 17: //Muestra Camino
			TrainingCube.help = false;
			text.fontSize=22;
			Arrows [0].gameObject.SetActive (true);
			next.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training17");
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;
		case 18: //Move right
			ShowSide=true;
			TrainingCube.MoveNeeded="Right";
			message.gameObject.SetActive(false);
			MLetrero.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [2];
			TrainingCube.help = true;
			break;
		case 19: //Explica cambio orient
			TrainingCube.help = false;
			MLetrero.gameObject.SetActive (false);
			text.fontSize=22;
			Arrows [1].gameObject.SetActive (true);
			next.gameObject.SetActive (true);
			text.text = LangTest.LMan.getString ("Training18");
			message.gameObject.SetActive(true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			break;
		case 20: // TowardsUpRight
			TrainingCube.MoveNeeded = "TowardUpRight";
			message.gameObject.SetActive(false);
			MLetrero.gameObject.SetActive (true);
			Movement.gameObject.SetActive (true);
			Movement.sprite = MovsSprites [4];
			TrainingCube.help = true;
			break;
		case 21: //Mensaje final
			text.fontSize=25;
			message.gameObject.SetActive(true);
			Correct.gameObject.SetActive(true);
			MLetrero.gameObject.SetActive (false);
			text.text = LangTest.LMan.getString ("Training19");
			next.gameObject.SetActive (true);
			next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("TrainingB1");
			break;
		}
	}
	void Error()
	{
		text.fontSize=25;
		message.gameObject.SetActive(true);
		MLetrero.gameObject.SetActive (false);
		TrainingCube.help = false;
		Anchor.gameObject.SetActive (false);
		text.text = LangTest.LMan.getString ("ErrorTraining");
		next.gameObject.SetActive (true);
		next.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("ErrorTrainingB");
		//order-=1;
	}
}
