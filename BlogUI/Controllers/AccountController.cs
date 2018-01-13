using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BlogUI.Models;
using Common.DomainEntities;
using Common.Services;
using Common.UI;
using DomainModel;
using Services;

namespace BlogUI.Controllers
{
    public class AccountController : GenericController<IUserDomainEntity>
    {
        public AccountController(IService<IUserDomainEntity> users) : base(users)
        {
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateUser model)
        {
          //  ValidateReCaptcha();
            if (!ModelState.IsValid || !await RegisterUser(model))
                return View(model);

            SendMsg(new ContactMessage
            {
                Email =ConfigurationManager.AppSettings["mailerUser"],
                Message = $"User with login: {model.Login} - {model.Name}, was successffully created.",
                Topic = $"Registration completed."
        });


            return RedirectToAction("OkResult", new{message = $"Hello {model.Name}, your account with Login {model.Login} was successfully created."});
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginUser model)
        {
            if (!ModelState.IsValid || !await SetAuthCookie(model))
                return View(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("OkResult", new { message = $"You have successfully logged out."});
        }

        public ActionResult Manage()
        {
            return View("UnderConstruction");
        }

        #region Registration

        private async Task<bool> RegisterUser(ICreateUser model)
        {
            if (!await IsLoginUnique(model)) return false;

            HashUserPassword(model);
            SetUserRoles(model, new List<string> { "User" });
            await ((UserService)Service).Put(model);

            return true;
        }
        private async Task<bool> IsLoginUnique(ICreateUser model)
        {
            var isLoginUnique = !await ((UserService)Service).Exists(new User { Login = model.Login });
            if (isLoginUnique) return true;

            ModelState.AddModelError("DuplicateLogin", "User with this login was already created.");
            return false;
        }

        private static void HashUserPassword(ICreateUser model)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 10000);
            var hash = pbkdf2.GetBytes(20);
            var hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            model.Password = Convert.ToBase64String(hashBytes);
        }
        private static void SetUserRoles(ICreateUser model, IEnumerable<string> roles)
        {
            model.Roles = string.Join(", ", roles);
        }
        private async Task<bool> SetAuthCookie(LoginUser model)
        {
            var user = await AuthorizeUser(model);
            if (user == null)
                return false;

            FormsAuthentication.SetAuthCookie(user.Login, false);
            var authTicket = new FormsAuthenticationTicket(
                1, user.Login, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Roles);
            HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                FormsAuthentication.Encrypt(authTicket)));
            return true;
        }

        private async Task<IUserDomainEntity> AuthorizeUser(LoginUser model)
        {
            IUserDomainEntity user = null;
            try
            {
                user = await ((UserService)Service).AuthorizeUser(new User
                {
                    Login = model.Login,
                    Password = model.Password
                });
            }
            catch (UnauthorizedAccessException e)
            {
                ModelState.AddModelError("InvalidLogin", e.Message);
            }

            return user;
        }

        #endregion
    }
}