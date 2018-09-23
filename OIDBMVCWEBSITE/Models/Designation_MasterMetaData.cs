using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIDBMVCWEBSITE.Models
{
    public partial class Designation_Master
    {
    }

    public class Designation_MasterMetaData
    {
        public int DesignationID { get; set; }
        public string Desigation_Name { get; set; }
        public string Status { get; set; }
        public Nullable<int> EntryBy { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string IpAddress { get; set; }

        
    }
}