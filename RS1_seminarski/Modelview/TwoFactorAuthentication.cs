using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class TwoFactorAuthentication
    {
        [Required(ErrorMessage = "Obavezno polje")]
        public string Kod { get; set; }
        public string Email { get; set; }
        public string BarcodeImageUrl { get; set; }
        public string SetupCode { get; set; }
        public bool AktiviranTFA { get; set; }
    }
}
