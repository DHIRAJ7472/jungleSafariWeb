using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.ViewModel
{
    public class VehicleView
    {
        public int VId { get; set; }
        public string VType { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> EntryCost { get; set; }
        public string Capacity { get; set; }
        public int ParkId { get; set; }
    }
}