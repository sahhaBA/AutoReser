using Podaci.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class AutomobilDetaljnoVM
    {
        public int AutomobilID { get; set; }
        public string Proizvodjac { get; set; }
        public string Model { get; set; }
        public string SifraAutomobila { get; set; }
        public int Zaliha { get; set; }
        public string Slika1 { get; set; }
        public string Slika2 { get; set; }
        public string Slika3 { get; set; }
        public string Slika4 { get; set; }
        public string Slika5 { get; set; }
        public int KategorijaID { get; set; }
        public string Kategorija { get; set; }
        public int PaketOpremeID { get; set; }
        public string PaketOpreme { get; set; }
        public bool Obrisan { get; set; }
        public List<Oprema> ListaOpreme { get; set; }
        public Karakteristike Karakteristike { get; set; }
    }
}
