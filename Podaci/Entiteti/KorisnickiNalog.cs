using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Podaci.Entiteti
{
    public class KorisnickiNalog : IdentityUser
    {
        public int ? OsobaID { get; set; }
        public Osoba Osoba { get; set; }
        [DataType(DataType.Date)]
        public DateTime DatumRegistracije { get; set; }
        public List<Korpa> Korpa { get; set; }
    }
}
