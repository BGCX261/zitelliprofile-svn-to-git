using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZitelliProfile.Presentation.Web.Models;
using ZitelliProfile.Presentation.Web.Utilities;

namespace ZitelliProfile.Presentation.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Message = "";
            var model = new HomePageModel();
            using (ProfileContext db = new ProfileContext())
            {
                model.Mails = db.Mails.ToList();
                model.UserSuggestions = db.UserSuggestions.ToList();
            }
            



			return View(model);
		}

		public ActionResult SendSuggestion(HomePageModel model)
		{
            SaveSuggestion(model.Email, model.UserName, model.Body);
			return RedirectToAction("Index","Home");
		}

		public ActionResult SendMail(HomePageModel model)
		{
			SendEmail(model.Email, model.UserName, model.Subject, model.Body);
			SaveEmail(model.Email, model.UserName, model.Subject, model.Body);
			return RedirectToAction("Index", "Home");
		}
        public void SaveSuggestion(string sender, string userName, string body)
        {

            using (ProfileContext db = new ProfileContext())
            {
                db.UserSuggestions.Add(
                    new UserSuggestion()
                    {
                        Datetime = DateTime.Now,
                        Email = sender,
                        UserName = userName,
                        Body = body 
                    }
                    );
                db.SaveChanges();
            }
        }

		public void SendEmail(string sender,string userName, string subject, string body)
		{ 
		    //TODO: realizar el envio de mails
			try
			{
				Mailer.MailNotifyServer = "smtp.gmail.com";
				Mailer.MailNotifyFrom = sender;
				Mailer.MailNotifyTo = "zitelli32@gmail.com";
				Mailer.MailNotifyCc = "";
				Mailer.EnableSsl = true;
				Mailer.MailAddressSeparator = ";";
				Mailer.Credentials = new System.Net.NetworkCredential("Zitelli32@gmail.com", "2173LL1crys");
				Mailer.Port = 587;
				Mailer.SendMail(subject, body);
			}
			catch (Exception ex)
			{ 
				
			}
		}

		public void SaveEmail(string sender, string userName, string subject, string body)
		{
			using (ProfileContext db = new ProfileContext())
			{
				db.Mails.Add(
					new Mail() 
					{ 
						Datetime = DateTime.Now,
						Email = sender,
						Subject = subject,
						UserName = userName,
                        Body = body
					}
					);
				db.SaveChanges();
			}
		}
	}
}
