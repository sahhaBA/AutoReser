using System.Collections.Generic;

namespace Podaci.Entiteti
{
     public class Oprema
    {
        public int OpremaID { get; set; }
        public string OpisOpreme { get; set; }
        public List<PaketOpremeOprema> PaketOpremeOprema { get; set; }
    }
}
