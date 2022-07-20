using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Podaci.Entiteti;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class UrediAutomobilVM
    {
        [StringLength(100, ErrorMessage = "Proizvodjac mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string Proizvodjac { get; set; }

        [StringLength(100, ErrorMessage = "Model mora sadrzavati minimalno 2 karaktera", MinimumLength = 2)]
        public string Model { get; set; }

        public string SifraAutomobila { get; set; }


        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Zaliha mora biti brojčana vrijednost")]
        public string Zaliha { get; set; }

        public string KategorijaID { get; set; }
        public List<SelectListItem> Kategorija { get; set; }
        [StringLength(100, ErrorMessage = "Kategorija mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
        public string NovaKategorija { get; set; }
        public string PaketOpremeID { get; set; }
        public List<SelectListItem> PaketOpreme { get; set; }
        public int OpremaID { get; set; }
        public List<SelectListItem> Oprema { get; set; }
        public string NovaOprema { get; set; }
        public string PoreznaStopaID { get; set; }
        public List<SelectListItem> PoreznaStopa { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Porezna stopa mora biti brojčana vrijednost")]
        public string NovaPoreznaStopa { get; set; }
        public string Slika1string { get; set; }
        public string Slika2string { get; set; }
        public string Slika3string { get; set; }
        public string Slika4string { get; set; }
        public string Slika5string { get; set; }
        public string UposlenikID { get; set; }
        public Uposlenik Uposlenik { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatumDodavanja { get; set; }
        public Karakteristiks Karakteristike { get; set; }
        public class Karakteristiks
        {
            public string StanjeID { get; set; }
            public List<SelectListItem> Stanje { get; set; }
            public string GodisteID { get; set; }
            public List<SelectListItem> Godiste { get; set; }

            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Cijena mora biti brojčana vrijednost")]
            public string Cijena { get; set; }

            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Kilometraza mora biti brojčana vrijednost")]
            public string Kilometraza { get; set; }

            public string Motor { get; set; }

            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Snaga mora biti brojčana vrijednost")]
            public string Snaga { get; set; }

            public string Zapremina { get; set; }
            public string GorivoID { get; set; }
            public List<SelectListItem> Gorivo { get; set; }
            public string BrojVrataID { get; set; }
            public List<SelectListItem> BrojVrata { get; set; }

            [StringLength(100, ErrorMessage = "Pogon mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
            public string Pogon { get; set; }

            public string TransmisijaID { get; set; }
            public List<SelectListItem> Transmisija { get; set; }

            [StringLength(100, ErrorMessage = "Boja mora sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
            public string Boja { get; set; }

            [StringLength(100, ErrorMessage = "Svjetla moraju sadrzavati minimalno 3 karaktera", MinimumLength = 3)]
            public string Svjetla { get; set; }
            public int AutomobilID { get; set; }
        }
    }
}
