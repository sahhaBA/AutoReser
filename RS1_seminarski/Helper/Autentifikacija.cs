using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Podaci.Entiteti;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;


namespace RS1_seminarski.Helper
{
    public static class Account
    {

        public static void SetLogiraniKorisnik(this HttpContext httpContext, KorisnickiNalog korisnik)
        {

            httpContext.Session.Set<KorisnickiNalog>("kljuc", korisnik);
        }

        public static KorisnickiNalog GetLogiraniKorisnik(this HttpContext httpContext)
        {
            var k = httpContext.Session.Get<KorisnickiNalog>("kljuc");
            return k;
        }
    }
}
