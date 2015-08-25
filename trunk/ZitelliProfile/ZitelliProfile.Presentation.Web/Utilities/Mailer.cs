using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ZitelliProfile.Presentation.Web.Utilities
{
	public class Mailer
	{
		public static string MailNotifyServer;
		public static string MailNotifyFrom;
		public static string MailNotifyTo;
		public static string MailNotifyCc;
		public static string MailAddressSeparator;
		public static System.Net.NetworkCredential Credentials;
		public static int Port = 25;
		public static bool EnableSsl = false;

		public static void SendMail(string subject)
		{
			SendMail(subject, subject);
		}

		public static void SendMail(string subject, string body)
		{
			try
			{
				MailMessage mail = new MailMessage();
				SmtpClient SmtpServer = new SmtpClient(MailNotifyServer, Port);
				SmtpServer.ServicePoint.MaxIdleTime = 1;
				SmtpServer.Timeout = 3000000;
				SmtpServer.Credentials = Credentials;
				mail.From = new MailAddress("contacto@cristianzitelli.com.ar");
				mail.To.Add(MailNotifyTo);
				mail.Body = body;
				mail.Sender = new MailAddress(MailNotifyFrom);
				mail.Subject = "Profile Page - " + subject + " - " + mail.Sender.Address;
				SmtpServer.EnableSsl = EnableSsl;

				if (!string.IsNullOrWhiteSpace(MailNotifyCc))
				{
					mail.CC.Add(MailNotifyCc);
				}

				SmtpServer.Send(mail);

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}