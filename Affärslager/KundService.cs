using DataLager;
using Entitetslager.Entiteter;

namespace Affärslager
{
    public class KundService
    {
        private UnitOfWork _unitOfWork;

        public KundService(UnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        public void SkapaKund(Kund kund)
        {
            _unitOfWork.KundRepo.SparaKund(kund);
            _unitOfWork.SaveChanges();
        }

        public Kund HämtaKund(string personnummer)
        {
            return _unitOfWork.KundRepo.HämtaKund(personnummer);
        }

        public void UppdateraKund(Kund kund)
        {
            _unitOfWork.KundRepo.UppdateraKund(kund); 
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Kund> HämtaAllaKunder()
        {
            return _unitOfWork.KundRepo.HämtaAllaKunder();
        }
    }
}
