using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OIDBMVCWEBSITE.Models
{
    [MetadataType(typeof(SiteUserMetaData))]
    public partial class SiteUser
    {

    }


    public class SiteUserMetaData
    {
        [Key]
        public int UserID { get; set; }
       
        [Required(ErrorMessage = "Please Enter User Name")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Select Designation")]
        [Display(Name = "Designation")]
        public Nullable<int> DesignationID { get; set; }

        [Required(ErrorMessage ="Please Enter Email Address")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage ="Please Enter Valid Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required (ErrorMessage ="Please Select User Type")]
        [Display(Name = "User Type")]
        public Nullable<int> User_Type_ID { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Contact Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please Enter Valid Contact Number")]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Orgnisation Name")]
        [Display(Name = "Organisation Name")]
        public string OrgnisationName { get; set; }

        public string SessionID { get; set; }

        public Nullable<int> EntryBy { get; set; }

        public Nullable<System.DateTime> EntryDate { get; set; }

        public string IpAddress { get; set; }

        public string Status { get; set; }

        
       
    }


    public class PasswordChangeModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage ="Please Enter New Password")]
        [Display(Name ="New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    

}