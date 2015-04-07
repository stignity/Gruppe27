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
            return View(db.createpost.ToList());
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

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Byporten.Models.UserCreateModel createuser)
        {
            if (ModelState.IsValid)
            {
                using(var db = new byportenEntities()) 
                 {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encryptPass = crypto.Compute(createuser.Password);

                    var sysUser = db.user.Create();

                    sysUser.FullName = createuser.FullName;
                    sysUser.Email = createuser.Email;
                    sysUser.Birthday = createuser.Birthday;
                    sysUser.ZipCode = createuser.ZipCode;
                    sysUser.City = createuser.City;
                    sysUser.Password = encryptPass;
                    sysUser.PasswordSalt = crypto.Salt;

                    db.user.Add(sysUser);
                    db.SaveChanges();

                    return RedirectToAction("Kundeklubb", "Home");
                }
            }
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