using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OIDBMVCWEBSITE.Areas.Webmaster.Models;
using OIDBMVCWEBSITE.Models;

namespace OIDBMVCWEBSITE.Areas.Webmaster.Controllers
{
    public class UsersController : Controller
    {
        private OIDBEntities db = new OIDBEntities();

        // GET: /Webmaster/Users/

        public ActionResult Index()
        {
            List<UserDesignationTypeViewModel> userDesignationTypeViewModel = new List<UserDesignationTypeViewModel>();
            var result = (from siteUser in db.SiteUsers
                          join designation in db.Designation_Master on siteUser.DesignationID equals designation.DesignationID
                          join pType in db.St_PermissionType on siteUser.User_Type_ID equals pType.PTypeID
                          where siteUser.Status == "Active"
                          select new UserDesignationTypeViewModel
                          {
                              UserID = siteUser.UserID,
                              UserName = siteUser.UserName,
                              Desigation_Name = designation.Desigation_Name,
                              Email = siteUser.Email,
                              PType = pType.PType,
                              Name = siteUser.Name,
                              Address = siteUser.Address,
                              ContactNumber = siteUser.ContactNumber,
                              OrgnisationName = siteUser.OrgnisationName
                          });
            userDesignationTypeViewModel = result.ToList();
            return View(userDesignationTypeViewModel);
        }


        // GET: /Webmaster/Users/Create
        public ActionResult Create()
        {

            ViewBag.Designation = new SelectList(db.Designation_Master.Where(s => s.Status == "Active"), "DesignationID", "Desigation_Name");
            ViewBag.UserType = new SelectList(db.St_PermissionType.Where(s => s.status == "Active"), "PTypeID", "PType");
            return View();
        }

        // POST: /Webmaster/Users/Create
        [HttpPost]
        public ActionResult Create(SiteUser siteUser)
        {
            if (ModelState.IsValid)
            {
                siteUser.Password = DataUtility.MD5Hash(siteUser.Password);
                siteUser.Status = "Active";
                siteUser.IpAddress = HttpContext.Request.UserHostAddress;
                db.SiteUsers.Add(siteUser);
                int i = db.SaveChanges();
                if (i > 0)
                {
                    TempData["alert"] = "User Created Successfully";
                }
                else
                {
                    TempData["alert"] = "User Not Created Please Try Again.";
                }
                return RedirectToAction("Index");

            }
            ViewBag.Designation = new SelectList(db.Designation_Master.Where(s => s.Status == "Active"), "DesignationID", "Desigation_Name");
            ViewBag.UserType = new SelectList(db.St_PermissionType.Where(s => s.status == "Active"), "PTypeID", "PType");
            return View(siteUser);
        }

        // GET: /Webmaster/Users/Edit/5
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Designation = new SelectList(db.Designation_Master.Where(s => s.Status == "Active"), "DesignationID", "Desigation_Name");
            ViewBag.UserType = new SelectList(db.St_PermissionType.Where(s => s.status == "Active"), "PTypeID", "PType");
            SiteUser siteuser = db.SiteUsers.Find(id);
            if (siteuser == null)
            {
                return HttpNotFound();
            }
            return View(siteuser);
        }

        // POST: /Webmaster/Users/Edit/5
        [HttpPost]
        public ActionResult Edit(SiteUser siteUser)
        {
            if (ModelState.IsValid)
            {
                siteUser.Status = "Active";
                siteUser.IpAddress = HttpContext.Request.UserHostAddress;
                db.Entry(siteUser).State = EntityState.Modified;
                int i = db.SaveChanges();
                if (i > 0)
                {
                    TempData["alert"] = "User Updated Successfully";
                }
                else
                {
                    TempData["alert"] = "User Not Updated Please Try Again.";
                }
                return RedirectToAction("Index");
            }
            ViewBag.Designation = new SelectList(db.Designation_Master.Where(s => s.Status == "Active"), "DesignationID", "Desigation_Name");
            ViewBag.UserType = new SelectList(db.St_PermissionType.Where(s => s.status == "Active"), "PTypeID", "PType");
            return View(siteUser);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult DeleteFunc(int id)
        {
            var flag = false;
            try
            {
                var data = db.SiteUsers.FirstOrDefault(x => x.UserID == id);
                if (data != null)
                {
                    data.Status = "Deleted";
                    db.SaveChanges();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return Json(new { success = flag }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UserChangePasswordModal(int id)
        {
            ViewBag.Id = id;
            return PartialView("UserChangePassword");
        }

        [HttpPost]
        public ActionResult UserChangePassword(PasswordChangeModel passwordChangeModel)
        {
            bool status = false;
            string msg = "Password Not changed";
            if (ModelState.IsValid)
            {
                var userPassword = db.SiteUsers.FirstOrDefault(m => m.UserID == passwordChangeModel.UserID);
                if (userPassword != null)
                {
                    userPassword.Password = DataUtility.MD5Hash(passwordChangeModel.NewPassword);
                    db.SaveChanges();
                    status = true;
                    msg = "Password changed successfully";
                }
            }

            if (Request.IsAjaxRequest())
            {
                return Json(new { success = status, message = msg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

    }
}