﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using Byporten.Models;

namespace Byporten.Controllers
{
    public class AdminController : Controller
    {

        private byportenEntities db = new byportenEntities();

        #region Administration Index
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                return View(db.createpost.ToList());
            }
        }
        #endregion

        #region Admin Portal for innlogging
        [HttpGet]
        public ActionResult Portal()
        {
            return View();
        }
        #endregion

        #region Admin Login HttpPost
        [HttpPost]
        public ActionResult Portal(Byporten.Models.AdminModel admin) {
            if (ModelState.IsValid)
            {
                if (IsValid(admin.Username, admin.Password))
                {
                    FormsAuthentication.SetAuthCookie(admin.Username, false);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Login Feilet, vennligst prøv igjen.");
                }
            }

            return View(admin);
        }
        #endregion

        #region Logout Block
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Portal", "Admin");
        }
        #endregion

        #region Password Encryption Block
        private bool IsValid(string username, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            using (db)
            {
                var user = db.adminuser.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    if (user.Password == crypto.Compute(password, user.PasswordSalt))
                    {
                        isValid = true;
                        Session["Username"] = user.Username;
                    }
                }
            }
            return isValid;
        }
        #endregion

        #region Post Details
        public ActionResult Details(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                createpost createpost = db.createpost.Find(id);
                if (createpost == null)
                {
                    return HttpNotFound();
                }
                return View(createpost);
            }
        }
        #endregion

        #region Create New Post
        public ActionResult Create()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(createpost createpost, HttpPostedFileBase imageURL)
        {

            const int ImageMinimumBytes = 1024;

            if (imageURL != null && imageURL.ContentLength > 0)
            {
                //Check image mime type
                if(imageURL.ContentType.ToLower() != "image/jpg" &&
                    imageURL.ContentType.ToLower() != "image/jpeg" &&
                    imageURL.ContentType.ToLower() != "image/png" &&
                    imageURL.ContentType.ToLower() != "image/gif" &&
                    imageURL.ContentType.ToLower() != "image/psd" &&
                    imageURL.ContentType.ToLower() != "image/pdf") 
                {
                    ModelState.AddModelError("ImageURL", "Må være bildefil");
                }

                try
                {
                    if(!imageURL.InputStream.CanRead)
                    {
                        ModelState.AddModelError("ImageURL", "Må være en bildefil!");
                    }
                    if(imageURL.ContentLength < ImageMinimumBytes) 
                    {
                        ModelState.AddModelError("ImageURL", "Bilde må være større enn 1024 bytes");
                    }

                    byte[] buffer = new byte[1024];
                    imageURL.InputStream.Read(buffer, 0, 1024);
                    string content = System.Text.Encoding.UTF8.GetString(buffer);
                    if(Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                    {
                        ModelState.AddModelError("model.ImageURL", "Potensiell farlig fil funnet!");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("model.ImageURL", "Noe gikk galt");
                }
            }

            if (ModelState.IsValid)
            {

                var imageName = Path.GetFileName(imageURL.FileName);
                var path = Path.Combine(Server.MapPath("~/images/uploads/"), imageName);

                imageURL.SaveAs(path);
                createpost.ImageURL = imageName;

                try
                {
                    db.createpost.Add(createpost);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("model.ImageURL", ex.Message);
                }
                
            }
            return View(createpost);
        }
        #endregion

        #region Edit a post
        public ActionResult Edit(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                createpost createpost = db.createpost.Find(id);
                if (createpost == null)
                {
                    //return HttpNotFound();
                    throw new HttpException(404, "Du har ikke valgt en artikkel!");
                }
                return View(createpost);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,CreateDate,ExpireDate,ImageURL,ExternalLinkURL")] createpost createpost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(createpost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(createpost);
        }
        #endregion

        #region Delete a post
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            createpost createpost = db.createpost.Find(id);
            if (createpost == null)
            {
                return HttpNotFound();
            }
            return View(createpost);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            createpost createpost = db.createpost.Find(id);

            try
            {
                var imageLocation = Path.Combine(Server.MapPath("~/images/uploads"), createpost.ImageURL);

                db.createpost.Remove(createpost);
                System.IO.File.Delete(imageLocation);
                db.SaveChanges();
                TempData["message"] = "Artikkelen har blitt slettet";
                return RedirectToAction("ViewAllArticles");
            }
            catch
            {
                db.createpost.Remove(createpost);
                db.SaveChanges();
                return RedirectToAction("ViewAllArticles");
            }
            
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region View All current Articles in a list
        public ActionResult ViewAllArticles()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                var message = TempData["message"];
                return View(db.createpost.ToList());
                
            }
        }
        #endregion

        #region View all current available positions
        public ActionResult AllPositions()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                return View(db.availablepositions.ToList());
            }
        }
        #endregion

        #region View all current stores
        public ActionResult ViewAllStores()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                return View(db.butikker.ToList());
            }
        }
        #endregion

        #region View all current offers
        public ActionResult ViewAllOffers()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                return View(db.aktuelt.ToList());
            }
        }
        #endregion

        /// <summary>
        /// Kode for å administrere ledige stillinger
        /// </summary>
        /// <returns></returns>

        #region create New position

        public ActionResult createNewPosition()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult createNewPosition(availablepositions availableposition, HttpPostedFileBase imageUrl)
        {
            if (imageUrl != null && imageUrl.ContentLength > 0)
            {
                var imageName = Path.GetFileName(imageUrl.FileName);
                var path = Path.Combine(Server.MapPath("~/images/positions/"), imageName);

                imageUrl.SaveAs(path);
                availableposition.ImageURL = imageName;
            }
            
            if(ModelState.IsValid) {
                db.availablepositions.Add(availableposition);
                db.SaveChanges();
                return RedirectToAction("createNewPosition", "Admin");
            }
            return View(availableposition);
        }

        #endregion

        #region Edit postition
        public ActionResult EditPosition(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                availablepositions availableposition = db.availablepositions.Find(id);
                if (availableposition == null)
                {
                    return HttpNotFound();
                }
                return View(availableposition);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPosition([Bind(Include = "Id, Title, Description, CreateDate, ExpireDate, ImageURL, ExternalLinkURL")] availablepositions availableposition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(availableposition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AllPositions");
            }
            return View(availableposition);
        }
        #endregion

        #region Position Details
        public ActionResult PositionDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                availablepositions availableposition = db.availablepositions.Find(id);
                if (availableposition == null)
                {
                   return HttpNotFound();
                }
                return View(availableposition);
            }
        }
        #endregion

        #region Delete Position
        public ActionResult DeletePosition(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            availablepositions availableposition = db.availablepositions.Find(id);
            if (availableposition == null)
            {
               HttpNotFound();
            }
            return View(availableposition);
        }

        [HttpPost, ActionName("DeletePosition")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePositionConfirmed(int? id)
        {
            availablepositions availableposition = db.availablepositions.Find(id);
            db.availablepositions.Remove(availableposition);
            db.SaveChanges();
            return RedirectToAction("AllPositions");
        }

        #endregion

        public ActionResult _TopPositions()
        {
            return PartialView(db.availablepositions.ToList());
        }

        /// <summary>
        /// Kode for å administrere butikker (stores)
        /// </summary>
        /// <returns></returns>

        #region Add a new store
        public ActionResult CreateStore()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStore(butikker butikker, HttpPostedFileBase logo)
        {
            if (logo != null && logo.ContentLength > 0)
            {
                var logoName = Path.GetFileName(logo.FileName);
                var path = Path.Combine(Server.MapPath("~/images/stores/"), logoName);

                logo.SaveAs(path);
                butikker.Logo = logoName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.butikker.Add(butikker);
                    db.SaveChanges();
                    return RedirectToAction("CreateStore");
                }
                catch (Exception ex)
                {
                    
                }
            }

            return View(butikker);
        }
        #endregion

        #region Edit an addded store

        public ActionResult EditStore(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                butikker butikker = db.butikker.Find(id);
                if (butikker == null)
                {
                    return HttpNotFound();
                }
                return View(butikker);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStore([Bind(Include = "Id, Navn, Kategori, Beskrivelse, Logo, Telefon, Hjemmeside")] butikker butikker) {
            if (ModelState.IsValid)
            {
                db.Entry(butikker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAllStores");
            }
            return View(butikker);
        }

        #endregion

        #region Store Details
        public ActionResult StoreDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                butikker butikker = db.butikker.Find(id);
                if (butikker == null)
                {
                    return HttpNotFound();
                }
                return View(butikker);
            }
        }
        #endregion

        #region Delete a store
        public ActionResult DeleteStore(int? id)
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

        [HttpPost, ActionName("DeleteStore")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStoreConfirmed(int? id)
        {
            butikker butikker = db.butikker.Find(id);
            db.butikker.Remove(butikker);
            db.SaveChanges();
            return RedirectToAction("ViewAllStores", "Admin");
        }
        #endregion

        /// <summary>
        /// Koden for å administrere aktuelle tilbud (offers)
        /// </summary>
        /// <returns></returns>

        #region Add a new offer
        public ActionResult CreateOffer()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOffer(aktuelt aktuelt, HttpPostedFileBase Bilde)
        {
            if (Bilde != null && Bilde.ContentLength > 0)
            {
                var imageName = Path.GetFileName(Bilde.FileName);
                var path = Path.Combine(Server.MapPath("~/images/offers/"), imageName);

                Bilde.SaveAs(path);
                aktuelt.Bilde = imageName;
            }
            if (ModelState.IsValid)
            {
                db.aktuelt.Add(aktuelt);
                db.SaveChanges();
                return RedirectToAction("CreateOffer");
            }
            return View(aktuelt);
        }
        #endregion

        #region Edit an added offer
        public ActionResult EditOffer(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
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
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOffer([Bind(Include = "Id, Tittel, Innhold, Startdato, Sluttdato, Bilde")] aktuelt aktuelt)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aktuelt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewAllOffers");
            }
            return View(aktuelt);
        }
        #endregion

        #region Offer details
        public ActionResult OfferDetails(int? id)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Portal", "Admin");
            }
            else
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
        }
        #endregion

        #region Delete an offer
        public ActionResult DeleteOffer(int? id)
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

        [HttpPost, ActionName("DeleteOffer")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOfferConfirmed(int? id)
        {
            aktuelt aktuelt = db.aktuelt.Find(id);
            db.aktuelt.Remove(aktuelt);
            db.SaveChanges();
            return RedirectToAction("ViewAllOffers", "Admin");
        }
        #endregion

        public ActionResult errorPage()
        {
            return View();
        }

        public ActionResult ImageList(Byporten.Models.ImageModel image)
        {
            var model = new ImageModel()
            {
                Images = Directory.EnumerateFiles(Server.MapPath("~/images/uploads")).Select(fn => "~/images/uploads/" + Path.GetFileName(fn))
            };
            return View(model);
        }
    }
}