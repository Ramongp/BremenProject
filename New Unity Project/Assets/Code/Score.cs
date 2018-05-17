using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public Text Results, CorrectAns, TScore, Avg;
	public Slider Stars;
	public Button Next;
	// Use this for initialization
	void Start () {
		Next.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("NextBox");
		Results.text = LangTest.LMan.getString ("Results");
		if (SendGmail.Level.Equals (1)) {
			CorrectAns.text = LangTest.LMan.getString ("CorrectAns") + " " + SendGmail.LockCorrAns.ToString () + "/10";
			Avg.text = LangTest.LMan.getString ("Avg") + " " + (SendGmail.LockAvgTime / 10).ToString ("0.0");
			TScore.text = LangTest.LMan.getString ("Score") + " " + SendGmail.LockScore.ToString ();
			getValueStars (SendGmail.LockScore, 6000, 10);
		}
		else {
			CorrectAns.text = LangTest.LMan.getString ("CorrectAns") + " " + SendGmail.TestCorrAns.ToString () + "/15";
			Avg.text = LangTest.LMan.getString ("Avg") + " " + (SendGmail.TestAvgTime / 15).ToString ("0.0");
			TScore.text = LangTest.LMan.getString ("Score") + " " + SendGmail.TestScore.ToString ();
			getValueStars (SendGmail.TestScore, 9250, 15);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NexScene()
	{
		if (SendGmail.Level.Equals (1)) {
			Application.LoadLevel ("Map Select Level");
		} 
		else {
			Application.LoadLevel ("Question");
		}
	}

	void getValueStars (float points,int max, int numtests)
	{
		float fracc= 0.5f;
		float cont;
		if (SendGmail.Level.Equals (2)) {
			fracc = 1 / 3f;
		}
		cont = fracc;
		for (int i =1; i<numtests+1;i++){
			if (points < (max / numtests) * i) {
				Stars.value = cont;
				return;
			}
			cont += fracc;
		}
		Stars.value = 5;
		return;
	}
}
