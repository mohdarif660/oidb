//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OIDBMVCWEBSITE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class st_components
    {
        public st_components()
        {
            this.map_component_usertype = new HashSet<map_component_usertype>();
            this.permissions = new HashSet<permission>();
        }
    
        public int comp_id { get; set; }
        public Nullable<int> mod_id { get; set; }
        public string comp_name { get; set; }
        public string status { get; set; }
        public string add_del { get; set; }
        public string comp_file { get; set; }
        public string comp_type { get; set; }
        public Nullable<int> entry_by { get; set; }
        public Nullable<System.DateTime> entry_date { get; set; }
        public string ip_addr { get; set; }
        public Nullable<int> flag { get; set; }
        public Nullable<int> pos { get; set; }
    
        public virtual st_module st_module { get; set; }
        public virtual ICollection<map_component_usertype> map_component_usertype { get; set; }
        public virtual ICollection<permission> permissions { get; set; }
    }
}