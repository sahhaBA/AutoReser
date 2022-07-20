using System.Collections.Generic;

namespace Podaci.Entiteti
{
   public  class PaketOpreme
    {
        public int PaketOpremeID { get; set; }
        public string Naziv { get; set; }
        public List<PaketOpremeOprema> PaketOpremeOprema { get; set; }
    }
}
