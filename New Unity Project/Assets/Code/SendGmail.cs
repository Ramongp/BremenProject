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
		StartInfo ();
	}

	public void Send ()
	{
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("CubeReasoning@gmail.com");
		mail.To.Add("CubeReasoning@gmail.com");
		mail.Subject = "Test Mail";
		mail.Body = "This is for testing SMTP mail from GMAIL";

	//	string path = Application.dataPath + "/Moves.csv";
		//Directory.GetFiles(System.Environment.CurrentDirectory+"/Resources","Moves");

		mail.Attachments.Add(new Attachment( getPath()));
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
		#if UNITY_EDITOR
		AssetDatabase.DeleteAsset (path);
		#endif

	}
	public string getPath ()
	{
		#if UNITY_EDITOR
		return  Application.dataPath + "/"+"Moves.csv";
		#elif UNITY_ANDROID
		/*File file = new File (Environment.getExternalStorageDirectory(),"Moves.csv");
		Uri uri = Uri.fromFile(file);
		return Uri.getPath();*/
		return Application.persistentDataPath+"/"+"Moves.csv";

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
		string test =  Environment.NewLine+Test;
		File.AppendAllText (filePath, test);
		}
		public void WriteCell (string info)
		{
		string filePath = getPath ();
		string delimiter = ",";  
		File.AppendAllText (filePath, delimiter+info);
		}
		public void WriteFirstCell (string info)
		{
		string filePath = getPath ();
		File.AppendAllText (filePath, info);
		}

		void StartInfo()
	{
		if (!File.Exists (getPath ())) {
		WriteFirstCell ("ID_Machine");WriteCell ("Date");WriteCell ("Time");WriteCell ("Question");WriteCell ("Cube");WriteCell ("Changes");WriteCell ("Answer");WriteCell ("Time");WriteCell ("Total Time");
		}
		WriteTest(SystemInfo.deviceUniqueIdentifier);
		WriteCell (System.DateTime.Now.ToString("dd/MM/yyyy")); 
		WriteCell (System.DateTime.Now.ToString("hh:mm:ss"));
		//Send ();
	}
}