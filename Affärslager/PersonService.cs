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

        public bool VerifieraInloggning(string användarnman, string lösenord) =>_unitOfWork.MekanikerRepo.VerifieraInloggning(användarnman, lösenord);
        
        public Kund HämtaKund(string personnummer) =>_unitOfWork.PersonRepo.HämtaKund(personnummer);

        public List<Kund> SökKund(string sökTerm) => _unitOfWork.PersonRepo.SökKund(sökTerm);
        
        public IEnumerable<Kund> HämtaAllaKunder() => _unitOfWork.PersonRepo.HämtaAllaKunder();
        
        public IEnumerable<Mekaniker> HämtaAllaMekaniker() => _unitOfWork.MekanikerRepo.HämtaAllaMekaniker();
        
        public Kund HämtaKundNr(int kundNr) => _unitOfWork.PersonRepo.HämtaKundNr(kundNr);
        
        public Mekaniker HämtaMekanikerNr(int anställningsNr) => _unitOfWork.MekanikerRepo.HämtaMekanikerNr(anställningsNr);

        public void SkapaKund(Kund kund)
        {
            _unitOfWork.PersonRepo.SparaKund(kund);
            _unitOfWork.SaveChanges();
        }

        public void UppdateraKund(Kund kund)
        {
            _unitOfWork.PersonRepo.UppdateraKund(kund); 
            _unitOfWork.SaveChanges();
        }
       

    }
}
