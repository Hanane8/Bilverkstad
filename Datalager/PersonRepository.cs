﻿using System;
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

        public IQueryable HämtaKund(string personnummer)
        {
            return _dbContext.Kunder.Where(c => c.Personnummer == personnummer);
        }

        public void SparaKund(Kund kund)
        {
           
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