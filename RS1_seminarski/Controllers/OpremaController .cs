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
    public class OpremaController : Controller
    {
        MyContext db = new MyContext();

        public IActionResult Prikaz()
        {
            var stavke = db.Oprema.Select(s => new OpremaPrikazWM.Row
            {
                OpremaID = s.OpremaID,
                OpisOpreme = s.OpisOpreme,
                PaketOpremeNaziv = s.PaketOpremeOprema.Where(poo => poo.OpremaID == s.OpremaID)
                .Select(a => a.PaketOpreme.Naziv).FirstOrDefault()
                //PaketOpremeNaziv = s.PaketOpreme.Naziv
            }).ToList();

            OpremaPrikazWM m = new OpremaPrikazWM();
            m.OpremaRow= stavke;
            return View(m);
        }

        public IActionResult Uredi(int ID)
        {
            var stavka = db.Oprema.Where(s => s.OpremaID == ID).Select(s => new OpremaUrediWM
            {

                OpremaID = s.OpremaID,
                OpisOpreme = s.OpisOpreme,
                PaketOpreme = db.PaketiOpreme.Select(s => new SelectListItem
                {
                    Value=s.PaketOpremeID.ToString(),
                    Text=s.Naziv

                }).ToList()



            }).FirstOrDefault();

            return View(stavka);
        }
        public IActionResult Dodaj()
        {
            OpremaUrediWM m = new OpremaUrediWM();
            m.PaketOpreme = db.PaketiOpreme.Select(s => new SelectListItem
            {
                Value = s.PaketOpremeID.ToString(),
                Text = s.Naziv

            }).ToList();

            return View("Uredi", m);
        }

        public IActionResult Obrisi(int ID)
        {
            var s = db.Oprema.Find(ID);
            db.Remove(s);
            db.SaveChanges();
            return Redirect("/Oprema/Prikaz");
        }

        public IActionResult Snimi(OpremaUrediWM x)
        {
            Oprema o;

            if (x.OpremaID == 0)
            {
                o = new Oprema ();
                db.Add(o);
            }
            else
            {
                o= db.Oprema.Find(x.OpremaID);
            }

            o.OpisOpreme = x.OpisOpreme;
            db.SaveChanges();

            return Redirect("/Oprema/Prikaz");
        }
    }
}
