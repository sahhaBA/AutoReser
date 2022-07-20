using System;
using System.Collections.Generic;

namespace Podaci.Entiteti
{
    public class Rezervacija
    {
        public int RezervacijaID { get; set; }
        public DateTime DatumRezervacije  { get; set; }
        public bool Aktivna  { get; set; }
        public bool Odobrena { get; set; }
        public bool Zavrsena  { get; set; }

        public int KlijentID { get; set; }
        public   Klijent Klijent  { get; set; }


        public int ? UposlenikID  { get; set; }
        public Uposlenik Uposlenik { get; set; }

        public string AdminNalogId { get; set; }
        public KorisnickiNalog AdminNalog { get; set; }

        public List<StavkeRezervacije> StavkeRezervacije { get; set; }
    }
}
