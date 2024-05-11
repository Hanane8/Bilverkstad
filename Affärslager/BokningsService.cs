using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bilverkstad1;
using DataLager;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;

namespace Affärslager
{
    public class BokningsService
    {
        private UnitOfWork _unitOfWork;
        public BokningsService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SkapaBokning(Bokning bokning)
        {
            _unitOfWork.BokningsRepo.SparaBokning(bokning);
        }

        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
        public void UppdateraBokning(Bokning bokning)
        {
            _unitOfWork.BokningsRepo.UppdateraBokning(bokning);
            _unitOfWork.SaveChanges();
        }

        //public List<Bokning> HämtaBokning(Kund kund) => _unitOfWork.BokningsRepo.HämtaBokning(kund);
        public List<Bokning> HämtaBokning(Kund kund)
        {
            var bokningar = _unitOfWork.BokningsRepo.HämtaBokning(kund);
            foreach (var bokning in bokningar)
            {
                // Hämta kundnamn för varje bokning
                bokning.Namn = kund.Namn;
            }
            return bokningar;
        }




        public IEnumerable<Bokning> HämtaAllaBokningar() =>_unitOfWork.BokningsRepo.HämtaAllaBokningar();
        
    }
}
