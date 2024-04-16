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

        public List<Bokning> HämtaBokning(string personnummer)
        {
           return _dbContext.Bokning.Find(personnummer);
        }
    }
}
