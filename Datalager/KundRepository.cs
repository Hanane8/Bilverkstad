using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;

namespace DataLager
{
    public class KundRepository
    {
        private EntityFramework _dbContext;


        public KundRepository(EntityFramework dbContext)
        {
            this._dbContext = dbContext;
        }

        public IQueryable HämtaKund(string personnummer)
        {
            return _dbContext.Kunder.Where(c => c.Personnummer == personnummer);
        }

        public void SparaKund(Kund kund)
        {
            Mekaniker mekaniker = new Mekaniker()
            {
                Lösenord = "test",
                Specialisering = "Bromssystemtekniker",
                Yrkesroll = "Bilmekaniker",
                Namn = "Jöns",
                Adress = "Gatan 2",
                Epost = "jons@test.com",
                TelefonNr = 0701231234,
                Personnummer = "0001010101"
                
            };
            _dbContext.Mekaniker.Add(mekaniker);
            _dbContext.Kunder.Add(kund);
        }

        public void UppdateraKund(Kund kund)
        {
            _dbContext.Entry(kund).State = EntityState.Modified;
        }

        public IEnumerable<Kund> HämtaAllaKunder()
        {
            return _dbContext.Kunder.ToList();
        }
    }
}
