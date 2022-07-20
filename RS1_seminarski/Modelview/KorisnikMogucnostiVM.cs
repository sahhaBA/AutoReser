using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class KorisnikMogucnostiVM
    {
        public KorisnikMogucnostiVM()
        {
            Mogucnosti = new List<KorisnikMogucnosti>();
        }

        public string Id { get; set; } //korisnikov id
        public List<KorisnikMogucnosti> Mogucnosti { get; set; }

        public class KorisnikMogucnosti
        {
            public string TipMogucnosti { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
