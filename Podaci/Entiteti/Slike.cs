using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Podaci.Entiteti
{
    public class Slike
    {
        [Key]
        public int SlikeID { get; set; }
        public int AutomobilID { get; set; }
        public Automobil Automobil { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Naziv slike")]
        public string NazivSlike1 { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        public IFormFile FajlSlike1 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Naziv slike")]
        public string NazivSlike2 { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        public IFormFile FajlSlike2 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Naziv slike")]
        public string NazivSlike3 { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        public IFormFile FajlSlike3 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Naziv slike")]
        public string NazivSlike4 { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        public IFormFile FajlSlike4 { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Naziv slike")]
        public string NazivSlike5 { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        public IFormFile FajlSlike5 { get; set; }
    }
}
