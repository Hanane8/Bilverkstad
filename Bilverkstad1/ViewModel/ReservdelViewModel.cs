using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Entitetslager.Entiteter;
using System.Collections.ObjectModel;
using Bilverkstad.PresentationLager;
using System.Windows.Input;
using DataLager;
using System.Xml.Linq;
using Affärslager;

namespace Bilverkstad1.ViewModel
{
    internal class ReservdelViewModel : INotifyPropertyChanged
    {
        //private UnitOfWork _unitOfWork;
        private readonly ReservDelService _reservDelService;
        private ReservDel _reservDel;
        private ReservDel _selectedReservDel;
        private ObservableCollection<ReservDel> _reservDels;
        public ICommand UpdateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }

        public string Namn
        {
            get { return _reservDel.Namn; }
            set
            {
                if (value == _reservDel.Namn) return;
                _reservDel.Namn = value;
                OnPropertyChanged(nameof(Namn));
            }
        }

        public double Pris
        {
            get { return _reservDel.Pris; }
            set
            {
                if (value == _reservDel.Pris) return;
                _reservDel.Pris = value;
                OnPropertyChanged(nameof(Pris));
            }
        }

        public int Kvantitet
        {
            get { return _reservDel.Kvantitet; }
            set
            {
                if (value == _reservDel.Kvantitet) return;
                _reservDel.Kvantitet = value;
                OnPropertyChanged(nameof(Kvantitet));
            }
        }

        public ReservDel SelectedReservDel
        {
            get { return _selectedReservDel; }
            set
            {
                _selectedReservDel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ReservDel> ReservDels
        {
            get { return _reservDels; }
            set
            {
                _reservDels = value;
                OnPropertyChanged();
            }
        }
        public ReservdelViewModel(ReservDelService reservDelService)
        {
            //_unitOfWork = unitOfWork;
            _reservDel = new ReservDel();
            _reservDelService = reservDelService;
            ReservDels = new ObservableCollection<ReservDel>();
            UpdateCommand = new RelayCommand(UpdateReservDel);
            SaveCommand = new RelayCommand(SaveReservDel);
            DeleteCommand = new RelayCommand(DeleteReservDel);
            AddCommand = new RelayCommand(AddReservDel);

        }

        private void UpdateReservDel(object parameter)
        {
            if (parameter is ReservDel updatedReservDel)
            {
                
                if (_selectedReservDel != null)
                {
                   
                    _selectedReservDel.Namn = updatedReservDel.Namn;
                    _selectedReservDel.Pris = updatedReservDel.Pris;
                    _selectedReservDel.Kvantitet = updatedReservDel.Kvantitet;

                   
                    OnPropertyChanged(nameof(SelectedReservDel));
                }
            }

        }
        private void UppdateraReservdelar()
        {
            ReservDels.Clear();
            var reservdelar = _reservDelService.HämtaAllaReservdel();
            foreach (var reservdel in reservdelar)
            {
                ReservDels.Add(reservdel);
            }
        }

        private void SaveReservDel(object parameter)
        {
            try
            {
                ReservDel nyReservdel = new ReservDel
                {
                    Namn = Namn,
                    Pris = Pris,
                    Kvantitet = Kvantitet
                };

                _reservDelService.SkapaReservDel(nyReservdel);

                UppdateraReservdelar();

                Namn = "";
                Pris = 0;
                Kvantitet = 0;
            }
            catch (Exception ex)
            {
               
               // MessageBox.Show("Fel vid sparande av reservdel: " + ex.Message);
            }
        }

        private void DeleteReservDel(object parameter)
        {
            
        }

        private void AddReservDel(object parameter)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
