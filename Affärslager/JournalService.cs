using DataLager;
using Entitetslager.Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Affärslager
{
    public class JournalService
    {
        private UnitOfWork _unitOfWork;

        public JournalService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SkapaJournal(Journal journal)
        {
            _unitOfWork.JournalRepo.SparaJournal(journal);
            _unitOfWork.SaveChanges();
        }

        /*public List<Journal> HämtaAllaJournaler()
        {
            return _unitOfWork.JournalRepo.Include(j => j.Bil)
                                      .Include(j => j.Bokning)
                                      .Include(j => j.JournalReservDelar)
                                      .ToList();
        }*/
        
    }
}

