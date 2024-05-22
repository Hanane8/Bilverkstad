using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;

namespace DataLager
{
    public class BokningsRepository
    {
        private EntityFramework _dbContext;

        public BokningsRepository(EntityFramework dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Bokning> HämtaBokning(Bokning bokning) =>_dbContext.Bokningar.Where(b => b.BokningsNr == bokning.BokningsNr).ToList();

        public List<Bokning> HämtaBokning(Kund kund) => _dbContext.Bokningar.Where(c => c.KundNr == kund.KundNr).ToList();


        public void UppdateraBokning(Bokning bokning)
        {
            _dbContext.Entry(bokning).State = EntityState.Modified;
        }

        public void SparaBokning(Bokning bokning)
        {
            _dbContext.Bokningar.Add(bokning);
            
        }
        
        public IEnumerable<Bokning> HämtaAllaBokningar() => _dbContext.Bokningar.ToList();
        
    }
}
