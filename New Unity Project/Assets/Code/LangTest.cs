/*
This example script demonstrates how to use the Unity Multiple Language Support Lang class in a simple GUI.
The accompanying lang.xml file should be placed into your project's Assests folder.
 
-Adam T. Ryder
C# version by O. Glorieux
*/

using System.IO;
#if UNITY_EDITOR
	using UnityEditor;
#endif

using UnityEngine.UI;
using UnityEngine;

public class LangTest : MonoBehaviour {

	public static Lang LMan;
	public static string currentLang = "English";
	public Text text, textB;
	public Toggle Comment, VFeedbacckToggle;
	public static bool Help, VisualFeedback;
	string ToggleText, ToggleTextVFeed;
	int cont;
	void Start()
		{
		DontDestroyOnLoad(this.gameObject);
		OnEnable ();
		}

	public void OnEnable()
	{
		/*
    Initialize the Lang class by providing a path to the desired language XML file, a default language
    and a boolean to indicate if we are operating on an XML file located from a downloaded resource or local.
    True if XML resource is on the web, false if local
 
    If initializing from a web based XML resource you'll need to supply the text of the downloaded resource in placed
    of the path.
 
    web example:
    var wwwXML : WWW = new WWW("http://www.exampleURL.com/lang.xml");
    yield wwwXML;
     
    LMan = new Lang(wwwXML.text, currentLang, true);
    */
		//Debug.Log (Path.Combine (Application.dataPath, "/XML/lang.xml").ToString ());
		//LMan = new Lang(Path.Combine(Application.dataPath, "/XML/lang.xml"), currentLang, false);
	//	GameObject.Find ("Message").transform.localScale = new Vector3 (1, 1, 1);
		if (Application.systemLanguage == SystemLanguage.Spanish) {
		//	GameObject.Find ("Message").transform.localScale = new Vector3 (0.8f, 0.8f, 1);
			currentLang = "Spanish";
		}
		if (Application.systemLanguage == SystemLanguage.German) {
		//	GameObject.Find ("Message").transform.localScale = new Vector3 (0.8f, 0.8f, 1);
			currentLang = "German";
		}
		LMan = new Lang( "lang", currentLang, false);
		text.text = LMan.getString ("select");
		textB.text = LMan.getString ("StartButton");
		VisualFeedback = true;
		Help = true;
		ToggleText = LMan.getString ("Comment");
		Comment.GetComponentInChildren<Text>().text=ToggleText;
		ToggleTextVFeed="VFeedBack";
		VFeedbacckToggle.GetComponentInChildren<Text>().text=LMan.getString (ToggleTextVFeed);
	}


	public void Spanish()
	{
		//GameObject.Find ("Message").transform.localScale = new Vector3 (0.8f, 0.8f, 1);
		currentLang = "Spanish";
		ChangeCanvas ();
	}
	public void English()
	{
		//GameObject.Find ("Message").transform.localScale = new Vector3 (1, 1, 1);
		currentLang = "English";
		ChangeCanvas ();
	}

	public void German()
	{
	//	GameObject.Find ("Message").transform.localScale = new Vector3 (0.8f, 0.8f, 1);
		currentLang = "German";
		ChangeCanvas ();

	}

	public void NextScene()
	{
		SendGmail.TestString += "," + Help + "," + VisualFeedback;
		//GameObject.Find ("Lenguage").GetComponent<SendGmail> ().WriteTest ("Comments"+ Comments.ToString());
		Application.LoadLevel ("Map Select Level");

	}

	void ChangeCanvas()
	{
		
		LMan.setLanguage("lang", currentLang);
		text.text = LMan.getString ("select");
		textB.text = LMan.getString ("StartButton");
		if (cont.Equals (1)) {
			ToggleText = LMan.getString ("NoComment");
		} else {
			ToggleText = LMan.getString ("Comment");
		}
		Comment.GetComponentInChildren<Text>().text=ToggleText;
		VFeedbacckToggle.GetComponentInChildren<Text> ().text =  LMan.getString(ToggleTextVFeed);
		GameObject.Find ("PauseCanvas").GetComponent<ExitGame> ().Escribir ();
	}

	public void ChangeToggle()
	{
		
		if (cont.Equals(0)) {
			Help = false;
			ToggleText = LMan.getString ("NoComment");
			Comment.GetComponentInChildren<Text>().text=ToggleText;
			cont++;
		} 
		else {
			Help = true;
			ToggleText = LMan.getString ("Comment");
			Comment.GetComponentInChildren<Text> ().text = ToggleText;
			cont = 0;
		}
	}


	public void ChangeVisualToggle( bool VFeedback)
	{
		VisualFeedback = VFeedback;
		if (VFeedback) {
			ToggleTextVFeed = "VFeedBack";
		} 
		else {
			ToggleTextVFeed = "NoVfeedBack";
		}
		VFeedbacckToggle.GetComponentInChildren<Text> ().text = LMan.getString(ToggleTextVFeed);
	}

}
