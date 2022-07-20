using System.Collections.Generic;

namespace Podaci.Entiteti
{
     public class Grad
    {
        public int GradID { get; set; }
        public string Naziv { get; set; }
        public string PostabskiBroj { get; set; }

        public List<Osoba> ListaOsoba { get; set; }
    }
}
