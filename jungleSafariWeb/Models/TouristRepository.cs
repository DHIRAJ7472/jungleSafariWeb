using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using jungleSafariWeb.ViewModel;

namespace jungleSafariWeb.Models
{
    public partial class TouristRepository
    {
        [Display(Name="Employee Id")]
        public int Id { get; set; }
        [Display(Name ="Name")]
        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        [Display(Name = "BirthDate")]
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
        [Remote("CheckEmail", "Emp", ErrorMessage = "This email is already present")]
        public string EmailId { get; set; }
        public string IdentityName { get; set; }
        [MaxLength(12)]
        public string IdentityNumber { get; set; }
        public int BookingId { get; set; }
    }

    [MetadataType(typeof(TouristRepository))]
    public partial class TouristVie{ }
}