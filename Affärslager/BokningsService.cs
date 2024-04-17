using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLager;
using Entitetslager.Entiteter;

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
            _unitOfWork.SaveChanges();
        }

        public void UppdateraBokning(Bokning bokning)
        {
            _unitOfWork.BokningsRepo.UppdateraBokning(bokning);
            _unitOfWork.SaveChanges();
        }

        //public void HämtaBokning()
        //{

        //}

        public IEnumerable<Bokning> HämtaAllaBokningar()
        {
            return _unitOfWork.BokningsRepo.HämtaAllaBokningar();
        }
    }
}
