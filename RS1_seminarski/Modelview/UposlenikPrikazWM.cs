using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class UposlenikPrikazWM
    {
        public class Row
        {
           

            public int UposlenikID { get; set; }
            public DateTime DatumZaposlenja { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Adresa { get; set; }
            public string KorisnickoIme { get; set; }
            public string Lozinka { get; set; }

            public string NazivGrada { get; set; }
            public int Iskustvo { get; set; }
            public float MinuliStaz { get; set; }
            public string JMBG { get; set; }
            public string StrucnaSprema { get; set; }
            public string Spol { get; set; }
            public string RadnoMjesto { get; set; }

        }
       
        public List <Row> UposleniciRows { get; set; }
    }
}
