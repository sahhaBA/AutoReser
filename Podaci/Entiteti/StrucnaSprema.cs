using System.ComponentModel.DataAnnotations;

namespace Podaci.Entiteti
{
   public class StrucnaSprema
    {
        [Key]
        public int StrucnaSpremaID { get; set; }
        public string Naziv { get; set; }
    }
}
