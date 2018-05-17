using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PreQuestionnaire : MonoBehaviour {

	// Use this for initialization
	public Text Question;
	public Button YesQuest,NoQuest;
	void Start () {
		Question.text=LangTest.LMan.getString ("ExplQuest");
		YesQuest.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("YesQuest");
		NoQuest.GetComponentInChildren<Text>().text = LangTest.LMan.getString ("NoQuest");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NoQuestMethod()
	{
		Application.LoadLevel ("Language");
	}
	public void YesQuestMethod()
	{
		Application.LoadLevel ("Questionnaire");
	}
}
