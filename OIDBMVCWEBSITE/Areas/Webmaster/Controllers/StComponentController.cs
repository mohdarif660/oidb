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
    public class StComponentController : Controller
    {
        private OIDBEntities db = new OIDBEntities();

        //
        // GET: /Webmaster/StComponent/

        public ActionResult Index()
        {
            var st_components = db.st_components.Include(s => s.st_module).Where(s => s.status == "Active");
            return View(st_components.ToList());
        }
        //
        // GET: /Webmaster/StComponent/Details/5

        public ActionResult Details(int id = 0)
        {
            st_components st_components = db.st_components.Find(id);
            if (st_components == null)
            {
                return HttpNotFound();
            }
            return View(st_components);
        }

        //
        // GET: /Webmaster/StComponent/Create

        public ActionResult Create()
        {
            ViewBag.mod_id = new SelectList(db.st_module, "mod_id", "module_name");
            return View();
        }

        //[HttpPost]
        //public ActionResult Create2()
        //{
        //    ViewBag.mod_id = new SelectList(db.st_module, "mod_id", "module_name");
        //    return PartialView("Create1");
        //}

        //
        // POST: /Webmaster/StComponent/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(st_components st_components)
        {
            if (ModelState.IsValid)
            {
                db.st_components.Add(st_components);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mod_id = new SelectList(db.st_module, "mod_id", "module_name");
            return View(st_components);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create1(st_components st_components1)
        {
            if (ModelState.IsValid)
            {
                st_components1.mod_id = st_components1.mod_id;
                st_components1.comp_name = st_components1.comp_name;
                st_components1.status = "Active";
                st_components1.add_del = st_components1.add_del;
                st_components1.comp_file = st_components1.comp_file;
                st_components1.comp_type = st_components1.comp_type;
                st_components1.entry_by = 0;
                st_components1.entry_date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                st_components1.ip_addr = HttpContext.Request.UserHostAddress;
                st_components1.flag = 0;
                st_components1.pos = 0;
                db.st_components.Add(st_components1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mod_id = new SelectList(db.st_module, "mod_id", "module_name");
            return View(st_components1);
        }
        //
        // GET: /Webmaster/StComponent/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ViewBag.mod_id = new SelectList(db.st_module, "mod_id", "module_name");
            st_components st_components = db.st_components.Find(id);
            if (st_components == null)
            {
                return HttpNotFound();
            }
           
            return View(st_components);
        }

        //
        // POST: /Webmaster/StComponent/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(st_components st_components)
        {
            if (ModelState.IsValid)
            {
                db.Entry(st_components).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mod_id = new SelectList(db.st_module, "mod_id", "module_name");
            return View(st_components);
        }

        //
        // GET: /Webmaster/StComponent/Delete/5

        public ActionResult Delete(int id = 0)
        {
            st_components st_components = db.st_components.Find(id);
            if (st_components == null)
            {
                return HttpNotFound();
            }
            return View(st_components);
        }

        //
        // POST: /Webmaster/StComponent/Delete/5

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




        public ActionResult AddEditComponent(int comp_id)
        {

            if (comp_id == 0)
            {

                ViewBag.mod_id = new SelectList(db.st_module, "mod_id", "module_name");
                return PartialView("Create1");
            }
            else
            {
                ViewBag.mod_id = new SelectList(db.st_module, "mod_id", "module_name");
                st_components st_components = db.st_components.Find(comp_id);
                if (st_components == null)
                {
                    return HttpNotFound();
                }
                return PartialView("Create1", st_components);

            }
        }
    }
}