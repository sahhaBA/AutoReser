using Microsoft.EntityFrameworkCore;
using Podaci.Entiteti;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Podaci.EF
{
     public class MyContext: IdentityDbContext
     {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=app.fit.ba,1431;Database=RS1_sem_1;trusted_connection=false;User ID=p2076;Password=23gghX!;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                    .ToTable("KorisnickiNalog", "dbo").Property(p => p.Id).HasColumnName("KorisnickiNalogID");
            modelBuilder.Entity<KorisnickiNalog>()
                    .ToTable("KorisnickiNalog", "dbo").Property(p => p.Id).HasColumnName("KorisnickiNalogID");
            modelBuilder.Entity<IdentityUser>()
                    .ToTable("KorisnickiNalog", "dbo").Property(p => p.UserName).HasColumnName("KorisnickoIme");
            modelBuilder.Entity<IdentityUser>()
                    .ToTable("KorisnickiNalog", "dbo").Property(p => p.Email).HasColumnName("Email");

            modelBuilder.Entity<Osoba>()
                .HasOne<KorisnickiNalog>(s => s.KorisnickiNalog)
                .WithOne(ad => ad.Osoba)
                .HasForeignKey<KorisnickiNalog>(ad => ad.OsobaID);

            modelBuilder.Entity<Osoba>()
                .HasOne<Klijent>(s => s.Klijent)
                .WithOne(ad => ad.Osoba)
                .HasForeignKey<Klijent>(ad => ad.OsobaID);

            modelBuilder.Entity<Osoba>()
               .HasOne<Uposlenik>(s => s.Uposlenik)
               .WithOne(ad => ad.Osoba)
               .HasForeignKey<Uposlenik>(ad => ad.OsobaID);

            modelBuilder.Entity<Osoba>()
           .HasOne<Grad>(s => s.Grad)
           .WithMany(g => g.ListaOsoba)
           .HasForeignKey(s => s.GradID);

            modelBuilder.Entity<Automobil>()
                .HasOne<Karakteristike>(s => s.Karakteristike)
                .WithOne(ad => ad.Automobil)
                .HasForeignKey<Karakteristike>(ad => ad.AutomobilID);

            modelBuilder.Entity<Korpa>().HasKey(st => new { st.KorisnickiNalogID, st.AutomobilID });
            //modelBuilder.Entity<Korpa>().HasNoKey();
            modelBuilder.Entity<PaketOpremeOprema>().HasKey(st => new { st.OpremaID, st.PaketOpremeID });
            modelBuilder.Entity<StavkeRezervacije>().HasKey(st => new { st.AutomobilID, st.RezervacijaID });
            modelBuilder.Entity<StavkeRacuna>().HasKey(st => new { st.AutomobilID, st.RacunID });
        }

        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Uposlenik> Uposlenici { get; set; }
        public DbSet<Klijent> Klijenti { get; set; }
        public DbSet<Spol> Spol { get; set; }
        public DbSet<RadnoMjesto> RadnoMjesto { get; set; }
        public DbSet<Izvjestaj> Izvjestaji { get; set; }
        public DbSet<StrucnaSprema> StrucnaSprema { get; set; }
        public DbSet<Rezervacija> Rezervacije { get; set; }
        public DbSet<NacinPlacanja> NacinPlacanja { get; set; }
        public DbSet<StavkeRezervacije> StavkeRezervacije { get; set; }
        public DbSet<Racun> Racuni { get; set; }
        public DbSet<Automobil> Automobili { get; set; }
        public DbSet<StavkeRacuna> StavkeRacuna { get; set; }
        public DbSet<PaketOpreme> PaketiOpreme { get; set; }
        public DbSet<PaketOpremeOprema> PaketOpremeOprema { get; set; }
        public DbSet<Oprema> Oprema { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<PoreznaStopa> PorezneStope { get; set; }
        public DbSet<Karakteristike> Karakteristike { get; set; }
        public DbSet<Osoba> Osobe { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<Slike> Slike { get; set; }
     }

}

