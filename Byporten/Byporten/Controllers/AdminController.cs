using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Data;

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
            if (imageURL != null && imageURL.ContentLength > 0)
            {
                var imageName = Path.GetFileName(imageURL.FileName);
                var path = Path.Combine(Server.MapPath("~/images/uploads/"), imageName);

                imageURL.SaveAs(path);
                createpost.ImageURL = imageName;
            }

            if (ModelState.IsValid)
            {
                db.createpost.Add(createpost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(createpost);
        }
        #endregion

        #region Edit a post
        public ActionResult Edit(int? id)
        {
            if (Session["Username"] == null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                }
                createpost createpost = db.createpost.Find(id);
                if (createpost == null)
                {
                    return HttpNotFound();
                }
                return View(createpost);
            }
            else
            {
                return RedirectToAction("Portal", "Admin");
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
            db.createpost.Remove(createpost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region View All current Articles in a list
        public ActionResult ViewAllArticles()
        {
            return View(db.createpost.ToList());
        }
        #endregion
    }
}