using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class KlijentPrikazWM
    {
        public class Row
        {
            public int KlijentID { get; set; }
            public DateTime DatumRegistracije { get; set; }
            public bool Activan { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Adresa { get; set; }
            public string KorisnickoIme { get; set; }
            public string Lozinka { get; set; }
            public string NazivGrada { get; set; }    
        }
       
        public List <Row> KlijentRows { get; set; }
    }
}
