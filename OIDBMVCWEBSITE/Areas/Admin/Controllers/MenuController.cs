using OIDBMVCWEBSITE.Areas.Admin.Models;
using OIDBMVCWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIDBMVCWEBSITE.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private OIDBEntities db = new OIDBEntities();
     
        // GET: /Admin/Menu/
        [ChildActionOnly]
        public ActionResult _MenuPartial()
        {
            DataUtility dataUtility = new DataUtility();
            var queryMain = (from stmod in db.st_module
                             join stper in db.permissions on stmod.mod_id equals stper.mod_id
                             join stcom in db.st_components on stper.comp_id equals stcom.comp_id
                             where stper.mod_id == stcom.mod_id && stper.user_id == 1 && (stper.add_perm != 0 || stper.mod_perm != 0 || stper.del_perm != 0 || stper.notappl != 0)
                             && stmod.status == "Active" && stcom.status == "Active"
                             orderby stmod.pos
                             select (new { stmod.mod_id, stmod.module_name, stmod.pos })).Distinct();
            List<MenuViewModel> list = new List<Models.MenuViewModel>();
            DataTable dt = dataUtility.ToDataTable(queryMain.ToList());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int modid1 = Convert.ToInt32(dt.Rows[i]["mod_id"].ToString());
                var queryMenu = from stmod in db.st_module
                                join stper in db.permissions on stmod.mod_id equals stper.mod_id
                                join stcom in db.st_components on stper.comp_id equals stcom.comp_id
                                where stper.mod_id == stcom.mod_id && stper.user_id == 1 && stmod.mod_id == modid1
                                && (stper.add_perm != 0 || stper.mod_perm != 0 || stper.del_perm != 0 || stper.notappl != 0) && stmod.status == "Active" && stcom.status == "Active"
                                orderby stcom.pos, stcom.comp_name
                                select new
                                {
                                    stcom.comp_id,
                                    stmod.mod_id,
                                    stcom.comp_name,
                                    stcom.comp_file,
                                    stper.add_perm,
                                    stper.del_perm,
                                    stper.mod_perm,
                                    stper.notappl,
                                    stmod.module_name

                                };
                DataTable dtSubMenu = dataUtility.ToDataTable(queryMenu.ToList());
                for (int m = 0; m < dtSubMenu.Rows.Count; m++)
                {
                    int compid = Convert.ToInt32(dtSubMenu.Rows[m]["comp_id"]);
                    int modid = Convert.ToInt32(dtSubMenu.Rows[m]["mod_id"]);
                    int addper = Convert.ToInt32(dtSubMenu.Rows[m]["add_perm"]);
                    int delper = Convert.ToInt32(dtSubMenu.Rows[m]["del_perm"]);
                    int modper = Convert.ToInt32(dtSubMenu.Rows[m]["mod_perm"]);
                    int notappper = Convert.ToInt32(dtSubMenu.Rows[m]["notappl"]);

                    list.Add(new MenuViewModel
                    {
                        Component_Id = compid,
                        Module_Id = modid,
                        Component_Name = dtSubMenu.Rows[m]["comp_name"].ToString(),
                        Component_File = dtSubMenu.Rows[m]["comp_file"].ToString(),
                        Add_Permission = addper,
                        Del_Permission = delper,
                        Edit_Permission = modper,
                        NotAppPermission = notappper,
                        Module_Name = dtSubMenu.Rows[m]["module_name"].ToString()

                    });
                }
            }
            return PartialView("_MenuPartial", list);
        }

    }
}
