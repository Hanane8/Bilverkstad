using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLager;
using Entitetslager.Entiteter;

namespace Affärslager
{
    public class BilService
    {
        private UnitOfWork _unitOfWork;

        public BilService(UnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }
        public bool UppdateraBil(string regNr, Bil bil)
        {
            try
            {
                _unitOfWork.BilRepo.UppdateraBil(regNr, bil);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
               
        }
        public List<Bil> HämtaBilar(Kund kund) => _unitOfWork.BilRepo.HämtaBilar(kund);


        public void SkapaBil(Bil bil)
        {
            _unitOfWork.BilRepo.SkapaBil(bil);
            _unitOfWork.SaveChanges();
        }

        public Bil SökBil(string regNr) => _unitOfWork.BilRepo.SökBil(regNr);
    }
}
