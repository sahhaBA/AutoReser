using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class LoginWM
    {
        [Required(ErrorMessage = "Obavezno polje")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Neispravna email adresa")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [DataType(DataType.Password)]
        public string Lozinka { get; set; }

        [Display(Name = "Zapamti lozinku")]
        public bool ZapamtiLozinku { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
