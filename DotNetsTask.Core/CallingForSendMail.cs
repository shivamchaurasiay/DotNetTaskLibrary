using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetsTasks.Core
{
	public class CallingForSendMail
	{
		string SenderName = "";
		public CallingForSendMail() {
			SenderName = "DotNetsTasks";// We can use to dynamic name to call from appsetting
		}


		#region Common Method For Send Mail
		//Start : Code  Send Passcode on mail id
		public string SendRequestForEmailPasscodeMail(string emailId, string passcode)
		{
			string msg = "Dear user, Your Email Passcode for DotNetsTasks.(U.P.) is : " + passcode;
			string Subject = "DotNetsTasks.Forgot your Password";
			//ContainForgetMailOTP(emailId, "");

			EmailSender objes = new EmailSender(SenderName,emailId, Subject, msg);
			bool mailstatus = objes.SendEmail();
			if (mailstatus)
			{
				return "success";
			}
			else
			{
				return "error";
			}
		}
		//End : Code  Send Passcode on mail id
		//Start : Code for Send General Mail For Single User
		public string SendRequestForSinglUserMail(string emailId,string Subject, string Message)
		{

			string _Message = Message;
			string _Subject = Subject;

			EmailSender objes = new EmailSender(SenderName,emailId, _Subject, _Message);
			bool mailstatus = objes.SendEmail();
			if (mailstatus)
			{
				return "success";
			}
			else
			{
				return "error";
			}
		}
		//End : Code for Send General Mail For Single User
		//Start : Code for Send Mail for Single and CC mail user
		public string SendRequestForSingleAndCCMail(string emailId, string CCMail, string Subject, string Message)
		{
			string _CC= CCMail;
			string _BCC = "";
			string _Message = Message;
			string _Subject = Subject;
			string _CCAndBCCType = "CC";

			EmailSender objes = new EmailSender(SenderName, emailId, _CC, _BCC, _Subject, _Message, _CCAndBCCType);
			bool mailstatus = objes.SendEmail();
			if (mailstatus)
			{
				return "Success";
			}
			else
			{
				return "Error";
			}
		}
		//End : Code for Send Mail for Single and CC mail user
		//Start : Code  for Send Mail for Single and BCC mail users
		public string SendRequestForSingleAndBCCMail(string emailId,string BCCMail, string Subject, string Message)
		{

			string _CC = "";
			string _BCC = BCCMail;
			string _Message = Message;
			string _Subject = Subject;
			string _CCAndBCCType = "BCC";

			EmailSender objes = new EmailSender(SenderName, emailId, _CC, _BCC, _Subject, _Message, _CCAndBCCType);
			bool mailstatus = objes.SendEmail();
			if (mailstatus)
			{
				return "Success";
			}
			else
			{
				return "Error";
			}
		}
		//End : Code  for Send Mail for Single and BCC mail users
		// Start : Code for Send Single CC and BCC user mail
		public string SendRequestForSingleAndCCAndBCCMail(string emailId,string CCMail, string BCCMail, string Subject, string Message)
		{

			string _CC = CCMail;
			string _BCC = BCCMail;
			string _Message = Message;
			string _Subject = Subject;
			string _CCAndBCCType = "CCandBCC";

			EmailSender objes = new EmailSender(SenderName, emailId, _CC, _BCC, _Subject, _Message, _CCAndBCCType);
			bool mailstatus = objes.SendEmail();
			if (mailstatus)
			{
				return "Success";
			}
			else
			{
				return "Error";
			}
		}
		// End : Code for Send Single CC and BCC user mail


		#endregion

		public string GetPasscode()
		{
			string st = "";
			StringBuilder sb = new StringBuilder();
			Random r = new Random();
			string ss = "0123456789";
			for (int i = 0; i < 6; i++)
			{
				sb.Append(r.Next(ss.Length));
			}
			st = sb.ToString();
			return st;
		}
	}
}
