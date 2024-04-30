using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;

namespace DataLager
{
    public class PersonRepository
    {
        private EntityFramework _dbContext;

        public PersonRepository(EntityFramework dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool VerifieraInloggning(string användarnman, string lösenord)
        {

            Mekaniker? användare = _dbContext.Mekaniker.FirstOrDefault(x => x.Namn == användarnman);

            if (användare == null)
            {
                return false;
            }

            return användare.Lösenord == lösenord;
        }

        public Kund HämtaKund(string personnummer) => _dbContext.Kunder.FirstOrDefault(k => k.Personnummer == personnummer);
        

        public void SparaKund(Kund kund)
        {
         
            _dbContext.Kunder.Add(kund);
        }

        public List<Kund> SökKund(string sökTerm) => _dbContext.Kunder.Where(k => k.Namn.Contains(sökTerm)).ToList();
        

        public void UppdateraKund(Kund kund)
        {
            var existerandeKund = HämtaKund(kund.Personnummer);
            if (existerandeKund != null)
            {
                existerandeKund.Namn = kund.Namn;
                existerandeKund.TelefonNr = kund.TelefonNr;
                existerandeKund.Adress = kund.Adress;
                existerandeKund.Epost = kund.Epost;
            }
            _dbContext.Entry(existerandeKund).State = EntityState.Modified;
        }

        public IEnumerable<Kund> HämtaAllaKunder() => _dbContext.Kunder.ToList();
        
        public IEnumerable<Mekaniker> HämtaAllaMekaniker() =>_dbContext.Mekaniker.ToList();
        
        public Mekaniker HämtaMekanikerNr(int anställningsNr) =>_dbContext.Mekaniker.FirstOrDefault(m => m.AnställningsNr == anställningsNr);
        
        public Kund HämtaKundNr(int kundNr) => _dbContext.Kunder.FirstOrDefault(k => k.KundNr == kundNr);
        

        public List<Bil> HämtaBilar(Kund kund) => _dbContext.Bilar.Where( b => b.KundNr == kund.KundNr).ToList();
        public Bil SökBil(string regnr) => _dbContext.Bilar.FirstOrDefault(b => b.RegNr == regnr);
        public void SkapaBil(Bil bil)
        {
            _dbContext.Bilar.Add(bil);
        }
        public Bil Hämtabil(string regnr) => _dbContext.Bilar.FirstOrDefault(b => b.RegNr == regnr);
    }
}
