using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RS1_seminarski.Modelview
{
    public class AutobiliUrediWM
    {
        public int AutomobilID { get; set; }
        public string Proizvodjac { get; set; }
        public string Model { get; set; }
        public string SifraAutomobila { get; set; }
        public int Zaliha { get; set; }


        public int KategorijaID { get; set; }
        public List<SelectListItem> KategorijaStavke { get; set; }

        public int PaketOpremeID { get; set; }
        public List<SelectListItem> PaketOpremeStavke { get; set; }

        public int PoreznaStopaID { get; set; }
        public List<SelectListItem> PoreznaStopaStavke { get; set; }
    }
}
