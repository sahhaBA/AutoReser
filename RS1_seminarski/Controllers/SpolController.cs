using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podaci.EF;
using RS1_seminarski.Modelview;
using Podaci.Entiteti;
using RS1_seminarski.Helper;


namespace RS1_seminarski.Controllers
{
    public class SpolController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Spol.Select(s => new SpolPrikazWM.Row
            {
                SpolID = s.SpolID,

                Naziv = s.Naziv

            })
            .ToList();

            SpolPrikazWM m = new SpolPrikazWM();
            m.SpolRow = stavke;

            return View(m);

        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.Spol.Where(s => s.SpolID == ID).Select(s => new SpolUrediWM
            {
                SpolID = s.SpolID,
                Naziv = s.Naziv

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            SpolUrediWM m = new SpolUrediWM();


            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.Spol.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/Spol/Prikaz");
        }

        public IActionResult Snimi(SpolUrediWM x)
        {
            if (Account.GetLogiraniKorisnik(HttpContext) == null)
            {
                Redirect("/Autentifikacija/Index");
            }

            Spol k;

            if (x.SpolID == 0)
            {
                k = new Spol();
                db.Add(k);
            }
            else
            {
                k = db.Spol.Find(x.SpolID);
            }


            k.Naziv = x.Naziv;

            db.SaveChanges();

            return Redirect("/Spol/Prikaz");
        }

    }
}

