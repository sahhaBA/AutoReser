using Podaci.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class RezervacijeVM
    {
        public List<Korisnik> NaloziSaAktivnimRezervacijama { get; set; }
        public class Korisnik
        {
            public string KorisnickoIme { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Email { get; set; }
            public List<Reser> Rezervacije { get; set; }
        }
        public class Reser
        {
            public int RezervacijaID { get; set; }
            public string DatumRezervacije { get; set; }
            public bool Aktivna { get; set; }
            public bool Odobrena { get; set; }
            public bool Zavrsena { get; set; }
            public int ? UposlenikID { get; set; }
            public string Uposlenik { get; set; }
            public string AdminNalogID { get; set; }
            public string AdminNalog { get; set; }
            public Stavke StavkeRezervacije { get; set; }
            public class Stavke
            {
                public int RezervacijaID { get; set; }
                public int AutomobilID { get; set; }
                public string Automobil { get; set; }
                public float Cijena { get; set; }
                public float Popust { get; set; }
                public string Slika { get; set; }
            }
        }
    }
}
