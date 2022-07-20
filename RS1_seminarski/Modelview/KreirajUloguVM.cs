using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class KreirajUloguVM
    {
        [Required(ErrorMessage = "Obavezno polje")]
        public string NazivUloge { get; set; }
    }
}
