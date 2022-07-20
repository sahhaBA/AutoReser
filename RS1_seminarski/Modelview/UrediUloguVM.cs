using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class UrediUloguVM
    {
        public UrediUloguVM()
        {
            KorisnickiNalozi = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        public string NazivUloge { get; set; }

        public List<string> KorisnickiNalozi { get; set; }
    }
}
