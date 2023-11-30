using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.Models
{
    public class ParkRepository
    {
        public int ParkId { get; set; }
        [Display(Name ="Park Name")]
        public string Name { get; set; }
        public string Location { get; set; }
        [Display(Name ="Park Entry Fee")]
        public decimal Fee { get; set; }
    }
    [MetadataType(typeof(ParkRepository))]
    partial class Park { }

}