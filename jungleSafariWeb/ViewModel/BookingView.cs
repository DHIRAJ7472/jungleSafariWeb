using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.ViewModel
{
    public class BookingView
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int PId { get; set; }
        public int SafariId { get; set; }
        public int GateId { get; set; }
        public int VehicleId { get; set; }
        public decimal TotalCost { get; set; }
    }
}