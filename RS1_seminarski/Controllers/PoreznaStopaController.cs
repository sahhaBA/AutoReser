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
    public class PoreznaStopaController : Controller
    {

        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.PorezneStope.Select(s => new PoreznaStopaWM.Row

            {
               PoreznaStopaID=s.PoreznaStopaID,
               Naziv=s.Naziv,
               Iznos=s.Iznos,

            })
            .ToList();

            PoreznaStopaWM  m= new PoreznaStopaWM();
            m.PoreznaStopaRows= stavke;

            return View(m);
        }

        public IActionResult  Uredi (int ID)
        {
            var stavka = db.PorezneStope.Where(s => s.PoreznaStopaID == ID).Select(s => new PoreznaStopaUrediWM
            {
                PoreznaStopaID = s.PoreznaStopaID,
                Naziv = s.Naziv,
                Iznos = s.Iznos,

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            PoreznaStopaUrediWM m = new PoreznaStopaUrediWM();


            return View("Uredi",m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.PorezneStope.Find(ID);
            db.Remove(s);
            db.SaveChanges();
                return Redirect("/PoreznaStopa/Prikaz");
        }

        public IActionResult Snimi(PoreznaStopaUrediWM x)
        {
            PoreznaStopa p;
            
            if(x.PoreznaStopaID==0)
            {
                p = new PoreznaStopa();
                db.Add(p);
            }
            else
            {
                p = db.PorezneStope.Find(x.PoreznaStopaID);
            }

            
            p.Naziv = x.Naziv;
            p.Iznos = x.Iznos;

            db.SaveChanges();

            return  Redirect("/PoreznaStopa/Prikaz");
        }
             
    }
}
