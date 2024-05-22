using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datalager;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;

namespace DataLager
{
    public class PersonRepository: GenericRepository<Kund>
    {


        public PersonRepository(EntityFramework dbContext) : base(dbContext)
        {

        }

        /// <summary>
        /// Hämtar och returnerar en kund baserat på personnummer
        /// </summary>
        /// <param name="personnummer"></param>
        /// <returns></returns>
        public Kund HämtaKund(string personnummer)
        {
            return SingleOrDefault(k => k.Personnummer == personnummer);
        }
        
        /// <summary>
        /// Sparar önskad kund i databasen
        /// </summary>
        /// <param name="kund"></param>
        public void SparaKund(Kund kund)
        {
            LäggTill(kund);
        }

        /// <summary>
        /// Söker och returnerar en lista av alla kunder som matchar
        /// ett sök kriterie
        /// </summary>
        /// <param name="sökTerm"></param>
        /// <returns></returns>
        public List<Kund> SökKund(string sökTerm)
        {
            return HämtaLista(k => k.Namn.Contains(sökTerm));
        }

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
            Uppdatera(existerandeKund);
        }

        /// <summary>
        /// Hämtar och returnerar alla kunder i databasen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Kund> HämtaAllaKunder() => Hämta();
        
        
        /// <summary>
        /// Hämtar och returnerar Kund baserat på kund nummer
        /// </summary>
        /// <param name="kundNr"></param>
        /// <returns></returns>
        public Kund HämtaKundNr(int kundNr)
        {
            return SingleOrDefault(k => k.KundNr == kundNr);
        }

      

       
    }
}
