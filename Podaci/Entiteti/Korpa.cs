namespace Podaci.Entiteti
{
    public class Korpa
    {
        public string KorisnickiNalogID { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public int AutomobilID { get; set; }
        public Automobil Automobil { get; set; }
    }
}
