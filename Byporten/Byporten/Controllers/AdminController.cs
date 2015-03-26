using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;

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

        #region Admin Portal
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

        #region CRUD Details
        public ActionResult Details(int? id)
        {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            createpost createpost = db.createpost.Find(id);
            if (createpost == null)
            {
                return HttpNotFound();
            }
            return View(createpost);
        }
        #endregion

        #region Create New Post
        public ActionResult Create()
        {
            return View();
        }
        #endregion

        //#region Create New Post HttpPost
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult(createpost createpost, HttpPostedFileBase imageURL) {

        //    if(imageURL != null && imageURL.ContentLength > 0) {
        //        var imagename = Path.GetFileName(imageURL.FileName);
        //        var path = Path.Combine(Server.MapPath("~/images/uploads/"), imagename);

        //        imageURL.SaveAs(path);
        //        createpost.ImageURL = imagename;
        //    }

        //    if(ModelState.IsValid) {
        //        db.createpost.Add(createpost);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(createpost);

        //}
        //#endregion
    }
}