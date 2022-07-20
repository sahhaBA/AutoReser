using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class DodajLozinkuVM
    {
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
