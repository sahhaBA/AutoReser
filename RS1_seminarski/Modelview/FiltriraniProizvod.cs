using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Podaci.EF;
using Podaci.Entiteti;
using RS1_seminarski.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS1_seminarski.Modelview
{
    public class FiltriraniProizvod
    {
        private MyContext db = new MyContext();
        public IQueryable<Automobil> DohvatiPodatke(FilterVM searchModel)
        {
            IQueryable<Automobil> auta = db.Automobili.AsQueryable();
            if (searchModel != null)
            {
                auta = db.Automobili
                    .Where(x => ((searchModel.StanjeID == "---") && (x.Karakteristike.Stanje.Contains(""))
                               || (x.Karakteristike.Stanje.Contains(searchModel.StanjeID)))
                               && (((string.Compare(searchModel.ProizvodjacID, "---") == 0) && (x.Proizvodjac.Contains("")))
                               || (x.Proizvodjac == searchModel.ProizvodjacID))
                               && (((string.IsNullOrEmpty(searchModel.ModelID)) && (x.Model.Contains("")))
                               || ((!string.IsNullOrEmpty(searchModel.ModelID)) && (x.Model == searchModel.ModelID)))
                               && (((searchModel.GodisteODID == 0) && (x.Karakteristike.Godiste > 0))
                               || ((searchModel.GodisteODID != 0) && (x.Karakteristike.Godiste >= searchModel.GodisteODID)))
                               && (((searchModel.GodisteDOID == 0) && (x.Karakteristike.Godiste > 0))
                               || ((searchModel.GodisteDOID != 0) && (x.Karakteristike.Godiste <= searchModel.GodisteDOID)))
                               && (((searchModel.KilometrazaODID == 0) && (x.Karakteristike.Kilometraza >= 0))
                               || ((searchModel.KilometrazaODID != 0) && (x.Karakteristike.Kilometraza >= searchModel.KilometrazaODID)))
                               && (((searchModel.KilometrazaDOID == 0) && (x.Karakteristike.Kilometraza >= 0))
                               || ((searchModel.KilometrazaDOID != 0) && (x.Karakteristike.Kilometraza <= searchModel.KilometrazaDOID)))
                               && (((searchModel.CijenaODID == 0) && (x.Karakteristike.Cijena > 0))
                               || ((searchModel.CijenaODID != 0) && (x.Karakteristike.Cijena >= searchModel.CijenaODID)))
                               && (((searchModel.CijenaDOID == 0) && (x.Karakteristike.Cijena > 0))
                               || ((searchModel.CijenaDOID != 0) && (x.Karakteristike.Cijena <= searchModel.CijenaDOID)))
                               && ((searchModel.BojaID == "---") && (x.Karakteristike.Boja.Contains(""))
                               || (x.Karakteristike.Boja.Contains(searchModel.BojaID)))
                               && (((string.Compare(searchModel.BrojVrataID, "---") == 0) && (x.Karakteristike.BrojVrata.Contains("")))
                               || (x.Karakteristike.BrojVrata == searchModel.BrojVrataID))
                               && (((string.Compare(searchModel.GorivoID, "---") == 0) && (x.Karakteristike.Gorivo.Contains("")))
                               || ((string.Compare(searchModel.GorivoID, "---") != 0) && (x.Karakteristike.Gorivo == searchModel.GorivoID)))
                               && (((string.Compare(searchModel.PogonID, "---") == 0) && (x.Karakteristike.Pogon.Contains("")))
                               || ((string.Compare(searchModel.PogonID, "---") != 0) && (x.Karakteristike.Pogon == searchModel.PogonID)))
                               && (((searchModel.SnagaODID == 0) && (x.Karakteristike.Snaga > 0))
                               || ((searchModel.SnagaODID != 0) && (x.Karakteristike.Snaga >= searchModel.SnagaODID)))
                               && (((searchModel.SnagaDOID == 0) && (x.Karakteristike.Snaga > 0))
                               || ((searchModel.SnagaDOID != 0) && (x.Karakteristike.Snaga <= searchModel.SnagaDOID)))
                               && (((string.Compare(searchModel.SvjetlaID, "---") == 0) && (x.Karakteristike.Svjetla.Contains("")))
                               || ((string.Compare(searchModel.SvjetlaID, "---") != 0) && (x.Karakteristike.Svjetla == searchModel.SvjetlaID)))
                               && (((string.Compare(searchModel.TransmisijaID, "---") == 0) && (x.Karakteristike.Transmisija.Contains("")))
                               || ((string.Compare(searchModel.TransmisijaID, "---") != 0) && (x.Karakteristike.Transmisija == searchModel.TransmisijaID))));
            }
            return auta;
        }
    }
}
