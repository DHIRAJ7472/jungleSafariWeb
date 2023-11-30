using jungleSafariWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace jungleSafariWeb.ViewModel
{
    public class TouristView
    {
        [Display(Name = "Employee Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tourist Name")]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DateCheck(ErrorMessage = "age must be greator than 22")]
        public System.DateTime DateOfBirth { get; set; }

        [MaxLength(10)]
        public string MobileNo { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [Display()]
        [DataType(DataType.EmailAddress)]
        //[Remote("CheckEmail", "Emp", ErrorMessage = "This email is already present")]
        public string EmailId { get; set; }
        [Required]
        public string IdentityName { get; set; }
        [Required]
        public string IdentityNumber { get; set; }
        public int BookingId { get; set; }
    }
}