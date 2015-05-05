using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Byporten.Controllers
{
    public class HomeController : Controller
    {
        private byportenEntities db = new byportenEntities();
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                return View(db.createpost.ToList());
            }
            catch
            {
                return RedirectToAction("serverError");
            }
        }

        public ActionResult Aktuelt()
        {
            try
            {
                return View(db.aktuelt.ToList());
            }
            catch
            {
                return RedirectToAction("serverError");
            }
        }

        public ActionResult Butikker()
        {
            try
            {
                return View(db.butikker.ToList());
            }
            catch
            {
                return RedirectToAction("serverError");
            }
        }

        [HttpGet]
        public ActionResult Kundeklubb()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Kundeklubb(Byporten.Models.UserLoginModel user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (IsValdid(user.Email, user.Password))
        //        {
        //            FormsAuthentication.SetAuthCookie(user.Email, false);
        //            return RedirectToAction("Subscription", "Home");                    
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Feil Brukernavn eller Passord.");
        //        }
        //    }
        //    return View(user);
        //}

        [HttpGet]
        public ActionResult Registrering()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrering(Byporten.Models.UserRegModel user)
        {
            if (ModelState.IsValid)
            {
                using (var db = new bplussuserEntities())
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encryptPass = crypto.Compute(user.Password);

                    var sysUser = db.bplususer.Create();

                    sysUser.Name = user.Name;
                    sysUser.Email = user.Email;
                    sysUser.EmailRepeat = user.EmailRepeat;
                    sysUser.PhoneNumber = user.PhoneNumber;
                    sysUser.Gender = user.Gender;
                    sysUser.Birthdate = user.Birthdate;
                    sysUser.Zipcode = user.Zipcode;
                    sysUser.BirthYearChild1 = user.BirthYearChild1;
                    sysUser.BirthYearChild2 = user.BirthYearChild2;
                    sysUser.BirthYearChild3 = user.BirthYearChild3;
                    sysUser.BirthYearChild4 = user.BirthYearChild4;
                    sysUser.Interests = user.Interests;

                    sysUser.Password = encryptPass;
                    sysUser.PasswordSalt = crypto.Salt;

                    try
                    {
                        db.bplususer.Add(sysUser);
                        db.SaveChanges();
                        return RedirectToAction("Kundeklubb");
                    }
                    catch(Exception ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }

                }
            }

            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Kundeklubb");
        }


        public ActionResult Gavekort()
        {
            return View();
        }

        public ActionResult Senterinformasjon()
        {
            return View();
        }

        public ActionResult Stillinger(int? id)
        {
            try
            {
                return View(db.availablepositions.ToList());
            }
            catch
            {
                return RedirectToAction("serverError");
            }
        }

        public ActionResult viewArticles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            createpost createpost = db.createpost.Find(id);

            if (createpost == null)
            {
                return RedirectToAction("errorPage");
            }

            return View(createpost);
        }

        public ActionResult viewStore(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            butikker butikker = db.butikker.Find(id);

            if (butikker == null)
            {
                return HttpNotFound();
            }

            return View(butikker);
        }

        public ActionResult viewPosition(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            availablepositions availablepositions = db.availablepositions.Find(id);

            if (availablepositions == null)
            {
                return HttpNotFound();
            }

            return View(availablepositions);
        }

        public ActionResult viewOffers(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                aktuelt aktuelt = db.aktuelt.Find(id);

                if (aktuelt == null)
                {
                    return HttpNotFound();
                }

                return View(aktuelt);
            }
            catch
            {
                return RedirectToAction("serverError");
            }
        }

        public ActionResult SearchResult(string searchString)
        {
            var articles = from a in db.createpost
                           select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(a => a.Title.ToLower().Contains(searchString.ToLower())
                                            || a.Content.ToLower().Contains(searchString.ToLower()));
            }
            return View(articles.ToList());
        }

        public ActionResult errorPage()
        {
            return View();
        }

        public ActionResult serverError()
        {
            return View();
        }

        public ActionResult Subscription()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Kundeklubb");
            }
            else
            {
                return View();
            }
        }

        public bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            using (var db = new bplussuserEntities())
            {
                var user = db.bplususer.FirstOrDefault(e => e.Email == email);
                if (email != null)
                {
                    if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}