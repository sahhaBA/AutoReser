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
    public class RegistracijaVM : Osoba
    {
        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(100, ErrorMessage = "Korisnicko ime mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string KorisnickoIme { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(100, ErrorMessage = "Email mora sadrzavati minimalno 15 karaktera", MinimumLength = 15)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Nepravilna email adresa")]
        [Remote(action: "MailZauzet", controller:"Account")]
        public string Email { get; set; }
        public string SpolstrID { get; set; }
        public List<SelectListItem> Spolstr { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(100, ErrorMessage = "Grad mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string GradNaziv { get; set; }

        [StringLength(100, ErrorMessage = "Postanski broj mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string PostanskiBroj { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("Lozinka", ErrorMessage = "Lozinke se ne podudaraju")]
        public string PotvrdiLozinku { get; set; }
    }
}
