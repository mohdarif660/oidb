using OIDBMVCWEBSITE.Areas.Admin.Models;
using OIDBMVCWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OIDBMVCWEBSITE.Areas.Admin.Controllers
{
   
    public class AccountController : Controller
    {
        private OIDBEntities db = new OIDBEntities();
        //
        // GET: /Admin/Account/
        DataUtility dataUtility = new DataUtility();
            
        public ActionResult Login()
        {
            //Random random = new Random();
            //long leftNumber = random.Next(10000, 99999);
            //long rightNumber = random.Next(10000, 99999);
            //string seedValue = leftNumber + "." + rightNumber;
            ViewBag.Seedvalues = dataUtility.GetSeedValue();
            return View();
           
        }

        [HttpPost]
        public ActionResult Login(LoginModels loginModels)
        { 
            //object objectCaptcha = Session["captcha"];
          //if (loginModels.SecurityCode == Convert.ToString(objectCaptcha).ToUpper())
         
            string dbUserName;
            string dbPassword;
            string encodePassword;
            string encodeUserName;
            if (ModelState.IsValid)
            {


                try
                {
                    string _userName;
                    SiteUser checkUser = (from siteUser in db.SiteUsers where siteUser.UserName == loginModels.UserName && siteUser.Status == "Active" select siteUser).FirstOrDefault();
                    if (checkUser != null)
                    {
                        dbUserName = checkUser.UserName;
                        dbPassword = checkUser.Password;
                        encodePassword = DataUtility.MD5Hash(dbPassword + loginModels.hiddenRandomNumber);
                        encodeUserName = DataUtility.MD5Hash(dbUserName);
                        if (encodeUserName == loginModels.hiddenUserName)
                        {
                            ViewBag.UserId = checkUser.UserID;
                            _userName = checkUser.UserName;
                        }
                        if (encodePassword == loginModels.hiddenPassword && encodeUserName == loginModels.hiddenUserName)
                        {
                            Session["UserId"] = checkUser.UserID;
                            Session["UserTypeId"] = checkUser.User_Type_ID;
                            Session["UserName"] = checkUser.UserName;
                            Session["Name"] = checkUser.Name;




                            if (checkUser.FailureAttemptCount > 0)
                            {
                                int cvalue = db.Database.ExecuteSqlCommand("Update SiteUsers set FailureAttemptCount = '0' where UserID = '" + checkUser.UserID + "'");
                            }

                            // Logoff Details Enter LoginLogOffUserDetail Table
                            LoginLogOffUserDetail loginLogOffUserDetail = new LoginLogOffUserDetail();
                            loginLogOffUserDetail.Login = dbUserName;
                            loginLogOffUserDetail.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                            db.LoginLogOffUserDetails.Add(loginLogOffUserDetail);
                            db.SaveChanges();

                            return RedirectToAction("Dashboard", "Dashboard", new { area = "Admin" });
                        }
                        else
                        {
                            // Failure Details Enter Failed_Login_Attempts Table
                            Failed_Login_Attempts failed_Login_Attempts = new Failed_Login_Attempts();
                            failed_Login_Attempts.Login = dbUserName;
                            failed_Login_Attempts.user_type_id = 1;
                            failed_Login_Attempts.IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                            failed_Login_Attempts.Dated = DateTime.Now;
                            db.Failed_Login_Attempts.Add(failed_Login_Attempts);
                            db.SaveChanges();
                            if (checkUser.FailureAttemptCount == null)
                            {
                                db.Database.ExecuteSqlCommand("Update SiteUsers set FailureAttemptCount ='1' where UserID = '" + checkUser.UserID + "'");
                            }
                            else if (checkUser.FailureAttemptCount >= 0)
                            {
                                checkUser.FailureAttemptCount += 1;
                                db.Database.ExecuteSqlCommand("Update SiteUsers set FailureAttemptCount = '" + checkUser.FailureAttemptCount + "' where UserID = '" + checkUser.UserID + "'");
                            }
                            ViewBag.Message = "Username or Password Not Found !!";

                        }
                        SiteUser failureAttemp = (from siteUser in db.SiteUsers where siteUser.UserName == loginModels.UserName && siteUser.Status == "Active" select siteUser).FirstOrDefault();
                        if (failureAttemp != null)
                        {
                            if (failureAttemp.FailureAttemptCount % 5 == 0)
                            {
                                string newChangePassword = Guid.NewGuid().ToString().Substring(0, 8);
                                string encodeNewChangePassword = DataUtility.MD5Hash(newChangePassword);
                                int ret = db.Database.ExecuteSqlCommand("Update SiteUsers set Password = '" + encodeNewChangePassword + "',IpAddress='" + Request.ServerVariables["REMOTE_ADDR"] + "' where UserID = '" + checkUser.UserID + "'");
                                if (ret > 0)
                                {
                                    ViewBag.Message = "Password for the site has been blocked due to continuous unsuccessful attempts to access the site !!";
                                }
                                else
                                {

                                }
                                //string subject = "New Credenatils For website:" + (cs.site_config()[9].ToString().Replace("'", ""));
                                //string msgbody = "<html><body>Password has been changed due to illegal attempts.<br>User Name is : " + dr["email"].ToString().Trim() + "<br>New Password is " + newpass.Trim() + " <br>From<br>" + (cs.site_config()[9].ToString()) + "</body></html>";
                                //bool YN = cs.send_mail(dr["Email"].ToString(), dr["Name"].ToString(), subject, msgbody, true);
                                //lb_error.Text = "Password for the site has been blocked following continuous unsuccessful attempts to access the site !!";
                                //string msg = "Password for the site has been blocked following continuous unsuccessful attempts to access the site !!";
                                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + msg + "');</script>", false);
                                //CodeNumberTextBox.Value = "";
                            }
                        }

                    }
                    else
                    {
                        ViewBag.Message = "Username and Password Not Found !!";
                    }

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    return View();
                     
                }
               
                ViewBag.Seedvalues = dataUtility.GetSeedValue();
            }
            else
            {
             
            }
            return View();
        }

    }
}
