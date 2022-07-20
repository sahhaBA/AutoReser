using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podaci.EF;
using RS1_seminarski.Modelview;
using Podaci.Entiteti;


namespace RS1_seminarski.Controllers
{
    public class AutoKarakteristikeController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Karakteristike.Select(s => new AutoKarakteristikePrikazWM.Row
            {

                KarakteristikeID = s.KarakteristikeID,
                Stanje = s.Stanje,
                Godiste = s.Godiste,
                Cijena = s.Cijena,
                Kilometraza = s.Kilometraza,
                Snaga = s.Snaga,
                Zapremina = s.Zapremina,
                Gorivo = s.Gorivo,
                BrojVrata = s.BrojVrata,
                Pogon = s.Pogon,
                Transmisija = s.Transmisija,
                Boja = s.Boja,
                Svjetla = s.Svjetla,
                ModelAutomobila = s.Automobil.Proizvodjac + " " + s.Automobil.Model + " | " + s.Automobil.PaketOpreme.Naziv



            }).ToList();


            AutoKarakteristikePrikazWM m = new AutoKarakteristikePrikazWM();

            m.OsobineRows = stavke;
            return View(m);
        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.Karakteristike.Where(s => s.KarakteristikeID == ID).Select(s => new AutoKarakteristikeUrediWM
            {
                KarakteristikeID = s.KarakteristikeID,
                Stanje = s.Stanje,
                Godiste = s.Godiste,
                Cijena = s.Cijena,
                Kilometraza = s.Kilometraza,
                Snaga = s.Snaga,
                Zapremina = s.Zapremina,
                Gorivo = s.Gorivo,
                BrojVrata = s.BrojVrata,
                Pogon = s.Pogon,
                Transmisija = s.Transmisija,
                Boja = s.Boja,
                Svjetla = s.Svjetla,
                
                ModelAutomobila = db.Automobili.Select(a => new SelectListItem
                {
                    Value=a.AutomobilID.ToString(),
                    Text=a.Proizvodjac + " " + a.Model
                }).ToList()

            }).FirstOrDefault();

            return View(stavka);
        }

        public IActionResult Dodaj()
        {
            AutoKarakteristikeUrediWM m = new AutoKarakteristikeUrediWM();

            m.ModelAutomobila = db.Automobili.Select(a => new SelectListItem
            {
                Value = a.AutomobilID.ToString(),
                Text = a.Proizvodjac + " " + a.Model
            }).ToList();


            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.Karakteristike.Find(ID);
            var auto = db.Automobili.Where(a => a.AutomobilID == s.AutomobilID).Single(); ;
            db.Remove(auto);

            
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/AutoKarakteristike/Prikaz");
        }

        public IActionResult Snimi(AutoKarakteristikeUrediWM x)
        {
            Karakteristike a;

            if (x.KarakteristikeID == 0)
            {
                a = new Karakteristike();
                db.Add(a);
            }
            else
            {
                a = db.Karakteristike.Find(x.KarakteristikeID);
            }

            a.Stanje = x.Stanje;
            a.Godiste = x.Godiste;
            a.Cijena = x.Cijena;
            a.Kilometraza = x.Kilometraza;
            a.Snaga = x.Snaga;
            a.Zapremina = x.Zapremina;
            a.Gorivo = x.Gorivo;
            a.BrojVrata = x.BrojVrata;
            a.Pogon = x.Pogon;
            a.Transmisija = x.Transmisija;
            a.Boja = x.Boja;
            a.Svjetla = x.Svjetla;
            a.AutomobilID = x.AutomobilID;

            db.SaveChanges();

            return Redirect("/AutoKarakteristike/Prikaz");
        }
    }
}
