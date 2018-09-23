using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OIDBMVCWEBSITE.Models
{
    [MetadataType(typeof(StComponentsMetaData))]
    public partial class st_components
    {
    }

    public class StComponentsMetaData
    {
        [Key]
        public int comp_id { get; set; }


        [Display(Name = "Module Name")]
        [Required(ErrorMessage = "Please Select Module Name")]
        public Nullable<int> mod_id { get; set; }


        [Display(Name = "Component Name")]
        [Required(ErrorMessage = "Please Enter Component Name")]
        public string comp_name { get; set; }

        public string status { get; set; }


        [Display(Name = "Add/Edit/Delete Option")]
        [Required(ErrorMessage = "Please Select Add/Edit/Delete Option")]
        public string add_del { get; set; }


        [Display(Name = "Component File")]
        [Required(ErrorMessage = "Please Enter Component File")]
        public string comp_file { get; set; }

        public string comp_type { get; set; }

        public Nullable<int> entry_by { get; set; }

        public Nullable<System.DateTime> entry_date { get; set; }

        public string ip_addr { get; set; }

        public Nullable<int> flag { get; set; }

        public Nullable<int> pos { get; set; }

    }
}