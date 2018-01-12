using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlogUI.Models;
using Common.DomainEntities;
using Common.Services;

namespace BlogUI.Controllers
{
    public class HomeController : GenericController<IDiaryDomainEntity>
    {
        public HomeController(IService<IDiaryDomainEntity> service) : base(service)
        {
        }

        public async Task<ActionResult> Index(int page = 0)
        {
            var diary = await Service.GetById(1);

            return View(new DisplayBlog {Diary = diary, Page = page});
        }

        public async Task<ActionResult> Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactMessage msg)
        {
            SendMsg(msg);

            return RedirectToAction("OkResult", new { message = "Email was sent, thank you :)"});
        }

        public void SendMsg(ContactMessage msg)
        {
            var mail = ConfigurationManager.AppSettings["mailerUser"];
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    mail,
                    ConfigurationManager.AppSettings["mailerPassword"]),
                EnableSsl = true
            };

            client.Send(new MailMessage(msg.Email, mail,
                $"Message from {msg.Name} - {msg.Email}", msg.Message));
        }

        public virtual async Task<ActionResult> About()
        {
            return View();
        }
    }
}