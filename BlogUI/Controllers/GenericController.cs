using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using BlogUI.Models;
using Common.DomainEntities;
using Common.Services;
using Newtonsoft.Json;
using Services;

namespace BlogUI.Controllers
{
    public abstract class GenericController<T> : AsyncController where T : class, IDomainEntity
    {
        protected GenericController(IService<T> service)
        {
            Service = service;
        }

        public ActionResult OkResult(string message)
        {
            ModelState.Clear();
            ViewBag.Message = message;
            return View("_Ok");
        }

     
        protected IService<T> Service { get; }

        //protected Dictionary<string, string> GetModelStateErrors(string key)
        //{

        //    return new Dictionary<string, string>
        //    {
        //        {
        //            key, ModelState.Where(x => x.Key == key).Select(x => x.Value).Select(x => x.Errors)
        //                .Select(x => x.ToString()).FirstOrDefault()
        //        }
        //    };
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Service?.Dispose();
            base.Dispose(disposing);
        }
        public void SendMsg(ContactMessage msg)
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["mailerUser"]);
            var message = new MailMessage
            {
                From = fromAddress,
                Subject = msg.Topic ?? $"Message from {msg.Email} - {msg.Name}",
                IsBodyHtml = true,
                Body = msg.Message
            };
            message.To.Add(msg.MailTo ?? "konrad.jagusiak@umyslzamkniety.pl");

            try
            {
                GetSmtpClient().Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static SmtpClient GetSmtpClient()
        {
            var basicCredential = new NetworkCredential(ConfigurationManager.AppSettings["mailerUser"], ConfigurationManager.AppSettings["mailerPassword"]);
            var smtpClient = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["mailerHost"],
                Port = int.Parse(ConfigurationManager.AppSettings["mailerPort"]),
                EnableSsl = bool.Parse(ConfigurationManager.AppSettings["mailerSsl"]),
                UseDefaultCredentials = false,
                Credentials = basicCredential
            };
            return smtpClient;
        }

        protected bool ValidateReCaptcha()
        {
            var secretKey = ConfigurationManager.AppSettings["captchaAppSecretKey"];
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(
                new WebClient().DownloadString(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={Request["g-recaptcha-response"]}"));

            if (captchaResponse.Success) return true;

            if (captchaResponse.ErrorCodes.Count <= 0)
            {
                ModelState.AddModelError("Recaptcha", "Recaptcha error occured. Please try again");
                return false;
            }

            switch (captchaResponse.ErrorCodes[0].ToLower())
            {
                case "missing-input-secret":
                    ModelState.AddModelError("Recaptcha", "Recaptcha secret parameter is missing.");
                    break;
                case "invalid-input-secret":
                    ModelState.AddModelError("Recaptcha", "Recaptcha secret parameter is invalid or malformed.");
                    break;
                case "missing-input-response":
                    ModelState.AddModelError("Recaptcha", "Recaptcha response parameter is missing.");
                    break;
                case "invalid-input-response":
                    ModelState.AddModelError("Recaptcha", "Recaptcha response parameter is invalid or malformed.");
                    break;
                default:
                    ModelState.AddModelError("Recaptcha", "Recaptcha error occured. Please try again");
                    break;
            }

            return false;
        }
    }
}