using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SendGmail : MonoBehaviour {

	void Start()
	{
		#if UNITY_EDITOR
		DeletePreviousText ();
		#endif
	}

	public void Send ()
	{
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("CubeReasoning@gmail.com");
		mail.To.Add("CubeReasoning@gmail.com");
		mail.Subject = "Test Mail";
		mail.Body = "This is for testing SMTP mail from GMAIL";

		string path = Application.dataPath + "/Moves.csv";

		mail.Attachments.Add(new Attachment(path));
		//mail.Attachments.Add(new Attachment("/CSV/Moves.csv"));

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("CubeReasoning@gmail.com", "cubereasoning2018") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
		smtpServer.Send(mail);
	//	Debug.Log("success");

	}

	void DeletePreviousText()
	{
		//string path = Application.dataPath + "/Moves";
		string path= getPath();
		path = path.Replace(Application.dataPath, "Assets");
		AssetDatabase.DeleteAsset (path);
	}
	private string getPath ()
	{
		#if UNITY_EDITOR
		return  Application.dataPath + "/"+"Moves.csv";
		#elif UNITY_ANDROID
		return Application.persistentDataPath+"Moves.csv";
		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/"+"Moves.csv";
		#else
		return Application.dataPath +"/"+"Moves.csv";
		#endif
		} 

		public void SaveMove (string move)
		{
		if(Unfold.Test){
		string filePath = getPath ();
		string delimiter = "-";  

		//This is the writer, it writes to the filepath
		//StreamWriter writer = new StreamWriter (filePath);

		//This is writing the line of the type, name, damage... etc... (I set these)
		//writer.Write(move);
		File.AppendAllText (filePath, move);
		//This loops through everything in the inventory and sets the file to these.
		//writer.Flush ();
		//This closes the file
		//	writer.Close ();
		}
		}
		public void WriteTest (string Test)
		{
		string filePath = getPath ();
		//string delimiter = ",";  
		string test =  Test+Environment.NewLine;
		File.AppendAllText (filePath, test);
		}
		public void WriteCell (string info)
		{
		string filePath = getPath ();
		string delimiter = ",";  
		File.AppendAllText (filePath, info+delimiter);
		}


}