using DataLager;
using Entitetslager.Entiteter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Affärslager
{
    public class ReservDelService
    {
        private UnitOfWork _unitOfWork;

        public ReservDelService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void SkapaReservDel(ReservDel reservDel)
        {
            _unitOfWork.ReservDelRepo.SparaReservDel(reservDel);
            _unitOfWork.SaveChanges();
        }

        public void UppdateraReservDel(ReservDel reservDel)
        {
            _unitOfWork.ReservDelRepo.UppdateraReservDel(reservDel);
            _unitOfWork.SaveChanges();
        }

        public IEnumerable<ReservDel> HämtaAllaReservdel() => _unitOfWork.ReservDelRepo.HämtaAllaReservDelar();

        public void DeleteReservDel(ReservDel selectedReservDel)
        {
            _unitOfWork.ReservDelRepo.DeleteReservDel(selectedReservDel);
            _unitOfWork.SaveChanges();
        }
    }
}
