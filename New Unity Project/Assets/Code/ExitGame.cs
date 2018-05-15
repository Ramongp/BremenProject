using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {

	// Use this for initialization
	public GameObject Panel, Bacground;
	public Text text;
	public Button Exit ,SiExit, Cancel;
	void Start () {
		DontDestroyOnLoad(this.gameObject);

		Escribir ();
		Cancelar ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Cancelar()
	{
		Exit.gameObject.SetActive (true);
		Bacground.gameObject.SetActive (false);
		Panel.gameObject.SetActive (false);
	}

	public void ShowExit()
	{
		Exit.gameObject.SetActive (false);
		Bacground.gameObject.SetActive (true);
		Panel.gameObject.SetActive (true);
	}
	 public void ExitGameButton()
	{
		Application.Quit ();
	}
	public	void Escribir()
	{
		Exit.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("Exit");
		SiExit.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("YesExit");
		Cancel.GetComponentInChildren<Text>().text= LangTest.LMan.getString ("NoExit");
		text.text=  LangTest.LMan.getString ("ExitQuestion");
	}
}
