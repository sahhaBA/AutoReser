using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podaci.EF;
using Podaci.Entiteti;
using RS1_seminarski.Modelview;
using RS1_seminarski.Helper;

namespace RS1_seminarski.Controllers
{
    [Autorizacija(true,false)]

    public class KlijentController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Klijenti.Select(s => new KlijentPrikazWM.Row
            {
                KlijentID = s.KlijentID,
                Ime = s.Osoba.Ime,
                Prezime = s.Osoba.Prezime,
                KorisnickoIme = s.Osoba.KorisnickiNalog.UserName,
                 NazivGrada = s.Osoba.Grad.Naziv,
                Adresa = s.Osoba.Adresa,
                DatumRegistracije = s.DatumRegistracije,
                Activan = s.Activan

            })
            .ToList();

            KlijentPrikazWM m = new KlijentPrikazWM();
            m.KlijentRows = stavke;
            return View(m);
        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.Klijenti.Where(s => s.KlijentID == ID).Select(s => new KlijentUrediWM
            {
                KlijentID=s.KlijentID,
                Ime=s.Osoba.Ime,
                OsobaID=s.OsobaID,
                Prezime=s.Osoba.Prezime,
               
              
                Adresa = s.Osoba.Adresa,
                DatumRegistracije = s.DatumRegistracije,
                Activan = s.Activan,
                GradoviStavka=db.Gradovi.Select(s=>new SelectListItem
                {
                    Value=s.GradID.ToString(),
                    Text=s.Naziv

                }).ToList(),

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            KlijentUrediWM m = new KlijentUrediWM();

            m.GradoviStavka = db.Gradovi.Select(s => new SelectListItem
            {
                Value = s.GradID.ToString(),
                Text = s.Naziv

            }).ToList();



            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.Klijenti.Find(ID);
            var kljuc = s.OsobaID;

            var kn = db.KorisnickiNalog.Where(k => k.OsobaID == kljuc).Single();
            db.Remove(kn);

            var o = db.Osobe.Find(kljuc);
            db.Remove(o);

          
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/Klijent/Prikaz");
        }

        public IActionResult Snimi(KlijentUrediWM x)
        {
            Klijent k;

            if (x.KlijentID == 0)
            {
                Osoba o = new Osoba {
                    Ime = x.Ime,
                    Prezime = x.Prezime,                
                    Adresa = x.Adresa,
                    GradID=x.GradID
                };
               

                db.Add(o);
                db.SaveChanges();

                KorisnickiNalog kn = new KorisnickiNalog
                {
                    //Lozinka = x.Lozinka,
                    UserName = x.KorisnickoIme,
                    OsobaID = o.OsobaID
                };

                db.Add(kn);


                k = new Klijent();
                k.OsobaID = o.OsobaID;
                k.DatumRegistracije = x.DatumRegistracije;
                k.Activan = x.Activan;
                db.Add(k);
            }
            else
            {

              

                k = db.Klijenti.Find(x.KlijentID);

                var kn = db.KorisnickiNalog.Where(n => n.OsobaID == k.OsobaID).Single();
                kn.UserName = x.KorisnickoIme;
                
                var osoba = db.Osobe.Where(o => o.OsobaID == k.OsobaID).FirstOrDefault();
                osoba.Prezime = x.Prezime;
                osoba.Ime = x.Ime;            
                osoba.GradID = x.GradID;
                osoba.Adresa = x.Adresa;
               
                k.DatumRegistracije = x.DatumRegistracije;
                k.Activan = x.Activan;
              
            }

            db.SaveChanges();

            return Redirect("/Klijent/Prikaz");
        }
    }


}

