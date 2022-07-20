using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class ZaboravljenaLozinkaVM
    {
        [Required(ErrorMessage = "Obavezno polje")]
        [EmailAddress(ErrorMessage = "Nepravilna email adresa")]
        public string Email { get; set; }
    }
}
