using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.ViewModel
{
    public class BookingMod
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string SafariName { get; set; }
        
        public string VTYpe { get; set; }
        public decimal TotalCost { get; set; }
    }
}