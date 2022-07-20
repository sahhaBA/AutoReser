using Microsoft.AspNetCore.Mvc.Rendering;
using Podaci.Entiteti;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class FilterVM
    {
        public string ProizvodjacID { get; set; }
        public List<SelectListItem> Proizvodjac { get; set; }
        public string ModelID { get; set; }
        public List<SelectListItem> Model { get; set; }
        public int GodisteODID { get; set; }
        public List<SelectListItem> GodisteOD { get; set; }
        public int GodisteDOID { get; set; }
        public List<SelectListItem> GodisteDO { get; set; }
        public int CijenaODID { get; set; }
        public List<SelectListItem> CijenaOD { get; set; }
        public int CijenaDOID { get; set; }
        public List<SelectListItem> CijenaDO { get; set; }
        public int KilometrazaODID { get; set; }
        public List<SelectListItem> KilometrazaOD { get; set; }
        public int KilometrazaDOID { get; set; }
        public List<SelectListItem> KilometrazaDO { get; set; }
        public int SnagaODID { get; set; }
        public List<SelectListItem> SnagaOD { get; set; }
        public int SnagaDOID { get; set; }
        public List<SelectListItem> SnagaDO { get; set; }
        public string GorivoID { get; set; }
        public List<SelectListItem> Gorivo { get; set; }
        public string BrojVrataID { get; set; }
        public List<SelectListItem> BrojVrata { get; set; }
        public string PogonID { get; set; }
        public List<SelectListItem> Pogon { get; set; }
        public string TransmisijaID { get; set; }
        public List<SelectListItem> Transmisija { get; set; }
        public string BojaID { get; set; }
        public List<SelectListItem> Boja { get; set; }
        public string SvjetlaID { get; set; }
        public List<SelectListItem> Svjetla { get; set; }
        public string StanjeID { get; set; }
        public List<SelectListItem> Stanje { get; set; }
        public List<Vozilo> Vozila { get; set; }
        public class Vozilo
        {
            public int AutomobilID { get; set; }
            public string Proizvodjac { get; set; }
            public string Model { get; set; }
            public string Slika { get; set; }
            public bool Obrisan { get; set; }
            public Karakteristike Karakteristike { get; set; }
        }
    }
}
