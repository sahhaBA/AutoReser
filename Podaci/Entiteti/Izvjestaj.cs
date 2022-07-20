using System;
using System.ComponentModel.DataAnnotations;

namespace Podaci.Entiteti
{
   public  class Izvjestaj
    {
        public int IzvjestajID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string AutomobilID { get; set; }
        public string UposlenikKreiraIzvjestaj { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DatumIzvjestaja { get; set; }


        public string KorisnickiNalogID { get; set; }
        public KorisnickiNalog Korisnickinalog { get; set; }
    }
}
