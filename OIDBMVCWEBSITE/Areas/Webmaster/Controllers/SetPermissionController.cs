using OIDBMVCWEBSITE.Areas.Webmaster.Models;
using OIDBMVCWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIDBMVCWEBSITE.Areas.Webmaster.Controllers
{
    public class SetPermissionController : Controller
    {
        private OIDBEntities db = new OIDBEntities();
        //
        // GET: /Webmaster/SetPermission/
        // [HttpPost]
        public ActionResult SetPermission(int id)
        {
            DataUtility dataUtility = new DataUtility();
            DataTable dataTable = new DataTable();
            var query = from siteuser in db.SiteUsers
                        join mapcomtype in db.map_component_usertype on siteuser.User_Type_ID equals mapcomtype.ut_id
                        join stcomp in db.st_components on mapcomtype.comp_id equals stcomp.comp_id
                        join stmod in db.st_module on stcomp.mod_id equals stmod.mod_id
                        where siteuser.Status == "Active" && mapcomtype.status == "Active" && siteuser.UserID == id
                        orderby stmod.module_name, stcomp.comp_name
                        select new { siteuser.UserID, mapcomtype.mpu, siteuser.User_Type_ID, stcomp.mod_id, stcomp.comp_id, stcomp.comp_name, stmod.module_name };

            dataTable = dataUtility.ToDataTable(query.ToList());
            List<SetPermissionViewModel> listSetPermissionViewModel = new List<Models.SetPermissionViewModel>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                bool addper = false;
                bool editper = false;
                bool deleteper = false;
                bool naper = false;
                int userId = Convert.ToInt32(dataTable.Rows[i]["UserID"].ToString());
                int comId = Convert.ToInt32(dataTable.Rows[i]["comp_id"].ToString());
                int modId = Convert.ToInt32(dataTable.Rows[i]["mod_id"].ToString());

                var permissionData = from setper in db.permissions where setper.user_id == userId && setper.mod_id == modId && setper.comp_id == comId select setper;

                DataTable datatableper = new DataTable();
                datatableper = dataUtility.ToDataTable(permissionData.ToList());
                if (datatableper.Rows.Count > 0)
                {
                    if (datatableper.Rows[0]["add_perm"].ToString() == "1")
                    {
                        addper = true;
                    }
                    if (datatableper.Rows[0]["mod_perm"].ToString() == "1")
                    {
                        editper = true;
                    }
                    if (datatableper.Rows[0]["del_perm"].ToString() == "1")
                    {
                        deleteper = true;
                    }
                    if (datatableper.Rows[0]["notappl"].ToString() == "1")
                    {
                        naper = true;
                    }
                }
                listSetPermissionViewModel.Add(new SetPermissionViewModel()
                {
                    Component_Id = comId,
                    Module_Id = modId,
                    Component_Name = dataTable.Rows[i]["comp_name"].ToString(),
                    Module_Name = dataTable.Rows[i]["module_name"].ToString(),
                    AddCheckBox = addper,
                    EditCheckBox = editper,
                    DeletedCheckBox = deleteper,
                    NotApplicableCheckBox = naper,
                    User_Id = userId
                });
            }
            var displayData = from siteuser in db.SiteUsers
                              join stper in db.St_PermissionType on siteuser.User_Type_ID equals stper.PTypeID
                              where siteuser.Status == "Active" && siteuser.UserID == id
                              select new
                              {
                                  siteuser.Email,
                                  siteuser.Name,
                                  stper.PType
                              };

            ViewBag.UserName = displayData.FirstOrDefault().Name;
            ViewBag.Email = displayData.FirstOrDefault().Email;
            ViewBag.PType = displayData.FirstOrDefault().PType;
            return View(listSetPermissionViewModel);
        }


        [HttpPost]
        [ActionName("SetPermission")]
        public ActionResult SetPermissionToUser(List<SetPermissionViewModel> setPermissionViewModel)
        {
            foreach (var item in setPermissionViewModel)
            {
                // var getpermission =from perm in db.permissions   where perm.user_id == item.User_Id && perm.mod_id == item.Module_Id && perm.comp_id == item.Component_Id select perm;
                //if(getpermission!=null)
                //{
                //var permId = getpermission.FirstOrDefault().perm_id;
                permission per = db.permissions.FirstOrDefault(x => x.user_id == item.User_Id && x.mod_id == item.Module_Id && x.comp_id == item.Component_Id);
                if (per != null)
                {
                    per.add_perm = CheckPerm(item.AddCheckBox);
                    per.mod_perm = CheckPerm(item.EditCheckBox);
                    per.del_perm = CheckPerm(item.DeletedCheckBox);
                    per.notappl = CheckPerm(item.NotApplicableCheckBox);
                }
                else
                {
                    permission per1 = new permission();
                    per1.user_id = item.User_Id;
                    per1.mod_id = item.Module_Id;
                    per1.comp_id = item.Component_Id;
                    per1.add_perm = CheckPerm(item.AddCheckBox);
                    per1.mod_perm = CheckPerm(item.EditCheckBox);
                    per1.del_perm = CheckPerm(item.DeletedCheckBox);
                    per1.notappl = CheckPerm(item.NotApplicableCheckBox);
                    db.permissions.Add(per1);
                }
                //}
                // else
                //{

                //}
            }
            int i = db.SaveChanges();

            TempData["alert"] = "User Permission Set Successfully";

            return RedirectToAction("SetPermission", "SetPermission");
            //return View();
        }

        public int CheckPerm(bool checkBox)
        {
            if (checkBox)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
