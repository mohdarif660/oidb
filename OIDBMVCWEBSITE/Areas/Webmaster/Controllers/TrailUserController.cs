using OIDBMVCWEBSITE.Areas.Webmaster.Models;
using OIDBMVCWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIDBMVCWEBSITE.Areas.Webmaster.Controllers
{
 
    public class TrailUserController : Controller
    {
        private OIDBEntities db = new OIDBEntities();
        //
        // GET: /Webmaster/TrailUser/
 
        public ActionResult Index()
        {
            MasterViewModel masterViewModel = new MasterViewModel();
            ViewBag.UserName= new SelectList(db.SiteUsers.Where(s => s.Status == "Active"), "UserID", "UserName");
            ViewBag.ActionType = new SelectList(new[] { new { Value = "1", Text = "Login Attempts" }, new { Value = "2", Text = "Unsucessful Attempts" }, new { Value = "3", Text = "User Trail" } },"Value","Text");
            return View(masterViewModel);
        }

        [HttpPost]
        public ActionResult Index(int? ddlUserId ,int? ddlActionId,DateTime? txtFromDate,DateTime? txtTodate)
        {
            MasterViewModel masterViewModel = new MasterViewModel();
            if (ddlUserId == null)
            {
                ViewBag.errorUserId = "Please Select User";
            }
            if (ddlActionId == null)
            {
                ViewBag.errorActionId = "Please Select Action";
            }
            if (txtFromDate.ToString() == "")
            {
                ViewBag.errorFromDate = "Please Enter From Date";
            }
            if (txtTodate.ToString() == "")
            {
                ViewBag.errorToDate = "Please Enter To Date";
            }
            if (ddlUserId != null && ddlActionId != null && txtFromDate.ToString() != "" && txtTodate.ToString() != "")
            {
                if (ddlActionId == 1)
                {
                    SiteUser su = db.SiteUsers.Find(ddlUserId);
                    //string fromDate = todate(txtFromDate.ToString());
                    //string toDate = todate(txtTodate.ToString());
                    //var loginQuery = db.LoginLogOffUserDetails.SqlQuery("select * from LoginLogOffUserDetails where Login = '" + su.UserName + "' and DateOfLogin >= '" + fromDate + " 00:00:00.000'and DateOfLogin <= '" + toDate + " 23:59:59.000' order by LId desc");
                    var loginQuery = from logintbl in db.LoginLogOffUserDetails where logintbl.Login == su.UserName&&(logintbl.DateOfLogin>= txtFromDate && logintbl.DateOfLogin <= EntityFunctions.AddDays(txtTodate,1)) select logintbl;
                    masterViewModel.listloginLogOffUserDetail = loginQuery.ToList();
                }
                else if (ddlActionId == 2)
                {
                    SiteUser su = db.SiteUsers.Find(ddlUserId);
                    var loginQuery = from logintbl in db.Failed_Login_Attempts where logintbl.Login == su.UserName && (logintbl.Dated >= txtFromDate && logintbl.Dated <= EntityFunctions.AddDays(txtTodate, 1)) select logintbl;
                    masterViewModel.listfailed_Login_Attempts = loginQuery.ToList();
                }
                else
                {
                    var loginQuery = from logintbl in db.UserTrails where logintbl.UserID == ddlUserId && (logintbl.Dated >= txtFromDate && logintbl.Dated <= EntityFunctions.AddDays(txtTodate, 1)) select logintbl;
                    masterViewModel.listUserTrail = loginQuery.ToList();
                }
            }
            ViewBag.UserName = new SelectList(db.SiteUsers.Where(s => s.Status == "Active"), "UserID", "UserName");
            ViewBag.ActionType = new SelectList(new[] { new { Value = "1", Text = "Login Attempts" }, new { Value = "2", Text = "Unsuccessful Attempts" }, new { Value = "3", Text = "User Trail" } }, "Value", "Text");
            return View(masterViewModel);

        }

        public string todate(string datevar)
        {
            string date_format = "";
            try
            {
                string[] sdate = { "/" };
                string[] date_split1 = datevar.Split(sdate, StringSplitOptions.None);
                string day = date_split1[0];
                string month = date_split1[1];
                string year = date_split1[2];
                date_format = year  + "-" + month + "-" + day;
            }
            catch (Exception ex)
            {
                

            }
            return date_format;
        }



    }
}
