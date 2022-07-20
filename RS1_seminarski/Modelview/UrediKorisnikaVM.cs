using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podaci.Entiteti;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class UrediKorisnikaVM : Osoba
    {
        public UrediKorisnikaVM()
        {
            Claims = new List<string>();
            Uloge = new List<string>();
        }

        public string Id { get; set; }
        public string DatumReg { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Uloge { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(100, ErrorMessage = "Korisnicko ime mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(100, ErrorMessage = "Email mora sadrzavati minimalno 15 karaktera", MinimumLength = 15)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Neispravna email adresa")]
        [Remote(action: "MailZauzet", controller: "Account")]
        public string Email { get; set; }
        public string SpolstrID { get; set; }
        public List<SelectListItem> Spolstr { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(100, ErrorMessage = "Grad mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string GradNaziv { get; set; }

        [StringLength(100, ErrorMessage = "Postanski broj mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string PostanskiBroj { get; set; }


        public string DatumZaposlenja { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Iskustvo mora biti brojčana vrijednost")]
        public string Iskustvo { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Staž mora biti brojčana vrijednost")]
        public string MinuliStaz { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "JMBG mora biti brojčana vrijednost")]
        public string JMBG { get; set; }
        public string StrucnaSpremaID { get; set; }
        public List<SelectListItem> StrucnaSprema { get; set; }
        [StringLength(100, ErrorMessage = "Stručna sprema mora sadržavati minimalno 3 karaktera", MinimumLength = 3)]
        public string NovaStrucnaSprema { get; set; }

        public string RadnoMjestoID { get; set; }
        public List<SelectListItem> RadnoMjesto { get; set; }
        [StringLength(100, ErrorMessage = "Radno mjesto mora sadržavati minimalno 3 karaktera", MinimumLength = 3)]
        public string NovoRadnoMjesto { get; set; }
    }
}
