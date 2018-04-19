using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SendMailTest : MonoBehaviour {

	public Text AnsW1,AnsW2;

	// Use this for initialization
	void Start () {
		AnsW1.text = "Nada escrito";
		AnsW2.text = "Nada enviado";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void WriteMail ()
	{
		//AnsW1.text = "Aun no ha escrito "+GameObject.Find ("MailManager").GetComponent<SendGmail> ().getPath ();
		GameObject.Find ("MailManager").GetComponent<SendGmail> ().WriteTest ("Llega a escribir");
		AnsW1.text = "Consigue escribir path " + GameObject.Find ("MailManager").GetComponent<SendGmail> ().getPath ();
	}
	public void SendMail ()
	{
		GameObject.Find ("MailManager").GetComponent<SendGmail> ().Send();
		AnsW2.text = "llega a enviar";
	}
}
