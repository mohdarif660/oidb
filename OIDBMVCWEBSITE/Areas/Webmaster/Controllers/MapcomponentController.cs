using OIDBMVCWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OIDBMVCWEBSITE;
using OIDBMVCWEBSITE.Areas.Webmaster.Models;

namespace OIDBMVCWEBSITE.Areas.Webmaster.Controllers
{
    public class MapcomponentController : Controller
    {
        private OIDBEntities db = new OIDBEntities();

        public ActionResult Index()
        {
            ViewBag.Usertype = new SelectList(db.St_PermissionType.Where(s => s.status == "Active"), "PTypeID", "PType");
            return View();
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Index(List<MapComponentViewModel> mapComponentViewModel)
        {
            foreach (var item in mapComponentViewModel)
            {
                if (item.MapCheckBox)
                {
                    bool recordExist = db.map_component_usertype.Any(s => (s.comp_id == item.Component_Id) && (s.ut_id == item.UserType_Id));
                    if (recordExist)
                    {
                        map_component_usertype mapcomponentusertype1 = db.map_component_usertype.Single(xm => (xm.comp_id == item.Component_Id && xm.ut_id == item.UserType_Id));
                        mapcomponentusertype1.comp_id = item.Component_Id;
                        mapcomponentusertype1.ut_id = item.UserType_Id;
                        mapcomponentusertype1.status = "Active";
                        mapcomponentusertype1.entryby = 0;
                        mapcomponentusertype1.entrydate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                        mapcomponentusertype1.ip_addr = HttpContext.Request.UserHostAddress;
                    }
                    else
                    {
                        map_component_usertype mapcomponentusertype = new map_component_usertype();
                        mapcomponentusertype.comp_id = item.Component_Id;
                        mapcomponentusertype.ut_id = item.UserType_Id;
                        mapcomponentusertype.status = "Active";
                        mapcomponentusertype.entryby = 0;
                        mapcomponentusertype.entrydate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                        mapcomponentusertype.ip_addr = HttpContext.Request.UserHostAddress;
                        db.map_component_usertype.Add(mapcomponentusertype);
                    }
                }
                else
                {
                    bool recordExist = db.map_component_usertype.Any(s => (s.comp_id == item.Component_Id) && (s.ut_id == item.UserType_Id));
                    if (recordExist)
                    {
                        map_component_usertype mapcomponentusertype = db.map_component_usertype.Single(x => (x.comp_id == item.Component_Id) && (x.ut_id == item.UserType_Id));
                        mapcomponentusertype.comp_id = item.Component_Id;
                        mapcomponentusertype.ut_id = item.UserType_Id;
                        mapcomponentusertype.status = "Deleted";
                    }
                }
            }
            int im = db.SaveChanges();

            TempData["alert"] = "Component Mapped Successfully";

            ViewBag.Usertype = new SelectList(db.St_PermissionType.Where(s => s.status == "Active"), "PTypeID", "PType");
            return View();
        }


        [HttpPost]
        public ActionResult GetComponent(int id)
        {
            DataUtility dataUtility = new DataUtility();
            DataTable dataTable = new DataTable();
            dataTable = dataUtility.ToDataTable(db.st_components.Where(s => s.status == "Active").OrderBy(s => s.comp_type).ThenBy(s => s.comp_name).ToList());
            // MapCheckBoxList mapCheckBoxList = new MapCheckBoxList();
            List<MapComponentViewModel> listMapComponentViewModel = new List<Models.MapComponentViewModel>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                bool j = false;
                int com_id = Convert.ToInt32(dataTable.Rows[i]["comp_id"].ToString());
                var mapdata = from m in db.map_component_usertype where m.status == "Active" && m.comp_id == com_id && m.ut_id == id select m;
                DataTable dataTable1 = new DataTable();
                dataTable1 = dataUtility.ToDataTable(mapdata.ToList());
                if (dataTable1.Rows.Count > 0)
                {
                    //for (int p = 0; p < dataTable1.Rows.Count; p++)
                    //{
                    if (Convert.ToInt32(dataTable.Rows[i]["comp_id"].ToString()) == Convert.ToInt32(dataTable1.Rows[0]["comp_id"].ToString()))
                    {
                        j = true;
                    }
                    // }
                }

                listMapComponentViewModel.Add(new MapComponentViewModel()
                {
                    Component_Id = Convert.ToInt32(dataTable.Rows[i]["comp_id"].ToString()),
                    Component_Name = dataTable.Rows[i]["comp_name"].ToString(),
                    Component_Type = dataTable.Rows[i]["comp_type"].ToString(),
                    UserType_Id = id,
                    MapCheckBox = j
                });
            }
            //mapCheckBoxList.CheckBoxItem = listMapComponentViewModel;
            return PartialView("_MapPartialView", listMapComponentViewModel);
        }
    }
}
