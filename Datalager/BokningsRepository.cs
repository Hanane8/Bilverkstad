using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager.Entiteter;

namespace DataLager
{
    public class BokningsRepository
    {
        private EntityFramework _dbContext;

        public BokningsRepository(EntityFramework dbContext)
        {
            this._dbContext = dbContext;
        }

        public List<Bokning> HämtaBokning(Kund kund)
        {
           return _dbContext.Bokningar.Where(c => c.KundNr == kund.KundNr).ToList();
        }
    }
}
