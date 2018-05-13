using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// Use this for initialization
	public static float TimeLeft, AnimationTime;
	public Slider TimeSlider;
	public static bool start, end, animation;
	public static float TimeLevel;
	public static int pointsLevel, points;
	public int currentValue, answer;
	public int moneySpeed; 
	public Text Solution, PointText, ExplQuest;
	public Image TimeFill,Bag, FondoQuest;
	public Button TaparN, TaparE, NextBoxB, YesQuest, NoQuest, BackC,DownC,LeftC,StartTestButton;
	public Image Message, StarSlid1, StarSlid2, StarSlid3, StarP1, StarP2, StarP3;
	public Image[] MArrows,Arrows;
	public Color StarOn, StarOff;
	public Sprite[] ArrowsSp;
	public float TotalTime;
	public GameObject PregQuest, PregCorrectPanel;
	void Start () {
		
		moneySpeed = 20;
		ExplHelp ();

		StartTestButton.gameObject.SetActive (true);
		StartTestButton.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Understood");
		if (LangTest.Help) {
			Solution.fontSize = 14;
			Message.transform.localScale = new Vector3 (1.6f, 1.6f, 1f);
			Solution.text = LangTest.LMan.getString ("NoExplHelp")+" "+LangTest.LMan.getString ("ExplHelp");
		} else {
			Solution.text = LangTest.LMan.getString ("NoExplHelp");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			SetTimer ();
			start = false;
		}
		if ((!end)&& (TimeLeft>0)) {
			TotalTime += Time.deltaTime;
			TimeLeft -= Time.deltaTime;
			TimeSlider.value = TimeLeft;
			if (TimeLeft < 40) {
				StarSlid1.color= StarOff;
			}
			if (TimeLeft < 20) {
				StarSlid2.color= StarOff;
			}
			if (TimeLeft<1) {
				StarSlid3.color= StarOff;
			}
			} 

		}

	void ExplHelp()
	{
		StartTestButton.gameObject.SetActive (false);
		PregCorrectPanel.gameObject.SetActive (false);
		FondoQuest.gameObject.SetActive (false);
		PregQuest.SetActive (false);
		Bag.gameObject.SetActive (false);
		TimeSlider.gameObject.SetActive (true);
		//GameObject.Find ("Reward 1").GetComponent<Points> ().Set ();
		StarSlid1.color= StarOn;
		StarSlid2.color= StarOn;
		StarSlid3.color= StarOn;
		StarP1.color= StarOn;
		StarP2.color= StarOn;
		StarP3.color= StarOn;
		TimeLevel = 60;
		pointsLevel = 10;
		points = pointsLevel;
		TimeLeft = TimeLevel;
		TimeSlider.maxValue = TimeLeft;
		TimeSlider.value = TimeSlider.maxValue;
		TaparN.gameObject.SetActive (false);
		TaparE.gameObject.SetActive (false);
		NextBoxB.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NextBox");
		NextBoxB.gameObject.SetActive (false);
		animation = false;
		foreach (Image i in MArrows) {
			i.gameObject.SetActive (false);
		}
	}

	void SetTimer()
	{
		
		StartTestButton.gameObject.SetActive (false);
		PregCorrectPanel.gameObject.SetActive (false);
		FondoQuest.gameObject.SetActive (false);
		PregQuest.SetActive (false);
		Bag.gameObject.SetActive (false);
		TimeSlider.gameObject.SetActive (true);
		//GameObject.Find ("Reward 1").GetComponent<Points> ().Set ();
		StarSlid1.color= StarOn;
		StarSlid2.color= StarOn;
		StarSlid3.color= StarOn;
		StarP1.color= StarOn;
		StarP2.color= StarOn;
		StarP3.color= StarOn;
		Message.gameObject.SetActive (false);
		TimeLevel = 60;
		pointsLevel = 10;
		points = pointsLevel;
		TimeLeft = TimeLevel;
		TimeSlider.maxValue = TimeLeft;
		TimeSlider.value = TimeSlider.maxValue;
		TaparN.gameObject.SetActive (false);
		TaparE.gameObject.SetActive (false);
		NextBoxB.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NextBox");
		NextBoxB.gameObject.SetActive (false);
		animation = false;
		end = false;
		Solution.gameObject.SetActive (false);
		foreach (Image i in MArrows) {
			i.gameObject.SetActive (false);
		}
		points = pointsLevel;
	}

	public void Animation(bool correct,int answer)
	{
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().SaveMove (Unfold.Info);
		Unfold.Info="";
		this.answer = answer;
		Message.gameObject.SetActive (true);
		TaparE.gameObject.SetActive (true);
		animation = true;
		AnimationTime = 2;
		Solution.fontSize = 40;
		Message.transform.localScale = new Vector3 (0.8f, 0.8f, 1f);
		if (correct) {
			SendGmail.TestString+=",Correct";
			switch (answer) {
			case 0:
				Solution.text = LangTest.LMan.getString ("YesSame");
				break;
			case 1:
				Solution.text = LangTest.LMan.getString ("YesDiff");
				break;
			}
			Solution.gameObject.SetActive (true);
			Solution.GetComponent<Animator> ().SetBool ("Correct", true);
			currentValue += points;
		} 
		else {
			SendGmail.TestString+=",Failed";
			switch (answer) {
			case 2:
				Solution.text = LangTest.LMan.getString ("NoDiff");
				break;
			case 3:
				Solution.text = LangTest.LMan.getString ("NoSame");
				break;
			}
			Solution.gameObject.SetActive (true);
			Solution.GetComponent<Animator> ().SetBool ("Wrong", true);
		}
		SendGmail.TestString+=","+ TimeLeft.ToString();
	}

	public void NextBox()
	{
			Solution.color = Color.black;
			TaparN.gameObject.SetActive (false);
			Message.gameObject.SetActive (false);
			Solution.gameObject.SetActive (false);
			Unfold.ShowExpl = false;
		TimeSlider.gameObject.SetActive (false);
		PointText.text="+ "+((int)(TimeLeft*10)).ToString () + LangTest.LMan.getString ("Points");
		if (TimeLeft < 40) {
			StarP3.color= StarOff;
		}
		if (TimeLeft < 20) {
			StarP2.color= StarOff;
		}
		if (TimeLeft<1) {
			StarP1.color= StarOff;
		}
		Bag.gameObject.SetActive (true);
		NextBoxB.gameObject.SetActive (true);
			//GameObject.Find ("Reward 1").GetComponent<Points> ().RewardAnimation (TimeLeft);
	}

	public void PostReward()
	{
		
		if (Cube.Test < 15) { //In final version probably 10, for testing 5
			//if (!Points.exchange) { // If animation is over
			GameObject.Find ("Camera").GetComponent<Cube> ().Restart ();
			//}
		} else {
			//GameObject.Find ("Lenguage").GetComponent<SendGmail> ().Send ();
			//GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteTest ("Questionnaire");
			FondoQuest.gameObject.SetActive (true);
			PregQuest.SetActive (true);
			ExplQuest.text = LangTest.LMan.getString ("ExplQuest");
			YesQuest.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("YesQuest");
			NoQuest.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NoQuest");
			Cube.help = false;
			//GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (TotalTime.ToString());
			//Application.LoadLevel ("Questionnaire");
		}
	}

	public void Explicacion()
	{
		Message.transform.localScale = new Vector3 (1f, 1f, 1f);
		if (Cube.help) { //Set at the beggining of the test
			Unfold.button=true;
			GameObject.Find ("CubePl").GetComponent<Unfold> ().finalRotation= Unfold.AfterRandom;
			Unfold.moving = true;
		}
		
		//animation = false;
		TaparE.gameObject.SetActive (false);

		Bag.gameObject.SetActive (false);
		Solution.fontSize = 25;
		if (answer < 2) {
			Solution.GetComponent<Animator> ().SetBool ("Correct", false);
			PregCorrect ();
		} else {
			TaparN.gameObject.SetActive (true);
			Solution.GetComponent<Animator> ().SetBool ("Wrong", false);
			if (LangTest.VisualFeedback) {
				if (answer.Equals (3)) { //Explanation Same cube
					Solution.text = LangTest.LMan.getString ("NoFaceSame");

					if (!SameCube.Fx.symbol.Equals ("NoFaceSame")) {

						if (Unfold.Fold.Equals (1)) {
							GameObject.Find ("CubePl").GetComponent<Unfold> ().UnfoldBox ();
						}

						Cube.help = false;
						Solution.text = LangTest.LMan.getString (SameCube.Fx.symbol);
						GameObject.Find ("CubePl").GetComponent<Unfold> ().CreateWay (true);
						CreateArrows ();
					}
				} else {
					Cube.help = true;
					if (Cube.change.Equals (0)) {
						Solution.text = LangTest.LMan.getString ("DiffBecause") + LangTest.LMan.getString (TradLocaton (SameCube.Fx.localization)) + LangTest.LMan.getString ("DontMatchSym");
					} else {
						Solution.text = LangTest.LMan.getString ("DiffBecause") + LangTest.LMan.getString (TradLocaton (SameCube.Fx.localization)) + LangTest.LMan.getString ("DontMatchOri");
					}
					GameObject.Find ("Camera").GetComponent<Cube> ().GBox [SameCube.Fx.localization].GetComponent<Animator> ().SetBool ("Highlight", true);
					GameObject.Find ("Camera").GetComponent<Cube> ().OGbox [SameCube.Fx.localization].GetComponent<Animator> ().SetBool ("Highlight", true);
					if (Unfold.Fold.Equals (0)) {
						GameObject.Find ("CubePl").GetComponent<Unfold> ().UnfoldBox ();
					}
					GameObject.Find ("CubePl Sin Codigo (L)").GetComponent<Animator> ().SetBool ("Unfold", true);
				}
			} else {
				NextBox ();
			}
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

	public static string TradLocaton(int l)
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
		way = expl.Split ('_');

		for (int i = 0; i < way.Length-1; i++) {
			string Move = way [way.Length-2-i];
			MArrows [i].gameObject.SetActive (true);
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

	public void NoQuestMethod()
	{
		Application.LoadLevel ("Map Select Level");
	}
	public void YesQuestMethod()
	{
		SendGmail.TestString+=","+ TotalTime.ToString();
		Application.LoadLevel ("Questionnaire");
	}

	public void PregCorrect()
	{
		Cube.help = false;
		if ((SameCube.SameSymbols > 0) && (SameCube.SameSymbols < 3)&& (answer.Equals(0))) {
			Solution.text = LangTest.LMan.getString ("PregCorrect");
			PregCorrectPanel.gameObject.SetActive (true);
			BackC.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Back");
			DownC.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Down");
			LeftC.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Left");
			GameObject.Find ("Camera").GetComponent<Cube> ().GBox [SameCube.Tquest.orientation].GetComponent<Animator> ().SetBool ("Training", true);
			GameObject.Find ("Camera").GetComponent<Cube> ().OGbox [SameCube.Tquest.orientation].GetComponent<Animator> ().SetBool ("Training", true);
			Debug.Log ("Pregunta");
		} 
		else {
			NextBox ();
		}
	}

	public void BonusAnswer(int i)
	{
		Cube.help = true;
		PregCorrectPanel.gameObject.SetActive (false);
		if (i.Equals (SameCube.Tquest.localization)) {
			SendGmail.TestString += ",Correct";
			Solution.text = LangTest.LMan.getString ("Bonus");
		} else {
			SendGmail.TestString+=",Failed";
			Solution.text = LangTest.LMan.getString ("NoBonus");
		}
		TaparN.gameObject.SetActive (true);
	}
		
}
