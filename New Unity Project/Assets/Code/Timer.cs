using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	// Use this for initialization
	public static float TimeLeft, AnimationTime;
	public Slider TimeSlider;
	public static bool start, end, animation, BonusCorrect, HelpPressed, BonusQuestion;
	public static float TimeLevel;
	public static int pointsLevel, points;
	public int currentValue, answer, ContExpl;
	public int moneySpeed;
	public Text Solution, PointText, ExplQuest;
	public Image TimeFill,Bag, FondoQuest;
	public Button TaparN, TaparE, NextBoxB, YesQuest, NoQuest, BackC,DownC,LeftC,StartTestButton,TrasBonusPreg, ButtonPath;
	public Image Message, StarSlid1, StarSlid2, StarSlid3, StarP1, StarP2, StarP3;
	public Image[] MArrows,Arrows;
	public Color StarOn, StarOff;
	public Sprite[] ArrowsSp;
	public Button[] Explicaciones;
	public float TotalTime;
	public  bool CorrectAnswer;
	public GameObject PregQuest, PregCorrectPanel;
	void Start () {

		//Empezamos a escribir los datos.
		SendGmail.TestString +=SystemInfo.deviceUniqueIdentifier+","+System.DateTime.Now.ToString("dd/MM/yyyy")+","+System.DateTime.Now.ToString("hh:mm:ss")+","+LangTest.Help+","+LangTest.VisualFeedback+",";
		SendGmail.TestScore = 0;
		SendGmail.TestCorrAns = 0;
		SendGmail.TestAvgTime = 0;



		for (int i = 0; i < Explicaciones.Length; i++) {
			Explicaciones[i].gameObject.SetActive (false);
		}
		ButtonPath.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Path");
		TaparN.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("SeePoints");
		TaparE.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
		TrasBonusPreg.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
		moneySpeed = 20;
		ExplHelp ();
		end = true;
		StartTestButton.gameObject.SetActive (true);
		StartTestButton.GetComponentInChildren<Text> ().fontSize = 20;
		ContExpl = 0;
		StartTestButton.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Continue");
			Solution.fontSize = 20;
			Solution.text = LangTest.LMan.getString ("ExplTest1");
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
		if ((TimeLeft < 0)&&(!end)) {
			end = true;
			TimeUp ();
		}

		}

	void ExplHelp()
	{
		ButtonPath.gameObject.SetActive (false);
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
		//pointsLevel = 10;
		//points = pointsLevel;
		TimeLeft = TimeLevel;
		TimeSlider.maxValue = TimeLeft;
		TimeSlider.value = TimeSlider.maxValue;
		TaparN.gameObject.SetActive (false);
		TrasBonusPreg.gameObject.SetActive (false);
		TaparE.gameObject.SetActive (false);
		NextBoxB.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NextBox");
		NextBoxB.gameObject.SetActive (false);
		animation = false;
		foreach (Image i in MArrows) {
			i.GetComponent<CanvasGroup>().alpha=0;
		}
	}

	void SetTimer()
	{
		BonusQuestion = false;
		CorrectAnswer = false;
		HelpPressed = false;
		BonusCorrect = false;
		ButtonPath.gameObject.SetActive (false);	
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
		//pointsLevel = 10;
		//points = pointsLevel;
		TimeLeft = TimeLevel;
		TimeSlider.maxValue = TimeLeft;
		TimeSlider.value = TimeSlider.maxValue;
		TaparN.gameObject.SetActive (false);
		TaparE.gameObject.SetActive (false);
		NextBoxB.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NextBox");
		NextBoxB.gameObject.SetActive (false);
		animation = false;
		end = false;
		//Solution.gameObject.SetActive (false);
		foreach (Image i in MArrows) {
			i.GetComponent<CanvasGroup>().alpha=0;
		}
		//points = pointsLevel;
	}

	public void TimeUp()
	{
		StopSetHelp ();
		TimeLeft = 0;
		GameObject.Find ("Camera").GetComponent<Cube> ().Hide ();
		Solution.fontSize = 20;
		string resp;
		if (Cube.change.Equals (1)) {
			answer = 3;
			resp = "SameTestTimeUp";
		} else {
			answer = 2;
			resp = "DiffTestTimeUp";
		}
		Message.gameObject.SetActive (true);
		TaparE.gameObject.SetActive (true);
		Solution.GetComponent<Animator> ().SetBool ("Wrong", true);
		if (LangTest.VisualFeedback) {
			Solution.text = LangTest.LMan.getString (resp)+" "+LangTest.LMan.getString ("TryHelp");
		} 
		else {
			Solution.text = LangTest.LMan.getString (resp);
		}


		Unfold.Info="";
	}

	public void Animation(bool correct,int answer) // Calcular puntuacion sin bonus
	{
		StopSetHelp ();
		Unfold.Info="";
		this.answer = answer;
		Message.gameObject.SetActive (true);
		TaparE.gameObject.SetActive (true);
		animation = true;
		AnimationTime = 2;
		CorrectAnswer = correct;

		if (correct) {
			Solution.fontSize = 30;
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
			//currentValue += points;
		} 
		else {
			Solution.fontSize = 35;
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
	}

	public void NextBox() //Calcular los puntos y enviar datos
	{
		
			Solution.color = Color.black;
			TaparN.gameObject.SetActive (false);
			Message.gameObject.SetActive (false);
			//Solution.gameObject.SetActive (false);
			Unfold.ShowExpl = false;
		TimeSlider.gameObject.SetActive (false);
		int ContCorrect = 0;
		float HelpReductor = 0;
		int PlusBonus = 0;
		if (CorrectAnswer) {
			ContCorrect = 1;
		}
		if (HelpPressed) { //usar la ayuda quita un sexo
			HelpReductor = 1 / 6f;
		}
		if (BonusCorrect) {
			PlusBonus = 50;
		}
		SendGmail.TestCorrAns += ContCorrect;
		SendGmail.TestAvgTime += 60-TimeLeft;
		SendGmail.TestScore += (int)((((TimeLeft - (TimeLeft * HelpReductor)) * 10)+PlusBonus) * ContCorrect);
		SendGmail.TestString += Cube.Test.ToString() +","+ Cube.expChange +","+ HelpPressed +","+ CorrectAnswer +","+(60-TimeLeft).ToString()+","+ BonusQuestion +","+ BonusCorrect+",";

		PointText.text="+"+((int)((((TimeLeft - (TimeLeft * HelpReductor)) * 10)+PlusBonus) * ContCorrect)).ToString () +" "+ LangTest.LMan.getString ("Points");
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
			//FondoQuest.gameObject.SetActive (true);
			//PregQuest.SetActive (true);
			//ExplQuest.text = LangTest.LMan.getString ("ExplQuest");
		//	YesQuest.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("YesQuest");
		//	NoQuest.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NoQuest");
			SendGmail.Level=2;
			Cube.help = false;
			SendGmail.TestString += SendGmail.TestAvgTime+",";
			//GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (TotalTime.ToString());
			Application.LoadLevel ("Score");
		}
	}
		

	public void Explicacion()
	{


			
		if (Cube.help) { //Set at the beggining of the test
			Unfold.button=true;
			GameObject.Find ("CubePl").GetComponent<Unfold> ().finalRotation= Unfold.AfterRandom;
			Unfold.moving = true;
		}
		
		//animation = false;
		TaparE.gameObject.SetActive (false);
		Bag.gameObject.SetActive (false);
		Solution.fontSize = 25;
		if (answer.Equals(0)) {
			Solution.GetComponent<Animator> ().SetBool ("Correct", false);
			PregCorrect ();
		} else {
			TaparN.gameObject.SetActive (true);
			Solution.GetComponent<Animator> ().SetBool ("Wrong", false);
			if (LangTest.VisualFeedback) {
				if (answer.Equals (3)) { //Explanation Same cube
					Solution.text = LangTest.LMan.getString ("NoFaceSame");

					if (!SameCube.Fx.explicacion.Equals ("NoFaceSame")) {

						if (Unfold.Fold.Equals (1)) {
							GameObject.Find ("CubePl").GetComponent<Unfold> ().UnfoldBox ();
						}

						Cube.help = false;
						Solution.text = LangTest.LMan.getString (SameCube.Fx.explicacion);
						GameObject.Find ("CubePl").GetComponent<Unfold> ().CreateWay (true);
						CreateArrows (SameCube.Way);
					}
				} else {
					if (answer.Equals (1)) {
						Solution.GetComponent<Animator> ().SetBool ("Correct", false);
					}
					//Cube.help = true;
					if (Cube.change.Equals (0)) {
						Solution.text = LangTest.LMan.getString ("DiffBecause")+ LangTest.LMan.getString (SameCube.Fx.symbol) +" "+ LangTest.LMan.getString ("DiffBecause2")+" "+  LangTest.LMan.getString (TradLocaton (SameCube.Fx.localization)) +" "+ LangTest.LMan.getString ("DontMatchSym")+" "+LangTest.LMan.getString (Cube.SideWithChange.symbol) +".";
					} else {
						Solution.text = LangTest.LMan.getString ("DiffBecause")+ LangTest.LMan.getString (SameCube.Fx.symbol)+" "+ LangTest.LMan.getString ("DiffBecause2")+" "+ LangTest.LMan.getString (TradLocaton (SameCube.Fx.localization)) +" "+ LangTest.LMan.getString ("DontMatchOri");
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

	void CreateArrows(string path)
	{
		ButtonPath.gameObject.SetActive (true);
		string[] way;
		way = path.Split ('_');

		for (int i = 0; i < way.Length-1; i++) {
			string Move = way [way.Length-2-i];
			MArrows [i].GetComponent<CanvasGroup>().alpha =1;
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
		Application.LoadLevel ("Questionnaire");
	}

	public void PregCorrect()
	{
		Cube.help = false;
		if ((SameCube.SameSymbols > 0) && (SameCube.SameSymbols < 3)&& (answer.Equals(0))) {
			Solution.text = LangTest.LMan.getString ("PregCorrect")+" "+LangTest.LMan.getString (SameCube.Tquest.symbol)+"?";
			PregCorrectPanel.gameObject.SetActive (true);
			BackC.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Back");
			DownC.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Down");
			LeftC.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Left");
			GameObject.Find ("Camera").GetComponent<Cube> ().GBox [SameCube.Tquest.orientation].GetComponent<Animator> ().SetBool ("Training", true);
			GameObject.Find ("Camera").GetComponent<Cube> ().OGbox [SameCube.Tquest.orientation].GetComponent<Animator> ().SetBool ("Training", true);
			Debug.Log ("Pregunta");
		} 
		else {
			answer = 3;
			Explicacion();
		}
	}

	public void BonusAnswer(int i)
	{
		BonusQuestion = true;
		PregCorrectPanel.gameObject.SetActive (false);
		if (i.Equals (SameCube.Tquest.localization)) {
			BonusCorrect = true;
			Solution.text = LangTest.LMan.getString ("Bonus")+" "+LangTest.LMan.getString(TradLocaton(SameCube.Tquest.localization))+".";
		} else {
			Solution.text = LangTest.LMan.getString ("NoBonus")+" "+LangTest.LMan.getString(TradLocaton(SameCube.Tquest.localization))+".";
		}

		string BonusPath=" ";
		switch (SameCube.Tquest.localization) {

		case 1:
			BonusPath = "Left_Left_Restart";
			break;
		case 3:
			BonusPath = "Up_Restart";
			break;
		case 5:
			BonusPath = "Right_Restart";
			break;
		default:
			break;
		}

		GameObject.Find ("CubePl").GetComponent<Unfold> ().CreateBonusWay (BonusPath);
		CreateArrowsBonus (BonusPath);
		answer = 3;
		TrasBonusPreg.gameObject.SetActive (true);
	}
	void CreateArrowsBonus(string path)
	{
		ButtonPath.gameObject.SetActive (true);
		string[] way;
		way = path.Split ('_');

		for (int i = 0; i < way.Length-1; i++) {
			string Move = way [way.Length-2-i];
			MArrows [i].GetComponent<CanvasGroup>().alpha =1;
			switch (Move) { //Do the opposite Move
			case "Up":
				Arrows [i].sprite = ArrowsSp [0];
				break;
			case "Down":
				Arrows [i].sprite = ArrowsSp [1];
				break;
			case "Left":
				Arrows [i].sprite = ArrowsSp [2];
				break;
			case "Right":
				Arrows [i].sprite = ArrowsSp [3];
				break;
			case "Toward-up-right":
				Arrows [i].sprite = ArrowsSp [4];
				break;
			case "Toward-up-left":
				Arrows [i].sprite = ArrowsSp [5];
				break;
			}
		}
	}

	public void TrasBonus()
	{
		GameObject.Find ("CubePl").GetComponent<Unfold> ().SetAfterBonus ();
		GameObject.Find ("Camera").GetComponent<Cube> ().GBox [SameCube.Tquest.orientation].GetComponent<Animator> ().SetBool ("Training", false);
		GameObject.Find ("Camera").GetComponent<Cube> ().OGbox [SameCube.Tquest.orientation].GetComponent<Animator> ().SetBool ("Training", false);

		foreach (Image i in MArrows) {
			i.GetComponent<CanvasGroup>().alpha=0;
		}
		TrasBonusPreg.gameObject.SetActive (false);
		Explicacion ();
	}

	public void Sethelp() //La ayuda resalta los simbolos que tienen que ver con el cambio
	{
		HelpPressed = true;
		Solution.fontSize = 25;
		GameObject.Find ("Camera").GetComponent<Cube> ().BHelp.gameObject.GetComponent<CanvasGroup>().interactable=false;
		if (SameCube.SameSymbols > 0) {
			Message.gameObject.SetActive (true);
			Face pista = GameObject.Find ("Camera").GetComponent<SameCube> ().So1;
			GameObject.Find ("Camera").GetComponent<Cube> ().GBox [pista.localization].GetComponent<Animator> ().SetBool ("Training", true);
			GameObject.Find ("Camera").GetComponent<Cube> ().OGbox [pista.localization].GetComponent<Animator> ().SetBool ("Training", true);
			Solution.text = LangTest.LMan.getString ("SameSymbolHintPart1")+" "+LangTest.LMan.getString (pista.symbol)+" "+LangTest.LMan.getString ("SameSymbolHintPart2");
		} 
		else {
			Message.gameObject.SetActive (true);
			Solution.text = LangTest.LMan.getString ("NoSameSymbolHint");
		}
		Invoke("StopSetHelp", 3);
	}


	public void StopSetHelp()
	{
		if(!end){
			GameObject.Find ("Camera").GetComponent<Cube> ().BHelp.gameObject.GetComponent<CanvasGroup>().interactable = true;
			Message.gameObject.SetActive (false);
		}
		if (SameCube.SameSymbols > 0) {
			Face pista = GameObject.Find ("Camera").GetComponent<SameCube> ().So1;
			GameObject.Find ("Camera").GetComponent<Cube> ().GBox [pista.localization].GetComponent<Animator> ().SetBool ("Training", false);
			GameObject.Find ("Camera").GetComponent<Cube> ().OGbox [pista.localization].GetComponent<Animator> ().SetBool ("Training", false);
		}
	}



		public void ExplainTest()
		{
			switch(ContExpl)
			{
		case 0: //Explica Simbolos differents
			Solution.text = LangTest.LMan.getString ("ExplTest2");
			GameObject.Find ("Camera").GetComponent<Cube> ().CubePL.GetComponent<Unfold> ().UnfoldBox ();
			GameObject.Find ("CubePl Sin Codigo (L)").GetComponent<Animator> ().SetBool ("Unfold", true);
			ContExpl++;
			break;
		case 1: //Explicar Caras visibles
			Solution.text = LangTest.LMan.getString ("ExplTest3");
			GameObject.Find ("Camera").GetComponent<Cube> ().CubePL.GetComponent<Unfold> ().UnfoldBox ();
			GameObject.Find ("CubePl Sin Codigo (L)").GetComponent<Animator> ().SetBool ("Unfold", false);
			for (int i = 0; i < 6; i++) {
				Explicaciones [i].gameObject.SetActive (true);
			}
			Explicaciones[0].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Front");
			Explicaciones[5].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Front");
			Explicaciones[1].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Up");
			Explicaciones[4].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Up");
			Explicaciones[2].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Right");
			Explicaciones[3].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("Right");
			ContExpl++;
			break;
		case 2: //Explicar Cofre izq
			for (int i = 0; i < 6; i++) {
				Explicaciones [i].gameObject.SetActive (false);
			}
			Explicaciones[6].gameObject.SetActive (true);
			Solution.text = LangTest.LMan.getString ("ExplTest4");
			ContExpl++;
			break;
		case 3: //Explicar Cofre Der
			Explicaciones[6].gameObject.SetActive (false);
			Explicaciones[7].gameObject.SetActive (true);
			Solution.text = LangTest.LMan.getString ("ExplTest5");
			ContExpl++;
			break;
		case 4: //Explicar Que pueden girar
			Explicaciones[7].gameObject.SetActive (true);
			Solution.text = LangTest.LMan.getString ("ExplTest6");
			GameObject.Find ("Camera").GetComponent<Cube> ().CubePL.GetComponent<Unfold> ().MoveUpRight90 ();
			ContExpl++;
			break;
		case 5: //Explicar Timer
			Explicaciones[7].gameObject.SetActive (false);
			Explicaciones[8].gameObject.SetActive (true);
			Explicaciones[8].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("ExplTest7");
			Message.gameObject.SetActive (false);
			ContExpl++;
			break;
		case 6: //Explicar Same
			Explicaciones[8].gameObject.SetActive (false);
			Explicaciones[9].gameObject.SetActive (true);
			Explicaciones[9].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("ExplTest8");
			ContExpl++;
			break;
		case 7: //Explicar Different
			Explicaciones[9].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("ExplTest9");
			ContExpl++;
			break;
		case 8: //Explicar Ayuda
			ContExpl++;
			if (LangTest.Help) {
				Explicaciones [9].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("ExplTestHelp");
			} 
			else {
				ExplainTest ();
			}
			break;
		case 9: //Explicar empezar juego
			Explicaciones[9].gameObject.SetActive (false);
			Message.gameObject.SetActive (true);
			StartTestButton.GetComponentInChildren<Text> ().text=LangTest.LMan.getString ("TrainingB0");
			Solution.text = LangTest.LMan.getString ("ExplTest10");
			ContExpl++;
			break;
		case 10: //Empezar juego
			Message.gameObject.SetActive (false);
			StartTestButton.gameObject.SetActive (false);
			GameObject.Find ("Camera").GetComponent<Cube> ().Restart ();
			break;
			}

			
		}
}
