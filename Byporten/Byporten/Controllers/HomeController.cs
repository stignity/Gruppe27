﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            return View(db.createpost.ToList());
        }

        public ActionResult Aktuelt()
        {
            return View();
        }

        public ActionResult Butikker()
        {
            return View(db.butikker.ToList());
        }

        [HttpGet]
        public ActionResult Kundeklubb()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kundeklubb(Byporten.Models.UserLoginModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValdid(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Feil Brukernavn eller Passord.");
                }
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Registrering()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrering(Byporten.Models.UserRegModel user) {
            if (ModelState.IsValid)
            {
                using (var db = new userEntities())
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encryptPass = crypto.Compute(user.Password);
                    var sysUser = db.user.Create();

                    sysUser.Name = user.Name;
                    sysUser.Email = user.Email;
                    sysUser.Birthdate = user.Birthdate;
                    sysUser.ZipCode = user.ZipCode;
                    sysUser.City = user.City;
                    sysUser.Password = encryptPass;
                    sysUser.PasswordSalt = crypto.Salt;

                    db.user.Add(sysUser);
                    db.SaveChanges();

                    return RedirectToAction("kundeklubb", "Home");
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

        public ActionResult Stillinger(int? id)
        {
            return View(db.availablepositions.ToList());
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
                return HttpNotFound();
            }

            return View(createpost);
        }

        private bool IsValdid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;
            using (var db = new userEntities())
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

        public ActionResult errorPage()
        {
            return View();
        }
    }
}