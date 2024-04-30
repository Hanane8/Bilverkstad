using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLager
{
    public class ReservDelRepository
    {
        private readonly EntityFramework _dbContext;

        public ReservDelRepository(EntityFramework dbContext)
        {
            _dbContext = dbContext;
        }

        public void SparaReservDel(ReservDel reservDel)
        {
            _dbContext.ReservDelar.Add(reservDel);
        }
        public void UppdateraReservDel(ReservDel reservDel)
        {
            _dbContext.Entry(reservDel).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
       

        public IEnumerable<ReservDel> HämtaAllaReservDelar() => _dbContext.ReservDelar.ToList();
        
    }
}
