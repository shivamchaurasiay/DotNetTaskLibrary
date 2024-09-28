using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTasks.Core
{
	public class EmailSender
	{
		//SMTP(Simple mail transfer protocol)
		string SenderId, SenderPass;
		public string SendTo { get; set; }
		public string CC { get; set; }
		public string BCC { get; set; }
		public Attachment AttachFile { get; set; }
		public string Subject { get; set; }
		public string Message { get; set; }
		public string SenderName { get; set; }
		public EmailSender(string SenderName,string SendTo, string Subject, string Message)
		{
			this.SenderId = "sumitbharti2412@gmail.com";//ConfigurationManager.AppSettings["EmailId"];
			this.SenderPass = "sykfznqkhuarkoxj";//ConfigurationManager.AppSettings["MailPass"]; 
			this.SendTo = SendTo;
			this.Subject = Subject;
			this.Message = Message;
			this.SenderName = SenderName;

		}
		public EmailSender(string SenderName, string SendTo,string CCMail, string BCCMail, string Subject, string Message,string CCAndBCCType)
		{
			this.SenderId = "";//ConfigurationManager.AppSettings["EmailId"];
			this.SenderPass = "";//ConfigurationManager.AppSettings["MailPass"]; 
			this.SendTo = SendTo;
			this.Subject = Subject;
			this.Message = Message;
			if(CCAndBCCType=="CC")
			{
				this.CC = CCMail;
				this.BCC = "";
			}
			else if (CCAndBCCType == "BCC")
			{
				this.CC = "";
				this.BCC = BCCMail;
			}
			else if (CCAndBCCType == "CCandBCC")
			{
				this.CC = CCMail;
				this.BCC = BCCMail;
			}
			this.SenderName= SenderName;

		}
		
		internal bool SendEmail()
		{
			try
			{
				MailMessage msg = new MailMessage();
				//for set mail sender requariment
				SmtpClient client = new SmtpClient();
				//for Send main and given right

				MailAddress SenderMail = new MailAddress(SenderId, SenderName);
				msg.Sender = SenderMail;
				msg.To.Add(SendTo);
				if (CC != null && CC!="")
				{
					msg.CC.Add(CC);
				}
				if (BCC != null && BCC!="")
				{
					msg.Bcc.Add(BCC);
				}
				msg.Subject = Subject;
				msg.Body = Message;

				if (AttachFile != null)
				{
					//Attachment at = new Attachment();
					msg.Attachments.Add(AttachFile);
				}
				msg.From = SenderMail;
				//Start work to send mail

				client.UseDefaultCredentials = false; //use to don't send default Email 
				NetworkCredential nc = new NetworkCredential(SenderId, SenderPass);
				client.Credentials = nc;// add credential my sender id and pas
				client.EnableSsl = true; //get right
				client.Port = 587;//465;// 587; //port num
				client.Host = "smtp.gmail.com";// "pat@recipient.com";// "smtp.gmail.com";// "smtp.server.address";// "smtp.gmail.com";
				client.Send(msg);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
