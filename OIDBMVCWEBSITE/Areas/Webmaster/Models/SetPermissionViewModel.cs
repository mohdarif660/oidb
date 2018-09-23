using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OIDBMVCWEBSITE.Areas.Webmaster.Models
{
    public class SetPermissionViewModel
    {
        public int Component_Id { get; set; }
        public int Module_Id { get; set; }
        public string Component_Name { get; set; }
        public string Module_Name { get; set; }
        public bool AddCheckBox { get; set; }
        public bool EditCheckBox { get; set; }
        public bool DeletedCheckBox { get; set; }
        public bool NotApplicableCheckBox { get; set; }
        public int User_Id { get; set; }
        
    }
}