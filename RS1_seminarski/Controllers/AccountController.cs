using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podaci.EF;
using Podaci.Entiteti;
using RS1_seminarski.Modelview;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Google.Authenticator;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RS1_seminarski.Controllers
{
    public class AccountController : Controller
    {
        MyContext db = new MyContext();

        private readonly UserManager<KorisnickiNalog> userManager;
        private readonly SignInManager<KorisnickiNalog> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<KorisnickiNalog> userManager, SignInManager<KorisnickiNalog> signInManager,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginWM input, string returnUrl)
        {
            input.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(input.Email);

                if (user != null)
                {
                    if (!user.EmailConfirmed && (await userManager.CheckPasswordAsync(user, input.Lozinka)))
                    {
                        ModelState.AddModelError(string.Empty, "Email potrebno potvrditi!");
                        return View("Index", input);
                    }

                    var TFAOBavezan = user.TwoFactorEnabled;
                    user.TwoFactorEnabled = false;

                    var result = await userManager.CheckPasswordAsync(user, input.Lozinka);

                    if (result)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            if (TFAOBavezan)
                            {
                                return RedirectToAction("tfa", new { Email = input.Email });
                            }

                            await signInManager.PasswordSignInAsync(user.UserName, input.Lozinka, input.ZapamtiLozinku, false);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Pogrešan email ili lozinka!");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Pogrešan email ili lozinka!");
                }
            }

            return View("Index", input);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult tfa(string Email)
        {
            var m = new TwoFactorAuthentication()
            {
                Email = Email
            };

            return View(m);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> tfa(TwoFactorAuthentication vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(vm.Email);

                var accountSecretKey = user.Id;
                var twoFactorAuthenticator = new TwoFactorAuthenticator();
                var result = twoFactorAuthenticator
                    .ValidateTwoFactorPIN(accountSecretKey, vm.Kod);

                if (result)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.NetacanKod = "da";
                return View(vm);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> UspostavaTFAkoda()
        {
            var user = await userManager.GetUserAsync(User);

            var accountSecretKey = user.Id;
            var twoFactorAuthenticator = new TwoFactorAuthenticator();
            var setupCode = twoFactorAuthenticator.GenerateSetupCode("Two Factor Demo App", user.Email,
                Encoding.ASCII.GetBytes(accountSecretKey));

            var m = new TwoFactorAuthentication()
            {
                Email = user.Email,
                BarcodeImageUrl = setupCode.QrCodeSetupImageUrl,
                SetupCode = setupCode.ManualEntryKey,
                AktiviranTFA = user.TwoFactorEnabled
            };

            return PartialView(m);
        }

        [HttpPost]
        public async Task<IActionResult> UspostavaTFAkoda(TwoFactorAuthentication vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(vm.Email);

                var accountSecretKey = user.Id;
                var twoFactorAuthenticator = new TwoFactorAuthenticator();
                var result = twoFactorAuthenticator
                    .ValidateTwoFactorPIN(accountSecretKey, vm.Kod);

                if (result)
                {
                    user.TwoFactorEnabled = true;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UspostavaTFAkoda", "Account");
                }

                ViewBag.NetacanKod = "da";
                return PartialView(vm);
            }

            return PartialView(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeaktivirajTFA(TwoFactorAuthentication vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(vm.Email);

                var accountSecretKey = user.Id;
                var twoFactorAuthenticator = new TwoFactorAuthenticator();
                var result = twoFactorAuthenticator
                    .ValidateTwoFactorPIN(accountSecretKey, vm.Kod);

                if (result)
                {
                    user.TwoFactorEnabled = false;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("UspostavaTFAkoda", "Account");
                }

                ViewBag.NetacanKod = "da";
                return PartialView("UspostavaTFAkoda", vm);
            }

            return PartialView("UspostavaTFAkoda", vm);
        }
        
        [HttpGet]
        public async Task<IActionResult> DodajLozinku()
        {
            var user = await userManager.GetUserAsync(User);

            var userImaLozinku = await userManager.HasPasswordAsync(user);

            if (userImaLozinku)
            {
                return RedirectToAction("PromijeniLozinku");
            }

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> DodajLozinku(DodajLozinkuVM vm)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                var result = await userManager.AddPasswordAsync(user, vm.Lozinka);

                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return PartialView();
                }

                await signInManager.RefreshSignInAsync(user);

                ViewBag.SuccessTitle = "Uspješno postavljena lozinka";
                ViewBag.SuccessMessage = "Sada možete koristiti i lokalni nalog da se logirate";
                return PartialView("Success");
            }

            return PartialView();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var m = new LoginWM
            {
                ZapamtiLozinku = true,
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            
            return View(m);
        }

        int getSpol(string spol)
        {
            if (spol.StartsWith("M") || spol.StartsWith("m"))
            {
                return db.Spol.Where(s => s.Naziv.StartsWith("M") || s.Naziv.StartsWith("m")).Select(s => s.SpolID).FirstOrDefault(); ;
            }
            else
            {
                return db.Spol.Where(s => s.Naziv.StartsWith("F") || s.Naziv.StartsWith("f")).Select(s => s.SpolID).FirstOrDefault();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogins(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Account", new { returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallBack(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var m = new LoginWM
            {
                ZapamtiLozinku = true,
                ReturnUrl = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Greška vanjskog linka: {remoteError}");

                return View("Index", m);
            }

            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, $"Greška u učitavanju informacija vanjskog linka");
                return View("Index", m);
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            KorisnickiNalog user = null;

            if (email != null)
            {
                user = await userManager.FindByEmailAsync(email);

                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email potrebno potvrditi!");
                    return View("Index", m);
                }

                if (user != null && user.TwoFactorEnabled)
                {
                    return RedirectToAction("tfa", new { Email = email });
                }
            }

            var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                               isPersistent: false, bypassTwoFactor: false);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                if (email != null)
                {
                    if (user == null)
                    {
                        user = new KorisnickiNalog
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            DatumRegistracije = DateTime.Now
                        };

                        var result = await userManager.CreateAsync(user);

                        if (result.Succeeded)
                        {
                            var noviGrad = db.Gradovi.Where(g => g.Naziv == info.Principal.FindFirstValue(ClaimTypes.Locality)).FirstOrDefault();

                            if (noviGrad == null)
                            {
                                noviGrad = new Grad
                                {
                                    Naziv = info.Principal.FindFirstValue(ClaimTypes.Locality),
                                    PostabskiBroj = info.Principal.FindFirstValue(ClaimTypes.PostalCode)
                                };

                                db.Gradovi.Add(noviGrad);
                                db.SaveChanges();
                            }

                            var spol = info.Principal.FindFirstValue(ClaimTypes.Gender);
                            var novaOsoba = new Osoba
                            {
                                Ime = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                                Prezime = info.Principal.FindFirstValue(ClaimTypes.Surname),
                                Adresa = info.Principal.FindFirstValue(ClaimTypes.StreetAddress),
                                GradID = noviGrad.GradID,
                                SpolID = spol == null ? null : (int?)getSpol(spol)
                            };

                            db.Osobe.Add(novaOsoba);
                            db.SaveChanges();

                            user.OsobaID = novaOsoba.OsobaID;

                            await userManager.UpdateAsync(user);

                            await userManager.AddToRoleAsync(user, "Korisnik");

                            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                            var confirmationLink = Url.Action("PotvrdiEmail", "Account", new { Id = user.Id, token = token }, Request.Scheme);
                            logger.Log(LogLevel.Warning, confirmationLink);

                            EmailSender e = new EmailSender();
                            await e.SendEmailAsync(user.Email, "AutoReser - Potvrdite registraciju", confirmationLink);

                            ViewBag.SuccessTitle = "Registracija uspješna";
                            ViewBag.SuccessMessage = "Prije nego se logirate molimo potvrdite registraciju klikom na link koji je poslan na email";

                            return View("Success");
                        }
                    }

                    await userManager.AddLoginAsync(user, info);
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                ViewBag.ErrorTitle = $"Email na: {info.LoginProvider} ne postoji";
                ViewBag.ErrorMessage = "Please contact support on semasenior@gmail.com";

                return View("Error");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ZaboravljenaLozinka()
        {
            var m = new ZaboravljenaLozinkaVM();

            return View(m);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ZaboravljenaLozinka(ZaboravljenaLozinkaVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(vm.Email);

                if(user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = vm.Email, token = token }, Request.Scheme);
                    logger.Log(LogLevel.Warning, passwordResetLink);

                    EmailSender e = new EmailSender();
                    await e.SendEmailAsync(user.Email, "AutoReser - Potvrdite RESET lozinke", passwordResetLink);

                    ViewBag.SuccessTitle = "Poslan je email sa instrukcijama za reset lozinke";
                    return View("Success");
                }
                ViewBag.ErrorTitle = "Email nije potvrđen ili ne postoji nalog sa unesenim emailom!";
                return View("Success");
            }
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> PromijeniLozinku()
        {
            var user = await userManager.GetUserAsync(User);

            var userImaLozinku = await userManager.HasPasswordAsync(user);

            if (!userImaLozinku)
            {
                return RedirectToAction("DodajLozinku");
            }

            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> PromijeniLozinku(PromijeniLozinkuVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);

                if(user == null)
                {
                    return RedirectToAction("Index");
                }

                var result = await userManager.ChangePasswordAsync(user, vm.TrenutnaLozinka, vm.NovaLozinka);

                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return PartialView();
                }
                await signInManager.RefreshSignInAsync(user);
                ViewBag.SuccessTitle = "Uspješno promijenjena lozinka";
                return PartialView("Success");
            }
            return PartialView(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Nevažeći token reseta lozinke");
            }

            var m = new ResetPasswordVM();
            return View(m);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(vm.Email);

                if(user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, vm.Token, vm.Lozinka);

                    if (result.Succeeded)
                    {
                        ViewBag.SuccessTitle = "Lozinka je uspješno promijenjena";
                        ViewBag.DugmeZaLogin = "PrikaziDugmeZaLogin";
                        return View("Success");
                    }

                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(vm);
                }
                ViewBag.ErrorTitle = "Ne postoji račun sa unesenim emailom";
                return View("Success");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> LogOutPotvrda()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registracija()
        {
            var spolovi = new List<SelectListItem>();
            spolovi.AddRange(db.Spol.Select(s => new SelectListItem
            {
                Value = s.SpolID.ToString(),
                Text = s.Naziv
            }).ToList());

            RegistracijaVM m = new RegistracijaVM
            {
                Spolstr = new List<SelectListItem>(spolovi)
            };

            if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {
                ViewData["AdminJe"] = "AdminJe";
                return View(m);
            }

            return View(m);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> MailZauzet(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} je zauzet");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> KreiranjeAccounta(RegistracijaVM vm)
        {
            if (ModelState.IsValid)
            {
                var user = new KorisnickiNalog { 
                    UserName = vm.KorisnickoIme, 
                    Email = vm.Email,
                    DatumRegistracije = DateTime.Now
                };

                var result = await userManager.CreateAsync(user, vm.Lozinka);

                if (result.Succeeded)
                {
                    var noviGrad = db.Gradovi.Where(g => g.Naziv == vm.GradNaziv).FirstOrDefault();

                    if (noviGrad == null)
                    {
                        noviGrad = new Grad
                        {
                            Naziv = vm.GradNaziv,
                            PostabskiBroj = vm.PostanskiBroj
                        };

                        db.Gradovi.Add(noviGrad);
                        db.SaveChanges();
                    }

                    var novaOsoba = new Osoba
                    {
                        Ime = vm.Ime,
                        Prezime = vm.Prezime,
                        Adresa = vm?.Adresa,
                        GradID = noviGrad.GradID,
                        SpolID = int.Parse(vm.SpolstrID)
                    };

                    db.Osobe.Add(novaOsoba);
                    db.SaveChanges();

                    user.OsobaID = novaOsoba.OsobaID;

                    await userManager.UpdateAsync(user);

                    await userManager.AddToRoleAsync(user, "Korisnik");

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("PotvrdiEmail", "Account", new { Id = user.Id, token = token }, Request.Scheme);
                    logger.Log(LogLevel.Warning, confirmationLink);

                    EmailSender e = new EmailSender();
                    await e.SendEmailAsync(user.Email, "AutoReser - Potvrdite registraciju", confirmationLink);

                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("ListaKorisnika", "Administracija");
                    }

                    ViewBag.SuccessTitle = "Registracija uspješna";
                    ViewBag.SuccessMessage = "Prije nego se logirate molimo potvrdite registraciju klikom na link koji je poslan na email";

                    return View("Success");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            var spolovi = new List<SelectListItem>();
            spolovi.AddRange(db.Spol.Select(s => new SelectListItem
            {
                Value = s.SpolID.ToString(),
                Text = s.Naziv
            }).ToList());

            RegistracijaVM m = new RegistracijaVM
            {
                Spolstr = new List<SelectListItem>(spolovi)
            };

            return View("Registracija", m);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PotvrdiEmail(string Id, string token) 
        {
            if (Id == null || token == null) 
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa Id-jem = {Id} ne postoji";
                return View("NePostoji");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                ViewBag.SuccessTitle = "Uspješno potvrđena registracija";
                ViewBag.DugmeZaLogin = "PrikaziDugmeZaLogin";
                return View("Success");
            }

            ViewBag.ErrorTitle = "Nemoguće potvrditi email";
            return View("Success");
        }

        [HttpGet]
        public IActionResult Opcije()
        {
            return View();
        }
    }
}
