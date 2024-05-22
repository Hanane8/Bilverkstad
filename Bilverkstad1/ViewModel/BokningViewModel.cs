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
using System.Reflection;
using System.Windows;

namespace Bilverkstad1.ViewModel
{
    public class BokningViewModel : INotifyPropertyChanged
    {
        private readonly BokningsService _bokningService;
        private readonly PersonService _personService;
        private readonly ReservDelService _reservDelService;
        private readonly JournalService _journalService;
        private readonly BilService _bilService;
        private Bokning _bokning;
        private Bokning _selectedBokning;
        private Kund _selectedKund;
        private Mekaniker _selectedMekaniker;
        private ReservDel _selectedReservdel;
        private ObservableCollection<Bokning> _boknings;
        private ObservableCollection<Kund> _kunder;
        private ObservableCollection<Mekaniker> _mekaniker;
        private ObservableCollection<ReservDel> _reservDelar;
        private ObservableCollection<Bil> _bil;
        public ICommand UpdateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddCommand { get; }

        public BokningViewModel()
        {
            IServiceProvider serviceProvider = BokningVy._serviceProvider;
            _bokningService = serviceProvider.GetRequiredService<BokningsService>();
            _personService = serviceProvider.GetRequiredService<PersonService>();
            _reservDelService = serviceProvider.GetRequiredService<ReservDelService>();
            _journalService = serviceProvider.GetRequiredService<JournalService>();
            _bilService = serviceProvider.GetRequiredService<BilService>();

            _bokning = new Bokning();

            Boknings = new ObservableCollection<Bokning>();
            Kunder = new ObservableCollection<Kund>();
            Mekaniker = new ObservableCollection<Mekaniker>();
            ReservDelar = new ObservableCollection<ReservDel>();
            Bilar = new ObservableCollection<Bil>();

            UpdateCommand = new RelayCommand(UpdateBokning);
            SaveCommand = new RelayCommand(SaveBokning);
            DeleteCommand = new RelayCommand(DeleteBokning);
            AddCommand = new RelayCommand(AddBokning);
            UppdateraBokning();
        }
        public int BokningsNr
        {
            get { return _bokning.BokningsNr; }
            set
            {
                if (value == _bokning.BokningsNr) return;
                _bokning.BokningsNr = value;
                OnPropertyChanged(nameof(BokningsNr));
            }
        }

        public DateTime? InlämningsDatum
        {
            get { return _bokning.InlämningsDatum; }
            set
            {
                if (value == _bokning.InlämningsDatum) return;
                _bokning.InlämningsDatum = value;
                OnPropertyChanged(nameof(InlämningsDatum));
            }
        }
        public DateTime? UtlämningsDatum
        {
            get { return _bokning.UtlämningsDatum; }
            set
            {
                if (value == _bokning.UtlämningsDatum) return;
                _bokning.UtlämningsDatum = value;
                OnPropertyChanged(nameof(UtlämningsDatum));
            }
        }
        public int KundNr
        {
            get { return _bokning.KundNr; }
            set
            {
                if (value == _bokning.KundNr) return;
                _bokning.KundNr = value;
                OnPropertyChanged(nameof(KundNr));
            }
        }
        public string Namn
        {
            get { return _bokning.Namn; }
            set
            {
                if (value == _bokning.Namn) return;
                _bokning.Namn = value;
                OnPropertyChanged(nameof(Namn));
            }
        }
        public int AnställningsNr
        {
            get { return _bokning.AnställningsNr; }
            set
            {
                if (value == _bokning.AnställningsNr) return;
                _bokning.AnställningsNr = value;
                OnPropertyChanged(nameof(AnställningsNr));
            }
        }
        private string _atgarder;

        public string Åtgärder
        {
            get { return _atgarder; }
            set
            {
                if (value == _atgarder) return;
                _atgarder = value;
                OnPropertyChanged(nameof(Åtgärder));
            }
        }
        public Bokning Bokning
        {
            get { return _bokning; }
            set
            {
                if (value == _bokning) return;
                _bokning = value;
                OnPropertyChanged(nameof(Bokning));
            }
        }

        public ObservableCollection<Bokning> Boknings
        {
            get { return _boknings; }
            set
            {
                if (value == _boknings) return;
                _boknings = value;
                OnPropertyChanged(nameof(Boknings));
            }
        }
        public ObservableCollection<Kund> Kunder
        {
            get { return _kunder; }
            set
            {
                if (value == _kunder) return;
                _kunder = value;
                OnPropertyChanged(nameof(Kunder));
            }
        }
        public ObservableCollection<Mekaniker> Mekaniker
        {
            get { return _mekaniker; }
            set
            {
                if (value == _mekaniker) return;
                _mekaniker = value;
                OnPropertyChanged(nameof(Mekaniker));
            }
        }
        public ObservableCollection<ReservDel> ReservDelar
        {
            get { return _reservDelar; }
            set
            {
                if (value == _reservDelar) return;
                _reservDelar = value;
                OnPropertyChanged(nameof(ReservDelar));
            }
        }
        public ObservableCollection<Bil> Bilar
        {
            get { return _bil; }
            set
            {
                if (value == _bil) return;
                _bil = value;
                OnPropertyChanged(nameof(Bilar));
            }
        }

        public Bokning SelectedBokning
        {
            get { return _selectedBokning; }
            set
            {
                if (value == _selectedBokning) return;
                _selectedBokning = value;
                OnPropertyChanged(nameof(SelectedBokning));

                SetData();
            }
        }
        public Kund SelectedKund
        {
            get { return _selectedKund; }
            set
            {
                if (value == _selectedKund) return;
                _selectedKund = value;
                OnPropertyChanged(nameof(SelectedKund));
            }
        }
        public Mekaniker SelectedMekaniker
        {
            get { return _selectedMekaniker; }
            set
            {
                if (value == _selectedMekaniker) return;
                _selectedMekaniker = value;
                OnPropertyChanged(nameof(SelectedMekaniker));
            }
        }
        public ReservDel SelectedReservdel
        {
            get { return _selectedReservdel; }
            set
            {
                if (value == _selectedReservdel) return;
                _selectedReservdel = value;
                OnPropertyChanged(nameof(SelectedReservdel));
            }
        }
        private void ClearTextBoxes()
        {
            InlämningsDatum = null;
            UtlämningsDatum = null;
            SelectedKund = null;
            SelectedMekaniker = null;
            SelectedReservdel = null;
            BokningsNr = 0;
            Namn = string.Empty;
            AnställningsNr = 0;

        }
        private void SetData()
        {
            if (SelectedBokning != null)
            {
                KundNr = SelectedBokning.KundNr;
                Namn = SelectedBokning.Namn;
                AnställningsNr = SelectedBokning.AnställningsNr;
                InlämningsDatum = SelectedBokning.InlämningsDatum;
                UtlämningsDatum = SelectedBokning.UtlämningsDatum;
            }
        }

        private void AddBokning(object obj)
        {
            ClearTextBoxes();
        }

        private void DeleteBokning(object obj)
        {
            if (SelectedBokning != null)
            {
                try
                {
                    _bokningService.DeleteBokning(SelectedBokning);
                    MessageBox.Show("Bokningen har raderats!");
                    UppdateraBokning();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fel vid radering av bokning: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ingen bokning är vald för radering.");
            }
        }

        private void SaveBokning(object obj)
        {
            if (SelectedKund != null && SelectedMekaniker != null)
            {
                if (InlämningsDatum == null)
                {
                    MessageBox.Show("Ange ett inlämningsdatum för bokningen.");
                    return;
                }

                if (string.IsNullOrEmpty(Åtgärder))
                {
                    MessageBox.Show("Ange åtgärder för journalen.");
                    return;
                }

                Bokning newBokning = new Bokning
                {
                    // BokningsNr ska inte sättas här eftersom det är en identitetskolumn
                    KundNr = SelectedKund.KundNr,
                    Namn = SelectedKund.Namn,
                    AnställningsNr = SelectedMekaniker.AnställningsNr,
                    InlämningsDatum = InlämningsDatum,
                    UtlämningsDatum = UtlämningsDatum
                };

                _bokningService.SkapaBokning(newBokning);

                var bilar = _bilService.HämtaBilar(SelectedKund);

                if (bilar != null && bilar.Count > 0)
                {
                    var newJournal = new Journal
                    {
                        Bokning = newBokning,
                        Åtgärder = Åtgärder,
                        AnställningsNr = SelectedMekaniker.AnställningsNr,
                        RegNr = bilar[0].RegNr
                    };

                    _journalService.SkapaJournal(newJournal);
                }

                _bokningService.SaveChanges();
                MessageBox.Show("Bokningen har sparats!");
                UppdateraBokning();
            }
            else
            {
                MessageBox.Show("Välj både kund och mekaniker, samt ange inlämningsdatum och utlämningsdatum för att spara bokningen.");
            }
        }



        private void UpdateBokning(object obj)
        {
            if (SelectedBokning != null)
            {
                if (SelectedKund != null && SelectedMekaniker != null)
                {
                    if (InlämningsDatum == null)
                    {
                        MessageBox.Show("Ange ett inlämningsdatum för bokningen.");
                        return;
                    }

                    if (string.IsNullOrEmpty(Åtgärder))
                    {
                        MessageBox.Show("Ange åtgärder för journalen.");
                        return;
                    }

                    SelectedBokning.KundNr = SelectedKund.KundNr;
                    SelectedBokning.Namn = SelectedKund.Namn;
                    SelectedBokning.AnställningsNr = SelectedMekaniker.AnställningsNr;
                    SelectedBokning.InlämningsDatum = InlämningsDatum;
                    SelectedBokning.UtlämningsDatum = UtlämningsDatum;

                    try
                    {
                        _bokningService.UppdateraBokning(SelectedBokning);

                        _bokningService.SaveChanges();

                        MessageBox.Show("Bokning och journal uppdaterade!");
                        UppdateraBokning();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fel vid uppdatering av bokning: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Välj både kund och mekaniker för att uppdatera bokningen.");
                }
            }
            else
            {
                MessageBox.Show("Ingen bokning är vald för uppdatering.");
            }
        }




        private void UppdateraBokning()
        {
            Boknings = new ObservableCollection<Bokning>(_bokningService.HämtaAllaBokningar());
            Kunder = new ObservableCollection<Kund>(_personService.HämtaAllaKunder());
            Mekaniker = new ObservableCollection<Mekaniker>(_personService.HämtaAllaMekaniker());
            ReservDelar = new ObservableCollection<ReservDel>(_reservDelService.HämtaAllaReservdel());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
