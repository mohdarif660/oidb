using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIDBMVCWEBSITE.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
      
        // GET: /Admin/Dashboard/

        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
