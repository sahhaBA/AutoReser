using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class ListaKorisnikaVM
    {
        public string Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Spol { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string PoštanskiBr { get; set; }
        public string DatumReg { get; set; }
    }
}
