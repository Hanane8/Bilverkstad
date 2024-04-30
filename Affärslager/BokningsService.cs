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
            //_unitOfWork.SaveChanges();
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

        public List<Bokning> HämtaBokning(Kund kund) => _unitOfWork.BokningsRepo.HämtaBokning(kund);


        public void AvbokaBokning(Bokning bokning)
        {
            _unitOfWork.BokningsRepo.AvbokaBokning(bokning);
            _unitOfWork.SaveChanges();
        }
        //public Bokning HämtaBokningNr(int bokningsNr) => _unitOfWork.BokningsRepo.HämtaBokningNr(bokningsNr);
        public List<Bokning> HämtaBokning(Bokning bokning) => _unitOfWork.BokningsRepo.HämtaBokning(bokning);


        public IEnumerable<Bokning> HämtaAllaBokningar() =>_unitOfWork.BokningsRepo.HämtaAllaBokningar();
        
    }
}
