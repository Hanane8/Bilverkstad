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
        /// <summary>
        /// Sparar en ny reservdel till databasen
        /// </summary>
        /// <param name="reservDel"></param>
        public void SparaReservDel(ReservDel reservDel)
        {
            _dbContext.ReservDelar.Add(reservDel);
        }

        /// <summary>
        /// Uppdaterar en reservdel baserat på ny information
        /// </summary>
        /// <param name="reservDel"></param>
        public void UppdateraReservDel(ReservDel reservDel)
        {
            _dbContext.Entry(reservDel).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
       
        /// <summary>
        /// Hämtar och returnerar alla reservdelar från databasen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ReservDel> HämtaAllaReservDelar() => _dbContext.ReservDelar.ToList();
        
    }
}
