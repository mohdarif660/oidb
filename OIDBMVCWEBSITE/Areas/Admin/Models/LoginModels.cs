using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OIDBMVCWEBSITE.Areas.Admin.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Please Enter User Name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Security Code")]
        [Display(Name = "Security Code")]
        public string SecurityCode { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string hiddenRandomNumber { get; set; }
        public string hiddenUserName { get; set; }
        public string hiddenPassword { get; set; }
    }
}