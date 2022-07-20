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
    public class StrucnaSpremaController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.StrucnaSprema.Select(s => new StrucnaSpremaPrikazWM.Row
            {
               StrucnaSpremaID=s.StrucnaSpremaID,
               Naziv=s.Naziv

            })
            .ToList();

            StrucnaSpremaPrikazWM m = new StrucnaSpremaPrikazWM();
            m.StrucnaSpremaRow = stavke;

            return View(m);

        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.StrucnaSprema.Where(s => s.StrucnaSpremaID == ID).Select(s => new StrucnaSpremaUrediWM
            {
                StrucnaSpremaID=s.StrucnaSpremaID,
                Naziv = s.Naziv
            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            StrucnaSpremaUrediWM m = new StrucnaSpremaUrediWM();


            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var stavke = db.StrucnaSprema.Where(s => s.StrucnaSpremaID == ID).FirstOrDefault();
            db.RemoveRange(stavke);

            var s = db.StrucnaSprema.Find(ID);
            db.Remove(s);
            db.SaveChanges();

            return Redirect("/StrucnaSprema/Prikaz");
        }

        public IActionResult Snimi(StrucnaSpremaUrediWM x)
        {
            StrucnaSprema k;

            if (x.StrucnaSpremaID == 0)
            {
                k = new StrucnaSprema();
                db.Add(k);
            }
            else
            {
                k = db.StrucnaSprema.Find(x.StrucnaSpremaID);
            }


            k.Naziv = x.Naziv;

            db.SaveChanges();

            return Redirect("/StrucnaSprema/Prikaz");
        }

    }
}

