using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class RezervacijaDetaljiWM
    {
        public int RezervacijaID { get; set; }

        public DateTime DatumRezervacije { get; set; }
        
        public string ImePrezimeKlijenta { get; set; }
      

    }

}
