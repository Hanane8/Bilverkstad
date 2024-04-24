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

       
        public Kund HämtaKund(string personnummer)
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
<<<<<<< Updated upstream

=======
        public IEnumerable<Mekaniker> HämtaAllaMekaniker()
        {
            return _unitOfWork.PersonRepo.HämtaAllaMekaniker();
        }
        public Kund HämtaKundNr(int kundNr)
        {
            return _unitOfWork.PersonRepo.HämtaKundNr(kundNr);
        }
        public Mekaniker HämtaMekanikerNr(int anställningsNr)
        {
            return _unitOfWork.PersonRepo.HämtaMekanikerNr(anställningsNr);
        }
>>>>>>> Stashed changes
    }
}
