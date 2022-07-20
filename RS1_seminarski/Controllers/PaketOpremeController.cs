using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podaci.EF;
using Podaci.Entiteti;
using RS1_seminarski.Modelview;


namespace RS1_seminarski.Controllers
{
    public class PaketOpremeController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.PaketiOpreme.Select(s => new PaketOpremePrikazWM.Row
            {
                PaketOpremeID = s.PaketOpremeID,
                Naziv = s.Naziv
            })
            .ToList();

            PaketOpremePrikazWM m = new PaketOpremePrikazWM();
            m.PaketOpremeRow = stavke;
            return View(m);
        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.PaketiOpreme.Where(s => s.PaketOpremeID == ID).Select(s => new PaketOpremeUrediWM
            {
               PaketOPremeID=s.PaketOpremeID,
               Naziv=s.Naziv

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            PaketOpremeUrediWM m = new PaketOpremeUrediWM();


            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.PaketiOpreme.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/PaketOpreme/Prikaz");
        }

        public IActionResult Snimi(PaketOpremeUrediWM x)
        {
            PaketOpreme p;

            if (x.PaketOPremeID == 0)
            {
                p = new PaketOpreme();
                db.Add(p);
            }
            else
            {
                p = db.PaketiOpreme.Find(x.PaketOPremeID);
            }

            p.Naziv = x.Naziv;
         
           db.SaveChanges();

            return Redirect("/PaketOpreme/Prikaz");
        }
    }
}
