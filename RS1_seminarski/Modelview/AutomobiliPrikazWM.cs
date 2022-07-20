using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class AutomobiliPrikazWM
    {

        public  class Row{

            public int AutomobilID { get; set; }
        public string Proizvodjac { get; set; }
        public string Model { get; set; }
        public string SifraAutomobila { get; set; }
        public int Zaliha { get; set; }


        public string nazivKategorije { get; set; }


        public string PaketOpremeNaziv{ get; set; }


        public string  PoreznaStopaNaziv { get; set; }
        }

        public List<Row> AutoRows { get; set; }

    }
}