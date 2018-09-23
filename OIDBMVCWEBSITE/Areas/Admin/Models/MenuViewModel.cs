using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OIDBMVCWEBSITE.Areas.Admin.Models
{
    public class MenuViewModel
    {
        public int Component_Id { get; set; }
        public int Module_Id { get; set; }
        public string Component_Name { set;get;}
        public string Component_File { set; get; }
        public string Module_Name { set; get; }
        public int Add_Permission { get; set; }
        public int Del_Permission { get; set; }
        public int Edit_Permission { get; set; }
        public int NotAppPermission { get; set; }
    }
    
}