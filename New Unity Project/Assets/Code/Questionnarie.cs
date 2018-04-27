using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Questionnarie : MonoBehaviour {

	// Use this for initialization
	public GameObject Panel0, Panel1,Panel2;
	int cont0,cont1,cont2;
	public Text Question1,Question2,Question3, Answ1,Answ2,Answ3;
	public string LangA1, LangA2, LangA3;
	public Button[] Q1Answ, Q2Answ, Q3Answ;
	public string[] StringsQ2 = new string[]{"Estudios0","Estudios1","Estudios2","Estudios3","Estudios4","Estudios5"},StringsQ3 = new string[]{"Rama0","Rama1","Rama2","Rama3","Rama4","Rama5"};
	public Button Send;
	void Start () {
		Panel0.SetActive (false);
		Panel1.SetActive (false);
		Panel2.SetActive (false);
		Question1.text=LangTest.LMan.getString ("Question1");
		Question2.text=LangTest.LMan.getString ("Question2");
		Question3.text=LangTest.LMan.getString ("Question3");
		//Poner a los botones el texto de Lang
		for (int i = 0; i < StringsQ2.Length; i++) {
			Q2Answ[i].GetComponentInChildren<Text>().text= LangTest.LMan.getString (StringsQ2[i]);
			Q3Answ[i].GetComponentInChildren<Text>().text= LangTest.LMan.getString (StringsQ3[i]);
		}
		Answ1.text = Q1Answ [0].GetComponentInChildren<Text> ().text;
		Answ2.text = Q2Answ [0].GetComponentInChildren<Text> ().text;
		Answ3.text = Q3Answ [0].GetComponentInChildren<Text> ().text;
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
		case 2:
			if (cont2.Equals (0)) {
				Panel2.SetActive (true);
				cont2++;
			} else {
				Panel2.SetActive (false);
				cont2 = 0;
			}
			break;
		}
	}

	public void AnswerQ1 (string LangAnsw)
	{
		Answ1.text = LangAnsw;
		LangA1 = Answ1.text;
	}

	public void AnswerQ2 (string LangAnws)
	{
		LangA2 = LangAnws;
		Answ2.text = LangTest.LMan.getString (LangAnws);
	}
	public void AnswerQ3 (string LangAnws)
	{
		LangA3 = LangAnws;
		Answ3.text = LangTest.LMan.getString (LangAnws);
	}

	public void SendInfo()
	{
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (LangA1);
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (LangA2);
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteCell (LangA3);
		GameObject.Find ("Lenguage").GetComponent<SendGmail> ().Send();
		Application.LoadLevel ("Language");
		
	}
}
