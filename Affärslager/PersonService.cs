using DataLager;
using Entitetslager.Entiteter;

namespace Affärslager
{
    public class PersonService
    {
        private UnitOfWork _unitOfWork;

        public PersonService(UnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        public bool VerifieraInloggning(string användarnman, string lösenord)
        {
           return _unitOfWork.PersonRepo.VerifieraInloggning(användarnman, lösenord);
        }

        public void SkapaKund(Kund kund)
        {
            _unitOfWork.PersonRepo.SparaKund(kund);
            _unitOfWork.SaveChanges();
        }

       
        public IQueryable HämtaKund(string personnummer)
        {
            return _unitOfWork.PersonRepo.HämtaKund(personnummer);
        }

        public void UppdateraKund(Kund kund)
        {
            _unitOfWork.PersonRepo.UppdateraKund(kund); 
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<Kund> HämtaAllaKunder()
        {
            return _unitOfWork.PersonRepo.HämtaAllaKunder();
        }
    }
}
