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
    public class KategorijaController : Controller
    {

        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Kategorije.Select(s => new KategorijaWM.Row
            {
                KategorijaID = s.KategorijaID,
                naziv = s.Naziv

            })
            .ToList();

            KategorijaWM m = new KategorijaWM();
            m.KategorijaRow = stavke;

            return View(m);
        }

        public IActionResult  Uredi (int ID)
        {
            var stavka = db.Kategorije.Where(s => s.KategorijaID == ID).Select(s => new KategorijaUrediWM
            {
                KategorijaID=s.KategorijaID,
                Naziv=s.Naziv

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            KategorijaUrediWM m = new KategorijaUrediWM();


            return View("Uredi",m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.Kategorije.Find(ID);
            db.Remove(s);
            db.SaveChanges();
                return Redirect("/Kategorija/Prikaz");
        }

        public IActionResult Snimi(KategorijaUrediWM x)
        {
            Kategorija k;
            
            if(x.KategorijaID==0)
            {
                k = new Kategorija();
                db.Add(k);
            }
            else
            {
                k = db.Kategorije.Find(x.KategorijaID);
            }

            
            k.Naziv = x.Naziv;

            db.SaveChanges();

            return  Redirect("/Kategorija/Prikaz");
        }
             
    }
}
