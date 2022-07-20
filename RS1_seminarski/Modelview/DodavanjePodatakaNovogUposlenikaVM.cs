using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class DodavanjePodatakaNovogUposlenikaVM
    {
        public string Id { get; set; }
        public string noviUposlenik { get; set; }

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
