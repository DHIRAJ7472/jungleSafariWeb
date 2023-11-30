using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.ViewModel
{
    public partial class SafariDetailView
    {

        public int SafariId { get; set; }
        public string SafariName { get; set; }
        public System.DateTime SafariDate { get; set; }
        public string SafariTime { get; set; }
        public int ParkId { get; set; }
        public decimal SafariCost { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public decimal Fee { get; set; }

    }
}