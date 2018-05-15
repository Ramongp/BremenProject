using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockTimer : MonoBehaviour {

	public float TimeLeft, maxTime;
	public Slider TimeSlider;
	public Button Tapar, TaparNextLevel, NextBoxB;
	public GameObject Key;
	public Text Text, PointText, PathText;
	public ParticleSystem Correct;
	public Image StarSlid1, StarSlid2, StarSlid3, StarP1, StarP2, StarP3, Bag, Message;
	public static bool testing;
	public GameObject Buttons;
	public Color StarOn,StarOff;
	public int correctLock;
	// Use this for initialization
	void Start () {
		PathText.text = LangTest.LMan.getString ("Path");
		Tapar.GetComponentInChildren<Text> ().text = LangTest.LMan.getString ("SeePoints");
		Set ();
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
				StarSlid2.color= StarOff;
			}
			if (TimeLeft<1) {
				StarSlid3.color= StarOff;
			}
			if (TimeLeft < 0) {
				TimeUp ();
			}
		}
	}

	public void  Pass()
	{
		correctLock = 1;
		testing = false;
		Correct.gameObject.SetActive (true);
		Tapar.interactable = true; 
		Tapar.gameObject.SetActive (true);
		Text.gameObject.SetActive (true);
		Text.text = LangTest.LMan.getString ("PassedLockTest");
		Key.gameObject.GetComponent<Animator> ().SetTrigger ("Passed");
		Buttons.SetActive (false);


		
	}
	public void  GiveUp()
	{
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



	}

	public void TimeUp()
	{
		Message.gameObject.SetActive (true);
		Message.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("LockTimeUp")+" "+LangTest.LMan.getString ("GiveUp");
		GameObject.Find ("Main Camera").GetComponent<LockCube> ().ShowWay (false);
		correctLock = 0;
		testing = false;
		Tapar.interactable = true; 
		Tapar.gameObject.SetActive (true);
		Buttons.SetActive (false);
		
	}
	public void Points()
	{
		Message.gameObject.SetActive (false);
		Unfold.ShowExpl = false;
		Tapar.interactable = false; 
		//	GameObject.Find ("Reward").GetComponent<Points> ().RewardAnimation (TimeLeft);
		PointText.text="+"+((int)(TimeLeft*10*correctLock)).ToString () + LangTest.LMan.getString ("Points");
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
		if (LockCube.Test < 4) {
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
			StarSlid1.color= StarOn;
			StarSlid2.color= StarOn;
			StarSlid3.color= StarOn;
			StarP1.color= StarOn;
			StarP2.color= StarOn;
			StarP3.color= StarOn;
			NextBoxB.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NextBox");
		} else {
			TaparNextLevel.gameObject.SetActive (true);
			TaparNextLevel.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("PassedLockTestLevel");
		}
	}
	public void nextLvl()
	{
		Application.LoadLevel ("Map Select Level");	
	}
}
