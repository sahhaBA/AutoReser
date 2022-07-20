﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Podaci.EF;

namespace Podaci.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220216145555_Povezivanje_Entiteta_Oprema_PaketOpremeOprema_I_PaketOpreme")]
    partial class Povezivanje_Entiteta_Oprema_PaketOpremeOprema_I_PaketOpreme
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Podaci.Entiteti.Automobil", b =>
                {
                    b.Property<int>("AutomobilID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("KategorijaID")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaketOpremeID")
                        .HasColumnType("int");

                    b.Property<int>("PoreznaStopaID")
                        .HasColumnType("int");

                    b.Property<string>("Proizvodjac")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SifraAutomobila")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Slika")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Zaliha")
                        .HasColumnType("int");

                    b.HasKey("AutomobilID");

                    b.HasIndex("KategorijaID");

                    b.HasIndex("PaketOpremeID");

                    b.HasIndex("PoreznaStopaID");

                    b.ToTable("Automobili");
                });

            modelBuilder.Entity("Podaci.Entiteti.Grad", b =>
                {
                    b.Property<int>("GradID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostabskiBroj")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GradID");

                    b.ToTable("Gradovi");
                });

            modelBuilder.Entity("Podaci.Entiteti.Izvjestaj", b =>
                {
                    b.Property<int>("IzvjestajID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UposlenikID")
                        .HasColumnType("int");

                    b.HasKey("IzvjestajID");

                    b.HasIndex("UposlenikID");

                    b.ToTable("Izvjestaji");
                });

            modelBuilder.Entity("Podaci.Entiteti.Karakteristike", b =>
                {
                    b.Property<int>("KarakteristikeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AutomobilID")
                        .HasColumnType("int");

                    b.Property<string>("Boja")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojVrata")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Cijena")
                        .HasColumnType("real");

                    b.Property<int>("Godiste")
                        .HasColumnType("int");

                    b.Property<string>("Gorivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kilometraza")
                        .HasColumnType("int");

                    b.Property<string>("Motor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pogon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Snaga")
                        .HasColumnType("int");

                    b.Property<string>("Stanje")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Svjetla")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Transmisija")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Zapremina")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KarakteristikeID");

                    b.HasIndex("AutomobilID")
                        .IsUnique();

                    b.ToTable("Karakteristike");
                });

            modelBuilder.Entity("Podaci.Entiteti.Kategorija", b =>
                {
                    b.Property<int>("KategorijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategorijaID");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("Podaci.Entiteti.Klijent", b =>
                {
                    b.Property<int>("KlijentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Activan")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DatumRegistracije")
                        .HasColumnType("datetime2");

                    b.Property<int>("OsobaID")
                        .HasColumnType("int");

                    b.HasKey("KlijentID");

                    b.HasIndex("OsobaID")
                        .IsUnique();

                    b.ToTable("Klijenti");
                });

            modelBuilder.Entity("Podaci.Entiteti.KorisnickiNalog", b =>
                {
                    b.Property<int>("KorisnickiNalogID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OsobaID")
                        .HasColumnType("int");

                    b.HasKey("KorisnickiNalogID");

                    b.HasIndex("OsobaID")
                        .IsUnique();

                    b.ToTable("KorisnickiNalog");
                });

            modelBuilder.Entity("Podaci.Entiteti.NacinPlacanja", b =>
                {
                    b.Property<int>("NacinPlacanjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NacinPlacanjaID");

                    b.ToTable("NacinPlacanja");
                });

            modelBuilder.Entity("Podaci.Entiteti.Oprema", b =>
                {
                    b.Property<int>("OpremaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("OpisOpreme")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OpremaID");

                    b.ToTable("Oprema");
                });

            modelBuilder.Entity("Podaci.Entiteti.Osoba", b =>
                {
                    b.Property<int>("OsobaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Adresa")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("GradID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Prezime")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("OsobaID");

                    b.HasIndex("GradID");

                    b.ToTable("Osobe");
                });

            modelBuilder.Entity("Podaci.Entiteti.PaketOpreme", b =>
                {
                    b.Property<int>("PaketOpremeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaketOpremeID");

                    b.ToTable("PaketiOpreme");
                });

            modelBuilder.Entity("Podaci.Entiteti.PaketOpremeOprema", b =>
                {
                    b.Property<int>("OpremaID")
                        .HasColumnType("int");

                    b.Property<int>("PaketOpremeID")
                        .HasColumnType("int");

                    b.HasKey("OpremaID", "PaketOpremeID");

                    b.HasIndex("PaketOpremeID");

                    b.ToTable("PaketOpremeOprema");
                });

            modelBuilder.Entity("Podaci.Entiteti.PoreznaStopa", b =>
                {
                    b.Property<int>("PoreznaStopaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<float>("Iznos")
                        .HasColumnType("real");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PoreznaStopaID");

                    b.ToTable("PorezneStope");
                });

            modelBuilder.Entity("Podaci.Entiteti.Racun", b =>
                {
                    b.Property<int>("RacunID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DatumDospijeca")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumRacuna")
                        .HasColumnType("datetime2");

                    b.Property<int>("NacinPlacanjaID")
                        .HasColumnType("int");

                    b.Property<string>("Napomena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RezervacijaID")
                        .HasColumnType("int");

                    b.HasKey("RacunID");

                    b.HasIndex("NacinPlacanjaID");

                    b.HasIndex("RezervacijaID");

                    b.ToTable("Racuni");
                });

            modelBuilder.Entity("Podaci.Entiteti.RadnoMjesto", b =>
                {
                    b.Property<int>("RadnoMjestoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RadnoMjestoID");

                    b.ToTable("RadnoMjesto");
                });

            modelBuilder.Entity("Podaci.Entiteti.Rezervacija", b =>
                {
                    b.Property<int>("RezervacijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Aktivna")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DatumRezervacije")
                        .HasColumnType("datetime2");

                    b.Property<int>("KlijentID")
                        .HasColumnType("int");

                    b.Property<bool>("Odobrena")
                        .HasColumnType("bit");

                    b.Property<int?>("UposlenikID")
                        .HasColumnType("int");

                    b.Property<bool>("Zavrsena")
                        .HasColumnType("bit");

                    b.HasKey("RezervacijaID");

                    b.HasIndex("KlijentID");

                    b.HasIndex("UposlenikID");

                    b.ToTable("Rezervacije");
                });

            modelBuilder.Entity("Podaci.Entiteti.Spol", b =>
                {
                    b.Property<int>("SpolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpolID");

                    b.ToTable("Spol");
                });

            modelBuilder.Entity("Podaci.Entiteti.StavkeRacuna", b =>
                {
                    b.Property<int>("AutomobilID")
                        .HasColumnType("int");

                    b.Property<int>("RacunID")
                        .HasColumnType("int");

                    b.Property<float>("Cijena")
                        .HasColumnType("real");

                    b.Property<float>("Popust")
                        .HasColumnType("real");

                    b.HasKey("AutomobilID", "RacunID");

                    b.HasIndex("RacunID");

                    b.ToTable("StavkeRacuna");
                });

            modelBuilder.Entity("Podaci.Entiteti.StavkeRezervacije", b =>
                {
                    b.Property<int>("AutomobilID")
                        .HasColumnType("int");

                    b.Property<int>("RezervacijaID")
                        .HasColumnType("int");

                    b.Property<float>("Cijena")
                        .HasColumnType("real");

                    b.Property<float>("Popust")
                        .HasColumnType("real");

                    b.HasKey("AutomobilID", "RezervacijaID");

                    b.HasIndex("RezervacijaID");

                    b.ToTable("StavkeRezervacije");
                });

            modelBuilder.Entity("Podaci.Entiteti.StrucnaSprema", b =>
                {
                    b.Property<int>("StrucnaSpremaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StrucnaSpremaID");

                    b.ToTable("StrucnaSprema");
                });

            modelBuilder.Entity("Podaci.Entiteti.Uposlenik", b =>
                {
                    b.Property<int>("UposlenikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DatumZaposljenja")
                        .HasColumnType("datetime2");

                    b.Property<int>("Iskustvo")
                        .HasColumnType("int");

                    b.Property<string>("JMBG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("MinuliStaz")
                        .HasColumnType("real");

                    b.Property<int>("OsobaID")
                        .HasColumnType("int");

                    b.Property<int>("RadnoMjestoID")
                        .HasColumnType("int");

                    b.Property<int>("SpolID")
                        .HasColumnType("int");

                    b.Property<int>("StrucnaSpremaID")
                        .HasColumnType("int");

                    b.HasKey("UposlenikID");

                    b.HasIndex("OsobaID")
                        .IsUnique();

                    b.HasIndex("RadnoMjestoID");

                    b.HasIndex("SpolID");

                    b.HasIndex("StrucnaSpremaID");

                    b.ToTable("Uposlenici");
                });

            modelBuilder.Entity("Podaci.Entiteti.Automobil", b =>
                {
                    b.HasOne("Podaci.Entiteti.Kategorija", "Kategorija")
                        .WithMany()
                        .HasForeignKey("KategorijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.PaketOpreme", "PaketOpreme")
                        .WithMany()
                        .HasForeignKey("PaketOpremeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.PoreznaStopa", "PoreznaStopa")
                        .WithMany()
                        .HasForeignKey("PoreznaStopaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategorija");

                    b.Navigation("PaketOpreme");

                    b.Navigation("PoreznaStopa");
                });

            modelBuilder.Entity("Podaci.Entiteti.Izvjestaj", b =>
                {
                    b.HasOne("Podaci.Entiteti.Uposlenik", "Uposlenik")
                        .WithMany()
                        .HasForeignKey("UposlenikID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uposlenik");
                });

            modelBuilder.Entity("Podaci.Entiteti.Karakteristike", b =>
                {
                    b.HasOne("Podaci.Entiteti.Automobil", "Automobil")
                        .WithOne("Karakteristike")
                        .HasForeignKey("Podaci.Entiteti.Karakteristike", "AutomobilID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Automobil");
                });

            modelBuilder.Entity("Podaci.Entiteti.Klijent", b =>
                {
                    b.HasOne("Podaci.Entiteti.Osoba", "Osoba")
                        .WithOne("Klijent")
                        .HasForeignKey("Podaci.Entiteti.Klijent", "OsobaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Osoba");
                });

            modelBuilder.Entity("Podaci.Entiteti.KorisnickiNalog", b =>
                {
                    b.HasOne("Podaci.Entiteti.Osoba", "Osoba")
                        .WithOne("KorisnickiNalog")
                        .HasForeignKey("Podaci.Entiteti.KorisnickiNalog", "OsobaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Osoba");
                });

            modelBuilder.Entity("Podaci.Entiteti.Osoba", b =>
                {
                    b.HasOne("Podaci.Entiteti.Grad", "Grad")
                        .WithMany("ListaOsoba")
                        .HasForeignKey("GradID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grad");
                });

            modelBuilder.Entity("Podaci.Entiteti.PaketOpremeOprema", b =>
                {
                    b.HasOne("Podaci.Entiteti.Oprema", "Oprema")
                        .WithMany("PaketOpremeOprema")
                        .HasForeignKey("OpremaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.PaketOpreme", "PaketOpreme")
                        .WithMany("PaketOpremeOprema")
                        .HasForeignKey("PaketOpremeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Oprema");

                    b.Navigation("PaketOpreme");
                });

            modelBuilder.Entity("Podaci.Entiteti.Racun", b =>
                {
                    b.HasOne("Podaci.Entiteti.NacinPlacanja", "NacinPlacanja")
                        .WithMany()
                        .HasForeignKey("NacinPlacanjaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.Rezervacija", "Rezervacija")
                        .WithMany()
                        .HasForeignKey("RezervacijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NacinPlacanja");

                    b.Navigation("Rezervacija");
                });

            modelBuilder.Entity("Podaci.Entiteti.Rezervacija", b =>
                {
                    b.HasOne("Podaci.Entiteti.Klijent", "Klijent")
                        .WithMany()
                        .HasForeignKey("KlijentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.Uposlenik", "Uposlenik")
                        .WithMany()
                        .HasForeignKey("UposlenikID");

                    b.Navigation("Klijent");

                    b.Navigation("Uposlenik");
                });

            modelBuilder.Entity("Podaci.Entiteti.StavkeRacuna", b =>
                {
                    b.HasOne("Podaci.Entiteti.Automobil", "Automobil")
                        .WithMany("StavkeRacuna")
                        .HasForeignKey("AutomobilID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.Racun", "Racun")
                        .WithMany("StavkeRacuna")
                        .HasForeignKey("RacunID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Automobil");

                    b.Navigation("Racun");
                });

            modelBuilder.Entity("Podaci.Entiteti.StavkeRezervacije", b =>
                {
                    b.HasOne("Podaci.Entiteti.Automobil", "Automobil")
                        .WithMany("StavkeRezervacije")
                        .HasForeignKey("AutomobilID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.Rezervacija", "Rezervacija")
                        .WithMany("StavkeRezervacije")
                        .HasForeignKey("RezervacijaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Automobil");

                    b.Navigation("Rezervacija");
                });

            modelBuilder.Entity("Podaci.Entiteti.Uposlenik", b =>
                {
                    b.HasOne("Podaci.Entiteti.Osoba", "Osoba")
                        .WithOne("Uposlenik")
                        .HasForeignKey("Podaci.Entiteti.Uposlenik", "OsobaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.RadnoMjesto", "RadnoMjesto")
                        .WithMany()
                        .HasForeignKey("RadnoMjestoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.Spol", "Spol")
                        .WithMany()
                        .HasForeignKey("SpolID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podaci.Entiteti.StrucnaSprema", "StrurucnaSprema")
                        .WithMany()
                        .HasForeignKey("StrucnaSpremaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Osoba");

                    b.Navigation("RadnoMjesto");

                    b.Navigation("Spol");

                    b.Navigation("StrurucnaSprema");
                });

            modelBuilder.Entity("Podaci.Entiteti.Automobil", b =>
                {
                    b.Navigation("Karakteristike");

                    b.Navigation("StavkeRacuna");

                    b.Navigation("StavkeRezervacije");
                });

            modelBuilder.Entity("Podaci.Entiteti.Grad", b =>
                {
                    b.Navigation("ListaOsoba");
                });

            modelBuilder.Entity("Podaci.Entiteti.Oprema", b =>
                {
                    b.Navigation("PaketOpremeOprema");
                });

            modelBuilder.Entity("Podaci.Entiteti.Osoba", b =>
                {
                    b.Navigation("Klijent");

                    b.Navigation("KorisnickiNalog");

                    b.Navigation("Uposlenik");
                });

            modelBuilder.Entity("Podaci.Entiteti.PaketOpreme", b =>
                {
                    b.Navigation("PaketOpremeOprema");
                });

            modelBuilder.Entity("Podaci.Entiteti.Racun", b =>
                {
                    b.Navigation("StavkeRacuna");
                });

            modelBuilder.Entity("Podaci.Entiteti.Rezervacija", b =>
                {
                    b.Navigation("StavkeRezervacije");
                });
#pragma warning restore 612, 618
        }
    }
}
