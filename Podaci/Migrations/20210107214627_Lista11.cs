using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class Lista11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    GradID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostabskiBroj = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.GradID);
                });

            migrationBuilder.CreateTable(
                name: "Kategorije",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorije", x => x.KategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "NacinPlacanja",
                columns: table => new
                {
                    NacinPlacanjaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPlacanja", x => x.NacinPlacanjaID);
                });

            migrationBuilder.CreateTable(
                name: "PaketiOpreme",
                columns: table => new
                {
                    PaketOpremeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaketiOpreme", x => x.PaketOpremeID);
                });

            migrationBuilder.CreateTable(
                name: "PorezneStope",
                columns: table => new
                {
                    PoreznaStopaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iznos = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PorezneStope", x => x.PoreznaStopaID);
                });

            migrationBuilder.CreateTable(
                name: "RadnoMjesto",
                columns: table => new
                {
                    RadnoMjestoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadnoMjesto", x => x.RadnoMjestoID);
                });

            migrationBuilder.CreateTable(
                name: "Spol",
                columns: table => new
                {
                    SpolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spol", x => x.SpolID);
                });

            migrationBuilder.CreateTable(
                name: "StrurucnaSprema",
                columns: table => new
                {
                    StrucnaSpremaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrurucnaSprema", x => x.StrucnaSpremaID);
                });

            migrationBuilder.CreateTable(
                name: "Osoba",
                columns: table => new
                {
                    OsobaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osoba", x => x.OsobaID);
                    table.ForeignKey(
                        name: "FK_Osoba_Gradovi_GradID",
                        column: x => x.GradID,
                        principalTable: "Gradovi",
                        principalColumn: "GradID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oprema",
                columns: table => new
                {
                    OpremaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpisOpreme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaketOpremeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oprema", x => x.OpremaID);
                    table.ForeignKey(
                        name: "FK_Oprema_PaketiOpreme_PaketOpremeID",
                        column: x => x.PaketOpremeID,
                        principalTable: "PaketiOpreme",
                        principalColumn: "PaketOpremeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Automobili",
                columns: table => new
                {
                    AutomobilID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Proizvodjac = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SifraAutomobila = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zaliha = table.Column<int>(type: "int", nullable: false),
                    KategorijaID = table.Column<int>(type: "int", nullable: false),
                    PaketOpremeID = table.Column<int>(type: "int", nullable: false),
                    PoreznaStopaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobili", x => x.AutomobilID);
                    table.ForeignKey(
                        name: "FK_Automobili_Kategorije_KategorijaID",
                        column: x => x.KategorijaID,
                        principalTable: "Kategorije",
                        principalColumn: "KategorijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Automobili_PaketiOpreme_PaketOpremeID",
                        column: x => x.PaketOpremeID,
                        principalTable: "PaketiOpreme",
                        principalColumn: "PaketOpremeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Automobili_PorezneStope_PoreznaStopaID",
                        column: x => x.PoreznaStopaID,
                        principalTable: "PorezneStope",
                        principalColumn: "PoreznaStopaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Klijenti",
                columns: table => new
                {
                    KlijentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumRegistracije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activan = table.Column<bool>(type: "bit", nullable: false),
                    OsobaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijenti", x => x.KlijentID);
                    table.ForeignKey(
                        name: "FK_Klijenti_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisnickiNalog",
                columns: table => new
                {
                    KorisnickiNalogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OsobaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnickiNalog", x => x.KorisnickiNalogID);
                    table.ForeignKey(
                        name: "FK_KorisnickiNalog_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uposlenici",
                columns: table => new
                {
                    UposlenikID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumZaposljenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Iskustvo = table.Column<int>(type: "int", nullable: false),
                    MinuliStaz = table.Column<float>(type: "real", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OsobaID = table.Column<int>(type: "int", nullable: false),
                    SpolID = table.Column<int>(type: "int", nullable: false),
                    StrucnaSpremaID = table.Column<int>(type: "int", nullable: false),
                    StrurucnaSpremaStrucnaSpremaID = table.Column<int>(type: "int", nullable: true),
                    RadnoMjestoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uposlenici", x => x.UposlenikID);
                    table.ForeignKey(
                        name: "FK_Uposlenici_Osoba_OsobaID",
                        column: x => x.OsobaID,
                        principalTable: "Osoba",
                        principalColumn: "OsobaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uposlenici_RadnoMjesto_RadnoMjestoID",
                        column: x => x.RadnoMjestoID,
                        principalTable: "RadnoMjesto",
                        principalColumn: "RadnoMjestoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uposlenici_Spol_SpolID",
                        column: x => x.SpolID,
                        principalTable: "Spol",
                        principalColumn: "SpolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uposlenici_StrurucnaSprema_StrurucnaSpremaStrucnaSpremaID",
                        column: x => x.StrurucnaSpremaStrucnaSpremaID,
                        principalTable: "StrurucnaSprema",
                        principalColumn: "StrucnaSpremaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Karakteristike",
                columns: table => new
                {
                    KarakteristikeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stanje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    Kilometraza = table.Column<int>(type: "int", nullable: false),
                    Snaga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zapremina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gorivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojVrata = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pogon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transmisija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Boja = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Svjetla = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutomobilID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Karakteristike", x => x.KarakteristikeID);
                    table.ForeignKey(
                        name: "FK_Karakteristike_Automobili_AutomobilID",
                        column: x => x.AutomobilID,
                        principalTable: "Automobili",
                        principalColumn: "AutomobilID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Izvjestaji",
                columns: table => new
                {
                    IzvjestajID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UposlenikID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izvjestaji", x => x.IzvjestajID);
                    table.ForeignKey(
                        name: "FK_Izvjestaji_Uposlenici_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Uposlenici",
                        principalColumn: "UposlenikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacije",
                columns: table => new
                {
                    RezervacijaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumRezervacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Aktivna = table.Column<bool>(type: "bit", nullable: false),
                    Odobrena = table.Column<bool>(type: "bit", nullable: false),
                    Zavrsena = table.Column<bool>(type: "bit", nullable: false),
                    KlijentID = table.Column<int>(type: "int", nullable: false),
                    UposlenikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacije", x => x.RezervacijaID);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Klijenti_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijenti",
                        principalColumn: "KlijentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rezervacije_Uposlenici_UposlenikID",
                        column: x => x.UposlenikID,
                        principalTable: "Uposlenici",
                        principalColumn: "UposlenikID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    RacunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumRacuna = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumDospijeca = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NacinPlacanjaID = table.Column<int>(type: "int", nullable: false),
                    RezervacijaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.RacunID);
                    table.ForeignKey(
                        name: "FK_Racuni_NacinPlacanja_NacinPlacanjaID",
                        column: x => x.NacinPlacanjaID,
                        principalTable: "NacinPlacanja",
                        principalColumn: "NacinPlacanjaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Racuni_Rezervacije_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacije",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkeRezervacije",
                columns: table => new
                {
                    AutomobilID = table.Column<int>(type: "int", nullable: false),
                    RezervacijaID = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    Popust = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeRezervacije", x => new { x.AutomobilID, x.RezervacijaID });
                    table.ForeignKey(
                        name: "FK_StavkeRezervacije_Automobili_AutomobilID",
                        column: x => x.AutomobilID,
                        principalTable: "Automobili",
                        principalColumn: "AutomobilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeRezervacije_Rezervacije_RezervacijaID",
                        column: x => x.RezervacijaID,
                        principalTable: "Rezervacije",
                        principalColumn: "RezervacijaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StavkeRacuna",
                columns: table => new
                {
                    RacunID = table.Column<int>(type: "int", nullable: false),
                    AutomobilID = table.Column<int>(type: "int", nullable: false),
                    Cijena = table.Column<float>(type: "real", nullable: false),
                    Popust = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeRacuna", x => new { x.AutomobilID, x.RacunID });
                    table.ForeignKey(
                        name: "FK_StavkeRacuna_Automobili_AutomobilID",
                        column: x => x.AutomobilID,
                        principalTable: "Automobili",
                        principalColumn: "AutomobilID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StavkeRacuna_Racuni_RacunID",
                        column: x => x.RacunID,
                        principalTable: "Racuni",
                        principalColumn: "RacunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_KategorijaID",
                table: "Automobili",
                column: "KategorijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_PaketOpremeID",
                table: "Automobili",
                column: "PaketOpremeID");

            migrationBuilder.CreateIndex(
                name: "IX_Automobili_PoreznaStopaID",
                table: "Automobili",
                column: "PoreznaStopaID");

            migrationBuilder.CreateIndex(
                name: "IX_Izvjestaji_UposlenikID",
                table: "Izvjestaji",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_Karakteristike_AutomobilID",
                table: "Karakteristike",
                column: "AutomobilID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Klijenti_OsobaID",
                table: "Klijenti",
                column: "OsobaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KorisnickiNalog_OsobaID",
                table: "KorisnickiNalog",
                column: "OsobaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Oprema_PaketOpremeID",
                table: "Oprema",
                column: "PaketOpremeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Osoba_GradID",
                table: "Osoba",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_NacinPlacanjaID",
                table: "Racuni",
                column: "NacinPlacanjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_RezervacijaID",
                table: "Racuni",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_KlijentID",
                table: "Rezervacije",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacije_UposlenikID",
                table: "Rezervacije",
                column: "UposlenikID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeRacuna_RacunID",
                table: "StavkeRacuna",
                column: "RacunID");

            migrationBuilder.CreateIndex(
                name: "IX_StavkeRezervacije_RezervacijaID",
                table: "StavkeRezervacije",
                column: "RezervacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_OsobaID",
                table: "Uposlenici",
                column: "OsobaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_RadnoMjestoID",
                table: "Uposlenici",
                column: "RadnoMjestoID");

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_SpolID",
                table: "Uposlenici",
                column: "SpolID");

            migrationBuilder.CreateIndex(
                name: "IX_Uposlenici_StrurucnaSpremaStrucnaSpremaID",
                table: "Uposlenici",
                column: "StrurucnaSpremaStrucnaSpremaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Izvjestaji");

            migrationBuilder.DropTable(
                name: "Karakteristike");

            migrationBuilder.DropTable(
                name: "KorisnickiNalog");

            migrationBuilder.DropTable(
                name: "Oprema");

            migrationBuilder.DropTable(
                name: "StavkeRacuna");

            migrationBuilder.DropTable(
                name: "StavkeRezervacije");

            migrationBuilder.DropTable(
                name: "Racuni");

            migrationBuilder.DropTable(
                name: "Automobili");

            migrationBuilder.DropTable(
                name: "NacinPlacanja");

            migrationBuilder.DropTable(
                name: "Rezervacije");

            migrationBuilder.DropTable(
                name: "Kategorije");

            migrationBuilder.DropTable(
                name: "PaketiOpreme");

            migrationBuilder.DropTable(
                name: "PorezneStope");

            migrationBuilder.DropTable(
                name: "Klijenti");

            migrationBuilder.DropTable(
                name: "Uposlenici");

            migrationBuilder.DropTable(
                name: "Osoba");

            migrationBuilder.DropTable(
                name: "RadnoMjesto");

            migrationBuilder.DropTable(
                name: "Spol");

            migrationBuilder.DropTable(
                name: "StrurucnaSprema");

            migrationBuilder.DropTable(
                name: "Gradovi");
        }
    }
}
