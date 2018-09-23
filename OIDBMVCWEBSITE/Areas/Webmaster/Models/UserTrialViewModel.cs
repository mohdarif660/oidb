using OIDBMVCWEBSITE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OIDBMVCWEBSITE.Areas.Webmaster.Models
{
    public class MasterViewModel
    {
         
        public List<LoginLogOffUserDetail> listloginLogOffUserDetail { get; set; }
        public List<Failed_Login_Attempts> listfailed_Login_Attempts { get; set; }
        public List<UserTrail> listUserTrail { get; set; }
       
    }
}