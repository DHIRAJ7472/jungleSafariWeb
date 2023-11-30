using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.ViewModel
{
    public class ParkView
    {
        public int ParkId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Fee { get; set; }
    }
}