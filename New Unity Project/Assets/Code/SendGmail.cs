using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class SendGmail : MonoBehaviour {

	void Main ()
	{
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("CubeReasoning@gmail.com");
		mail.To.Add("CubeReasoning@gmail.com");
		mail.Subject = "Test Mail";
		mail.Body = "This is for testing SMTP mail from GMAIL";
		mail.Attachments.Add(new Attachment("Assets/CSV/Moves.csv"));

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("CubeReasoning@gmail.com", "cubereasoning2018") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
		smtpServer.Send(mail);
		Debug.Log("success");

	}
}