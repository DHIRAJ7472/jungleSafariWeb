using jungleSafariWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jungleSafariWeb.ViewModel
{
    public class ParkVehicleGate
    {


            public IEnumerable<GateView> Gates { get; set; }
            public IEnumerable<VehicleView> Vehicles { get; set; }
        public GateView Gate { get; set; }
        public VehicleView Vehicle { get; set; } 
        
    }
}