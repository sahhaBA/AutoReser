using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podaci.EF;
using RS1_seminarski.Modelview;
using Podaci.Entiteti;


namespace RS1_seminarski.Controllers
{
    public class RadnoMjestoController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.RadnoMjesto.Select(s => new RadnoMjestoPrikazWM.Row
            {
                RadnoMjestoID= s.RadnoMjestoID,

                Naziv = s.Naziv

            })
            .ToList();

            RadnoMjestoPrikazWM m = new RadnoMjestoPrikazWM();
            m.RadnoMjestoRow = stavke;

            return View(m);

        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.RadnoMjesto.Where(s => s.RadnoMjestoID == ID).Select(s => new RadnoMjestoUrediWM
            {
                RadnoMjestoID = s.RadnoMjestoID,
                Naziv = s.Naziv

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            RadnoMjestoUrediWM m = new RadnoMjestoUrediWM();


            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.RadnoMjesto.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/RadnoMjesto/Prikaz");
        }

        public IActionResult Snimi(RadnoMjestoUrediWM x)
        {
            RadnoMjesto k;

            if (x.RadnoMjestoID == 0)
            {
                k = new RadnoMjesto();
                db.Add(k);
            }
            else
            {
                k = db.RadnoMjesto.Find(x.RadnoMjestoID);
            }


            k.Naziv = x.Naziv;

            db.SaveChanges();

            return Redirect("/RadnoMjesto/Prikaz");
        }

    }
}

