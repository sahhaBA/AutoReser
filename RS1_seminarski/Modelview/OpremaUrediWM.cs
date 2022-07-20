using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class OpremaUrediWM
    {
        public int OpremaID { get; set; }
        public string OpisOpreme { get; set; }

        public  int  PaketOpremeID { get; set; }
        public List<SelectListItem> PaketOpreme { get; set; }
    }
}
