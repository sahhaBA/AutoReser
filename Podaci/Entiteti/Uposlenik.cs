using System;

namespace Podaci.Entiteti
{
   public class Uposlenik
    {
        public int UposlenikID { get; set; }
        public DateTime DatumZaposljenja { get; set; }
        public int Iskustvo  { get; set; }
        public float MinuliStaz { get; set; }
        public string JMBG { get; set; }

        public int  OsobaID { get; set; }
        public Osoba Osoba { get; set; }

        public int StrucnaSpremaID { get; set; }
        public StrucnaSprema StrurucnaSprema { get; set; }

        public int RadnoMjestoID { get; set; }
        public RadnoMjesto RadnoMjesto { get; set; }
    }
}
