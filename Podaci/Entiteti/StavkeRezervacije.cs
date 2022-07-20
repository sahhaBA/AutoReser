namespace Podaci.Entiteti
{
   public class StavkeRezervacije
    {
        public float Cijena { get; set; }
        public float Popust  { get; set; }

        public int AutomobilID { get; set; }
        public Automobil Automobil { get; set; }


        public int RezervacijaID  { get; set; }
        public Rezervacija Rezervacija { get; set; }
    }
}
