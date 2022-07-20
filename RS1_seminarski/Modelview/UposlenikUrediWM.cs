using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class UposlenikUrediWM
    {
        public int UposlenikID { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

       

        public int Iskustvo { get; set; }
        public float MinuliStaz { get; set; }
        public string JMBG { get; set; }

        public int StrucnaSpremaID { get; set; }
        public List<SelectListItem> StrucnaSpremaStavke { get; set; }

       
        public int SpolID { get; set; }
        public List<SelectListItem> SpolStavka { get; set; }
     
        public int RadnoMjestoID { get; set; }
        public List<SelectListItem> RadnoMjestoStavka { get; set; }

        public int GradID { get; set; }
        public List<SelectListItem> GradoviStavka { get; set; }

     
    }
}
