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
    public class NacinPlacanjaController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.NacinPlacanja.Select(s => new NacinPlacanjaPrikazWM.Row
            {
                NacinPlacanjaID  = s.NacinPlacanjaID,

                Naziv = s.Naziv

            })
            .ToList();

            NacinPlacanjaPrikazWM m = new NacinPlacanjaPrikazWM();
            m.NacinPlacanjaRow= stavke;

            return View(m);

        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.NacinPlacanja.Where(s => s.NacinPlacanjaID == ID).Select(s => new NacinPlacanjaUrediWM
            {
                NacinPlacanjaID = s.NacinPlacanjaID,
                Naziv = s.Naziv

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            NacinPlacanjaUrediWM m = new NacinPlacanjaUrediWM();


            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.NacinPlacanja.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/NacinPlacanja/Prikaz");
        }

        public IActionResult Snimi(NacinPlacanjaUrediWM x)
        {
            NacinPlacanja k;

            if (x.NacinPlacanjaID == 0)
            {
                k = new NacinPlacanja();
                db.Add(k);
            }
            else
            {
                k = db.NacinPlacanja.Find(x.NacinPlacanjaID);
            }


            k.Naziv = x.Naziv;

            db.SaveChanges();

            return Redirect("/NacinPlacanja/Prikaz");
        }

    }
}

