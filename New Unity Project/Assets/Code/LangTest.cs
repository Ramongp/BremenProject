/*
This example script demonstrates how to use the Unity Multiple Language Support Lang class in a simple GUI.
The accompanying lang.xml file should be placed into your project's Assests folder.
 
-Adam T. Ryder
C# version by O. Glorieux
*/

using System.IO;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

public class LangTest : MonoBehaviour {

	public static Lang LMan;
	public static string currentLang = "English";
	public Text text, textB;
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
		if (Application.systemLanguage == SystemLanguage.Spanish) {
			currentLang = "Spanish";
		}
		if (Application.systemLanguage == SystemLanguage.German) {
			currentLang = "German";
		}
		LMan = new Lang("Assets/XML/lang.xml", currentLang, false);
		text.text = LMan.getString ("select");
		textB.text = LMan.getString ("NextButton");
	}


	public void Spanish()
	{
		currentLang = "Spanish";
		LMan.setLanguage("Assets/XML/lang.xml", currentLang);
		text.text = LMan.getString ("select");
		textB.text = LMan.getString ("NextButton");
	}
	public void English()
	{
		currentLang = "English";
		LMan.setLanguage("Assets/XML/lang.xml", currentLang);
		text.text = LMan.getString ("select");
		textB.text = LMan.getString ("NextButton");
	}

	public void German()
	{
		currentLang = "German";
		LMan.setLanguage("Assets/XML/lang.xml", currentLang);
		text.text = LMan.getString ("select");
		textB.text = LMan.getString ("NextButton");
	}

	public void NextScene()
	{
		Application.LoadLevel ("Map Select Level");

	}
}
