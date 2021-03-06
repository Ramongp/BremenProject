﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockTimer : MonoBehaviour {

	public float TimeLeft, maxTime;
	public Slider TimeSlider;
	public Button Tapar, TaparNextLevel, NextBoxB, ExplainButton, Pista;
	public GameObject Key;
	public Text Text, PointText, PathText;
	public ParticleSystem Correct;
	public Image StarSlid1, StarSlid2, StarSlid3, StarP1, StarP2, StarP3, Bag, Message;
	public static bool testing;
	public GameObject Buttons;
	public Color StarOn,StarOff;
	public int correctLock;
	public int ContExpl;
	public Button[] ExplButtons;
	public static bool PressedRestart, PressedHint;
	// Use this for initialization
	void Start () {

		//Empezamos a escribir los datos.
		SendGmail.LockString+=SystemInfo.deviceUniqueIdentifier+","+System.DateTime.Now.ToString("dd/MM/yyyy")+","+System.DateTime.Now.ToString("hh:mm:ss")+","+LangTest.Help+",";
		SendGmail.LockScore = 0;
		SendGmail.LockCorrAns = 0;
		SendGmail.LockAvgTime = 0;
		SendGmail.TrainingDone = true;
		SendGmail.Level = 1;

		Pista.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("HintButton");
		ExplainButton.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("Continue");
		Pista.gameObject.SetActive (false);
		Bag.gameObject.SetActive (false);
		NextBoxB.gameObject.SetActive (false);
		TaparNextLevel.gameObject.SetActive (false);
		TimeSlider.gameObject.SetActive (true);
		Tapar.gameObject.SetActive (false);
		Text.gameObject.SetActive (false);
		Correct.gameObject.SetActive (false);
		maxTime = 60;
		TimeLeft = maxTime;
		TimeSlider.maxValue = maxTime;
		TimeSlider.value = maxTime;
		Buttons.SetActive (true);
		StarSlid1.color= StarOn;
		StarSlid2.color= StarOn;
		StarSlid3.color= StarOn;
		StarP1.color= StarOn;
		StarP2.color= StarOn;
		StarP3.color= StarOn;
		GameObject.Find("Way").GetComponentInChildren<Text>().text= LangTest.LMan.getString ("GiveUpButton");
		GameObject.Find("Reset").GetComponentInChildren<Text>().text= LangTest.LMan.getString ("ResetButton");
		GameObject.Find ("Way").GetComponent<CanvasGroup> ().interactable = false;
		GameObject.Find ("Reset").GetComponent<CanvasGroup> ().interactable = false;
		ExplButtons[0].GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LocKExplTimer");
		ExplButtons[1].GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockExplPath");
		ExplButtons[2].GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockExplButtons1");
		ExplButtons [0].gameObject.SetActive (false);
		ExplButtons [1].gameObject.SetActive (false);
		ExplButtons [2].gameObject.SetActive (false);
		ExplainButton.gameObject.SetActive (true);
		ContExpl = 0;
		Message.gameObject.SetActive (true);
		Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockExpl1");
		PathText.text = LangTest.LMan.getString ("Path");
		Tapar.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("SeePoints");
		//Set ();
	}
	
	// Update is called once per frame
	void Update () {
		if (testing) {
			TimeLeft -= Time.deltaTime;
			TimeSlider.value = TimeLeft;
			if (TimeLeft < 40) {
				StarSlid1.color= StarOff;
			}
			if (TimeLeft < 20) {
				string[] check = LockCube.move.Split ('_');
				if (check.Length > 2 && LangTest.Help) {
					Pista.gameObject.SetActive (true);
				}
				StarSlid2.color= StarOff;
			}
			if (TimeLeft<1) {
				StarSlid3.color= StarOff;
			}
			if (TimeLeft < 0) {
				if (Unfold.Fold.Equals (1)) {
					GameObject.Find ("Main Camera").GetComponent<LockCube> ().UnfoldButton();
				}
				TimeUp ();
			}
		}
	}

	public void  Pass()
	{
		LockCube.help = false;
		correctLock = 1;
		testing = false;
		Correct.gameObject.SetActive (true);
		Tapar.interactable = true; 
		Tapar.gameObject.SetActive (true);
		Text.gameObject.SetActive (true);
		Text.text = LangTest.LMan.getString ("PassedLockTest");
		Key.gameObject.GetComponent<Animator> ().SetTrigger ("Passed");
		Buttons.SetActive (false);
		Pista.gameObject.SetActive (false);


		
	}
	public void  GiveUp()
	{
		LockCube.help = false;
		Message.gameObject.SetActive (true);
		Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("GiveUp");
		correctLock = 0;
		testing = false;
		//Correct.gameObject.SetActive (true);
		Tapar.interactable = true; 
		Tapar.gameObject.SetActive (true);
		//Text.gameObject.SetActive (true);
		//Text.text = LangTest.LMan.getString ("PassedLockTest");
		//Key.gameObject.GetComponent<Animator> ().SetTrigger ("Passed");
		Buttons.SetActive (false);
		Pista.gameObject.SetActive (false);



	}

	public void TimeUp()
	{
		LockCube.NoTime = true;
		LockCube.help = false;
		Message.gameObject.SetActive (true);
		Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockTimeUp")+" "+LangTest.LMan.getString ("GiveUp");
		GameObject.Find ("Main Camera").GetComponent<LockCube> ().ShowWay (false);
		correctLock = 0;
		testing = false;
		Tapar.interactable = true; 
		Tapar.gameObject.SetActive (true);
		Buttons.SetActive (false);
		Pista.gameObject.SetActive (false);
		
	}
	public void Points()   //Se calcula la puntuacion y se escriben los datos
	{
		Message.gameObject.SetActive (false);
		Unfold.ShowExpl = false;
		Tapar.interactable = false; 
		//	GameObject.Find ("Reward").GetComponent<Points> ().RewardAnimation (TimeLeft);
		float hint = 0;
		float PrRestat =0;
		if (PressedHint) {
			hint = 1/2f;
		}
		if (PressedRestart) {
			PrRestat = 1/6f;
		}

		SendGmail.LockCorrAns += correctLock;
		SendGmail.LockAvgTime += 60 - TimeLeft;
		SendGmail.LockScore += (int)((TimeLeft - (hint*TimeLeft) - (PrRestat*TimeLeft))*10* correctLock);
		SendGmail.LockString += LockCube.Test+"," + PressedHint+"," + PressedRestart+"," + LockCube.move +","+(60-TimeLeft).ToString()+",";

		PointText.text="+ "+((int)((TimeLeft - (hint*TimeLeft) - (PrRestat*TimeLeft))*10* correctLock)).ToString () +" " + LangTest.LMan.getString ("Points");
		if (TimeLeft*correctLock < 40) {
			StarP3.color= StarOff;
		}
		if (TimeLeft*correctLock < 20) {
			StarP2.color= StarOff;
		}
		if (TimeLeft*correctLock<1) {
			StarP1.color= StarOff;
		}

		NextBoxB.gameObject.SetActive (true);
		Bag.gameObject.SetActive (true);
			TimeSlider.gameObject.SetActive (false);
			//Text.gameObject.SetActive (false);
			Tapar.gameObject.SetActive (false);

	}
	public void Set()
	{
		if (LockCube.Test < 10) {
			PressedHint = false;
			PressedRestart = false;
			Pista.gameObject.SetActive (false);
			Bag.gameObject.SetActive (false);
			NextBoxB.gameObject.SetActive (false);
			Message.gameObject.SetActive (false);
			TaparNextLevel.gameObject.SetActive (false);
			TimeSlider.gameObject.SetActive (true);
			Tapar.gameObject.SetActive (false);
			Text.gameObject.SetActive (false);
			Correct.gameObject.SetActive (false);
			testing = true;
			maxTime = 60;
			TimeLeft = maxTime;
			TimeSlider.maxValue = maxTime;
			TimeSlider.value = maxTime;
			Buttons.SetActive (true);
			GameObject.Find ("Way").GetComponent<CanvasGroup> ().interactable = true;
			GameObject.Find ("Reset").GetComponent<CanvasGroup> ().interactable = true;
			StarSlid1.color= StarOn;
			StarSlid2.color= StarOn;
			StarSlid3.color= StarOn;
			StarP1.color= StarOn;
			StarP2.color= StarOn;
			StarP3.color= StarOn;
			NextBoxB.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NextBox");
		} else {
			LockCube.help = false;
			GameObject.Find ("Main Camera").GetComponent<LockCube> ().HideArrows ();
			Bag.gameObject.SetActive (false);
			NextBoxB.gameObject.SetActive (false);
			Message.gameObject.SetActive (false);
			TaparNextLevel.gameObject.SetActive (true);
			Buttons.SetActive (false);
			SendGmail.LockString += SendGmail.LockAvgTime + ",";
			TaparNextLevel.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("PassedLockTestLevel");
		}
	}
	public void nextLvl()
	{
		Application.LoadLevel ("Score");	
	}
	public void ExplainTest()
	{
		switch (ContExpl) {

		case 0: //explicar Orient
			Message.gameObject.SetActive (true);
			Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockExpl2");
			ContExpl++;
			break;
		case 1: //explicar timer
			ExplButtons [0].gameObject.SetActive (true);
			Message.gameObject.SetActive (false);
			ContExpl++;
			break;
		case 2: //explicar Path
			ExplButtons [0].gameObject.SetActive (false);
			ExplButtons [1].gameObject.SetActive (true);
			ContExpl++;
			break;
		case 3: //explicar Rendirse
			ExplButtons [1].gameObject.SetActive (false);
			ExplButtons [2].gameObject.SetActive (true);
			Message.gameObject.SetActive (false);
			ContExpl++;
			break;
		case 4: //explicar Deshacer
			ExplButtons [2].gameObject.SetActive (true);
			ExplButtons [2].GetComponentInChildren<Text>().text=LangTest.LMan.getString ("LockExplButtons2");
			Message.gameObject.SetActive (false);
			ContExpl++;
			break;
		case 5: //explicar Pista Solo si tiene ayuda activada
			ContExpl++;
			if (LangTest.Help) {
				ExplButtons [2].gameObject.SetActive (true);
				ExplButtons [2].GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("LockExplButtons3");
				Message.gameObject.SetActive (false);
			} else {
				ExplainTest ();
			}
			break;
		case 6: //explicar Desplegar
			ExplButtons [2].gameObject.SetActive (false);
			Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockExpl3");
			Message.gameObject.SetActive (true);
			ContExpl++;
			break;
		case 7: //Explicar empezar LockTraining
			ExplButtons [2].gameObject.SetActive (false);
			Message.gameObject.SetActive (true);
			Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockExpl4");
			ContExpl++;
			break;
		case 8: //Empezar LockTraining
			Message.gameObject.SetActive (false);
			ExplainButton.gameObject.SetActive (false);
			GameObject.Find ("Main Camera").GetComponent<LockCube> ().Set ();
			break;
		}

	}

	public void NeedToReset(bool lockinFront)
	{
		LockCube.help = false;
		LockCube.WrongPath = true;
		Message.gameObject.SetActive (true);
		if (lockinFront) {
			Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockEWrongOrient");
		} else {
			Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockWrongSide");
		}
	}
}
