using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Podaci.EF;
using Podaci.Entiteti;
using RS1_seminarski.Models;
using RS1_seminarski.Modelview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RS1_seminarski.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministracijaController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<KorisnickiNalog> userManager;

        MyContext db = new MyContext();

        public AdministracijaController(RoleManager<IdentityRole> roleManager, UserManager<KorisnickiNalog> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult KreirajUlogu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KreirajUlogu(KreirajUloguVM vm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = vm.NazivUloge
                };

                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListaUloga", "Administracija");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(vm);
        }

        string Grad(int ? Gradid)
        {
            foreach(var g in db.Gradovi)
            {
                if (g.GradID == Gradid)
                    return g.Naziv;
            }
            return null;
        }

        string Spol(string Spolid)
        {
            foreach (var s in db.Spol)
            {
                if (s.SpolID.ToString() == Spolid)
                    return s.Naziv;
            }
            return null;
        }

        string PostanskiBr(int ? Gradid)
        {
            foreach (var g in db.Gradovi)
            {
                if (g.GradID == Gradid)
                    return g.PostabskiBroj;
            }
            return null;
        }

        [HttpGet]
        public IActionResult ListaKorisnika()
        {
            var posljednjiAdmin = TempData["posljednjiAdmin"] as string;

            var korisnici = userManager.Users.OrderByDescending(o=>o.DatumRegistracije);

            var m = new List<ListaKorisnikaVM>();

            foreach(var k in korisnici)
            {
                var korisnik = new ListaKorisnikaVM()
                {
                    Id = k.Id,
                    KorisnickoIme = k.UserName,
                    Email = k.Email,
                    Ime = db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.Ime).FirstOrDefault(),
                    Prezime = db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.Prezime).FirstOrDefault(),
                    Spol = (db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.SpolID).FirstOrDefault() == null) ? null : Spol(db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.SpolID.ToString()).FirstOrDefault()),
                    Adresa = db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.Adresa).FirstOrDefault(),
                    Grad = (db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.GradID).FirstOrDefault() == null) ? null : Grad(db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.GradID).FirstOrDefault()),
                    PoštanskiBr = (db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.GradID).FirstOrDefault() == null) ? null : PostanskiBr(db.Osobe.Where(o => o.OsobaID == k.OsobaID).Select(s => s.GradID).FirstOrDefault()),
                    DatumReg = k.DatumRegistracije.ToString()
                };

                m.Add(korisnik);
            }

            ViewData["posljednjiAdmin"] = posljednjiAdmin;
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> UrediKorisnika(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var osoba = db.Osobe.Find(user.OsobaID);
            var uposlenik = db.Uposlenici.Where(u => u.OsobaID == osoba.OsobaID).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }

            if(await userManager.IsInRoleAsync(user, "Uposlenik"))
            {
                ViewBag.Uposlenik = "da";
            }

            var spolovi = new List<SelectListItem>();
            spolovi.AddRange(db.Spol.Select(s => new SelectListItem
            {
                Value = s.SpolID.ToString(),
                Text = s.Naziv,
                Selected = (s.SpolID == osoba.SpolID) ? true : false
            }).ToList());

            List<SelectListItem> str = str = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            str.AddRange(db.StrucnaSprema.OrderByDescending(s => s.Naziv).Select(a => new SelectListItem
            {
                Value = a.StrucnaSpremaID.ToString(),
                Text = a.Naziv,
                Selected = (uposlenik != null) ? ((a.StrucnaSpremaID == uposlenik.StrucnaSpremaID) ? true : false) : false
            }).Distinct().ToList());

            List<SelectListItem> rmj = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            rmj.AddRange(db.RadnoMjesto.OrderByDescending(r => r.Naziv).Select(a => new SelectListItem
            {
                Value = a.RadnoMjestoID.ToString(),
                Text = a.Naziv,
                Selected = (uposlenik != null) ? ((a.RadnoMjestoID == uposlenik.RadnoMjestoID) ? true : false) : false
            }).Distinct().ToList());

            var userUloge = await userManager.GetRolesAsync(user);
            var userClaims = await userManager.GetClaimsAsync(user);

            var m = new UrediKorisnikaVM
            {
                Id = Id,
                KorisnickoIme = user.UserName,
                Email = user.Email,
                Ime = osoba.Ime,
                Prezime = osoba.Prezime,
                Spolstr = new List<SelectListItem>(spolovi),
                Adresa = osoba.Adresa,
                GradNaziv = (osoba?.GradID == null) ? null : Grad(osoba?.GradID),
                PostanskiBroj = PostanskiBr(osoba.GradID),
                DatumReg = user.DatumRegistracije.ToString(),
                Uloge = userUloge,
                Claims = userClaims.Select(c => c.Type + ": " + c.Value).ToList(),
                JMBG = uposlenik?.JMBG,
                Iskustvo = uposlenik?.Iskustvo.ToString(),
                MinuliStaz = uposlenik?.MinuliStaz.ToString(),
                DatumZaposlenja = uposlenik?.DatumZaposljenja.ToString(),
                StrucnaSprema = new List<SelectListItem>(str),
                RadnoMjesto = new List<SelectListItem>(rmj)
            };

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> UrediKorisnika(UrediKorisnikaVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(vm.Id);

                if (user == null)
                {
                    ViewBag.ErrorMessage = $"Korisnik sa Id-jem = {vm.Id} ne postoji";
                    return View("NePostoji");
                }
                else
                {
                    var osoba = db.Osobe.Find(user.OsobaID);
                    var grad = db.Gradovi.Find(osoba.GradID);
                    var uposlenik = db.Uposlenici.Where(u => u.OsobaID == osoba.OsobaID).FirstOrDefault();

                    if (!(string.IsNullOrEmpty(vm.NovaStrucnaSprema)) && STRNePostoji(vm.NovaStrucnaSprema))
                    {
                        var novaStrucnaSprema = new StrucnaSprema
                        {
                            Naziv = vm.NovaStrucnaSprema
                        };
                        db.StrucnaSprema.Add(novaStrucnaSprema);
                        db.SaveChanges();
                    }

                    if (!(string.IsNullOrEmpty(vm.NovoRadnoMjesto)) && NRMNePostoji(vm.NovoRadnoMjesto))
                    {
                        var novoRadnoMjesto = new RadnoMjesto
                        {
                            Naziv = vm.NovoRadnoMjesto
                        };
                        db.RadnoMjesto.Add(novoRadnoMjesto);
                        db.SaveChanges();
                    }

                    user.UserName = vm.KorisnickoIme;
                    user.Email = vm.Email;
                    osoba.Ime = vm.Ime;
                    osoba.Prezime = vm.Prezime;
                    osoba.SpolID = db.Spol.Where(s => s.SpolID.ToString() == vm.SpolstrID).Select(s => s.SpolID).FirstOrDefault();
                    osoba.Adresa = vm.Adresa;
                    grad.Naziv = vm.GradNaziv;
                    grad.PostabskiBroj = vm.PostanskiBroj;

                    if(uposlenik != null)
                    {
                        uposlenik.Iskustvo = (vm.Iskustvo != null) ? int.Parse(vm.Iskustvo) : 0;
                        uposlenik.JMBG = vm.JMBG;
                        uposlenik.MinuliStaz = (vm.MinuliStaz != null) ? int.Parse(vm.MinuliStaz) : 0;
                        uposlenik.StrucnaSpremaID = string.IsNullOrEmpty(vm.NovaStrucnaSprema) ? int.Parse(vm.StrucnaSpremaID) : db.StrucnaSprema.Where(str => str.Naziv == vm.NovaStrucnaSprema)
                                                          .Select(str => str.StrucnaSpremaID).FirstOrDefault();
                        uposlenik.RadnoMjestoID = string.IsNullOrEmpty(vm.NovoRadnoMjesto) ? int.Parse(vm.RadnoMjestoID) : db.RadnoMjesto.Where(rm => rm.Naziv == vm.NovoRadnoMjesto)
                                                        .Select(rm => rm.RadnoMjestoID).FirstOrDefault();

                        db.Entry(uposlenik).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    db.Entry(grad).State = EntityState.Modified;
                    db.SaveChanges();

                    db.Entry(osoba).State = EntityState.Modified;
                    db.SaveChanges();

                    var result = await userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListaKorisnika");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(vm);
                }
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult ListaUloga()
        {
            var roles = roleManager.Roles;
            var UlogaJeAdmin = TempData["UlogaJeAdmin"] as string;

            ViewData["UlogaJeAdmin"] = UlogaJeAdmin;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> UrediUlogu(string Id)
        {
            var uloga = await roleManager.FindByIdAsync(Id);
            
            if(uloga == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }

            var m = new UrediUloguVM
            {
                Id = uloga.Id,
                NazivUloge = uloga.Name
            };

            foreach(var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user, uloga.Name))
                {
                    m.KorisnickiNalozi.Add(user.UserName);
                }
            }

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> UrediUlogu(UrediUloguVM vm)
        {
            var uloga = await roleManager.FindByIdAsync(vm.Id);

            if (uloga == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa Id-jem = {vm.Id} ne postoji";
                return View("NePostoji");
            }
            else
            {
                uloga.Name = vm.NazivUloge;
                var result = await roleManager.UpdateAsync(uloga);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListaUloga");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> AzurirajNalogeSaUlogom(string Id)
        {
            ViewBag.Id = Id;

            var uloga = await roleManager.FindByIdAsync(Id);

            if (uloga == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }

            var m = new List<AzurirajNalogeSaUlogomVM>();

            foreach(var user in userManager.Users)
            {
                var userUloga = new AzurirajNalogeSaUlogomVM
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if(await userManager.IsInRoleAsync(user, uloga.Name))
                {
                    userUloga.IsSelected = true;
                }
                else
                {
                    userUloga.IsSelected = false;
                }

                if (!(await userManager.IsInRoleAsync(user, "Admin")))
                    m.Add(userUloga);
            }

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> AzurirajNalogeSaUlogom(List<AzurirajNalogeSaUlogomVM> vm, string Id)
        {
            var uloga = await roleManager.FindByIdAsync(Id);

            if (uloga == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }

            foreach (var x in vm)
            {
                var user = await userManager.FindByIdAsync(x.UserId);

                if (x.IsSelected && !(await userManager.IsInRoleAsync(user, uloga.Name)))
                {
                    if (uloga.Name == "Uposlenik")
                    {
                        var noviUposlenik = new Uposlenik
                        {
                            DatumZaposljenja = DateTime.Now,
                            Iskustvo = 0,
                            MinuliStaz = 0,
                            JMBG = null,
                            OsobaID = (int)user.OsobaID,
                            StrucnaSpremaID = db.StrucnaSprema.Select(str => str.StrucnaSpremaID).FirstOrDefault(),
                            RadnoMjestoID = db.RadnoMjesto.Select(rm => rm.RadnoMjestoID).FirstOrDefault()
                        };

                        db.Uposlenici.Add(noviUposlenik);
                        db.SaveChanges();
                    }

                    await userManager.AddToRoleAsync(user, uloga.Name);
                }
                else if (!x.IsSelected && await userManager.IsInRoleAsync(user, uloga.Name))
                {
                    await userManager.RemoveFromRoleAsync(user, uloga.Name);
                    if(uloga.Name == "Uposlenik") 
                    {
                        var obrisiUposlenog = db.Uposlenici.Where(u => u.OsobaID == user.OsobaID).FirstOrDefault();
                        if (obrisiUposlenog != null)
                        {
                            var res = db.Rezervacije.Where(r => r.UposlenikID == obrisiUposlenog.UposlenikID).ToList();
                            foreach (var u in res)
                            {
                                u.UposlenikID = null;
                                db.Entry(u).State = EntityState.Modified;
                                db.SaveChanges();
                            }

                            db.Remove(obrisiUposlenog);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    continue;
                }
            }

            return RedirectToAction("UrediUlogu", new { Id });
        }

        [HttpPost]
        public async Task<IActionResult> ObrisiUlogu(string Id)
        {
            var uloga = await roleManager.FindByIdAsync(Id);

            if (uloga == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }
            else if (uloga.Name == "Admin")
            {
                TempData["UlogaJeAdmin"] = "UlogaJeAdmin";
                return RedirectToAction("ListaUloga");
            }
            else
            {
                var result = await roleManager.DeleteAsync(uloga);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListaUloga");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListaUloga");
            }
        }

        
        [Authorize(Policy="BrisanjePolicyAdmin")]
        public async Task<IActionResult> ObrisiKorisnika(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            var osoba = db.Osobe.Find(user.OsobaID);
            var klijent = db.Klijenti.Where(k => k.OsobaID == osoba.OsobaID).FirstOrDefault();

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }
            else if(await userManager.IsInRoleAsync(user, "Admin"))
            {
                var brojAdmina = await userManager.GetUsersInRoleAsync("Admin");

                if (brojAdmina.Count() > 1)
                {
                    var izvjestaji = db.Izvjestaji.Where(i => i.KorisnickiNalogID == user.Id).ToList();
                    db.Izvjestaji.RemoveRange(izvjestaji);
                    db.SaveChanges();

                    var result = await userManager.DeleteAsync(user);

                    if (result.Succeeded)
                    {
                        if (klijent != null)
                        {
                            db.Klijenti.Remove(klijent);
                            db.SaveChanges();
                        }

                        db.Osobe.Remove(osoba);
                        db.SaveChanges();

                        return RedirectToAction("ListaKorisnika");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("ListaKorisnika");
                }
                else
                {
                    TempData["posljednjiAdmin"] = "posljednjiAdmin";
                    return RedirectToAction("ListaKorisnika");
                }
            }
            else
            {
                var izvjestaji = db.Izvjestaji.Where(i => i.KorisnickiNalogID == user.Id).ToList();
                db.Izvjestaji.RemoveRange(izvjestaji);
                db.SaveChanges();

                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    if (klijent != null)
                    {
                        db.Klijenti.Remove(klijent);
                        db.SaveChanges();
                    }

                    db.Osobe.Remove(osoba);
                    db.SaveChanges();

                    return RedirectToAction("ListaKorisnika");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListaKorisnika");
            }
        }

        private static bool IsSelected = false;

        [HttpGet]
        [Authorize(Policy = "UređivanjePolicyAdmin")]
        public async Task<IActionResult> AzurirajKorisnickeUloge(string Id)
        {
            ViewBag.Id = Id;

            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }

            var m = new List<AzurirajKorisnickeUlogeVM>();

            foreach(var uloga in roleManager.Roles)
            {
                var ulogaKorisnika = new AzurirajKorisnickeUlogeVM
                {
                    UlogaID = uloga.Id,
                    NazivUloge = uloga.Name
                };

                if (await userManager.IsInRoleAsync(user, uloga.Name))
                {
                    ulogaKorisnika.IsSelected = true;
                    if (uloga.Name == "Uposlenik")
                        IsSelected = true;
                }
                else
                {
                    ulogaKorisnika.IsSelected = false;
                }

                m.Add(ulogaKorisnika);
            }

            return View(m);
        }

        private static List<AzurirajKorisnickeUlogeVM> noveUloge = new List<AzurirajKorisnickeUlogeVM>();

        [HttpPost]
        [Authorize(Policy = "UređivanjePolicyAdmin")]
        public async Task<IActionResult> AzurirajKorisnickeUloge(List<AzurirajKorisnickeUlogeVM> vm, string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }

            noveUloge = vm;

            var Uloge = vm.Where(x => x.IsSelected).Select(s => s.NazivUloge);

            foreach(var u in Uloge)
            {
                if(u == "Uposlenik" && !IsSelected)
                {
                    return RedirectToAction("DodavanjePodatakaNovogUposlenika", new { Id });
                }
            }

            var uloge = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, uloge);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nemoguće obrisati postojeće uloge korisnika");
                return View(vm);
            }

            result = await userManager.AddToRolesAsync(user, noveUloge.Where(x => x.IsSelected).Select(s => s.NazivUloge));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Neuspješno mijenjanje uloga");
                return View(vm);
            }

            if (!await userManager.IsInRoleAsync(user, "Uposlenik"))
            {
                var obrisiUposlenog = db.Uposlenici.Where(u => u.OsobaID == user.OsobaID).FirstOrDefault();
                if (obrisiUposlenog != null)
                {
                    var res = db.Rezervacije.Where(r => r.UposlenikID == obrisiUposlenog.UposlenikID).ToList();
                    foreach(var u in res)
                    {
                        u.UposlenikID = null;
                        db.Entry(u).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    db.Remove(obrisiUposlenog);
                    db.SaveChanges();
                }

                IsSelected = false;
            }

            return RedirectToAction("UrediKorisnika", new { Id });
        }

        [HttpGet]
        [Authorize(Policy = "UređivanjePolicyAdmin")]
        public async Task<IActionResult> DodavanjePodatakaNovogUposlenika(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            List<SelectListItem> str = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            str.AddRange(db.StrucnaSprema.OrderByDescending(s => s.Naziv).Select(a => new SelectListItem
            {
                Value = a.StrucnaSpremaID.ToString(),
                Text = a.Naziv
            }).Distinct().ToList());

            List<SelectListItem> rmj = new List<SelectListItem>() { new SelectListItem { Text = "---" } };
            rmj.AddRange(db.RadnoMjesto.OrderByDescending(r => r.Naziv).Select(a => new SelectListItem
            {
                Value = a.RadnoMjestoID.ToString(),
                Text = a.Naziv
            }).Distinct().ToList());

            var m = new DodavanjePodatakaNovogUposlenikaVM
            {
                Id = Id,
                noviUposlenik = user.UserName,
                StrucnaSprema = new List<SelectListItem>(str),
                RadnoMjesto = new List<SelectListItem>(rmj)
            };

            return View(m);
        }

        public bool STRNePostoji(string novaStr)
        {
            foreach (var str in db.StrucnaSprema)
            {
                if (str.Naziv == novaStr)
                    return false;
            }
            return true;
        }

        public bool NRMNePostoji(string novoRM)
        {
            foreach (var nrm in db.RadnoMjesto)
            {
                if (nrm.Naziv == novoRM)
                    return false;
            }
            return true;
        }
        
        [HttpPost]
        [Authorize(Policy = "UređivanjePolicyAdmin")]
        public async Task<IActionResult> DodavanjePodatakaNovogUposlenika(DodavanjePodatakaNovogUposlenikaVM vm)
        {
            var user = await userManager.FindByIdAsync(vm.Id);
            var osoba = db.Osobe.Find(user.OsobaID);

            if (ModelState.IsValid)
            {
                var uloge = await userManager.GetRolesAsync(user);
                var result = await userManager.RemoveFromRolesAsync(user, uloge);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Nemoguće obrisati postojeće uloge korisnika");
                    return View(vm);
                }

                if (!(string.IsNullOrEmpty(vm.NovaStrucnaSprema)) && STRNePostoji(vm.NovaStrucnaSprema))
                {
                    var novaStrucnaSprema = new StrucnaSprema
                    {
                        Naziv = vm.NovaStrucnaSprema
                    };
                    db.StrucnaSprema.Add(novaStrucnaSprema);
                    db.SaveChanges();
                }

                if (!(string.IsNullOrEmpty(vm.NovoRadnoMjesto)) && NRMNePostoji(vm.NovoRadnoMjesto))
                {
                    var novoRadnoMjesto = new RadnoMjesto
                    {
                        Naziv = vm.NovoRadnoMjesto
                    };
                    db.RadnoMjesto.Add(novoRadnoMjesto);
                    db.SaveChanges();
                }

                var noviUposlenik = new Uposlenik
                {
                    DatumZaposljenja = DateTime.Now,
                    Iskustvo = !string.IsNullOrEmpty(vm.Iskustvo) ? int.Parse(vm.Iskustvo) : 0,
                    MinuliStaz = !string.IsNullOrEmpty(vm.MinuliStaz) ? int.Parse(vm.MinuliStaz) : 0,
                    JMBG = vm.JMBG,
                    OsobaID = osoba.OsobaID,
                    StrucnaSpremaID = string.IsNullOrEmpty(vm.NovaStrucnaSprema) ? int.Parse(vm.StrucnaSpremaID) : db.StrucnaSprema.Where(str => str.Naziv == vm.NovaStrucnaSprema)
                    .Select(str => str.StrucnaSpremaID).FirstOrDefault(),
                    RadnoMjestoID = string.IsNullOrEmpty(vm.NovoRadnoMjesto) ? int.Parse(vm.RadnoMjestoID) : db.RadnoMjesto.Where(rm => rm.Naziv == vm.NovoRadnoMjesto)
                    .Select(rm => rm.RadnoMjestoID).FirstOrDefault()
                };

                db.Uposlenici.Add(noviUposlenik);
                db.SaveChanges();

                result = await userManager.AddToRolesAsync(user, noveUloge.Where(x => x.IsSelected).Select(s => s.NazivUloge));

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Nemoguće obrisati postojeće uloge korisnika");
                    return View(vm);
                }

                return RedirectToAction("UrediKorisnika", new { Id = vm.Id });
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> AzurirajKorisnickeMogucnosti(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }

            var postojeceKorisnickeMogucnosti = await userManager.GetClaimsAsync(user);

            var m = new KorisnikMogucnostiVM
            {
                Id = user.Id
            };

            foreach(var mogucnost in SkladisteMogucnosti.SveMogucnosti)
            {
                KorisnikMogucnostiVM.KorisnikMogucnosti KorisnickaMogucnost = new KorisnikMogucnostiVM.KorisnikMogucnosti
                {
                    TipMogucnosti = mogucnost.Type
                };

                if(postojeceKorisnickeMogucnosti.Any(c => c.Type == mogucnost.Type && c.Value == "true"))
                {
                    KorisnickaMogucnost.IsSelected = true;
                }

                m.Mogucnosti.Add(KorisnickaMogucnost);
            }

            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> AzurirajKorisnickeMogucnosti(KorisnikMogucnostiVM vm)
        {
            var user = await userManager.FindByIdAsync(vm.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id-jem = {vm.Id} ne postoji";
                return View("NePostoji");
            }

            var mogucnosti = await userManager.GetClaimsAsync(user);
            var result = await userManager.RemoveClaimsAsync(user, mogucnosti);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nemoguće obrisati postojeće mogućnosti korisnika");
                return View(vm);
            }

            result = await userManager.AddClaimsAsync(user, vm.Mogucnosti.Where(x => x.IsSelected)
                .Select(s => new Claim(s.TipMogucnosti, s.IsSelected ? "true" : "false")));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Nemoguće obrisati postojeće mogućnosti korisnika");
                return View(vm);
            }

            return RedirectToAction("UrediKorisnika", new { vm.Id });
        }
    }
}
