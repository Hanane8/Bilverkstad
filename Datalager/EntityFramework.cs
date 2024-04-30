using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bilverkstad1;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;

namespace DataLager
{
    public class EntityFramework : DbContext
    {
        public DbSet<Kund> Kunder { get; set; }
        public DbSet<Bokning> Bokningar { get; set; }
        public DbSet<Mekaniker> Mekaniker { get; set; }

        public DbSet<ReservDel> ReservDelar { get; set; }
        public DbSet<Bil> Bilar { get; set; }
        public DbSet<Journal> Journaler { get; set; }
        public DbSet<JournalReservDel> JournalReservDelar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=sqlutb2-db.hb.se,56077; Database=oopc2416;User Id=oopc2416;Password=EHL583;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kund>().HasKey(k => k.KundNr);
            modelBuilder.Entity<Kund>().HasIndex(k => k.Personnummer).IsUnique();
            modelBuilder.Entity<Kund>().Property(k => k.Namn).IsRequired();
            modelBuilder.Entity<Kund>().Property(k => k.Personnummer).IsRequired();
            modelBuilder.Entity<Kund>().Property(k => k.Epost).IsRequired();
            modelBuilder.Entity<Kund>().Property(k => k.TelefonNr).IsRequired();
            modelBuilder.Entity<Kund>().Property(k => k.Adress).IsRequired();

            modelBuilder.Entity<Bil>().HasKey(b => b.RegNr);

            modelBuilder.Entity<Journal>().HasKey(j => j.JournalNr);
            modelBuilder.Entity<Journal>().HasOne(j => j.Bil).WithOne().HasForeignKey<Journal>(j => j.RegNr);
            modelBuilder.Entity<Journal>().HasOne(j => j.Bokning).WithOne().HasForeignKey<Bokning>(j => j.BokningsNr);
            //modelBuilder.Entity<Journal>().HasMany(j => j.ReservDelar).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<JournalReservDel>()
                .HasKey(jr => new { jr.JournalNr, jr.ReservdelNr }); // Configure the composite key

            modelBuilder.Entity<JournalReservDel>()
                .HasOne(jr => jr.Journal)
                .WithMany(j => j.JournalReservDelar)
                .HasForeignKey(jr => jr.JournalNr);

            modelBuilder.Entity<JournalReservDel>()
                .HasOne(jr => jr.ReservDel)
                .WithMany(r => r.JournalReservDelar)
                .HasForeignKey(jr => jr.ReservdelNr);
                

            modelBuilder.Entity<Bokning>().HasKey(b => b.BokningsNr);
            modelBuilder.Entity<Bokning>().Property(b => b.InlämningsDatum).IsRequired();
            modelBuilder.Entity<Bokning>().Property(b => b.UtlämningsDatum).IsRequired();
            modelBuilder.Entity<Bokning>().HasOne<Kund>().WithMany(m => m.Bokningar).HasForeignKey(b => b.KundNr);


            modelBuilder.Entity<Mekaniker>().HasKey(m => m.AnställningsNr);
            modelBuilder.Entity<Mekaniker>().Property(m => m.Yrkesroll).IsRequired();
            modelBuilder.Entity<Mekaniker>().Property(m => m.Specialisering).IsRequired();
            modelBuilder.Entity<Mekaniker>().Property(m => m.Lösenord).IsRequired();

            modelBuilder.Entity<ReservDel>().HasKey(r => r.ReservdelNr);
            modelBuilder.Entity<ReservDel>().Property(r => r.Namn).IsRequired();
            modelBuilder.Entity<ReservDel>().Property(r => r.Kvantitet).IsRequired();
            modelBuilder.Entity<ReservDel>().Property(r => r.Pris).IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
