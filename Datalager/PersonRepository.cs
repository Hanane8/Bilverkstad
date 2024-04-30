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

        /// <summary>
        /// Den metod som har hand om verifiering av inlogg
        /// </summary>
        /// <param name="användarnman"></param>
        /// <param name="lösenord"></param>
        /// <returns></returns>
        public bool VerifieraInloggning(string användarnman, string lösenord)
        {

            Mekaniker? användare = _dbContext.Mekaniker.FirstOrDefault(x => x.Namn == användarnman);

            //Om användaren inte finns
            if (användare == null)
            {
                return false;
            }

            return användare.Lösenord == lösenord;
        }

        /// <summary>
        /// Hämtar och returnerar en kund baserat på personnummer
        /// </summary>
        /// <param name="personnummer"></param>
        /// <returns></returns>
        public Kund HämtaKund(string personnummer) => _dbContext.Kunder.FirstOrDefault(k => k.Personnummer == personnummer);
        
        /// <summary>
        /// Sparar önskad kund i databasen
        /// </summary>
        /// <param name="kund"></param>
        public void SparaKund(Kund kund)
        {
         
            _dbContext.Kunder.Add(kund);
        }

        /// <summary>
        /// Söker och returnerar en lista av alla kunder som matchar
        /// ett sök kriterie
        /// </summary>
        /// <param name="sökTerm"></param>
        /// <returns></returns>
        public List<Kund> SökKund(string sökTerm) => _dbContext.Kunder.Where(k => k.Namn.Contains(sökTerm)).ToList();
        
        /// <summary>
        /// Metod som uppdaterar en kunds information
        /// </summary>
        /// <param name="kund"></param>
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

        /// <summary>
        /// Hämtar och returnerar alla kunder i databasen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Kund> HämtaAllaKunder() => _dbContext.Kunder.ToList();
        
        /// <summary>
        /// Hämtar och returnerar alla mekaniker i databasen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mekaniker> HämtaAllaMekaniker() =>_dbContext.Mekaniker.ToList();
        
        /// <summary>
        /// Hämtar och returnerar en mekaniker baserat på anställnings nummer
        /// </summary>
        /// <param name="anställningsNr"></param>
        /// <returns></returns>
        public Mekaniker HämtaMekanikerNr(int anställningsNr) =>_dbContext.Mekaniker.FirstOrDefault(m => m.AnställningsNr == anställningsNr);
        
        /// <summary>
        /// Hämtar och returnerar Kund baserat på kund nummer
        /// </summary>
        /// <param name="kundNr"></param>
        /// <returns></returns>
        public Kund HämtaKundNr(int kundNr) => _dbContext.Kunder.FirstOrDefault(k => k.KundNr == kundNr);
        
        /// <summary>
        /// Hämtar alla bilar som tilhör en specifik kund
        /// </summary>
        /// <param name="kund"></param>
        /// <returns></returns>
        public List<Bil> HämtaBilar(Kund kund) => _dbContext.Bilar.Where( b => b.KundNr == kund.KundNr).ToList();

        /// <summary>
        /// Söker och returnerar en specific bil och dess egenskaper
        /// </summary>
        /// <param name="regnr"></param>
        /// <returns></returns>
        public Bil SökBil(string regnr) => _dbContext.Bilar.FirstOrDefault(b => b.RegNr == regnr);

        /// <summary>
        /// Sparar en ny bil till databasen
        /// </summary>
        /// <param name="bil"></param>
        public void SkapaBil(Bil bil)
        {
            _dbContext.Bilar.Add(bil);
        }
    }
}
