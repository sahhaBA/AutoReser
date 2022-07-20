using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podaci.EF;
using RS1_seminarski.Modelview;
using Podaci.Entiteti;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RS1_seminarski.Controllers
{
    public class StavkeController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz(int ID)
        {
            var stavke = db.StavkeRezervacije.Where(s=>s.RezervacijaID==ID).Select(s => new StavkeRezervacijePrikazWM.Row
            {
                RezervacijaID=s.RezervacijaID,
                ModelAutomobila=s.Automobil.Model+" "+s.Automobil.Proizvodjac,
                Cijena=s.Cijena,
            
            })
            .ToList();

            StavkeRezervacijePrikazWM m = new StavkeRezervacijePrikazWM();
            m.StavkeRows = stavke;

            return View(m);

        }

        //public IActionResult Uredi(int ID)
        //{
        //    var stavka = db.StavkeRezervacije.Where(s => s.RezervacijaID == ID).Select(s => new StavkeRezervacijaUrediWM
        //    {
        //       RezervacijaID=s.RezervacijaID,
        //       Cijena=s.Cijena,
        //       StavkeAutomobili=db.Automobili.Select(s=>new SelectListItem
        //       {
        //           Value=s.AutomobilID.ToString(),
        //           Text=s.Model+" "+s.Proizvodjac,
        //       }).ToList(),

        //    }).Single();

        //    return View(stavka);

        //}
        public IActionResult Dodaj(int ID)
        {
            StavkeRezervacijaUrediWM m = new StavkeRezervacijaUrediWM();
            m.RezervacijaID = ID;
            m.StavkeAutomobili = db.Automobili.Select(s => new SelectListItem
            {
                Value = s.AutomobilID.ToString(),
                Text = s.Model + " " + s.Proizvodjac,
            }).ToList();

            return View(m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.StavkeRezervacije.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/Stavke/Prikaz");
        }

        public IActionResult Snimi(StavkeRezervacijaUrediWM x)
        {
            var stavka = db.StavkeRezervacije.Where(s => s.RezervacijaID == x.RezervacijaID && s.AutomobilID == x.AutomobilID).FirstOrDefault();
               if(stavka==null) //ovo je vazno da ne pravi bug
            {
                StavkeRezervacije s = new StavkeRezervacije
                {
                    RezervacijaID = x.RezervacijaID,
                    AutomobilID = x.AutomobilID,
                    Cijena = x.Cijena,
                    Popust = 0


                };
                db.Add(s);
            }
               else
            {
                stavka.Cijena = x.Cijena;
            }

                    
            db.SaveChanges();
            return Redirect("/Stavke/Prikaz?ID="+x.RezervacijaID);
        }

    }
}

