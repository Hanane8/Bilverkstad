using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager.Entiteter;

namespace DataLager
{
    public class KundRepository
    {
        private EntityFramework _dbContext;


        public KundRepository(EntityFramework dbContext)
        {
            this._dbContext = dbContext;
        }

        public Kund HämtaKund(string personnummer)
        {
            return _dbContext.Kund.Find(personnummer);
        }

        public void SparaKund(Kund kund)
        {

        }

        public void UppdateraKund(Kund kund)
        {

        }
    }
}
