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
    public class RacunController : Controller
    {
        MyContext db = new MyContext();


        public IActionResult Prikaz()
        {
            var stavke = db.Racuni.Select(s => new RacunPrikazWM.Row

            {
                RacunID = s.RacunID,
                DatumRacuna = s.DatumRacuna,
                BrojRezervacije = s.RezervacijaID,
                ImeKlijenta = s.Rezervacija.Klijent.Osoba.Ime+" "+s.Rezervacija.Klijent.Osoba.Prezime

            })
            .ToList();

            RacunPrikazWM m = new RacunPrikazWM();
            m.RacunRows= stavke;
            return View(m);

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




       

        //public IActionResult Obrisi(int ID)
        //{
        //    var stavke = db.StavkeRezervacije.Where(s => s.RezervacijaID == ID).ToList();

        //    if(stavke !=null)
        //    {
        //        db.RemoveRange(stavke);

        //    }    

        //    var s = db.Rezervacije.Find(ID);
        //    db.Remove(s);
        //    db.SaveChanges();
        //    return Redirect("/Rezervacija/Prikaz");
        //}

        public IActionResult KreirajRacune()
        {
            var stavke = db.Rezervacije.ToList();

            stavke.ForEach( s=>
              {
                  if (s.Odobrena == true)
                  {
                      Racun r = new Racun();
                      r.DatumRacuna = DateTime.Now;
                      r.RezervacijaID = s.RezervacijaID;
                      r.Napomena = " ";
                      r.NacinPlacanjaID = 1;
                      db.Add(r);
                      db.SaveChanges();

                      var auto = db.StavkeRezervacije.Where(q => q.RezervacijaID == s.RezervacijaID).FirstOrDefault();

                      StavkeRacuna nova = new StavkeRacuna
                      {
                          RacunID = r.RacunID,
                          AutomobilID = auto.AutomobilID,
                          Cijena = auto.Cijena,
                          Popust = auto.Popust
                      };


                      db.Add(nova);
                      s.Zavrsena = true; // oznaciti racun da je fakturisan 

                      db.SaveChanges();

                  }

              });
           

            return Redirect("/Racun/Prikaz");
        }
    }
}
