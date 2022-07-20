namespace Podaci.Entiteti
{
   public class StavkeRacuna
    {
        public float Cijena { get; set; }
        public float Popust  { get; set; }

        public int RacunID { get; set; }
        public Racun Racun  { get; set; }

        public int AutomobilID { get; set; }
        public Automobil Automobil  { get; set; }
    }
}
