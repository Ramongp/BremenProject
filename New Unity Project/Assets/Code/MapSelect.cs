using UnityEngine;
using System.Collections;

public class MapSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GoTutorial()
	{
		Application.LoadLevel ("Training");
	}

	public void GoLockTest()
	{
		Application.LoadLevel ("LockTraining");
	}

	public void GoPirateTestHelp()
	{
		Application.LoadLevel ("Test Pirate");
	}
}
