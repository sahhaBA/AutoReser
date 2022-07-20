using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class DodajOpremuVM
    {
        public int AutomobilID { get; set; }
        public int PaketOpremeID { get; set; }
        public int OpremaID { get; set; }
        [StringLength(100, ErrorMessage = "Naziv opreme mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]

        [Required(ErrorMessage = "Obavezno polje")]
        public string nazivOpreme { get; set; }
        public string Id { get; set; } //korisnikov id
    }
}
