using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class StavkeRezervacijaUrediWM
    {
        
            public int RezervacijaID { get; set; }
            public float Cijena  { get; set; }

            public int AutomobilID { get; set; }
            public List<SelectListItem> StavkeAutomobili { get; set; } 

    }
}
