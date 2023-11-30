using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.ViewModel
{
    public class BookingStatusView
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal TotalCost { get; set; }
       
        public string SafariName { get; set; }
        public System.DateTime SafariDate { get; set; }
        public string SafariTime { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string VType { get; set; }


    }
}