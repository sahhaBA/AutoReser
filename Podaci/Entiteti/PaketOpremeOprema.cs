namespace Podaci.Entiteti
{
    public class PaketOpremeOprema
    {
        public int OpremaID { get; set; }
        public Oprema Oprema { get; set; }
        public int PaketOpremeID { get; set; }
        public PaketOpreme PaketOpreme { get; set; }
    }
}
