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
    //[Autorizacija(false,true)]

    public class UposlenikController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Uposlenici.Select(s => new UposlenikPrikazWM.Row
            {
                UposlenikID = s.UposlenikID,
                Ime = s.Osoba.Ime,
                Prezime = s.Osoba.Prezime,
                KorisnickoIme = s.Osoba.KorisnickiNalog.UserName,
                NazivGrada = s.Osoba.Grad.Naziv,
                Adresa = s.Osoba.Adresa,
                DatumZaposlenja = s.DatumZaposljenja,
                Iskustvo = s.Iskustvo,
                MinuliStaz = s.MinuliStaz,
                JMBG = s.JMBG,
                StrucnaSprema = s.StrurucnaSprema.Naziv,
                RadnoMjesto = s.RadnoMjesto.Naziv


            }).ToList();

            UposlenikPrikazWM m = new UposlenikPrikazWM();
            m.UposleniciRows = stavke;
            return View(m);
        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.Uposlenici.Where(s => s.UposlenikID == ID).Select(s => new UposlenikUrediWM
            {
                UposlenikID = s.UposlenikID,
                Ime = s.Osoba.Ime,
                Prezime = s.Osoba.Prezime,             
                Adresa = s.Osoba.Adresa,
                DatumZaposlenja = s.DatumZaposljenja,
                Iskustvo = s.Iskustvo,
                MinuliStaz = s.MinuliStaz,
                JMBG = s.JMBG,

                StrucnaSpremaStavke= db.StrucnaSprema.Select(s => new SelectListItem
                {
                    Value = s.StrucnaSpremaID.ToString(),
                    Text = s.Naziv

                }).ToList(),

                SpolStavka = db.Spol.Select(s => new SelectListItem
                {
                    Value = s.SpolID.ToString(),
                    Text = s.Naziv

                }).ToList(),
                RadnoMjestoStavka = db.RadnoMjesto.Select(s => new SelectListItem
                {
                    Value = s.RadnoMjestoID.ToString(),
                    Text = s.Naziv

                }).ToList(),




                GradoviStavka = db.Gradovi.Select(s=>new SelectListItem
                {
                    Value=s.GradID.ToString(),
                    Text=s.Naziv

                }).ToList(),

            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            UposlenikUrediWM m = new UposlenikUrediWM();

           m. StrucnaSpremaStavke = db.StrucnaSprema.Select(s => new SelectListItem
            {
                Value = s.StrucnaSpremaID.ToString(),
                Text = s.Naziv

            }).ToList();

            m.SpolStavka = db.Spol.Select(s => new SelectListItem
            {
                Value = s.SpolID.ToString(),
                Text = s.Naziv

            }).ToList();
            m.RadnoMjestoStavka = db.RadnoMjesto.Select(s => new SelectListItem
            {
                Value = s.RadnoMjestoID.ToString(),
                Text = s.Naziv

            }).ToList();




            m.GradoviStavka = db.Gradovi.Select(s => new SelectListItem
            {
                Value = s.GradID.ToString(),
                Text = s.Naziv

            }).ToList();
           


            return View("Uredi", m);
        }

        //public IActionResult Obrisi(int ID)
        //{
        //    var stavke = db.Izvjestaji.Where(s => s.UposlenikID == ID).ToList();

        //    if(stavke!=null)
        //    {
        //        db.RemoveRange(stavke);
        //    }
           

        //    var s = db.Uposlenici.Find(ID);

        //    var osoba = db.Osobe.Where(o => o.OsobaID == s.OsobaID).Single();


        //    var kn = db.KorisnickiNalog.Where(k => k.OsobaID == osoba.OsobaID).Single();
        //    db.Remove(kn);
        //    db.Remove(osoba);
           


        //    db.Remove(s);
        //    db.SaveChanges();
        //    return Redirect("/Uposlenik/Prikaz");
        //}

        public IActionResult Snimi(UposlenikUrediWM x)
        {
            Uposlenik k;

            if (x.UposlenikID == 0)
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

             
                db.SaveChanges();

                k = new Uposlenik();
                k.OsobaID = o.OsobaID;          
                db.Add(k);
            }
            else
            {
                k = db.Uposlenici.Find(x.UposlenikID);

                var kn = db.KorisnickiNalog.Where(n => n.OsobaID == k.OsobaID).Single();
                kn.UserName = x.KorisnickoIme;

                var osoba = db.Osobe.Where(o => o.OsobaID == k.OsobaID).FirstOrDefault();
                osoba.Prezime = x.Prezime;
                osoba.Ime = x.Ime;            
                osoba.GradID = x.GradID;
                osoba.Adresa = x.Adresa;
           
              
            }
            k.DatumZaposljenja = x.DatumZaposlenja;
            k.Iskustvo = x.Iskustvo;
            k.JMBG = x.JMBG;
            k.MinuliStaz = x.MinuliStaz;
            k.RadnoMjestoID = x.RadnoMjestoID;
            k.StrucnaSpremaID = x.StrucnaSpremaID;


            db.SaveChanges();

            return Redirect("/Uposlenik/Prikaz");
        }
    }


}

