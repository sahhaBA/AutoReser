using System;
using System.Collections.Generic;

namespace Podaci.Entiteti
{
     public class Racun
    {
        public int RacunID { get; set; }
        public DateTime DatumRacuna  { get; set; }
        public DateTime DatumDospijeca { get; set;  }
        public string Napomena { get; set; }

        public int NacinPlacanjaID { get; set; }
        public NacinPlacanja NacinPlacanja  { get; set; }

        public int RezervacijaID { get; set; }
        public Rezervacija Rezervacija  { get; set; }

        public List<StavkeRacuna> StavkeRacuna { get; set; }
    }
}
