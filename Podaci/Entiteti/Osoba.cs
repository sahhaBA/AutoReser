using System.ComponentModel.DataAnnotations;

namespace Podaci.Entiteti
{
   public class Osoba
    {
        public int OsobaID { get; set;  }

        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(100, ErrorMessage = "Ime mora sadržavati minimalno 3 karaktera", MinimumLength = 3)]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(100, ErrorMessage = "Prezime mora sadržavati minimalno 3 karaktera", MinimumLength = 3)]
        public string Prezime { get; set; }

        [StringLength(100, ErrorMessage = "Adresa mora sadržavati minimalno 3 karaktera", MinimumLength = 3)]
        public string Adresa { get; set; }
        public int ? SpolID { get; set; }
        public Spol Spol { get; set; }
      
        public int ? GradID { get; set; }
        public Grad Grad  { get; set; }

        public Klijent Klijent { get; set; }
        public Uposlenik Uposlenik { get; set; }

        public KorisnickiNalog KorisnickiNalog { get; set; }
    }
}
