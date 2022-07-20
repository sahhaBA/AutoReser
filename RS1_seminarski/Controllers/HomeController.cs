using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RS1_seminarski.Models;
using RS1_seminarski.Helper;
using Podaci.EF;
using RS1_seminarski.Modelview;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podaci.Entiteti;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace RS1_seminarski.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<KorisnickiNalog> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<KorisnickiNalog> signInManager;

        MyContext db = new MyContext();

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment,
            RoleManager<IdentityRole> roleManager, UserManager<KorisnickiNalog> userManager, SignInManager<KorisnickiNalog> signInManager)
        {
            _logger = logger;
            this._hostEnvironment = hostEnvironment;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //if (HttpContext.GetLogiraniKorisnik() == null)
            //{
            //    return Redirect("/Autentifikacija/Index");
            //}

            string Id = null;
            if (signInManager.IsSignedIn(User))
            {
                var trenutniKorisnik = await userManager.GetUserAsync(User);
                Id = trenutniKorisnik.Id;
            }

            ViewBag.Id = Id;

            List<SelectListItem> km = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            for (int i = 25000; i <= 300000; i += 25000)
            {
                km.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            List<SelectListItem> price = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            for (int i = 5000; i <= 200000; i += 5000)
            {
                price.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            List<SelectListItem> god = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            for (int i = 2022; i >= 1970; i--)
            {
                god.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            }

            List<SelectListItem> pro = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            pro.AddRange(db.Automobili.OrderByDescending(o => o.Proizvodjac).Select(a => new SelectListItem
            {
                Value = a.Proizvodjac,
                Text = a.Proizvodjac
            }).Distinct().ToList());

            List<SelectListItem> sta = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            sta.AddRange(db.Karakteristike.OrderByDescending(o => o.Stanje).Select(a => new SelectListItem
            {
                Value = a.Stanje,
                Text = a.Stanje
            }).Distinct().ToList());

            var izvjestaji = db.Izvjestaji.ToList();
            var automobili = db.Automobili.ToList();
            List<Automobil> AktivnaAuta = new List<Automobil>(automobili);
            foreach (var i in izvjestaji)
            {
                foreach (var a in automobili)
                {
                    if (i.AutomobilID == a.AutomobilID.ToString())
                    {
                        if (i.Naziv.StartsWith("B"))
                            AktivnaAuta.Remove(a);
                    }
                }
            }

            var filter = new FilterVM
            {
                Proizvodjac = new List<SelectListItem>(pro),
                GodisteOD = new List<SelectListItem>(god),
                GodisteDO = new List<SelectListItem>(god),
                KilometrazaOD = new List<SelectListItem>(km),
                KilometrazaDO = new List<SelectListItem>(km),
                CijenaOD = new List<SelectListItem>(price),
                CijenaDO = new List<SelectListItem>(price),
                Stanje = new List<SelectListItem>(sta),
                Vozila = AktivnaAuta
                    .Select(a => new FilterVM.Vozilo
                    {
                        AutomobilID = a.AutomobilID,
                        Proizvodjac = a.Proizvodjac,
                        Model = a.Model,
                        Slika = db.Slike.Where(s => s.AutomobilID == a.AutomobilID).Select(a => a.NazivSlike1).FirstOrDefault(),
                        Karakteristike = db.Karakteristike.Where(k => k.AutomobilID == a.AutomobilID).Select(b => new Karakteristike
                        {
                            Motor = b.Motor,
                            Godiste = b.Godiste,
                            Cijena = b.Cijena,
                            Gorivo = b.Gorivo,
                            Kilometraza = b.Kilometraza,
                            Stanje = b.Stanje
                        }).FirstOrDefault()
                    }).ToList()
            };

            return View(filter);
        }

        [AllowAnonymous]
        public IActionResult Prikaz(FilterVM vm, string Id)
        {
            var fp = new FiltriraniProizvod();
            var auta = fp.DohvatiPodatke(vm).ToList();

            ViewBag.Id = Id;

            List<SelectListItem> boje = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            boje.AddRange(db.Karakteristike.OrderByDescending(o => o.BrojVrata).Select(a => new SelectListItem
            {
                Value = a.Boja,
                Text = a.Boja
            }).Distinct().ToList());

            List<SelectListItem> BrVr = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            BrVr.AddRange(db.Karakteristike.OrderByDescending(o => o.BrojVrata).Select(a => new SelectListItem
            {
                Value = a.BrojVrata,
                Text = a.BrojVrata
            }).Distinct().ToList());

            List<SelectListItem> gor = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            gor.AddRange(db.Karakteristike.OrderByDescending(o => o.Gorivo).Select(a => new SelectListItem
            {
                Value = a.Gorivo,
                Text = a.Gorivo
            }).Distinct().ToList());

            List<SelectListItem> pog = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            pog.AddRange(db.Karakteristike.OrderByDescending(o => o.Pogon).Select(a => new SelectListItem
            {
                Value = a.Pogon,
                Text = a.Pogon
            }).Distinct().ToList());

            List<SelectListItem> sna = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            sna.AddRange(db.Karakteristike.OrderByDescending(o => o.Snaga).Select(a => new SelectListItem
            {
                Value = a.Snaga.ToString(),
                Text = a.Snaga.ToString()
            }).Distinct().ToList());

            List<SelectListItem> svj = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            svj.AddRange(db.Karakteristike.OrderByDescending(o => o.Svjetla).Select(a => new SelectListItem
            {
                Value = a.Svjetla,
                Text = a.Svjetla
            }).Distinct().ToList());

            List<SelectListItem> tra = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            tra.AddRange(db.Karakteristike.OrderByDescending(o => o.Transmisija).Select(a => new SelectListItem
            {
                Value = a.Transmisija,
                Text = a.Transmisija
            }).Distinct().ToList());

            var izvjestaji = db.Izvjestaji.ToList();
            List<Automobil> AktivnaAuta = new List<Automobil>(auta);
            foreach (var i in izvjestaji)
            {
                foreach (var a in auta)
                {
                    if (i.AutomobilID == a.AutomobilID.ToString())
                    {
                        if (i.Naziv.StartsWith("B"))
                            AktivnaAuta.Remove(a);
                    }
                }
            }

            var m = new FilterVM()
            {
                ProizvodjacID = vm.ProizvodjacID,
                ModelID = vm.ModelID,
                GodisteODID = vm.GodisteODID,
                GodisteDOID = vm.GodisteDOID,
                KilometrazaODID = vm.KilometrazaODID,
                KilometrazaDOID = vm.KilometrazaDOID,
                CijenaODID = vm.CijenaODID,
                CijenaDOID = vm.CijenaDOID,
                StanjeID = vm.StanjeID,
                Boja = new List<SelectListItem>(boje),
                BrojVrata = new List<SelectListItem>(BrVr),
                Gorivo = new List<SelectListItem>(gor),
                Pogon = new List<SelectListItem>(pog),
                Transmisija = new List<SelectListItem>(tra),
                SnagaOD = new List<SelectListItem>(sna),
                SnagaDO = new List<SelectListItem>(sna),
                Svjetla = new List<SelectListItem>(svj),
                Vozila = AktivnaAuta
                .Select(a => new FilterVM.Vozilo
                {
                    AutomobilID = a.AutomobilID,
                    Proizvodjac = a.Proizvodjac,
                    Model = a.Model,
                    Slika = db.Slike.Where(s => s.AutomobilID == a.AutomobilID).Select(a => a.NazivSlike1).FirstOrDefault(),
                    Karakteristike = db.Karakteristike.Where(k => k.AutomobilID == a.AutomobilID)
                    .Select(b => new Karakteristike
                    {
                        Motor = b.Motor,
                        Kilometraza = b.Kilometraza,
                        Godiste = b.Godiste,
                        Cijena = b.Cijena,
                        Gorivo = b.Gorivo,
                        Stanje = b.Stanje
                    }).FirstOrDefault()
                }).ToList()
            };

            return PartialView(m);
        }

        [AllowAnonymous]
        public IActionResult PrikazDodatnog(FilterVM vm, string Id)
        {
            var fp = new FiltriraniProizvod();
            var auta = fp.DohvatiPodatke(vm).ToList();

            ViewBag.Id = Id;

            var izvjestaji = db.Izvjestaji.ToList();
            List<Automobil> AktivnaAuta = new List<Automobil>(auta);
            foreach (var i in izvjestaji)
            {
                foreach (var a in auta)
                {
                    if (i.AutomobilID == a.AutomobilID.ToString())
                    {
                        if (i.Naziv.StartsWith("B"))
                            AktivnaAuta.Remove(a);
                    }
                }
            }

            var m = new FilterVM()
            {
                Vozila = AktivnaAuta
                .Select(a => new FilterVM.Vozilo
                {
                    AutomobilID = a.AutomobilID,
                    Proizvodjac = a.Proizvodjac,
                    Model = a.Model,
                    Slika = db.Slike.Where(s => s.AutomobilID == a.AutomobilID).Select(a => a.NazivSlike1).FirstOrDefault(),
                    Karakteristike = db.Karakteristike.Where(k => k.AutomobilID == a.AutomobilID)
                    .Select(b => new Karakteristike
                    {
                        Motor = b.Motor,
                        Kilometraza = b.Kilometraza,
                        Godiste = b.Godiste,
                        Cijena = b.Cijena,
                        Gorivo = b.Gorivo,
                        Stanje = b.Stanje
                    }).FirstOrDefault()
                }).ToList()
            };

            return PartialView(m);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AutomobilDetaljno(int AutomobilID, string Id)
        {
            ViewBag.Id = Id;

            var znak = TempData["znak"] as string;
            var znak2 = TempData["znak2"] as string;
            var AutoJeUredjeno = TempData["AutoJeUredjeno"] as string;
            var AutoJeDodano = TempData["AutoJeDodano"] as string;
            var AutoObrisan = TempData["AutoObrisan"] as string;
            bool obrisan = false;
            foreach (var i in db.Izvjestaji)
            {
                if (i.AutomobilID == AutomobilID.ToString())
                {
                    if (i.Naziv.StartsWith("B"))
                        obrisan = true;
                }
            }

            var a = db.Automobili.Where(a => a.AutomobilID == AutomobilID)
                .Select(b => new AutomobilDetaljnoVM
                {
                    AutomobilID = b.AutomobilID,
                    Proizvodjac = b.Proizvodjac,
                    Model = b.Model,
                    Zaliha = b.Zaliha,
                    SifraAutomobila = b.SifraAutomobila,
                    Slika1 = db.Slike.Where(s => s.AutomobilID == AutomobilID).Select(a => a.NazivSlike1).FirstOrDefault(),
                    Slika2 = db.Slike.Where(s => s.AutomobilID == AutomobilID).Select(a => a.NazivSlike2).FirstOrDefault(),
                    Slika3 = db.Slike.Where(s => s.AutomobilID == AutomobilID).Select(a => a.NazivSlike3).FirstOrDefault(),
                    Slika4 = db.Slike.Where(s => s.AutomobilID == AutomobilID).Select(a => a.NazivSlike4).FirstOrDefault(),
                    Slika5 = db.Slike.Where(s => s.AutomobilID == AutomobilID).Select(a => a.NazivSlike5).FirstOrDefault(),
                    KategorijaID = b.KategorijaID,
                    Kategorija = db.Kategorije.Where(k => k.KategorijaID == b.KategorijaID)
                    .Select(c => c.Naziv).FirstOrDefault(),
                    PaketOpremeID = b.PaketOpremeID,
                    PaketOpreme = db.PaketiOpreme.Where(p => p.PaketOpremeID == b.PaketOpremeID)
                    .Select(d => d.Naziv).FirstOrDefault(),
                    ListaOpreme = db.PaketOpremeOprema.Where(po => po.PaketOpremeID == b.PaketOpremeID)
                        .Select(e => new Oprema
                        {
                            OpremaID = e.OpremaID,
                            OpisOpreme = e.Oprema.OpisOpreme
                        }).ToList(),
                    Obrisan = obrisan,
                    Karakteristike = db.Karakteristike.Where(k => k.AutomobilID == b.AutomobilID)
                    .Select(f => new Karakteristike
                    {
                        KarakteristikeID = f.KarakteristikeID,
                        AutomobilID = f.AutomobilID,
                        Boja = f.Boja,
                        BrojVrata = f.BrojVrata,
                        Cijena = f.Cijena,
                        Godiste = f.Godiste,
                        Gorivo = f.Gorivo,
                        Kilometraza = f.Kilometraza,
                        Motor = f.Motor,
                        Pogon = f.Pogon,
                        Snaga = f.Snaga,
                        Stanje = f.Stanje,
                        Svjetla = f.Svjetla,
                        Transmisija = f.Transmisija,
                        Zapremina = f.Zapremina
                    }).FirstOrDefault()
                }).FirstOrDefault();

            var pronadjen = "ne";
            foreach (var x in db.Korpa)
                if ((x.AutomobilID == AutomobilID) && (x.KorisnickiNalogID == Id))
                    pronadjen = "da";

            var rezervacija = "ne";

            if (Id != null)
            {
                var korisnik = db.KorisnickiNalog.Find(Id);
                var stavke = db.StavkeRezervacije.ToList();
                var klijent = db.Klijenti.Where(k => k.OsobaID == korisnik.OsobaID).FirstOrDefault();

                if (klijent != null)
                {
                    var res = db.Rezervacije.Where(r => r.KlijentID == klijent.KlijentID).ToList();

                    foreach (var r in res)
                        foreach (var s in stavke)
                            if (r.RezervacijaID == s.RezervacijaID && s.AutomobilID == AutomobilID)
                                rezervacija = "da";
                }
            }

            ViewData["znak"] = znak;
            ViewData["znak2"] = znak2;
            ViewData["kljuc"] = pronadjen;
            ViewData["rezervacija"] = rezervacija;
            ViewData["AutoJeDodano"] = AutoJeDodano;
            ViewData["AutoJeUredjeno"] = AutoJeUredjeno;
            ViewData["AutoObrisan"] = AutoObrisan;
            return View(a);
        }

        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Korpa()
        {
            var trenutniKorisnik = await userManager.GetUserAsync(User);
            string Id = trenutniKorisnik.Id;

            return RedirectToAction("KorpaPrikaz", new { Id = Id });
        }

        [Authorize(Roles = "Korisnik")]
        public IActionResult IzbaciIzKorpe_v2(int AutomobilID, string Id)
        {
            foreach (var x in db.Korpa)
                if ((x.AutomobilID == AutomobilID) && (x.KorisnickiNalogID == Id))
                    db.Korpa.Remove(x);

            db.SaveChanges();

            return RedirectToAction("Korpa");
        }

        [Authorize(Roles = "Korisnik")]
        public IActionResult KorpaPrikaz(string Id)
        {
            ViewBag.Id = Id;

            var automobili = db.Automobili.ToList();
            List<Automobil> auta = new List<Automobil>();
            FilterVM m = null;

            foreach (var x in automobili)
            {
                foreach (var y in db.Korpa)
                {
                    if ((x.AutomobilID == y.AutomobilID) && (y.KorisnickiNalogID == Id))
                        auta.Add(x);
                }
            }

            if (auta.Count() > 0)
            {
                m = new FilterVM()
                {
                    Vozila = auta
                   .Select(a => new FilterVM.Vozilo
                   {
                       AutomobilID = a.AutomobilID,
                       Proizvodjac = a.Proizvodjac,
                       Model = a.Model,
                       Slika = db.Slike.Where(s => s.AutomobilID == a.AutomobilID).Select(a => a.NazivSlike1).FirstOrDefault(),
                       Karakteristike = db.Karakteristike.Where(k => k.AutomobilID == a.AutomobilID)
                       .Select(b => new Karakteristike
                       {
                           Motor = b.Motor,
                           Kilometraza = b.Kilometraza,
                           Godiste = b.Godiste,
                           Cijena = b.Cijena,
                           Gorivo = b.Gorivo,
                           Stanje = b.Stanje
                       }).FirstOrDefault()
                   }).ToList()
                };
            }

            ViewData["korpa"] = "korpa";
            return View("PrikazDodatnog", m);
        }

        [Authorize(Roles = "Korisnik")]
        public IActionResult DodajUKorpu(int AutomobilID, string Id)
        {
            var novaKorpa = new Korpa
            {
                AutomobilID = AutomobilID,
                KorisnickiNalogID = Id
            };

            db.Korpa.Add(novaKorpa);
            db.SaveChanges();

            TempData["znak"] = "znak";
            return RedirectToAction("AutomobilDetaljno", new { AutomobilID = AutomobilID, Id = Id });
        }

        [Authorize(Roles = "Korisnik")]
        public IActionResult IzbaciIzKorpe(int AutomobilID, string Id)
        {
            foreach (var x in db.Korpa)
                if ((x.AutomobilID == AutomobilID) && (x.KorisnickiNalogID == Id))
                    db.Korpa.Remove(x);

            db.SaveChanges();

            TempData["znak2"] = "znak2";
            return RedirectToAction("AutomobilDetaljno", new { AutomobilID = AutomobilID, Id = Id });
        }

        bool postojiRezervacija(Klijent kl)
        {
            foreach (var r in db.Rezervacije)
                if (r.KlijentID == kl.KlijentID)
                    return true;
            return false;
        }

        [Authorize(Roles = "Admin, Uposlenik, Korisnik")]
        public IActionResult IzbaciIzRezervacije(int RezervacijaID, int AutomobilID, string Id)
        {
            KorisnickiNalog kn = db.KorisnickiNalog.Where(kn => kn.Id == Id).FirstOrDefault();
            Rezervacija res = db.Rezervacije.Where(r => r.RezervacijaID == RezervacijaID).FirstOrDefault();
            StavkeRezervacije sr = db.StavkeRezervacije.Where(s => s.RezervacijaID == RezervacijaID && s.AutomobilID == AutomobilID).FirstOrDefault();

            db.StavkeRezervacije.Remove(sr);
            db.Rezervacije.Remove(res);
            db.SaveChanges();

            Klijent kl = db.Klijenti.Where(k => k.OsobaID == kn.OsobaID).FirstOrDefault();
            if (!postojiRezervacija(kl))
            {
                kl.Activan = false;
                db.Entry(kl).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("Rezervacije", new { Id = Id });
        }
        bool NePostoji(Rezervacija r, List<Klijent> klijenti)
        {
            foreach (var k in klijenti)
            {
                if (r.KlijentID == k.KlijentID)
                    return false;
            }
            return true;
        }

        [Authorize(Roles = "Admin, Uposlenik, Korisnik")]
        public async Task<IActionResult> Rezervacije()
        {
            var trenutniKorisnik = await userManager.GetUserAsync(User);
            string Id = trenutniKorisnik.Id;

            ViewBag.Id = Id;

            KorisnickiNalog user = db.KorisnickiNalog.Where(kn => kn.Id == Id).FirstOrDefault();
            List<Rezervacija> res = db.Rezervacije.Where(r => r.Aktivna == true && r.Odobrena == false && r.Zavrsena == false).ToList();
            List<Klijent> Klijenti = new List<Klijent>();
            List<KorisnickiNalog> NaloziSaAktivnimRezervacijama = new List<KorisnickiNalog>();
            foreach (var r in res)
            {
                if (NePostoji(r, Klijenti))
                    Klijenti.Add(db.Klijenti.Where(k => k.KlijentID == r.KlijentID).FirstOrDefault());
            }
            foreach (var k in Klijenti)
                NaloziSaAktivnimRezervacijama.Add(db.KorisnickiNalog.Where(kn => kn.OsobaID == k.OsobaID).FirstOrDefault());

            if (await userManager.IsInRoleAsync(trenutniKorisnik, "Korisnik"))
            {
                Klijent kl = db.Klijenti.Where(k => k.OsobaID == user.OsobaID).FirstOrDefault();
                res = new List<Rezervacija>();

                if (kl != null)
                    res = db.Rezervacije.Where(r => (r.KlijentID == kl.KlijentID) && (r.Aktivna == true && r.Odobrena == false && r.Zavrsena == false)).ToList();
            }

            var m = new RezervacijeVM
            {
                NaloziSaAktivnimRezervacijama = NaloziSaAktivnimRezervacijama
                .Select(c => new RezervacijeVM.Korisnik
                {
                    KorisnickoIme = c.UserName,
                    Ime = db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.Ime).FirstOrDefault(),
                    Prezime = db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.Prezime).FirstOrDefault(),
                    Email = c.Email,
                    Rezervacije = res.Where(r => r.Klijent.OsobaID == db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.OsobaID).FirstOrDefault())
                    .Select(a => new RezervacijeVM.Reser
                    {
                        RezervacijaID = a.RezervacijaID,
                        DatumRezervacije = a.DatumRezervacije.ToString(),
                        Aktivna = a.Aktivna,
                        Odobrena = a.Odobrena,
                        Zavrsena = a.Zavrsena,
                        UposlenikID = a?.UposlenikID,
                        Uposlenik = (a?.UposlenikID == null) ? "" : db.Uposlenici.Where(u => u.UposlenikID == a.UposlenikID)
                        .Select(c => c.Osoba.Ime).FirstOrDefault() + " " + db.Uposlenici.Where(u => u.UposlenikID == a.UposlenikID)
                        .Select(c => c.Osoba.Prezime).FirstOrDefault(),
                        StavkeRezervacije = db.StavkeRezervacije.Where(sr => sr.RezervacijaID == a.RezervacijaID)
                        .Select(b => new RezervacijeVM.Reser.Stavke
                        {
                            RezervacijaID = b.RezervacijaID,
                            AutomobilID = b.AutomobilID,
                            Automobil = b.Automobil.Proizvodjac + " " + b.Automobil.Model,
                            Cijena = b.Cijena,
                            Popust = b.Popust,
                            Slika = db.Slike.Where(s => s.AutomobilID == b.AutomobilID).Select(a => a.NazivSlike1).FirstOrDefault()
                        }).FirstOrDefault()
                    }).ToList()
                }).ToList()
            };

            return View(m);
        }

        [Authorize(Roles = "Admin, Uposlenik, Korisnik")]
        public async Task<IActionResult> OdobreneRezervacije(string Id)
        {
            ViewBag.Id = Id;

            KorisnickiNalog user = db.KorisnickiNalog.Where(kn => kn.Id == Id).FirstOrDefault();
            List<Rezervacija> res = db.Rezervacije.Where(r => r.Aktivna == true && r.Odobrena == true && r.Zavrsena == false).ToList();
            List<Klijent> Klijenti = new List<Klijent>();
            List<KorisnickiNalog> NaloziSaAktivnimRezervacijama = new List<KorisnickiNalog>();
            foreach (var r in res)
            {
                if (NePostoji(r, Klijenti))
                    Klijenti.Add(db.Klijenti.Where(k => k.KlijentID == r.KlijentID).FirstOrDefault());
            }
            foreach (var k in Klijenti)
                NaloziSaAktivnimRezervacijama.Add(db.KorisnickiNalog.Where(kn => kn.OsobaID == k.OsobaID).FirstOrDefault());

            if (await userManager.IsInRoleAsync(user, "Korisnik"))
            {
                Klijent kl = db.Klijenti.Where(k => k.OsobaID == user.OsobaID).FirstOrDefault();
                res = new List<Rezervacija>();

                if (kl != null)
                    res = db.Rezervacije.Where(r => r.KlijentID == kl.KlijentID && (r.Aktivna == true && r.Odobrena == true && r.Zavrsena == false)).ToList();
            }

            var m = new RezervacijeVM
            {
                NaloziSaAktivnimRezervacijama = NaloziSaAktivnimRezervacijama
                .Select(c => new RezervacijeVM.Korisnik
                {
                    KorisnickoIme = c.UserName,
                    Ime = db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.Ime).FirstOrDefault(),
                    Prezime = db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.Prezime).FirstOrDefault(),
                    Email = c.Email,
                    Rezervacije = res.Where(r => r.Klijent.OsobaID == db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.OsobaID).FirstOrDefault())
                    .Select(a => new RezervacijeVM.Reser
                    {
                        RezervacijaID = a.RezervacijaID,
                        DatumRezervacije = a.DatumRezervacije.ToString(),
                        Aktivna = a.Aktivna,
                        Odobrena = a.Odobrena,
                        Zavrsena = a.Zavrsena,
                        UposlenikID = a?.UposlenikID,
                        Uposlenik = (a?.UposlenikID == null) ? null : db.Uposlenici.Where(u => u.UposlenikID == a.UposlenikID)
                        .Select(c => c.Osoba.Ime).FirstOrDefault() + " " + db.Uposlenici.Where(u => u.UposlenikID == a.UposlenikID)
                        .Select(c => c.Osoba.Prezime).FirstOrDefault(),
                        AdminNalogID = a?.AdminNalogId,
                        AdminNalog = (a?.AdminNalogId == null) ? null : db.KorisnickiNalog.Where(kn => kn.Id == a.AdminNalogId)
                        .Select(c => c.Osoba.Ime).FirstOrDefault() + " " + db.KorisnickiNalog.Where(kn => kn.Id == a.AdminNalogId)
                        .Select(c => c.Osoba.Prezime).FirstOrDefault(),
                        StavkeRezervacije = db.StavkeRezervacije.Where(sr => sr.RezervacijaID == a.RezervacijaID)
                        .Select(b => new RezervacijeVM.Reser.Stavke
                        {
                            RezervacijaID = b.RezervacijaID,
                            AutomobilID = b.AutomobilID,
                            Automobil = b.Automobil.Proizvodjac + " " + b.Automobil.Model,
                            Cijena = b.Cijena,
                            Popust = b.Popust,
                            Slika = db.Slike.Where(s => s.AutomobilID == b.AutomobilID).Select(a => a.NazivSlike1).FirstOrDefault()
                        }).FirstOrDefault()
                    }).ToList()
                }).ToList()
            };

            return View(m);
        }

        [Authorize(Roles = "Admin, Uposlenik, Korisnik")]
        public async Task<IActionResult> ZavrseneRezervacije(string Id)
        {
            ViewBag.Id = Id;

            KorisnickiNalog user = db.KorisnickiNalog.Where(kn => kn.Id == Id).FirstOrDefault();
            List<Rezervacija> res = db.Rezervacije.Where(r => r.Aktivna == false && r.Zavrsena == true).ToList();
            List<Klijent> Klijenti = new List<Klijent>();
            foreach (var r in res)
            {
                if (NePostoji(r, Klijenti))
                    Klijenti.Add(db.Klijenti.Where(k => k.KlijentID == r.KlijentID).FirstOrDefault());
            }
            List<KorisnickiNalog> NaloziSaAktivnimRezervacijama = new List<KorisnickiNalog>();
            foreach (var k in Klijenti)
                NaloziSaAktivnimRezervacijama.Add(db.KorisnickiNalog.Where(kn => kn.OsobaID == k.OsobaID).FirstOrDefault());

            if (await userManager.IsInRoleAsync(user, "Korisnik"))
            {
                Klijent kl = db.Klijenti.Where(k => k.OsobaID == user.OsobaID).FirstOrDefault();
                res = new List<Rezervacija>();

                if (kl != null)
                    res = db.Rezervacije.Where(r => r.KlijentID == kl.KlijentID && (r.Aktivna == false && r.Zavrsena == true)).ToList();
            }

            var m = new RezervacijeVM
            {
                NaloziSaAktivnimRezervacijama = NaloziSaAktivnimRezervacijama
                .Select(c => new RezervacijeVM.Korisnik
                {
                    KorisnickoIme = c.UserName,
                    Ime = db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.Ime).FirstOrDefault(),
                    Prezime = db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.Prezime).FirstOrDefault(),
                    Email = c.Email,
                    Rezervacije = res.Where(r => r.Klijent.OsobaID == db.Osobe.Where(o => o.OsobaID == c.OsobaID).Select(z => z.OsobaID).FirstOrDefault())
                    .Select(a => new RezervacijeVM.Reser
                    {
                        RezervacijaID = a.RezervacijaID,
                        DatumRezervacije = a.DatumRezervacije.ToString(),
                        Aktivna = a.Aktivna,
                        Odobrena = a.Odobrena,
                        Zavrsena = a.Zavrsena,
                        UposlenikID = a?.UposlenikID,
                        Uposlenik = (a?.UposlenikID == null) ? null : db.Uposlenici.Where(u => u.UposlenikID == a.UposlenikID)
                        .Select(c => c.Osoba.Ime).FirstOrDefault() + " " + db.Uposlenici.Where(u => u.UposlenikID == a.UposlenikID)
                        .Select(c => c.Osoba.Prezime).FirstOrDefault(),
                        AdminNalogID = a?.AdminNalogId,
                        AdminNalog = (a?.AdminNalogId == null) ? null : db.KorisnickiNalog.Where(kn => kn.Id == a.AdminNalogId)
                        .Select(c => c.Osoba.Ime).FirstOrDefault() + " " + db.KorisnickiNalog.Where(kn => kn.Id == a.AdminNalogId)
                        .Select(c => c.Osoba.Prezime).FirstOrDefault(),
                        StavkeRezervacije = db.StavkeRezervacije.Where(sr => sr.RezervacijaID == a.RezervacijaID)
                        .Select(b => new RezervacijeVM.Reser.Stavke
                        {
                            RezervacijaID = b.RezervacijaID,
                            AutomobilID = b.AutomobilID,
                            Automobil = b.Automobil.Proizvodjac + " " + b.Automobil.Model,
                            Cijena = b.Cijena,
                            Popust = b.Popust,
                            Slika = db.Slike.Where(s => s.AutomobilID == b.AutomobilID).Select(a => a.NazivSlike1).FirstOrDefault()
                        }).FirstOrDefault()
                    }).ToList()
                }).ToList()
            };

            return View(m);
        }

        bool provjeraKlijenta(KorisnickiNalog kor)
        {
            foreach (var k in db.Klijenti)
            {
                if (k.OsobaID == kor.OsobaID)
                    return false;
            }
            return true;
        }

        [Authorize(Roles = "Korisnik")]
        public IActionResult ZahtjevZaRezervaciju(string Id)
        {
            KorisnickiNalog kor = db.KorisnickiNalog.Find(Id);
            var Korpa = db.Korpa.Where(k => k.KorisnickiNalogID == Id).ToList();
            Osoba o = db.Osobe.Where(o => o.OsobaID == kor.OsobaID).FirstOrDefault();
            FilterVM m = null;

            List<Automobil> auta = new List<Automobil>();
            List<Automobil> auta2 = new List<Automobil>();

            foreach (var x in Korpa)
                auta.Add(db.Automobili.Where(a => a.AutomobilID == x.AutomobilID).FirstOrDefault());

            Klijent kl = null;
            if (provjeraKlijenta(kor))
            {
                kl = new Klijent
                {
                    OsobaID = kor.OsobaID,
                    Osoba = db.Osobe.Where(o => o.OsobaID == kor.OsobaID).FirstOrDefault(),
                    DatumRegistracije = DateTime.Now,
                    Activan = true
                };
                db.Klijenti.Add(kl);
            }
            else
            {
                kl = db.Klijenti.Where(o => o.OsobaID == kor.OsobaID).FirstOrDefault();
                kl.Activan = true;
                db.Entry(kl).State = EntityState.Modified;
            }
            db.SaveChanges();

            foreach (var x in auta)
            {
                var Res = new Rezervacija
                {
                    KlijentID = kl.KlijentID,
                    Klijent = kl,
                    Aktivna = true,
                    Odobrena = false,
                    Zavrsena = false,
                    DatumRezervacije = DateTime.Now
                };
                db.Rezervacije.Add(Res);
                db.SaveChanges();

                var sr = new StavkeRezervacije
                {
                    RezervacijaID = Res.RezervacijaID,
                    AutomobilID = x.AutomobilID,
                    Cijena = db.Karakteristike.Where(k => k.AutomobilID == x.AutomobilID).Select(c => c.Cijena).FirstOrDefault()
                };
                db.StavkeRezervacije.Add(sr);
            }

            db.Korpa.RemoveRange(Korpa);

            db.SaveChanges();
            ViewData["korpa"] = "korpa";
            ViewData["nekiZnak"] = "nekiZnak";
            return View("PrikazDodatnog", m);
        }

        [Authorize(Roles = "Admin, Uposlenik")]
        public IActionResult OdobriRezervaciju(int RezervacijaID, int AutomobilID, string Id, string Email)
        {
            var res = db.Rezervacije.Where(r => r.RezervacijaID == RezervacijaID).FirstOrDefault();
            var KorisnikKojiRezervise = db.KorisnickiNalog.Where(kn => kn.Email == Email).FirstOrDefault();
            var NalogUposlenikaKojiOdobravaRezervaciju = db.KorisnickiNalog.Where(kn => kn.Id == Id).FirstOrDefault();
            var Osoba = db.Osobe.Where(o => o.OsobaID == NalogUposlenikaKojiOdobravaRezervaciju.OsobaID).FirstOrDefault();
            var Uposlenik = db.Uposlenici.Where(u => u.OsobaID == NalogUposlenikaKojiOdobravaRezervaciju.OsobaID).FirstOrDefault();
            var stavkaRezervacije = db.StavkeRezervacije.Where(sr => sr.RezervacijaID == RezervacijaID && sr.AutomobilID == AutomobilID).FirstOrDefault();

            res.Odobrena = true;

            if (Uposlenik != null)
                res.UposlenikID = Uposlenik.UposlenikID;
            else
                res.AdminNalogId = NalogUposlenikaKojiOdobravaRezervaciju.Id;

            db.Entry(res).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Rezervacije");
        }

        [Authorize(Roles = "Admin, Uposlenik, Korisnik")]
        public IActionResult OdbijRezervaciju(int RezervacijaID, int AutomobilID, string Id, string Email)
        {
            var res = db.Rezervacije.Where(r => r.RezervacijaID == RezervacijaID).FirstOrDefault();
            var KorisnikKojiRezervise = db.KorisnickiNalog.Where(kn => kn.Email == Email).FirstOrDefault();
            var NalogUposlenikaKojiOdobravaRezervaciju = db.KorisnickiNalog.Where(kn => kn.Id == Id).FirstOrDefault();
            var Uposlenik = db.Uposlenici.Where(u => u.OsobaID == NalogUposlenikaKojiOdobravaRezervaciju.OsobaID).FirstOrDefault();
            var stavkaRezervacije = db.StavkeRezervacije.Where(sr => sr.RezervacijaID == RezervacijaID && sr.AutomobilID == AutomobilID).FirstOrDefault();

            res.Aktivna = false;
            res.Zavrsena = true;

            if (Uposlenik != null)
                res.UposlenikID = Uposlenik.UposlenikID;
            else
                res.AdminNalogId = NalogUposlenikaKojiOdobravaRezervaciju.Id;

            db.Entry(res).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Rezervacije");
        }

        [Authorize(Roles = "Admin, Uposlenik, Korisnik")]
        public IActionResult OdbijRezervacijuODOBRENE(int RezervacijaID, int AutomobilID, string Id, string Email)
        {
            var res = db.Rezervacije.Where(r => r.RezervacijaID == RezervacijaID).FirstOrDefault();
            var KorisnikKojiRezervise = db.KorisnickiNalog.Where(kn => kn.Email == Email).FirstOrDefault();
            var NalogUposlenikaKojiOdobravaRezervaciju = db.KorisnickiNalog.Where(kn => kn.Id == Id).FirstOrDefault();
            var Uposlenik = db.Uposlenici.Where(u => u.OsobaID == NalogUposlenikaKojiOdobravaRezervaciju.OsobaID).FirstOrDefault();
            var stavkaRezervacije = db.StavkeRezervacije.Where(sr => sr.RezervacijaID == RezervacijaID && sr.AutomobilID == AutomobilID).FirstOrDefault();

            res.Aktivna = false;
            res.Zavrsena = true;

            if (Uposlenik != null)
                res.UposlenikID = Uposlenik.UposlenikID;
            else
                res.AdminNalogId = NalogUposlenikaKojiOdobravaRezervaciju.Id;

            db.Entry(res).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("OdobreneRezervacije", new { Id = Id });
        }

        [Authorize(Roles = "Admin, Uposlenik")]
        public async Task<IActionResult> DodajVozilo()
        {
            var trenutniKorisnik = await userManager.GetUserAsync(User);
            string Id = trenutniKorisnik.Id;

            ViewBag.Id = Id;

            List<SelectListItem> kat = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            kat.AddRange(db.Kategorije.Select(a => new SelectListItem
            {
                Value = a.KategorijaID.ToString(),
                Text = a.Naziv
            }).ToList());

            List<SelectListItem> ps = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            ps.AddRange(db.PorezneStope.OrderBy(p=>p.Iznos).Select(a => new SelectListItem
            {
                Value = a.PoreznaStopaID.ToString(),
                Text = a.Iznos.ToString()
            }).ToList());

            List<SelectListItem> bv = new List<SelectListItem>() { new SelectListItem { Value = "2/3", Text = "2/3" } };
            bv.Add(new SelectListItem { Value = "4/5", Text = "4/5" });

            List<SelectListItem> gor = new List<SelectListItem>() { new SelectListItem { Value = "Dizel", Text = "Dizel" } };
            gor.Add(new SelectListItem { Value = "Benzin", Text = "Benzin" });
            gor.Add(new SelectListItem { Value = "Hibrid", Text = "Hibrid" });

            List<SelectListItem> st = new List<SelectListItem>() { new SelectListItem { Value = "Novo", Text = "Novo" } };
            st.Add(new SelectListItem { Value = "Polovno", Text = "Polovno" });

            List<SelectListItem> tra = new List<SelectListItem>() { new SelectListItem { Value = "Manuelni", Text = "Manuelni" } };
            tra.Add(new SelectListItem { Value = "Automatik", Text = "Automatik" });
            tra.Add(new SelectListItem { Value = "Tiptronik", Text = "Tiptronik" });

            List<SelectListItem> god = new List<SelectListItem>();
            for (int i = 2022; i >= 1970; i--)
                god.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });

            DodajVoziloVM m = new DodajVoziloVM
            {
                Kategorija = new List<SelectListItem>(kat),
                PaketOpreme = db.PaketiOpreme
                .Select(po => new SelectListItem
                {
                    Value = po.PaketOpremeID.ToString(),
                    Text = po.Naziv
                }).ToList(),
                Oprema = db.Oprema
                .Select(o => new SelectListItem
                {
                    Value = o.OpremaID.ToString(),
                    Text = o.OpisOpreme
                }).ToList(),
                PoreznaStopa = new List<SelectListItem>(ps),
                Karakteristike = new DodajVoziloVM.Karakteristiks
                {
                    BrojVrata = new List<SelectListItem>(bv),
                    Godiste = new List<SelectListItem>(god),
                    Gorivo = new List<SelectListItem>(gor),
                    Stanje = new List<SelectListItem>(st),
                    Transmisija = new List<SelectListItem>(tra)
                }
            };

            return View(m);
        }

        public bool PSNePostoji(string iznos)
        {
            foreach (var ps in db.PorezneStope)
            {
                if (ps.Iznos == float.Parse(iznos))
                    return false;
            }
            return true;
        }

        public bool KNePostoji(string novaKat)
        {
            foreach (var k in db.Kategorije)
            {
                if (k.Naziv == novaKat)
                    return false;
            }
            return true;
        }
        
        //public IActionResult DodavanjeVozila(DodajVoziloVM vm)
        //{
        //    var kor = db.KorisnickiNalog.Where(k => k.KorisnickiNalogID == vm.UposlenikID).FirstOrDefault();

        //    if (!string.IsNullOrEmpty(vm.NovaKategorija))
        //    {
        //        var novaKategorija = new Kategorija
        //        {
        //            Naziv = vm.NovaKategorija
        //        };
        //        db.Kategorije.Add(novaKategorija);
        //        db.SaveChanges();
        //    }

        //    if (!string.IsNullOrEmpty(vm.NovaPoreznaStopa.ToString()) && PSNePostoji(vm.NovaPoreznaStopa))
        //    {
        //        var novaPoreznaStopa = new PoreznaStopa
        //        {
        //            Iznos = vm.NovaPoreznaStopa,
        //            Naziv = "PoreznaStopa" + vm.NovaPoreznaStopa
        //        };
        //        db.PorezneStope.Add(novaPoreznaStopa);
        //        db.SaveChanges();
        //    }

        //    var noviAuto = new Automobil
        //    {
        //        Proizvodjac = vm.Proizvodjac,
        //        Model = vm.Model,
        //        SifraAutomobila = vm.SifraAutomobila,
        //        Zaliha = vm.Zaliha,
        //        KategorijaID = string.IsNullOrEmpty(vm.NovaKategorija) ? vm.KategorijaID : db.Kategorije.Where(k => k.Naziv == vm.NovaKategorija)
        //        .Select(k => k.KategorijaID).FirstOrDefault(),
        //        PaketOpremeID = vm.PaketOpremeID,
        //        PoreznaStopaID = string.IsNullOrEmpty(vm.NovaPoreznaStopa.ToString()) ? vm.PaketOpremeID : db.PorezneStope.Where(ps => ps.Iznos == vm.NovaPoreznaStopa)
        //        .Select(ps => ps.PoreznaStopaID).FirstOrDefault(),
        //        UposlenikID = db.Uposlenici.Where(u => u.OsobaID == kor.OsobaID).Select(u => u.UposlenikID).FirstOrDefault()
        //    };

        //    db.Automobili.Add(noviAuto);
        //    db.SaveChanges();

        //    var noveKarakeristike = new Karakteristike
        //    {
        //        Stanje = vm.Karakteristike.StanjeID,
        //        Godiste = vm.Karakteristike.GodisteID,
        //        Cijena = vm.Karakteristike.Cijena,
        //        Kilometraza = vm.Karakteristike.Kilometraza,
        //        Snaga = vm.Karakteristike.Snaga,
        //        Zapremina = vm.Karakteristike.Zapremina,
        //        Gorivo = vm.Karakteristike.GorivoID,
        //        BrojVrata = vm.Karakteristike.BrojVrataID,
        //        Pogon = vm.Karakteristike.Pogon,
        //        Transmisija = vm.Karakteristike.TransmisijaID,
        //        Boja = vm.Karakteristike.Boja,
        //        Svjetla = vm.Karakteristike.Svjetla,
        //        Motor = vm.Karakteristike.Motor,
        //        AutomobilID = noviAuto.AutomobilID
        //    };

        //    db.Karakteristike.Add(noveKarakeristike);
        //    db.SaveChanges();

        //    return RedirectToAction("DodajVozilo", new { kor.KorisnickiNalogID });
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Uposlenik")]
        public async Task<IActionResult> DodavanjeVozila(DodajVoziloVM vm)
        {
            var kor = db.KorisnickiNalog.Where(k => k.Id == vm.UposlenikID).FirstOrDefault();
            var osoba = db.Osobe.Find(kor.OsobaID);

            if (ModelState.IsValid)
            {

                if (!(string.IsNullOrEmpty(vm.NovaKategorija)) && KNePostoji(vm.NovaKategorija))
                {
                    var novaKategorija = new Kategorija
                    {
                        Naziv = vm.NovaKategorija
                    };
                    db.Kategorije.Add(novaKategorija);
                    db.SaveChanges();
                }

                if (!(string.IsNullOrEmpty(vm.NovaPoreznaStopa)) && PSNePostoji(vm.NovaPoreznaStopa))
                {
                    var novaPoreznaStopa = new PoreznaStopa
                    {
                        Iznos = float.Parse(vm.NovaPoreznaStopa),
                        Naziv = "PoreznaStopa" + vm.NovaPoreznaStopa
                    };
                    db.PorezneStope.Add(novaPoreznaStopa);
                    db.SaveChanges();
                }

                var noviAuto = new Automobil
                {
                    Proizvodjac = vm.Proizvodjac,
                    Model = vm.Model,
                    SifraAutomobila = vm.SifraAutomobila,
                    Zaliha = (string.IsNullOrEmpty(vm.Zaliha)) ? 1 : int.Parse(vm.Zaliha),
                    KategorijaID = string.IsNullOrEmpty(vm.NovaKategorija) ? int.Parse(vm.KategorijaID) : db.Kategorije.Where(k => k.Naziv == vm.NovaKategorija)
                    .Select(k => k.KategorijaID).FirstOrDefault(),
                    PaketOpremeID = vm.PaketOpremeID,
                    PoreznaStopaID = string.IsNullOrEmpty(vm.NovaPoreznaStopa) ? int.Parse(vm.PoreznaStopaID) : db.PorezneStope.Where(ps => ps.Iznos == float.Parse(vm.NovaPoreznaStopa))
                    .Select(ps => ps.PoreznaStopaID).FirstOrDefault()
                };

                db.Automobili.Add(noviAuto);
                db.SaveChanges();

                var noveKarakeristike = new Karakteristike
                {
                    Stanje = vm.Karakteristike.StanjeID,
                    Godiste = vm.Karakteristike.GodisteID,
                    Cijena = float.Parse(vm.Karakteristike.Cijena),
                    Kilometraza = int.Parse(vm.Karakteristike.Kilometraza),
                    Snaga = int.Parse(vm.Karakteristike.Snaga),
                    Zapremina = vm.Karakteristike.Zapremina,
                    Gorivo = vm.Karakteristike.GorivoID,
                    BrojVrata = vm.Karakteristike.BrojVrataID,
                    Pogon = vm.Karakteristike.Pogon,
                    Transmisija = vm.Karakteristike.TransmisijaID,
                    Boja = vm.Karakteristike.Boja,
                    Svjetla = vm.Karakteristike.Svjetla,
                    Motor = vm.Karakteristike.Motor,
                    AutomobilID = noviAuto.AutomobilID
                };

                db.Karakteristike.Add(noveKarakeristike);
                db.SaveChanges();

                var noviIzvjestaj = new Izvjestaj
                {
                    AutomobilID = noviAuto.AutomobilID.ToString(),
                    Naziv = "Dodavanje vozila",
                    Opis = noviAuto.Proizvodjac + " " + noviAuto.Model + " " + "[" + noviAuto.Karakteristike.Cijena + " KM]",
                    KorisnickiNalogID = kor.Id,
                    UposlenikKreiraIzvjestaj = kor.UserName + "(" + osoba.Ime + " " + osoba.Prezime + ")",
                    DatumIzvjestaja = DateTime.Now,
                };

                db.Izvjestaji.Add(noviIzvjestaj);
                db.SaveChanges();

                Slike Slike = new Slike
                {
                    FajlSlike1 = vm?.Slika1,
                    FajlSlike2 = vm?.Slika2,
                    FajlSlike3 = vm?.Slika3,
                    FajlSlike4 = vm?.Slika4,
                    FajlSlike5 = vm?.Slika5,
                    AutomobilID = noviAuto.AutomobilID
                };

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName1 = Path.GetFileNameWithoutExtension(Slike?.FajlSlike1?.FileName);
                string fileName2 = Path.GetFileNameWithoutExtension(Slike?.FajlSlike2?.FileName);
                string fileName3 = Path.GetFileNameWithoutExtension(Slike?.FajlSlike3?.FileName);
                string fileName4 = Path.GetFileNameWithoutExtension(Slike?.FajlSlike4?.FileName);
                string fileName5 = Path.GetFileNameWithoutExtension(Slike?.FajlSlike5?.FileName);

                string extension1 = Path.GetExtension(Slike?.FajlSlike1?.FileName);
                string extension2 = Path.GetExtension(Slike?.FajlSlike1?.FileName);
                string extension3 = Path.GetExtension(Slike?.FajlSlike1?.FileName);
                string extension4 = Path.GetExtension(Slike?.FajlSlike1?.FileName);
                string extension5 = Path.GetExtension(Slike?.FajlSlike1?.FileName);

                Slike.NazivSlike1 = fileName1 = fileName1 + DateTime.Now.ToString("ddMMyy") + extension1;
                Slike.NazivSlike2 = fileName2 = fileName2 + DateTime.Now.ToString("ddMMyy") + extension2;
                Slike.NazivSlike3 = fileName3 = fileName3 + DateTime.Now.ToString("ddMMyy") + extension3;
                Slike.NazivSlike4 = fileName4 = fileName4 + DateTime.Now.ToString("ddMMyy") + extension4;
                Slike.NazivSlike5 = fileName5 = fileName5 + DateTime.Now.ToString("ddMMyy") + extension5;

                string path1 = Path.Combine(wwwRootPath + "/Images/", fileName1);
                using (var fileStream = new FileStream(path1, FileMode.Create))
                    await Slike.FajlSlike1.CopyToAsync(fileStream);

                string path2 = Path.Combine(wwwRootPath + "/Images/", fileName2);
                using (var fileStream = new FileStream(path2, FileMode.Create))
                    await Slike.FajlSlike2.CopyToAsync(fileStream);

                string path3 = Path.Combine(wwwRootPath + "/Images/", fileName3);
                using (var fileStream = new FileStream(path3, FileMode.Create))
                    await Slike.FajlSlike3.CopyToAsync(fileStream);

                string path4 = Path.Combine(wwwRootPath + "/Images/", fileName4);
                using (var fileStream = new FileStream(path4, FileMode.Create))
                    await Slike.FajlSlike4.CopyToAsync(fileStream);

                string path5 = Path.Combine(wwwRootPath + "/Images/", fileName5);
                using (var fileStream = new FileStream(path5, FileMode.Create))
                    await Slike.FajlSlike5.CopyToAsync(fileStream);

                db.Slike.Add(Slike);
                await db.SaveChangesAsync();

                TempData["AutoJeDodano"] = "AutoJeDodano";
                return RedirectToAction("AutomobilDetaljno", new { AutomobilID = noviAuto.AutomobilID, Id = kor.Id });
            }

            List<SelectListItem> kat = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            kat.AddRange(db.Kategorije.Select(a => new SelectListItem
            {
                Value = a.KategorijaID.ToString(),
                Text = a.Naziv
            }).ToList());

            List<SelectListItem> ps = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            ps.AddRange(db.PorezneStope.Select(a => new SelectListItem
            {
                Value = a.PoreznaStopaID.ToString(),
                Text = a.Iznos.ToString()
            }).ToList());

            List<SelectListItem> bv = new List<SelectListItem>() { new SelectListItem { Value = "2/3", Text = "2/3" } };
            bv.Add(new SelectListItem { Value = "4/5", Text = "4/5" });

            List<SelectListItem> gor = new List<SelectListItem>() { new SelectListItem { Value = "Dizel", Text = "Dizel" } };
            gor.Add(new SelectListItem { Value = "Benzin", Text = "Benzin" });
            gor.Add(new SelectListItem { Value = "Hibrid", Text = "Hibrid" });

            List<SelectListItem> st = new List<SelectListItem>() { new SelectListItem { Value = "Novo", Text = "Novo" } };
            st.Add(new SelectListItem { Value = "Polovno", Text = "Polovno" });

            List<SelectListItem> tra = new List<SelectListItem>() { new SelectListItem { Value = "Manuelni", Text = "Manuelni" } };
            tra.Add(new SelectListItem { Value = "Automatik", Text = "Automatik" });
            tra.Add(new SelectListItem { Value = "Tiptronik", Text = "Tiptronik" });

            List<SelectListItem> god = new List<SelectListItem>();
            for (int i = 2022; i >= 1970; i--)
                god.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });

            DodajVoziloVM m = new DodajVoziloVM
            {
                Kategorija = new List<SelectListItem>(kat),
                PaketOpreme = db.PaketiOpreme
                .Select(po => new SelectListItem
                {
                    Value = po.PaketOpremeID.ToString(),
                    Text = po.Naziv
                }).ToList(),
                Oprema = db.Oprema
                .Select(o => new SelectListItem
                {
                    Value = o.OpremaID.ToString(),
                    Text = o.OpisOpreme
                }).ToList(),
                PoreznaStopa = new List<SelectListItem>(ps),
                Karakteristike = new DodajVoziloVM.Karakteristiks
                {
                    BrojVrata = new List<SelectListItem>(bv),
                    Godiste = new List<SelectListItem>(god),
                    Gorivo = new List<SelectListItem>(gor),
                    Stanje = new List<SelectListItem>(st),
                    Transmisija = new List<SelectListItem>(tra)
                }
            };

            ViewData["AutoNijeDodano"] = "AutoNijeDodano";
            return View("DodajVozilo", m);
        }

        //public ActionResult RetrieveImage(int AutomobilID)
        //{
        //    byte[] cover = GetImageFromDataBase(AutomobilID);
        //    if (cover != null)
        //    {
        //        return File(cover, "image/jpg");
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        //public ActionResult RetrieveImage(byte[] sl)
        //{
        //    byte[] cover = sl;
        //    if (cover != null)
        //    {
        //        return File(cover, "image/jpg");
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public static byte[] GetBytes(IFormFile formFile)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        formFile.CopyToAsync(memoryStream);
        //        return memoryStream.ToArray();
        //    }
        //}

        //public byte[] GetImageFromDataBase(int Id)
        //{
        //    Slike s = db.Slike.Where(s => s.AutomobilID == Id).FirstOrDefault();
        //    //IFormFile q = null;
        //    //string[] str = Directory.GetFiles(Path.Combine(this._hostEnvironment.WebRootPath, "./Images/" + s.NazivSlike1));

        //    //string path = "./wwwroot/Images/" + s.NazivSlike1;
        //    //using (var stream = System.IO.File.OpenRead(str))
        //    //{
        //    //    q = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
        //    //}

        //    string file = Directory.GetFiles(@".\wwwroot\Images", s.NazivSlike1).FirstOrDefault();
        //    //var executingPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        //    byte[] q = Encoding.ASCII.GetBytes(file);

        //    //var bytes = GetBytes(q);

        //    return q;
        //}

        [Authorize(Roles = "Admin, Uposlenik", Policy = "UređivanjePolicy")]
        public IActionResult UrediOpremu(string Id, int AutomobilID)
        {
            ViewBag.Id = Id;

            var err = TempData["error"] as string;

            var kor = db.KorisnickiNalog.Find(Id);
            var auto = db.Automobili.Find(AutomobilID);
            var karakteristike = db.Karakteristike.Where(k => k.AutomobilID == AutomobilID).FirstOrDefault();
            var paketOpreme = db.PaketiOpreme.Where(po => po.PaketOpremeID == auto.PaketOpremeID).FirstOrDefault();
            var oprema = db.PaketOpremeOprema.Where(poo => poo.PaketOpremeID == paketOpreme.PaketOpremeID)
                        .Select(e => new Oprema
                        {
                            OpremaID = e.OpremaID,
                            OpisOpreme = e.Oprema.OpisOpreme
                        }).ToList();

            var m = new UrediOpremuVM
            {
                PaketOpremeID = paketOpreme.PaketOpremeID,
                PaketOpreme = paketOpreme.Naziv,
                ListaOpreme = new List<Oprema>(oprema),
                Automobil = auto.Proizvodjac + " " + auto.Model + " " + karakteristike.Motor + " " + karakteristike.Gorivo,
                AutomobilID = AutomobilID
            };

            ViewData["error"] = err;
            return View(m);
        }

        [Authorize(Roles = "Admin, Uposlenik", Policy = "UređivanjePolicy")]
        public IActionResult DodajOpremu(int AutomobilID, int PaketOpremeID, string Id)
        {
            ViewBag.Id = Id;

            var m = new DodajOpremuVM
            {
                AutomobilID = AutomobilID,
                PaketOpremeID = PaketOpremeID,
                Id = Id
            };

            return PartialView(m);
        }

        bool vecpostoji(string nazivOpreme)
        {
            foreach (var o in db.Oprema)
                if (o.OpisOpreme == nazivOpreme)
                    return true;
            return false;
        }

        [Authorize(Roles = "Admin, Uposlenik", Policy = "UređivanjePolicy")]
        public IActionResult DodavanjeNoveOpreme(DodajOpremuVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vecpostoji(vm.nazivOpreme))
                {
                    TempData["error"] = "vecpostoji";
                    return RedirectToAction("UrediOpremu", new { Id = vm.Id, AutomobilID = vm.AutomobilID });
                }
                else
                {
                    var novaOprema = new Oprema
                    {
                        OpisOpreme = vm.nazivOpreme
                    };
                    db.Oprema.Add(novaOprema);
                    db.SaveChanges();

                    var novipoo = new PaketOpremeOprema
                    {
                        PaketOpremeID = vm.PaketOpremeID,
                        OpremaID = novaOprema.OpremaID
                    };
                    db.PaketOpremeOprema.Add(novipoo);
                    db.SaveChanges();

                    var knalog = db.KorisnickiNalog.Find(vm.Id);
                    var osoba = db.Osobe.Find(knalog.OsobaID);
                    //var uposlenik = db.Uposlenici.Where(u => u.OsobaID == osoba.OsobaID).FirstOrDefault();

                    var auto = db.Automobili.Find(vm.AutomobilID);
                    var karakteristikeUredjivanogAuta = db.Karakteristike.Where(k => k.AutomobilID == auto.AutomobilID).FirstOrDefault();
                    var noviIzvjestaj = new Izvjestaj
                    {
                        AutomobilID = auto.AutomobilID.ToString(),
                        Naziv = "Uredjivanje vozila",
                        Opis = auto.Proizvodjac + " " + auto.Model + " " + "[" + karakteristikeUredjivanogAuta.Cijena + " KM]",
                        KorisnickiNalogID = knalog.Id,
                        UposlenikKreiraIzvjestaj = knalog.UserName + "(" + osoba.Ime + " " + osoba.Prezime + ")",
                        DatumIzvjestaja = DateTime.Now
                    };

                    db.Izvjestaji.Add(noviIzvjestaj);
                    db.SaveChanges();

                    return RedirectToAction("UrediOpremu", new { Id = vm.Id, AutomobilID = vm.AutomobilID });
                }
            }

            TempData["error"] = "error";
            return RedirectToAction("UrediOpremu", new { Id = vm.Id, AutomobilID = vm.AutomobilID });
        }

        [Authorize(Roles = "Admin, Uposlenik", Policy = "UređivanjePolicy")]
        public IActionResult ObrisiOpremu(int OpremaID, int AutomobilID, string Id)
        {
            var oprema = db.Oprema.Find(OpremaID);

            foreach (var poo in db.PaketOpremeOprema)
                if (poo.PaketOpremeID == oprema.OpremaID)
                    db.PaketOpremeOprema.Remove(poo);

            db.Oprema.Remove(oprema);
            db.SaveChanges();

            var knalog = db.KorisnickiNalog.Find(Id);
            var osoba = db.Osobe.Find(knalog.OsobaID);
            //var uposlenik = db.Uposlenici.Where(u => u.OsobaID == osoba.OsobaID).FirstOrDefault();

            var auto = db.Automobili.Find(AutomobilID);
            var karakteristikeUredjivanogAuta = db.Karakteristike.Where(k => k.AutomobilID == auto.AutomobilID).FirstOrDefault();
            var noviIzvjestaj = new Izvjestaj
            {
                AutomobilID = auto.AutomobilID.ToString(),
                Naziv = "Uredjivanje vozila",
                Opis = auto.Proizvodjac + " " + auto.Model + " " + "[" + karakteristikeUredjivanogAuta.Cijena + " KM]",
                KorisnickiNalogID = knalog.Id,
                UposlenikKreiraIzvjestaj = knalog.UserName + "(" + osoba.Ime + " " + osoba.Prezime + ")",
                DatumIzvjestaja = DateTime.Now
            };

            db.Izvjestaji.Add(noviIzvjestaj);
            db.SaveChanges();

            TempData["error"] = "uspjesnoObrisano";
            return RedirectToAction("UrediOpremu", new { Id = Id, AutomobilID = AutomobilID });
        }

        [Authorize(Roles = "Admin, Uposlenik", Policy = "UređivanjePolicy")]
        public IActionResult UrediNazivOpreme(int OpremaID, int AutomobilID, int PaketOpremeID, string Id)
        {
            ViewBag.Id = Id;

            var oprema = db.Oprema.Find(OpremaID);

            var m = new DodajOpremuVM
            {
                AutomobilID = AutomobilID,
                PaketOpremeID = PaketOpremeID,
                Id = Id,
                OpremaID = OpremaID,
                nazivOpreme = oprema.OpisOpreme,
            };

            return PartialView(m);
        }

        [Authorize(Roles = "Admin, Uposlenik", Policy = "UređivanjePolicy")]
        public IActionResult UpdateOpreme(DodajOpremuVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vecpostoji(vm.nazivOpreme))
                {
                    TempData["error"] = "vecpostoji";
                    return RedirectToAction("UrediOpremu", new { Id = vm.Id, AutomobilID = vm.AutomobilID });
                }
                else
                {
                    var novaOprema = db.Oprema.Find(vm.OpremaID);
                    novaOprema.OpisOpreme = vm.nazivOpreme;
                    db.Entry(novaOprema).State = EntityState.Modified;
                    db.SaveChanges();

                    var knalog = db.KorisnickiNalog.Find(vm.Id);
                    var osoba = db.Osobe.Find(knalog.OsobaID);
                    //var uposlenik = db.Uposlenici.Where(u => u.OsobaID == osoba.OsobaID).FirstOrDefault();

                    var auto = db.Automobili.Find(vm.AutomobilID);
                    var karakteristikeUredjivanogAuta = db.Karakteristike.Where(k => k.AutomobilID == auto.AutomobilID).FirstOrDefault();
                    var noviIzvjestaj = new Izvjestaj
                    {
                        AutomobilID = auto.AutomobilID.ToString(),
                        Naziv = "Uredjivanje vozila",
                        Opis = auto.Proizvodjac + " " + auto.Model + " " + "[" + karakteristikeUredjivanogAuta.Cijena + " KM]",
                        KorisnickiNalogID = knalog.Id,
                        UposlenikKreiraIzvjestaj = knalog.UserName + "(" + osoba.Ime + " " + osoba.Prezime + ")",
                        DatumIzvjestaja = DateTime.Now
                    };

                    db.Izvjestaji.Add(noviIzvjestaj);
                    db.SaveChanges();

                    TempData["error"] = "uspjesnoEvidentirano";
                    return RedirectToAction("UrediOpremu", new { Id = vm.Id, AutomobilID = vm.AutomobilID });
                }
            }

            TempData["error"] = "error";
            return RedirectToAction("UrediOpremu", new { Id = vm.Id, AutomobilID = vm.AutomobilID });
        }

        [Authorize(Roles = "Admin, Uposlenik", Policy = "UređivanjePolicy")]
        public IActionResult UrediAutomobil(string Id, int AutomobilID)
        {
            ViewBag.Id = Id;

            var autoZaModifikovanje = db.Automobili.Find(AutomobilID);
            var karakteristikeZaModifikovanje = db.Karakteristike.Where(k => k.AutomobilID == AutomobilID).FirstOrDefault();
            var selektovanaKategorija = db.Kategorije.Find(autoZaModifikovanje.KategorijaID);
            var selektovanaPS = db.PorezneStope.Find(autoZaModifikovanje.PoreznaStopaID);

            List<SelectListItem> kat = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            kat.AddRange(db.Kategorije.Select(a => new SelectListItem
            {
                Value = a.KategorijaID.ToString(),
                Text = a.Naziv,
                Selected = (a.Naziv == selektovanaKategorija.Naziv) ? true : false
            }).ToList());

            List<SelectListItem> ps = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            ps.AddRange(db.PorezneStope.OrderBy(p => p.Iznos).Select(a => new SelectListItem
            {
                Value = a.PoreznaStopaID.ToString(),
                Text = a.Iznos.ToString(),
                Selected = (a.Iznos == selektovanaPS.Iznos) ? true : false
            }).ToList());

            List<SelectListItem> bv = new List<SelectListItem>() { new SelectListItem { Value = "2/3", Text = "2/3" } };
            bv.Add(new SelectListItem { Value = "4/5", Text = "4/5" });
            foreach (var item in bv)
            {
                if (item.Text == karakteristikeZaModifikovanje.BrojVrata)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> gor = new List<SelectListItem>() { new SelectListItem { Value = "Dizel", Text = "Dizel" } };
            gor.Add(new SelectListItem { Value = "Benzin", Text = "Benzin" });
            gor.Add(new SelectListItem { Value = "Hibrid", Text = "Hibrid" });
            foreach (var item in gor)
            {
                if (item.Text == karakteristikeZaModifikovanje.Gorivo)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> st = new List<SelectListItem>() { new SelectListItem { Value = "Novo", Text = "Novo" } };
            st.Add(new SelectListItem { Value = "Polovno", Text = "Polovno" });
            foreach (var item in st)
            {
                if (item.Text == karakteristikeZaModifikovanje.Stanje)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> tra = new List<SelectListItem>() { new SelectListItem { Value = "Manuelni", Text = "Manuelni" } };
            tra.Add(new SelectListItem { Value = "Automatik", Text = "Automatik" });
            tra.Add(new SelectListItem { Value = "Tiptronik", Text = "Tiptronik" });
            foreach (var item in tra)
            {
                if (item.Text == karakteristikeZaModifikovanje.Transmisija)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> god = new List<SelectListItem>();
            for (int i = 2022; i >= 1970; i--)
                god.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString(), Selected = (i == karakteristikeZaModifikovanje.Godiste) ? true : false });

            var po = db.PaketiOpreme
                .Select(po => new SelectListItem
                {
                    Value = po.PaketOpremeID.ToString(),
                    Text = po.Naziv,
                    Selected = (po.PaketOpremeID == autoZaModifikovanje.PaketOpremeID) ? true : false
                }).ToList();

            var sl1 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike1).FirstOrDefault();
            var sl2 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike2).FirstOrDefault();
            var sl3 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike3).FirstOrDefault();
            var sl4 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike4).FirstOrDefault();
            var sl5 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike5).FirstOrDefault();

            UrediAutomobilVM m = new UrediAutomobilVM
            {
                Proizvodjac = autoZaModifikovanje.Proizvodjac,
                Model = autoZaModifikovanje.Model,
                SifraAutomobila = autoZaModifikovanje.SifraAutomobila,
                Zaliha = autoZaModifikovanje.Zaliha.ToString(),
                Kategorija = new List<SelectListItem>(kat),
                PaketOpreme = new List<SelectListItem>(po),
                PoreznaStopa = new List<SelectListItem>(ps),
                Slika1string = sl1,
                Slika2string = sl2,
                Slika3string = sl3,
                Slika4string = sl4,
                Slika5string = sl5,
                Karakteristike = new UrediAutomobilVM.Karakteristiks
                {
                    BrojVrata = new List<SelectListItem>(bv),
                    Godiste = new List<SelectListItem>(god),
                    Gorivo = new List<SelectListItem>(gor),
                    Stanje = new List<SelectListItem>(st),
                    Transmisija = new List<SelectListItem>(tra),
                    Cijena = karakteristikeZaModifikovanje.Cijena.ToString(),
                    Kilometraza = karakteristikeZaModifikovanje.Kilometraza.ToString(),
                    Motor = karakteristikeZaModifikovanje.Motor,
                    Snaga = karakteristikeZaModifikovanje.Snaga.ToString(),
                    Zapremina = karakteristikeZaModifikovanje?.Zapremina,
                    Pogon = karakteristikeZaModifikovanje.Pogon,
                    Boja = karakteristikeZaModifikovanje.Boja,
                    Svjetla = karakteristikeZaModifikovanje.Svjetla,
                    AutomobilID = autoZaModifikovanje.AutomobilID
                }
            };
            return View(m);
        }


        [Authorize(Roles = "Admin, Uposlenik", Policy = "UređivanjePolicy")]
        public IActionResult UredjivanjeVozila(UrediAutomobilVM vm)
        {
            var kor = db.KorisnickiNalog.Where(k => k.Id == vm.UposlenikID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                //var uposlenik = db.Uposlenici.Where(u => u.OsobaID == kor.OsobaID).FirstOrDefault();
                var osoba = db.Osobe.Find(kor.OsobaID);

                if (!string.IsNullOrEmpty(vm.NovaKategorija))
                {
                    var novaKategorija = new Kategorija
                    {
                        Naziv = vm.NovaKategorija
                    };
                    db.Kategorije.Add(novaKategorija);
                    db.SaveChanges();
                }

                if (!(string.IsNullOrEmpty(vm.NovaPoreznaStopa)) && PSNePostoji(vm.NovaPoreznaStopa))
                {
                    var novaPoreznaStopa = new PoreznaStopa
                    {
                        Iznos = float.Parse(vm.NovaPoreznaStopa),
                        Naziv = "PoreznaStopa" + vm.NovaPoreznaStopa
                    };
                    db.PorezneStope.Add(novaPoreznaStopa);
                    db.SaveChanges();
                }

                var uredjivaniAuto = db.Automobili.Find(vm.Karakteristike.AutomobilID);
                var karakteristikeUredjivanogAuta = db.Karakteristike.Where(k => k.AutomobilID == uredjivaniAuto.AutomobilID).FirstOrDefault();

                uredjivaniAuto.Proizvodjac = vm.Proizvodjac;
                uredjivaniAuto.Model = vm.Model;
                uredjivaniAuto.SifraAutomobila = vm.SifraAutomobila;
                uredjivaniAuto.Zaliha = (string.IsNullOrEmpty(vm.Zaliha)) ? 1 : int.Parse(vm.Zaliha);
                uredjivaniAuto.KategorijaID = string.IsNullOrEmpty(vm.NovaKategorija) ? int.Parse(vm.KategorijaID) : db.Kategorije.Where(k => k.Naziv == vm.NovaKategorija)
                              .Select(k => k.KategorijaID).FirstOrDefault();
                uredjivaniAuto.PaketOpremeID = int.Parse(vm.PaketOpremeID);
                uredjivaniAuto.PoreznaStopaID = string.IsNullOrEmpty(vm.NovaPoreznaStopa) ? int.Parse(vm.PoreznaStopaID) : db.PorezneStope.Where(ps => ps.Iznos == float.Parse(vm.NovaPoreznaStopa))
                              .Select(ps => ps.PoreznaStopaID).FirstOrDefault();

                db.Entry(uredjivaniAuto).State = EntityState.Modified;
                db.SaveChanges();

                var noviIzvjestaj = new Izvjestaj
                {
                    AutomobilID = uredjivaniAuto.AutomobilID.ToString(),
                    Naziv = "Uredjivanje vozila",
                    Opis = uredjivaniAuto.Proizvodjac + " " + uredjivaniAuto.Model + " " + "[" + karakteristikeUredjivanogAuta.Cijena + " KM]",
                    KorisnickiNalogID = kor.Id,
                    UposlenikKreiraIzvjestaj = kor.UserName + "(" + osoba.Ime + " " + osoba.Prezime + ")",
                    DatumIzvjestaja = DateTime.Now
                };

                db.Izvjestaji.Add(noviIzvjestaj);
                db.SaveChanges();

                var uredjivaneKarakeristike = db.Karakteristike.Where(k => k.AutomobilID == vm.Karakteristike.AutomobilID).FirstOrDefault();

                uredjivaneKarakeristike.Stanje = vm.Karakteristike.StanjeID;
                uredjivaneKarakeristike.Godiste = int.Parse(vm.Karakteristike.GodisteID);
                uredjivaneKarakeristike.Cijena = float.Parse(vm.Karakteristike.Cijena);
                uredjivaneKarakeristike.Kilometraza = int.Parse(vm.Karakteristike.Kilometraza);
                uredjivaneKarakeristike.Snaga = int.Parse(vm.Karakteristike.Snaga);
                uredjivaneKarakeristike.Zapremina = vm.Karakteristike.Zapremina;
                uredjivaneKarakeristike.Gorivo = vm.Karakteristike.GorivoID;
                uredjivaneKarakeristike.BrojVrata = vm.Karakteristike.BrojVrataID;
                uredjivaneKarakeristike.Pogon = vm.Karakteristike.Pogon;
                uredjivaneKarakeristike.Transmisija = vm.Karakteristike.TransmisijaID;
                uredjivaneKarakeristike.Boja = vm.Karakteristike.Boja;
                uredjivaneKarakeristike.Svjetla = vm.Karakteristike.Svjetla;
                uredjivaneKarakeristike.Motor = vm.Karakteristike.Motor;
                uredjivaneKarakeristike.AutomobilID = vm.Karakteristike.AutomobilID;

                db.Entry(uredjivaneKarakeristike).State = EntityState.Modified;
                db.SaveChanges();

                TempData["AutoJeUredjeno"] = "AutoJeUredjeno";
                return RedirectToAction("AutomobilDetaljno", new { AutomobilID = vm.Karakteristike.AutomobilID, Id = kor.Id });
            }

            var autoZaModifikovanje = db.Automobili.Find(vm.Karakteristike.AutomobilID);
            var karakteristikeZaModifikovanje = db.Karakteristike.Where(k => k.AutomobilID == vm.Karakteristike.AutomobilID).FirstOrDefault();
            var selektovanaKategorija = db.Kategorije.Find(autoZaModifikovanje.KategorijaID);
            var selektovanaPS = db.PorezneStope.Find(autoZaModifikovanje.PoreznaStopaID);

            List<SelectListItem> kat = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            kat.AddRange(db.Kategorije.Select(a => new SelectListItem
            {
                Value = a.KategorijaID.ToString(),
                Text = a.Naziv
            }).ToList());
            foreach (var item in kat)
            {
                if (item.Text == selektovanaKategorija.Naziv)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> ps = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            ps.AddRange(db.PorezneStope.Select(a => new SelectListItem
            {
                Value = a.PoreznaStopaID.ToString(),
                Text = a.Iznos.ToString()
            }).ToList());

            foreach (var item in ps)
            {
                if (item.Text == selektovanaPS.Iznos.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> bv = new List<SelectListItem>() { new SelectListItem { Value = "2/3", Text = "2/3" } };
            bv.Add(new SelectListItem { Value = "4/5", Text = "4/5" });
            foreach (var item in bv)
            {
                if (item.Text == karakteristikeZaModifikovanje.BrojVrata)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> gor = new List<SelectListItem>() { new SelectListItem { Value = "Dizel", Text = "Dizel" } };
            gor.Add(new SelectListItem { Value = "Benzin", Text = "Benzin" });
            gor.Add(new SelectListItem { Value = "Hibrid", Text = "Hibrid" });
            foreach (var item in gor)
            {
                if (item.Text == karakteristikeZaModifikovanje.Gorivo)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> st = new List<SelectListItem>() { new SelectListItem { Value = "Novo", Text = "Novo" } };
            st.Add(new SelectListItem { Value = "Polovno", Text = "Polovno" });
            foreach (var item in st)
            {
                if (item.Text == karakteristikeZaModifikovanje.Stanje)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> tra = new List<SelectListItem>() { new SelectListItem { Value = "Manuelni", Text = "Manuelni" } };
            tra.Add(new SelectListItem { Value = "Automatik", Text = "Automatik" });
            tra.Add(new SelectListItem { Value = "Tiptronik", Text = "Tiptronik" });
            foreach (var item in tra)
            {
                if (item.Text == karakteristikeZaModifikovanje.Transmisija)
                {
                    item.Selected = true;
                    break;
                }
            }

            List<SelectListItem> god = new List<SelectListItem>();
            for (int i = 2022; i >= 1970; i--)
                god.Add(new SelectListItem { Value = i.ToString(), Text = i.ToString() });
            foreach (var item in god)
            {
                if (item.Value == karakteristikeZaModifikovanje.Godiste.ToString())
                {
                    item.Selected = true;
                }
            }

            var po = db.PaketiOpreme
                .Select(po => new SelectListItem
                {
                    Value = po.PaketOpremeID.ToString(),
                    Text = po.Naziv
                }).ToList();
            foreach (var item in po)
            {
                if (item.Value == autoZaModifikovanje.PaketOpremeID.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }

            var sl1 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike1).FirstOrDefault();
            var sl2 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike2).FirstOrDefault();
            var sl3 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike3).FirstOrDefault();
            var sl4 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike4).FirstOrDefault();
            var sl5 = db.Slike.Where(s => s.AutomobilID == autoZaModifikovanje.AutomobilID).Select(s => s.NazivSlike5).FirstOrDefault();

            UrediAutomobilVM m = new UrediAutomobilVM
            {
                Proizvodjac = autoZaModifikovanje.Proizvodjac,
                Model = autoZaModifikovanje.Model,
                SifraAutomobila = autoZaModifikovanje.SifraAutomobila,
                Zaliha = autoZaModifikovanje.Zaliha.ToString(),
                Kategorija = new List<SelectListItem>(kat),
                PaketOpreme = new List<SelectListItem>(po),
                PoreznaStopa = new List<SelectListItem>(ps),
                Slika1string = sl1,
                Slika2string = sl2,
                Slika3string = sl3,
                Slika4string = sl4,
                Slika5string = sl5,
                Karakteristike = new UrediAutomobilVM.Karakteristiks
                {
                    BrojVrata = new List<SelectListItem>(bv),
                    Godiste = new List<SelectListItem>(god),
                    Gorivo = new List<SelectListItem>(gor),
                    Stanje = new List<SelectListItem>(st),
                    Transmisija = new List<SelectListItem>(tra),
                    Cijena = karakteristikeZaModifikovanje.Cijena.ToString(),
                    Kilometraza = karakteristikeZaModifikovanje.Kilometraza.ToString(),
                    Motor = karakteristikeZaModifikovanje.Motor,
                    Snaga = karakteristikeZaModifikovanje.Snaga.ToString(),
                    Zapremina = karakteristikeZaModifikovanje?.Zapremina,
                    Pogon = karakteristikeZaModifikovanje.Pogon,
                    Boja = karakteristikeZaModifikovanje.Boja,
                    Svjetla = karakteristikeZaModifikovanje.Svjetla,
                    AutomobilID = autoZaModifikovanje.AutomobilID
                }
            };

            ViewData["AutoNijeUredjeno"] = "AutoNijeUredjeno";
            return View("UrediAutomobil", m);
        }

        [Authorize(Roles = "Admin, Uposlenik", Policy = "BrisanjePolicy")]
        public IActionResult ObrisiAutomobil(string Id, int AutomobilID)
        {
            var korisnik = db.KorisnickiNalog.Find(Id);
            var osoba = db.Osobe.Where(o => o.OsobaID == korisnik.OsobaID).FirstOrDefault();
            var auto = db.Automobili.Find(AutomobilID);
            var karakteristike = db.Karakteristike.Where(k => k.AutomobilID == AutomobilID).FirstOrDefault();

            var noviIzvjestaj = new Izvjestaj
            {
                AutomobilID = auto.AutomobilID.ToString(),
                Naziv = "Brisanje vozila",
                Opis = auto.Proizvodjac + " " + auto.Model + " " + "[" + karakteristike.Cijena + " KM]",
                KorisnickiNalogID = korisnik.Id,
                UposlenikKreiraIzvjestaj = korisnik.UserName + "(" + osoba.Ime + " " + osoba.Prezime + ")",
                DatumIzvjestaja = DateTime.Now
            };

            db.Izvjestaji.Add(noviIzvjestaj);
            db.SaveChanges();

            TempData["AutoObrisan"] = "AutoObrisan";
            return RedirectToAction("AutomobilDetaljno", new { AutomobilID = AutomobilID, Id = Id });
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Angular
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Izvjestaji(string q, int trenutnaStranica = 1, int brPodatakaPoStranici = 10)
        {
            var query = db.Izvjestaji.Where(i => q == null || (i.UposlenikKreiraIzvjestaj.StartsWith(q)));
            var total = query.Count();

            var m = new IzvjestajiVM
            {
                total = total,
                izvjestaji = query
                .OrderBy(o => o.Naziv).ThenByDescending(t => t.DatumIzvjestaja)
                .Skip((trenutnaStranica - 1) * brPodatakaPoStranici)
                .Take(brPodatakaPoStranici)
                .Select(i => new IzvjestajiVM.Izvjestaj
                {
                    izvjestajID = i.IzvjestajID,
                    naziv = i.Naziv,
                    automobilID = i.AutomobilID.ToString(),
                    opis = i.Opis,
                    datumIzvjestaja = i.DatumIzvjestaja.ToString(),
                    uposlenikKreiraIzvjestaj = i.UposlenikKreiraIzvjestaj,
                    korisnickiNalogID = i.KorisnickiNalogID
                }).ToList()
            };

            return Ok(m);
        }
        
        //Angular
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ObnoviAuto (int izvjestajID)
        {
            var izvjestaj = db.Izvjestaji.Find(izvjestajID);
            db.Izvjestaji.Remove(izvjestaj);
            db.SaveChanges();

            return Ok();
        }

        //Angular
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ObrisiIzvjestaj(int izvjestajID)
        {
            var izvjestaj = db.Izvjestaji.Find(izvjestajID);

            if(izvjestaj.Naziv.StartsWith("B"))
            {
                var auto = db.Automobili.Find(int.Parse(izvjestaj.AutomobilID));
                db.Automobili.Remove(auto);
            }

            db.Izvjestaji.Remove(izvjestaj);
            db.SaveChanges();

            return Ok();
        }

        //Angular
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SnimiPromjeneIzvjestaja([FromBody] IzvjestajiVM.Izvjestaj izvj)
        {
            var izvjestaj = db.Izvjestaji.Find(izvj.izvjestajID);
            izvjestaj.Opis = izvj.opis;
            db.Entry(izvjestaj).State = EntityState.Modified;
            db.SaveChanges();

            return Ok();
        }

        [AllowAnonymous]
        public IActionResult DohvatiModele(string q, string p)
        {
            var m = new List<Select2Model>();
            m = (string.IsNullOrEmpty(q)) ? db.Automobili.Where(a => a.Proizvodjac == p)
                .Select(b => new Select2Model
                {
                    id = b.Model,
                    text = b.Model
                }).Distinct().ToList() : m = db.Automobili.Where(a => (a.Proizvodjac == p) && (a.Model.ToLower().Contains(q.ToLower())))
                .Select(b => new Select2Model
                {
                    id = b.Model,
                    text = b.Model
                }).Distinct().ToList();

            return Json(new { items = m });
        }
    }
}

