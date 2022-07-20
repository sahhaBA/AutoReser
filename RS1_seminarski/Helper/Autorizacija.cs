using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Podaci.EF;
using Podaci.Entiteti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RS1_seminarski.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool klijent, bool uposlenik)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { klijent, uposlenik };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool klijent, bool uposlenik)
        {
            _klijent = klijent;
            _uposlenik = uposlenik;
        }
        private readonly bool _klijent;
        private readonly bool _uposlenik;
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            KorisnickiNalog k = filterContext.HttpContext.GetLogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { @area = "" });
                return;
            }

            //Preuzimamo DbContext preko app services
            // MyContext db = filterContext.HttpContext.RequestServices.GetService<MyContext>();

            MyContext db = new MyContext();

            //Klijenti  mogu pristupiti uposlenici ne
            if (_klijent && db.Klijenti.Any(s => s.OsobaID == k.OsobaID))
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            // Uposlenici mogu pristupiti Klijenti ne 
            if (_uposlenik && db.Uposlenici.Any(s => s.OsobaID == k.OsobaID))
            {
                await next();//ok - ima pravo pristupa
                return;
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.ViewData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}

