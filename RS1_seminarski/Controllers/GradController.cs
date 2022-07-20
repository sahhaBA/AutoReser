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
    public class GradController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Gradovi.Select(s => new GradPrikazWM.Row
            {
                GradID = s.GradID,

                Naziv = s.Naziv

            })
            .ToList();

            GradPrikazWM m = new GradPrikazWM();
            m.GradoviRow = stavke;

            return View(m);

        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.Gradovi.Where(s => s.GradID == ID).Select(s => new GradUrediWM
            {
                GradID = s.GradID,
                Naziv = s.Naziv

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            GradUrediWM m = new GradUrediWM();


            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.Gradovi.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/Grad/Prikaz");
        }

        public IActionResult Snimi(GradUrediWM x)
        {
            Grad k;

            if (x.GradID == 0)
            {
                k = new Grad();
                db.Add(k);
            }
            else
            {
                k = db.Gradovi.Find(x.GradID);
            }


            k.Naziv = x.Naziv;

            db.SaveChanges();

            return Redirect("/Grad/Prikaz");
        }

    }
}

