using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Affärslager;
using Bilverkstad.PresentationLager;
using Entitetslager.Entiteter;
using Microsoft.Extensions.DependencyInjection;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Bilverkstad1.ViewModel
{

    public class KundDataViewModel
    {
        public string Personnummer { get; set; }
        public string Namn { get; set; }
        public string Adress { get; set; }
        public string TelefonNr { get; set; }
        public string Epost { get; set; }
        public int Bokingar { get; set; }
        public string Bilar { get; set; }
    }
    public class KundViewModel: INotifyPropertyChanged
    {
        public ICommand NyKundCommand { get; }
        public ICommand UppdateraCommand { get; }
        public ICommand ÅterställCommand { get; }
        public ICommand FylliFältCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private PersonService _personService;
        private BokningsService _bokningsService;


        private Dictionary<string, string> kontaktInfo = new Dictionary<string, string>();
        private string _namn, _adress, _personnummer, _telefonnr, _epost, _märke, _regnr, _årsmodell;

        public ObservableCollection<KundDataViewModel> KundData { get; set; } =
            new ObservableCollection<KundDataViewModel>();
        public KundViewModel(IServiceProvider serviceProvider)
        {
            _personService = serviceProvider.GetRequiredService<PersonService>();
            _bokningsService = serviceProvider.GetRequiredService<BokningsService>();
            NyKundCommand = new RelayCommand(BtnNyKund);
            UppdateraCommand = new RelayCommand(BtnUppdatera);
            ÅterställCommand = new RelayCommand(BtnÅterställ);
            LaddaAllaKunder();
        }

        public string this[string namn]
        {
            get { return kontaktInfo.ContainsKey(namn) ? kontaktInfo[namn] : null; }
            set
            {
                kontaktInfo[namn] = value;
                OnPropertyChanged(namn);
            }
        }
      

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Bil SökBil(string regNr) => _personService.SökBil(regNr);


        private Kund NyKund()
        {
            Kund kund = new Kund
            {
                Namn = _namn,
                Adress = _adress,
                TelefonNr = Convert.ToInt64(_telefonnr),
                Personnummer = _personnummer,
                Epost = _epost
            };
            return kund;
        }
        private void LaddaAllaKunder()
        {
            IEnumerable<Kund> kunder = _personService.HämtaAllaKunder();
            FylliFält(kunder.ToList());
        }
        private void FylliFält(List<Kund> kunder)
        {
            KundData.Clear();
            //Rensar tidigare information för uppdatering
            foreach (var kund in kunder)
            {
                var antalBokningar = _bokningsService.HämtaBokning(kund).Count;
                var bilar = _personService.HämtaBilar(kund);
                string bilarRegNr = string.Join(", ", bilar.Select(b => b.RegNr));

                // Create a new KundDataViewModel instance
                var kundDataViewModel = new KundDataViewModel
                {
                    Personnummer = kund.Personnummer,
                    Namn = kund.Namn,
                    Adress = kund.Adress,
                    TelefonNr = kund.TelefonNr.ToString(),
                    Epost = kund.Epost,
                    Bokingar = antalBokningar,
                    Bilar = bilarRegNr
                };

                // Add the KundDataViewModel to KundData collection
                KundData.Add(kundDataViewModel);
            }

        }

        public void BtnNyKund(object x)
        {
            _namn = this["Namn"];
            _adress = this["Adress"];
            _epost = this["Epost"];
            _telefonnr = this["TelefonNr"];
            _personnummer = this["PersonNr"];
            _märke = this["Märke"];
            _regnr = this["RegNr"];
            _årsmodell = this["Årsmodell"];

            Kund? kund = _personService.HämtaKund(NyKund().Personnummer);
            if (kund != null)
            {
                MessageBox.Show("Kund finns");
            }
            else
            {
                //Om kund inte finns så sparas kunden in i databasen
                _personService.SkapaKund(NyKund());

                //Om bilen inte redan tillhör någon
                if (SökBil(_regnr) == null)
                {
                    //Skapar en ny bil
                    Bil bil = new Bil()
                    {
                        Årsmodell = Convert.ToInt16(_årsmodell),
                        KundNr = _personService.HämtaKund(NyKund().Personnummer).KundNr,
                        Märke = _märke,
                        RegNr = _regnr
                    };
                    _personService.SkapaBil(bil);

                    //Efter skapad och sparad bil nollställs fält och kunder laddas in igen
                    MessageBox.Show("Kund skapad");
                    //ÅterställFält();
                    //LaddaAllaKunder();
                }
                else
                {
                    MessageBox.Show("Bil finns redan");
                }
            }

        }

        public void BtnUppdatera(object x)
        {
            _namn = this["Namn"];
            _adress = this["Adress"];
            _epost = this["Epost"];
            _telefonnr = this["TelefonNr"];
            _personnummer = this["PersonNr"];
            _märke = this["Märke"];
            _regnr = this["RegNr"];
            _årsmodell = this["Årsmodell"];

            _personService.UppdateraKund(NyKund());
            MessageBox.Show("Kund uppdaterad");
        }

        public void BtnÅterställ(object x)
        {
            foreach (var key in kontaktInfo.Keys.ToList())
            {
                kontaktInfo[key] = "";
                OnPropertyChanged(key);
            }
        }

    }   
}
