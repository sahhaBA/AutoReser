using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class AutoKarakteristikePrikazWM
    {
        public class Row
        {
            public int KarakteristikeID { get; set; }
            public string Stanje { get; set; }
            public int Godiste { get; set; }
            public float Cijena { get; set; }
            public int Kilometraza { get; set; }
            public int Snaga { get; set; }
            public string Zapremina { get; set; }
            public string Gorivo { get; set; }
            public string BrojVrata { get; set; }
            public string Pogon { get; set; }
            public string Transmisija { get; set; }
            public string Boja { get; set; }
            public string Svjetla { get; set; }


            public string ModelAutomobila { get; set; }
        }
       
        public List<Row> OsobineRows { get; set; }

    }
}
