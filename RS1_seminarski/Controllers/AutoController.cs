using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podaci.EF;
using Podaci.Entiteti;
using RS1_seminarski.Modelview;

namespace RS1_seminarski.Controllers
{
    public class AutoController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Automobili.Select(s => new AutomobiliPrikazWM.Row
            {
                AutomobilID = s.AutomobilID,
                Proizvodjac = s.Proizvodjac,
                Model = s.Model,
                SifraAutomobila=s.SifraAutomobila,
                Zaliha=s.Zaliha,
                PaketOpremeNaziv=s.PaketOpreme.Naziv,
                PoreznaStopaNaziv=s.PoreznaStopa.Naziv,
                nazivKategorije=s.Kategorija.Naziv,


            }).ToList();

            AutomobiliPrikazWM m = new AutomobiliPrikazWM();
             m.AutoRows= stavke;
            return View(m);
        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.Automobili.Where(s => s.AutomobilID == ID).Select(s => new AutobiliUrediWM
            {
                AutomobilID = s.AutomobilID,
                Proizvodjac = s.Proizvodjac,
                Model = s.Model,
                SifraAutomobila = s.SifraAutomobila,
                Zaliha = s.Zaliha,

                KategorijaStavke = db.Kategorije.Select(s => new SelectListItem
                {
                    Value=s.KategorijaID.ToString(),
                    Text=s.Naziv
                }).ToList(),
                PaketOpremeStavke=db.PaketiOpreme.Select(s=>new SelectListItem
                {
                    Value=s.PaketOpremeID.ToString(),
                    Text=s.Naziv

                }).ToList(),

                
                PoreznaStopaStavke=db.PorezneStope.Select(s=>new SelectListItem
                {
                    Value=s.PoreznaStopaID.ToString(),
                    Text=s.Naziv

                }).ToList(),


            }).FirstOrDefault();

            return View(stavka);
        }

        public IActionResult Dodaj()
        {
            AutobiliUrediWM a = new AutobiliUrediWM();

            a.KategorijaStavke = db.Kategorije.Select(s => new SelectListItem
            {
                Value = s.KategorijaID.ToString(),
                Text = s.Naziv
            }).ToList();
            a.PaketOpremeStavke = db.PaketiOpreme.Select(s => new SelectListItem
            {
                Value = s.PaketOpremeID.ToString(),
                Text = s.Naziv

            }).ToList();


            a.PoreznaStopaStavke = db.PorezneStope.Select(s => new SelectListItem
            {
                Value = s.PoreznaStopaID.ToString(),
                Text = s.Naziv

            }).ToList();


           

            return View("Uredi", a);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.Automobili.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/Auto/Prikaz");
        }

        public IActionResult Snimi(AutobiliUrediWM x)
        {
            Automobil a;

            if (x.AutomobilID== 0)
            {
                a = new Automobil();
                db.Add(a);
            }
            else
            {
                a = db.Automobili.Find(x.AutomobilID);
            }

            a.Proizvodjac = x.Proizvodjac;
            a.Model = x.Model;
            a.SifraAutomobila = x.SifraAutomobila;
            a.Zaliha = x.Zaliha;
            a.KategorijaID = x.KategorijaID;
            a.PaketOpremeID = x.PaketOpremeID;
            a.PoreznaStopaID = x.PoreznaStopaID;

            db.SaveChanges();

            return Redirect("/Auto/Prikaz");
        }
    }
}

