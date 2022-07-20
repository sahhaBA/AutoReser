using System;

namespace Podaci.Entiteti
{
     public class Klijent
    {
        public int KlijentID { get; set; }
        public  DateTime DatumRegistracije { get; set; }
        public bool Activan { get; set; }

        public int ? OsobaID { get; set; }
        public Osoba Osoba { get; set; }
    }
}
