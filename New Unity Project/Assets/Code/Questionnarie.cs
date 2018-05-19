using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class Questionnarie : MonoBehaviour {

	// Use this for initialization
	public GameObject Panel0,Panel1, Panel4,Panel5;   // 0=Gender, 1= Age, 2= Nationality, 3 =Native lenguage, 4 = level of study, 5= fiel of study;
	int cont0,cont1,cont4,cont5;
	public Text Question0,Question1,Question2,Question3,Question4,Question5,Answ0, Answ1,Answ4,Answ5,RateGameText,RateHelpText,RateFeedbackText;
	public string LangA0,LangA1,Answ2, Answ3,LangA4,LangA5;
	public Button[] Q0Answ,Q1Answ, Q2Answ, Q3Answ;
	public string[] StringsQ0=new string[]{"Male","Female","NoGender"},  StringsQ2 = new string[]{"Estudios0","Estudios1","Estudios2","Estudios3","Estudios4","Estudios5"},StringsQ3 = new string[]{"Rama0","Rama1","Rama2","Rama3","Rama4","Rama5"};
	public Image[] StarsFun, StarsHelp, StarsFeed;
	public Slider RateFunSlider, RateHelpSlider, RateFeedSlider;
	public Button Send;
	public GameObject Rama;
	void Start () {
		Rama.GetComponent<CanvasGroup>().alpha=0;
		Rama.GetComponent<CanvasGroup>().blocksRaycasts=false;
		Panel0.SetActive (false);
		Panel1.SetActive (false);
		Panel4.SetActive (false);
		Panel5.SetActive (false);
		Question0.text=LangTest.LMan.getString ("Question0");
		Question1.text=LangTest.LMan.getString ("Question1");
		Question2.text=LangTest.LMan.getString ("Question2");
		Question3.text=LangTest.LMan.getString ("Question3");
		Question4.text=LangTest.LMan.getString ("Question4");
		Question5.text=LangTest.LMan.getString ("Question5");
		RateGameText.text=LangTest.LMan.getString ("RateGame");
		RateHelpText.text=LangTest.LMan.getString ("RateHelpText");
		RateFeedbackText.text=LangTest.LMan.getString ("RateFeedbackText");
		Send.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("Send");
		//Poner a los botones el texto de Lang
		for (int i = 0; i < StringsQ0.Length; i++) {
			Q0Answ[i].GetComponentInChildren<Text>().text= LangTest.LMan.getString (StringsQ0[i]);
		}
		for (int i = 0; i < StringsQ2.Length; i++) {
			Q2Answ[i].GetComponentInChildren<Text>().text= LangTest.LMan.getString (StringsQ2[i]);
			Q3Answ[i].GetComponentInChildren<Text>().text= LangTest.LMan.getString (StringsQ3[i]);
		}
		Answ0.text = Q0Answ [0].GetComponentInChildren<Text> ().text;
		Answ1.text = Q1Answ [0].GetComponentInChildren<Text> ().text;
		Answ4.text = Q2Answ [0].GetComponentInChildren<Text> ().text;
		Answ5.text = Q3Answ [0].GetComponentInChildren<Text> ().text;


		if (LangTest.Help) {
			RateHelpSlider.gameObject.SetActive (true);
		} else {
			RateHelpSlider.gameObject.SetActive (true);
		}
		if (LangTest.VisualFeedback) {
			RateFeedSlider.gameObject.SetActive (true);
		} else {
			RateFeedSlider.gameObject.SetActive (true);
		}
		//Cambiar de idioma los botones de nivel estudios y rama
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Show(int i)
	{
		switch (i) {
		case 0:
			if (cont0.Equals (0)) {
				Panel0.SetActive (true);
				cont0++;
			} else {
				Panel0.SetActive (false);
				cont0 = 0;
			}
			break;
		case 1:
			if (cont1.Equals (0)) {
				Panel1.SetActive (true);
				cont1++;
			} else {
				Panel1.SetActive (false);
				cont1 = 0;
			}
			break;
		case 4:
			if (cont4.Equals (0)) {
				Panel4.SetActive (true);
				cont4++;
			} else {
				Panel4.SetActive (false);
				cont4 = 0;
			}
			break;

		case 5:
			if (cont5.Equals (0)) {
				Panel5.SetActive (true);
				cont5++;
			} else {
				Panel5.SetActive (false);
				cont5 = 0;
			}
			break;
		}
	}

	public void AnswerQ0 (string LangAnsw)
	{
		Answ0.text = LangAnsw;
		LangA0 = LangAnsw;
		Answ0.text = LangTest.LMan.getString (LangAnsw);
		Panel0.SetActive (false);
		cont0 = 0;
	}

	public void AnswerQ1 (string LangAnsw)
	{
		Answ1.text = LangAnsw;
		LangA1 = Answ1.text;
		Panel1.SetActive (false);
		cont1 = 0;
	}
		
	public void AnswerQ2 (string LangAnsw)
	{
		Answ2 = LangAnsw;
	}
	public void AnswerQ3 (string LangAnsw)
	{
		Answ3 = LangAnsw;
	}

	public void AnswerQ4 (string LangAnws)
	{
		LangA4 = LangAnws;
		Answ4.text = LangTest.LMan.getString (LangAnws);
		Panel4.SetActive (false);
		cont4 = 0;
		if (Int32.Parse(LangAnws.Substring(LangAnws.Length-1)) > 3) {
			Rama.GetComponent<CanvasGroup>().alpha=1;
			Rama.GetComponent<CanvasGroup>().blocksRaycasts=true;
		} else {
			LangA5 = "";
			Rama.GetComponent<CanvasGroup>().alpha=0;
			Rama.GetComponent<CanvasGroup>().blocksRaycasts=false;
		}
	}
	public void AnswerQ5 (string LangAnws)
	{
		LangA5 = LangAnws;
		Answ5.text = LangTest.LMan.getString (LangAnws);
		Panel5.SetActive (false);
		cont5 = 0;
	}

	public void SendInfo()  //Escribir y enviar datos
	{
		String temp = LangTest.currentLang;
		LangTest.currentLang = "English";
		/*GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (LangA1);
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (LangTest.LMan.getString(LangA4));
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (LangTest.LMan.getString(LangA5));
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (Math.Ceiling (RateFunSlider.value).ToString());*/

		string valueHelp = "";
		string valueVisFeed = "";
		if (LangTest.Help) {
			valueHelp = Math.Ceiling (RateHelpSlider.value).ToString ();
		}
		if (LangTest.VisualFeedback) {
			valueVisFeed = Math.Ceiling (RateFeedSlider.value).ToString();
		}



		SendGmail.TestString+=LangA0+","+LangA1+","+Answ2+","+temp+","+Answ3+","+LangTest.LMan.getString(LangA4)+","+LangTest.LMan.getString(LangA5)+","+Math.Ceiling(RateFunSlider.value).ToString()+","+valueHelp+","+valueVisFeed;
		SendGmail.LockString+=LangA0+","+LangA1+","+Answ2+","+temp+","+Answ3+","+LangTest.LMan.getString(LangA4)+","+LangTest.LMan.getString(LangA5)+","+Math.Ceiling(RateFunSlider.value).ToString()+","+valueHelp+","+valueVisFeed;
		LangTest.currentLang = temp;
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().SendTest();
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().SendTraining();
		Application.LoadLevel ("Language");

		
	}

	public void RateGame ()
	{
		for (int i = 0; i < StarsFun.Length; i++) {
			StarsFun [i].color = Color.white;
		}
		for (int i = 0; i < Math.Ceiling(RateFunSlider.value); i++) {
			StarsFun [i].color = Color.yellow;
		}
	}
	public void RateHelp ()
	{
		for (int i = 0; i < StarsHelp.Length; i++) {
			StarsHelp [i].color = Color.white;
		}
		for (int i = 0; i < Math.Ceiling(RateHelpSlider.value); i++) {
			StarsHelp [i].color = Color.yellow;
		}
	}
	public void RateFeedback ()
	{
		for (int i = 0; i < StarsFeed.Length; i++) {
			StarsFeed [i].color = Color.white;
		}
		for (int i = 0; i < Math.Ceiling(RateFeedSlider.value); i++) {
			StarsFeed [i].color = Color.yellow;
		}
	}
}
