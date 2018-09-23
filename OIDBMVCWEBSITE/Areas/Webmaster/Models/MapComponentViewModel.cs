using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OIDBMVCWEBSITE.Areas.Webmaster.Models
{
    public class MapComponentViewModel
    {
        public int Component_Id { get; set; }
        public string Component_Name { get; set; }
        public string Component_Type { get; set; }
        public bool MapCheckBox { get; set; }
        public int UserType_Id { get; set; }
        public int PtypeId { get; set; }
    }


    //public class MapCheckBoxList
    //{
    //    public List<MapComponentViewModel> CheckBoxItem { get; set; }
    //    public int PtypeId { get; set; }
    //}
}