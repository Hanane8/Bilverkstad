using DataLager;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Datalager
{
    public class JournalRepository
    {
        private  EntityFramework _dbContext;

        public JournalRepository(EntityFramework dbContext)
        {
            _dbContext = dbContext;
        }

        public void SparaJournal(Journal journal)
        {
            _dbContext.Journaler.Add(journal);
        }
       
    }
}
