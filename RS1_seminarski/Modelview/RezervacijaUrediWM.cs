using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class RezervacijaUrediWM
    {
        public int RezervacijaID { get; set; }

        public DateTime DatumRezervacije { get; set; }
        public bool Aktivna { get; set; }
        public bool Odobrena { get; set; }
        public bool Zavrsena { get; set; }
        public int KlijentID { get; set; }
        public List<SelectListItem> KlijentiStavke { get; set; }

    }

}
