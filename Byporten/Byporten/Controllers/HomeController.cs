using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Byporten.Controllers
{
    public class HomeController : Controller
    {
        private byportenEntities db = new byportenEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Aktuelt()
        {
            return View();
        }

        public ActionResult Butikker()
        {
            return View();
        }

        public ActionResult Kundeklubb()
        {
            return View();
        }

        public ActionResult Gavekort()
        {
            return View();
        }

        public ActionResult Senterinformasjon()
        {
            return View();
        }

        public ActionResult Stillinger()
        {
            return View();
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;
            using (db)
            {
                var user = db.user.FirstOrDefault(u => u.Email == email);

                if (user != null)
                {
                    if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    {
                        isValid = true;
                        Session["User"] = user.Email;
                    }
                }
            }
            return isValid;
        }

    }
}