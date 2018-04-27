using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

	string NextLevel;
	public GameObject Panel;
	string LangExpli;
	public Text Explic;
	public Button Play,Cancel, Tutorial, LockTest,FinalTest;
	// Use this for initialization
	void Start () {
		LockTest.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("LockTest");
		FinalTest.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("FinalTest");
		Tutorial.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("Tutorial");
		Play.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("Play");
		Cancel.GetComponentInChildren<Text>().text=LangTest.LMan.getString ("Cancel");
		Hide ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoTutorial()
	{
		NextLevel ="Training";
		LangExpli = "ExplTraining";
		ExplLevel ();
	}

	public void GoLockTest()
	{
		NextLevel ="LockTraining";
		LangExpli = "ExplLock";
		ExplLevel ();
	}

	public void GoPirateTestHelp()
	{
		NextLevel ="Test Pirate";
		LangExpli = "ExplTest";
		ExplLevel ();
	}

	public void GoNextLevel()
	{
	//	GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteTest (NextLevel);
		Application.LoadLevel (NextLevel);
	}

	public void ExplLevel()
	{
		Panel.SetActive (true);
		Explic.text= LangTest.LMan.getString (LangExpli);

		
	}

	public void Hide()
	{
		Panel.SetActive (false);
	}
}
