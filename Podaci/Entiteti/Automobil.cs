using System.Collections.Generic;

namespace Podaci.Entiteti
{
    public class Automobil
    {
        public int AutomobilID { get; set; }
        public string Proizvodjac  { get; set; }
        public string Model { get; set; }
        public string SifraAutomobila { get; set; }
        public  int Zaliha { get; set; }
        public int KategorijaID { get; set; }
        public Kategorija  Kategorija { get; set; }

        public int PaketOpremeID { get; set; }
        public PaketOpreme PaketOpreme { get; set; }

        public int PoreznaStopaID { get; set; }
        public PoreznaStopa PoreznaStopa  { get; set; }
        public Karakteristike Karakteristike { get; set; }
        public List<StavkeRezervacije> StavkeRezervacije { get; set; }

        public List<StavkeRacuna> StavkeRacuna { get; set;  }
        public List<Korpa> Korpa { get; set; }
    }
}
