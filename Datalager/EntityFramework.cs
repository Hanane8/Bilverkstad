using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;

namespace DataLager
{
    public class EntityFramework : DbContext
    {
        public DbSet<Kund> Kunder { get; set; }
        public DbSet<Bokning> Bokningar { get; set; }
        public DbSet<Mekaniker> Mekaniker { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=sqlutb2-db.hb.se,56077;Database=oopc2416;User Id=oopc2416;Password=EHL583;TrustServerCertificate=True");
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


            modelBuilder.Entity<Bokning>().HasKey(b => b.BokningsNr);
            modelBuilder.Entity<Bokning>().HasOne(b => b.AnsvarigMekaniker).WithMany(m => m.Bokningar)
                .HasForeignKey(b => b.AnställningsID);

            modelBuilder.Entity<Mekaniker>().HasKey(m => m.AnställningsNr);
            modelBuilder.Entity<Mekaniker>().Property(m => m.Yrkesroll).IsRequired();
            modelBuilder.Entity<Mekaniker>().Property(m => m.Specialisering).IsRequired();
            modelBuilder.Entity<Mekaniker>().Property(m => m.Lösenord).IsRequired();
            

            base.OnModelCreating(modelBuilder);
        }
    }
}
