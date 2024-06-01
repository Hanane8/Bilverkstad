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
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using System.Windows;

namespace Bilverkstad1.ViewModel
{
    public class ReservdelViewModel : INotifyPropertyChanged
    {
        private readonly ReservDelService _reservDelService;
        private ReservDel _reservDel;
        private ReservDel _selectedReservDel;
        private ObservableCollection<ReservDel> _reservDels;

        public ICommand UpdateCommand { get; }
        public ICommand SaveCommand { get; }        
        public ICommand DeleteCommand { get; }      
        public ICommand AddCommand { get; }        
        public ICommand SearchCommand { get; }

        // Konstruktor
        public ReservdelViewModel()
        {
            IServiceProvider serviceProvider = ReservDelVy._serviceProvider;
            _reservDelService = serviceProvider.GetRequiredService<ReservDelService>();

            _reservDel = new ReservDel();

            ReservDels = new ObservableCollection<ReservDel>();
            UpdateCommand = new RelayCommand(UpdateReservDel);
            SaveCommand = new RelayCommand(SaveReservDel);
            DeleteCommand = new RelayCommand(DeleteReservDel);
            AddCommand = new RelayCommand(AddReservDel);
            SearchCommand = new RelayCommand(Search);

            UppdateraReservdelar();
        }

        private string _searchText;

        // Egenskap för söktext
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    ApplyFilter();
                }
            }
        }

        // Egenskap för reservdelens namn
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

        // Egenskap för reservdelens pris
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

        // Egenskap för reservdelens kvantitet
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

        // Egenskap för vald reservdel
        public ReservDel SelectedReservDel
        {
            get { return _selectedReservDel; }
            set
            {
                _selectedReservDel = value;
                OnPropertyChanged(nameof(SelectedReservDel));
                SetData();
            }
        }

        // Egenskap för listan av reservdelar
        public ObservableCollection<ReservDel> ReservDels
        {
            get { return _reservDels; }
            set
            {
                _reservDels = value;
                OnPropertyChanged(nameof(ReservDels));
            }
        }

        // Metod för att lägga till en ny reservdel
        private void AddReservDel(object parameter)
        {
            ClearTextBoxes();
        }

        // Metod för att sätta data från vald reservdel till egenskaper
        private void SetData()
        {
            if (_selectedReservDel != null)
            {
                Namn = _selectedReservDel.Namn;
                Pris = _selectedReservDel.Pris;
                Kvantitet = _selectedReservDel.Kvantitet;
            }
        }

        // Metod för att söka efter reservdelar
        private void Search(object parameter)
        {
            ApplyFilter();
        }

        // Metod för att filtrera reservdelar baserat på söktext
        private void ApplyFilter()
        {
            var allaReservdelar = _reservDelService.HämtaAllaReservdel();

            var matchandeReservdel = allaReservdelar.Where(reservDel =>
                reservDel.Namn.ToString().ToLower().Contains(SearchText.ToLower())
            ).ToList();

            ReservDels = new ObservableCollection<ReservDel>(matchandeReservdel);
        }

        // Metod för att uppdatera en reservdel
        private void UpdateReservDel(object parameter)
        {
            if (SelectedReservDel != null)
            {
                try
                {
                    SelectedReservDel.Namn = Namn;
                    SelectedReservDel.Pris = Pris;
                    SelectedReservDel.Kvantitet = Kvantitet;

                    _reservDelService.UppdateraReservDel(SelectedReservDel);

                    UppdateraReservdelar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ett fel inträffade vid uppdatering av reservdel: {ex.Message}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            ClearTextBoxes();
        }

        // Metod för att uppdatera listan av reservdelar
        private void UppdateraReservdelar()
        {
            ReservDels.Clear();
            var reservdelar = _reservDelService.HämtaAllaReservdel();
            foreach (var reservdel in reservdelar)
            {
                ReservDels.Add(reservdel);
            }
        }

        // Metod för att rensa textfälten
        private void ClearTextBoxes()
        {
            Namn = "";
            Pris = 0;
            Kvantitet = 0;
        }

        // Metod för att ta bort en reservdel
        private void DeleteReservDel(object parameter)
        {
            try
            {
                if (_selectedReservDel != null)
                {
                    _reservDelService.DeleteReservDel(_selectedReservDel);

                    UppdateraReservdelar();

                    ClearTextBoxes();
                }
                else
                {
                    Console.WriteLine("Ingen reservdel är vald för borttagning.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fel vid borttagning av reservdel: " + ex.Message);
            }
        }

        // Metod för att spara en ny reservdel
        private void SaveReservDel(object parameter)
        {
            try
            {
                ReservDel nyReservDel = new ReservDel
                {
                    Namn = Namn,
                    Pris = Pris,
                    Kvantitet = Kvantitet
                };

                // Kontrollera om reservdelen redan finns
                var befintligReservDel = _reservDelService.HämtaAllaReservdel()
                    .FirstOrDefault(rd => rd.Namn == nyReservDel.Namn);
                if (befintligReservDel != null)
                {
                    // Hantera fallet där reservdelen redan finns, t.ex. visa ett meddelande
                    MessageBox.Show("En reservdel med detta namn finns redan.", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _reservDelService.SkapaReservDel(nyReservDel);

                UppdateraReservdelar();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel inträffade: {ex.Message}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Händelse för egenskapsändring
        public event PropertyChangedEventHandler PropertyChanged;

        // Metod för att anropa händelsen när en egenskap ändras
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
