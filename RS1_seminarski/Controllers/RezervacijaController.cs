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
    public class RezervacijaController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Rezervacije.Select(s => new RezervacijaPrikazWM.Row

            {
                RezervacijaID = s.RezervacijaID,
                DatumRezervacije = s.DatumRezervacije,
                Aktivna = s.Aktivna,
                Odobrena=s.Odobrena,
                Zavrsena=s.Zavrsena,
                ImeKlijenta=s.Klijent.Osoba.Ime+" "+s.Klijent.Osoba.Prezime

            })
            .ToList();

            RezervacijaPrikazWM m = new RezervacijaPrikazWM();
            m.RezervacijaRows= stavke;
            return View(m);

        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.Rezervacije.Where(s => s.RezervacijaID == ID).Select(s => new RezervacijaUrediWM
            {
                RezervacijaID = s.RezervacijaID,
                DatumRezervacije = s.DatumRezervacije,
                Aktivna = s.Aktivna,
                Odobrena = s.Odobrena,
                Zavrsena = s.Zavrsena,

                KlijentiStavke = db.Klijenti.Select(s => new SelectListItem
                {
                    Value=s.KlijentID.ToString(),
                    Text=s.Osoba.Ime+" "+s.Osoba.Prezime

                }).ToList()

            }).FirstOrDefault();

            return View(stavka);
        }

        public IActionResult Detalji(int ID)
        {
            var stavka = db.Rezervacije.Where(s => s.RezervacijaID == ID).Select(s => new RezervacijaDetaljiWM
            {
                RezervacijaID = s.RezervacijaID,
                DatumRezervacije = s.DatumRezervacije,
                ImePrezimeKlijenta = s.Klijent.Osoba.Ime + " " + s.Klijent.Osoba.Prezime

            }).FirstOrDefault();

            return View("Detalji", stavka);
        }




        public IActionResult Dodaj()
        {
            RezervacijaUrediWM m = new RezervacijaUrediWM();
            m.KlijentiStavke = db.Klijenti.Select(s => new SelectListItem
            {
                Value = s.KlijentID.ToString(),
                Text = s.Osoba.Ime + " " + s.Osoba.Prezime

            }).ToList();

            return View("Uredi", m);
        }


        public IActionResult Obrisi(int ID)
        {
            var stavke = db.StavkeRezervacije.Where(s => s.RezervacijaID == ID).ToList();

            if(stavke !=null)
            {
                db.RemoveRange(stavke);

            }    

            var s = db.Rezervacije.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/Rezervacija/Prikaz");
        }

        public IActionResult Snimi(RezervacijaUrediWM x)
        {
            Rezervacija r;

            if (x.RezervacijaID == 0)
            {
                r = new Rezervacija ();
                db.Add(r);
            }
            else
            {
                r= db.Rezervacije.Find(x.RezervacijaID);
            }

            r.DatumRezervacije = x.DatumRezervacije;
            r.Aktivna = x.Aktivna;
            r.Odobrena = x.Odobrena;
            r.Zavrsena = x.Zavrsena;
            r.KlijentID = x.KlijentID;

           db.SaveChanges();

            return Redirect("/Rezervacija/Prikaz");
        }
    }
}
