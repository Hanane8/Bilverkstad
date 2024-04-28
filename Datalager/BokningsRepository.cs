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
            this._dbContext = dbContext;
        }

        public List<Bokning> HämtaBokning(Kund kund)
        {
          return _dbContext.Bokningar.Where(c => c.KundNr == kund.KundNr).ToList();
        }

        public void UppdateraBokning(Bokning bokning)
        {
            _dbContext.Entry(bokning).State = EntityState.Modified;
        }

        public void SparaBokning(Bokning bokning)
        {
            _dbContext.Bokningar.Add(bokning);
            
        }
        public void AvbokaBokning(Bokning bokning)
        {
            _dbContext.Bokningar.Remove(bokning);
        }
        public IEnumerable<Bokning> HämtaAllaBokningar()
        {
            return _dbContext.Bokningar.ToList();
        }
    }
}
