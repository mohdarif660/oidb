using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OIDBMVCWEBSITE.Models;

namespace OIDBMVCWEBSITE.Areas.Webmaster.Controllers
{
    public class ComponentController : Controller
    {
        private OIDBEntities db = new OIDBEntities();

        // GET: /Webmaster/Component/
        public ActionResult Index()
        {
            var st_components = db.st_components.Include(s => s.st_module).Where(s => s.status == "Active").OrderBy(s => s.comp_type).ThenBy(s=>s.comp_name);
            return View(st_components.ToList());
        }

        // GET: /Webmaster/Component/Create
         [HttpPost]
         
        public ActionResult Create(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.mod_id = new SelectList(db.st_module.Where(s => s.status == "Active").OrderBy(s => s.module_name), "mod_id", "module_name");
                return PartialView("Create");
            }
            else
            {
                st_components st_components = db.st_components.Find(id);
                ViewBag.mod_id1 = new SelectList(db.st_module.Where(s => s.status == "Active").OrderBy(s => s.module_name), "mod_id", "module_name", st_components.mod_id);
                if (st_components == null)
                {
                    return HttpNotFound();
                }
                return PartialView("Create",st_components);
            }


        }

        // POST: /Webmaster/Component/Create


        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create1(st_components st_components1)
        {
            bool status = false;
            string msg = "Wrong Data Entry";
            if (ModelState.IsValid)
            {
                if (st_components1.comp_id == 0)
                {
                    st_module stm = db.st_module.Find(st_components1.mod_id);
                    st_components1.mod_id = st_components1.mod_id;
                    st_components1.comp_name = st_components1.comp_name;
                    st_components1.status = "Active";
                    st_components1.add_del = st_components1.add_del;
                    st_components1.comp_file = st_components1.comp_file;
                    st_components1.comp_type = stm.module_name;
                    st_components1.entry_by = 0;
                    st_components1.entry_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                    st_components1.ip_addr = HttpContext.Request.UserHostAddress;
                    st_components1.flag = 0;
                    st_components1.pos = 0;
                    db.st_components.Add(st_components1);
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        status = true;
                        msg = "Component Save Successfully";
                        // return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        status = false;
                        msg = "Record Not Saved";
                        // return Json(new { success = false, message = "Record Not Saved" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    st_module stm = db.st_module.Find(st_components1.mod_id);
                    st_components1.mod_id = st_components1.mod_id;
                    st_components1.comp_name = st_components1.comp_name;
                    st_components1.status = "Active";
                    st_components1.add_del = st_components1.add_del;
                    st_components1.comp_file = st_components1.comp_file;
                    st_components1.comp_type = stm.module_name;
                    st_components1.entry_by = 0;
                    st_components1.entry_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                    st_components1.ip_addr = HttpContext.Request.UserHostAddress;
                    st_components1.flag = 0;
                    st_components1.pos = 0;
                    db.Entry(st_components1).State = EntityState.Modified;
                    int i = db.SaveChanges();
                    if (i > 0)
                    {
                        status = true;
                        msg = "Component Updated Successfully";
                    }
                    else
                    {
                        status = false;
                        msg = "Record Not Updated";
                    }
                }
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { success = status, message = msg }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.mod_id = new SelectList(db.st_module.Where(s => s.status == "Active").OrderBy(s => s.module_name), "mod_id", "module_name");
                return View(st_components1);
            }


        }

        // POST: /Webmaster/Component/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bool status = false;
            st_components st_components = db.st_components.Find(id);
            if (st_components != null)
            {
                st_components.status = "Deleted";
                db.SaveChanges();
                status = true;
            }
            return Json(new { success = status, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}