using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// Use this for initialization
	public static float TimeLeft, AnimationTime;
	public Slider TimeSlider, MoneySlider;
	public static bool start, end, animation;
	public static float TimeLevel;
	public static int pointsLevel, points;
	public int currentValue, answer;
	public int moneySpeed; 
	public Text Solution, PointText;
	public Image TimeFill,Bag;
	public Button TaparN, TaparE;
	public Image Message;
	public Image[] Arrows;
	public Sprite[] ArrowsSp;
	void Start () {
		
		Bag.gameObject.SetActive (false);
		PointText.gameObject.SetActive (false);moneySpeed = 20;
		MoneySlider.value = 0;
		MoneySlider.maxValue = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			SetTimer ();
			start = false;
		}
		if (!end) {
			TimeLeft -= Time.deltaTime;
			TimeSlider.value = TimeLeft;
				points = pointsLevel ;//Ecuacion puntos
				ColorBlock c = TimeSlider.colors;
				c.normalColor = Color.red;
			TimeFill.color= (Color.green/2+(Color.red/TimeLeft));
			} 
		if (MoneySlider.value <currentValue) {
			MoneySlider.value += Time.deltaTime*moneySpeed;
		}
	}

	void SetTimer()
	{
		GameObject.Find ("Reward 1").GetComponent<Points> ().Set ();
		Message.gameObject.SetActive (false);
		TimeLevel = 60;
		pointsLevel = 10;
		points = pointsLevel;
		TimeLeft = TimeLevel;
		TimeSlider.maxValue = TimeLeft;
		TimeSlider.value = TimeSlider.maxValue;
		TaparN.gameObject.SetActive (false);
		TaparE.gameObject.SetActive (false);
		animation = false;
		end = false;
		Solution.gameObject.SetActive (false);
		foreach (Image i in Arrows) {
			i.gameObject.SetActive (false);
		}
		points = pointsLevel;
	}

	public void Animation(bool correct,int answer)
	{
		this.answer = answer;
		Message.gameObject.SetActive (true);
		TaparE.gameObject.SetActive (true);
		animation = true;
		AnimationTime = 2;
		if (correct) {
			switch (answer) {
			case 0:
				Solution.text = "Yes, it is the same";
				break;
			case 1:
				Solution.text = "Yes, They are different";
				break;
			}
			Solution.gameObject.SetActive (true);
			Solution.GetComponent<Animator> ().SetBool ("Correct", true);
			currentValue += points;
		} 
		else {
			switch (answer) {
			case 2:
				Solution.text = "No, They are different";
				break;
			case 3:
				Solution.text = "No, it is the same";
				break;
			}
			Solution.gameObject.SetActive (true);
			Solution.GetComponent<Animator> ().SetBool ("Wrong", true);
		}
	}

	public void NextBox()
	{
			TaparN.gameObject.SetActive (false);
			Message.gameObject.SetActive (false);
			Solution.gameObject.SetActive (false);
			Unfold.ShowExpl = false;
			GameObject.Find ("Reward 1").GetComponent<Points> ().RewardAnimation (TimeLeft);
	}

	public void PostReward()
	{
		if (Cube.Test < 10) {
			//if (!Points.exchange) { // If animation is over
			GameObject.Find ("Camera").GetComponent<Cube> ().Restart ();
			//}
		} else {
			Application.LoadLevel ("Map Select Level");
		}
	}

	public void Explicacion()
	{
		
		//animation = false;
		TaparE.gameObject.SetActive (false);
		TaparN.gameObject.SetActive (true);
		Bag.gameObject.SetActive (false);
		PointText.gameObject.SetActive (false);

		if (answer < 2) {
			Solution.GetComponent<Animator> ().SetBool ("Correct", false);
		} else {
			Solution.GetComponent<Animator> ().SetBool ("Wrong", false);
		}
		if ((answer.Equals (0))|| (answer.Equals (3))){ //Explanation Same cube
			Solution.text =LangTest.LMan.getString ("NoFaceSame");

			if (!SameCube.Fx.symbol.Equals("NoFaceSame")) {
				Cube.help = false;
				Solution.text = LangTest.LMan.getString (SameCube.Fx.symbol);
				GameObject.Find ("CubePl").GetComponent<Unfold> ().CreateWay ();
				CreateArrows ();
			}
		} else {
			Solution.text = LangTest.LMan.getString ("DiffBecause")+ LangTest.LMan.getString (TradLocaton(SameCube.Fx.localization))+LangTest.LMan.getString ("DontMatch");
			GameObject.Find("Camera").GetComponent<Cube>().GBox[SameCube.Fx.localization].GetComponent<Animator> ().SetBool ("Highlight", true);
			GameObject.Find("Camera").GetComponent<Cube>().OGbox [SameCube.Fx.localization].GetComponent<Animator> ().SetBool ("Highlight", true);
			GameObject.Find ("CubePl").GetComponent<Unfold> ().UnfoldBox ();
			GameObject.Find ("CubePl Sin Codigo (L)").GetComponent<Animator> ().SetBool ("Unfold", true);
		}

		/*switch (answer) {
		case 0:
			Solution.GetComponent<Animator> ().SetBool ("Correct", false);

			break;
		case 1:
			Solution.GetComponent<Animator> ().SetBool ("Correct", false);
			Solution.text = "They are different because of "+SameCube.Fx.symbol;
			break;
		case 2:
			Solution.GetComponent<Animator> ().SetBool ("Wrong", false);
			Solution.text = "They are different, because of "+SameCube.Fx.symbol;
			break;
		case 3:
			Solution.GetComponent<Animator> ().SetBool ("Wrong", false);
			Solution.text = "No, it is the same";
			break;

		}*/

		
	}

	string TradLocaton(int l)
	{
		switch (l)
		{
		case 0:
			return "Front";
		case 1:
			return "Back";
		case 2:
			return "Up";
		case 3:
			return "Down";
		case 4:
			return "Right";
		case 5:
			return "Left";
		}
		return " ";
	}

	void CreateArrows()
	{
		string expl = SameCube.Way;
		string[] way;
		way = expl.Split (',');

		for (int i = 0; i < way.Length-1; i++) {
			string Move = way [way.Length-2-i];
			Arrows [i].gameObject.SetActive (true);
			switch (Move) { //Do the opposite Move
			case "Up":
				Arrows [i].sprite = ArrowsSp [1];
				break;
			case "Down":
				Arrows [i].sprite = ArrowsSp [0];
				break;
			case "Left":
				Arrows [i].sprite = ArrowsSp [3];
				break;
			case "Right":
				Arrows [i].sprite = ArrowsSp [2];
				break;
			case "Toward-up-right":
				Arrows [i].sprite = ArrowsSp [5];
				break;
			case "Toward-up-left":
				Arrows [i].sprite = ArrowsSp [4];
				break;
			}
		}
	}
		
}
