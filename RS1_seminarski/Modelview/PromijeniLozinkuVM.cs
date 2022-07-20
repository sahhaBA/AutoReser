using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class PromijeniLozinkuVM
    {
        [Required(ErrorMessage = "Obavezno polje")]
        [DataType(DataType.Password)]
        [Display(Name = "Trenutna lozinka")]
        public string TrenutnaLozinka { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova lozinka")]
        public string NovaLozinka { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [DataType(DataType.Password)]
        [Display(Name = "Potvrdi lozinku")]
        [Compare("NovaLozinka", ErrorMessage = "Lozinke se ne podudaraju")]
        public string PotvrdiLozinku { get; set; }
    }
}
