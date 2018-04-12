using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockTimer : MonoBehaviour {

	public float TimeLeft, maxTime;
	public Slider TimeSlider;
	public Button Tapar, TaparNextLevel;
	public GameObject Key;
	public Text Text;
	public ParticleSystem Correct;
	public static bool testing;
	public GameObject Buttons;
	// Use this for initialization
	void Start () {
		Set ();
	}
	
	// Update is called once per frame
	void Update () {
		if (testing) {
			TimeLeft -= Time.deltaTime;
			TimeSlider.value = TimeLeft;
		}
	}

	public void  Pass()
	{
		testing = false;
		Correct.gameObject.SetActive (true);
		Tapar.interactable = true; 
		Tapar.gameObject.SetActive (true);
		Text.gameObject.SetActive (true);
		Text.text = LangTest.LMan.getString ("PassedLockTest");
		Key.gameObject.GetComponent<Animator> ().SetTrigger ("Passed");
		Buttons.SetActive (false);

		
	}
	public void Points()
	{
		Tapar.interactable = false; 
			GameObject.Find ("Reward").GetComponent<Points> ().RewardAnimation (TimeLeft);
			TimeSlider.gameObject.SetActive (false);
			//Text.gameObject.SetActive (false);
			Tapar.gameObject.SetActive (false);

	}
	public void Set()
	{
		if (LockCube.Test < 3) {
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
