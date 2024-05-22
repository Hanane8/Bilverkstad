using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLager;

namespace Datalager
{
    public class BilRepository : GenericRepository<Bil>
    {
        public BilRepository(EntityFramework dbContext) : base(dbContext)
        {
        }
        public List<Bil> HämtaBilar(Kund kund)
        {
            return HämtaLista(b => b.KundNr == kund.KundNr);
        }
        /// <summary>
        /// Söker och returnerar en specific bil och dess egenskaper
        /// </summary>
        /// <param name="regnr"></param>
        /// <returns></returns>
        public Bil SökBil(string regnr)
        {
            return SingleOrDefault(b => b.RegNr == regnr);
        }

        /// <summary>
        /// Sparar en ny bil till databasen
        /// </summary>
        /// <param name="bil"></param>
        public void SkapaBil(Bil bil)
        {
            LäggTill(bil);
        }

        public bool UppdateraBil(string regNr, Bil bil)
        {
            var existerandeBil = SingleOrDefault(b => b.RegNr == regNr);
            if (existerandeBil != null)
            {
                existerandeBil.RegNr = bil.RegNr;
                existerandeBil.Märke = bil.Märke;
                existerandeBil.Årsmodell= bil.Årsmodell;
            }

            try
            {
                Uppdatera(existerandeBil);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
