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
	public static String TestString, LockString;
	public static int LockScore, LockCorrAns, TestScore, TestCorrAns;
	public static float LockAvgTime, TestAvgTime;
	public static int Level;
	public string TestCabecera, TrainingCabecera;
	public static bool TrainingDone;
	void Start()
	{
		TestString = "";
		LockString = "";
		Level = 0;
		#if UNITY_EDITOR
		DeletePreviousText ();
		#endif
	}

	public void SendTraining ()
	{
		checkTraining ();
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("CubeReasoning@gmail.com");
		mail.To.Add("CubeReasoning@gmail.com");
		mail.Subject = "Data from the Training";
		mail.Body = "Attached to this message is the information collected during the training.";

	//	string path = Application.dataPath + "/Moves.csv";
		//Directory.GetFiles(System.Environment.CurrentDirectory+"/Resources","Moves");

		mail.Attachments.Add(new Attachment( getPathTraining()));
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

	public void SendTest ()
	{
		checkTest ();
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("CubeReasoning@gmail.com");
		mail.To.Add("CubeReasoning@gmail.com");
		mail.Subject = "Data from the interactive version of the Cube Comparison Test";
		mail.Body = "Attached to this message is the information collected during the test.";

		//	string path = Application.dataPath + "/Moves.csv";
		//Directory.GetFiles(System.Environment.CurrentDirectory+"/Resources","Moves");

		mail.Attachments.Add(new Attachment( getPathTest()));
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
		string path1= getPathTest();
		string path2 = getPathTraining ();
		path1 = path1.Replace(Application.dataPath, "Assets");
		path2 = path2.Replace(Application.dataPath, "Assets");
		#if UNITY_EDITOR
		AssetDatabase.DeleteAsset (path1);
		AssetDatabase.DeleteAsset (path2);
		#endif

	}
	public string getPathTest ()
	{
		#if UNITY_EDITOR
		return  Application.dataPath + "/"+"TestInfo.csv";
		#elif UNITY_ANDROID
		return Application.persistentDataPath+"/"+"TestInfo.csv";

		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/"+"TestInfo.csv";
		#else
		return Application.dataPath +"/"+"TestInfo.csv";
		#endif
		} 

		public string getPathTraining ()
		{
		#if UNITY_EDITOR
		return  Application.dataPath + "/"+"TrainingInfo.csv";
		#elif UNITY_ANDROID
		return Application.persistentDataPath+"/"+"TrainingInfo.csv";

		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/"+"TrainingInfo.csv";
		#else
		return Application.dataPath +"/"+"TrainingInfo.csv";
		#endif
		}

		public void SaveMove (string move)
		{
		if(Unfold.Test){
		string filePath = getPathTest ();
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


		void checkTest()
	{
		string filePath = getPathTest ();
		if (!File.Exists (getPathTest ())) {
			string Message = "ID_Machine,Date,Time,Test with Help,Test with VisualFeedback," +	
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
							"Cube question,Change,HelpUsed,Answer,Time,Bonus Question,Answer Bonus Question," +
			                "Total Time Test,Gender,Age,Nationality,Lenguage of the Test,Native Lenguage,Level of Study,Field of Study,Rate Game,Rate Help,Rate visual FeedBack,";
		File.AppendAllText (filePath,Message+Environment.NewLine);
		}

		File.AppendAllText (filePath,TestString+Environment.NewLine);
	}


		void checkTraining()
		{
		string filePath = getPathTraining ();
		if (!File.Exists (getPathTraining ())) {
		string Message = "ID_Machine,Date,Time,Test with Help," +	
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
			"Cube question,HelpUsed,ResetUsed,Moves,Time" +
		"Total Time Test,Gender,Age,Nationality,Lenguage of the Test,Native Lenguage,Level of Study,Field of Study,Rate Game,Rate Help,Rate visual FeedBack,";
		File.AppendAllText (filePath,Message+Environment.NewLine);
		}

		File.AppendAllText (filePath,LockString+Environment.NewLine);
		}


}