using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RS1_seminarski.Models
{
    public static class SkladisteMogucnosti
    {
        public static List<Claim> SveMogucnosti = new List<Claim>()
        {
            new Claim("Kreiranje", "Kreiranje"),
            new Claim("Uređivanje", "Uređivanje"),
            new Claim("Brisanje", "Brisanje")
        };
    }
}
