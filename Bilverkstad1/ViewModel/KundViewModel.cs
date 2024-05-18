using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Affärslager;
using Bilverkstad.PresentationLager;
using Entitetslager.Entiteter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Bilverkstad1.ViewModel
{
    //TODO
    //1. Personnummer textchanged ändra färg

    public class KundViewModel : INotifyPropertyChanged
    {
        #region ICommands

        public ICommand ClickCommand { get; }
        public ICommand NyKundCommand { get; }
        public ICommand UppdateraCommand { get; }
        public ICommand ÅterställCommand { get; }
        public ICommand SökCommand { get; }
        public ICommand TextKontrollCommand { get; }

        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;
        private PersonService _personService;
        private BokningsService _bokningsService;


        private Dictionary<string, string> kontaktInfo = new Dictionary<string, string>();
        private string _märke, _regnr, _årsmodell, _sök;
        private bool korrektInput = false;

        public ObservableCollection<KundDataViewModel> KundData { get; set; } =
            new ObservableCollection<KundDataViewModel>();
        public KundViewModel()
        {
            IServiceProvider serviceProvider = KundVy._serviceProvider;
            _personService = serviceProvider.GetRequiredService<PersonService>();
            _bokningsService = serviceProvider.GetRequiredService<BokningsService>();

            NyKundCommand = new RelayCommand(BtnNyKund);
            UppdateraCommand = new RelayCommand(BtnUppdatera);
            ÅterställCommand = new RelayCommand(BtnÅterställ);
            SökCommand = new RelayCommand(SökKund);
            ClickCommand = new RelayCommand(KolumnClick);

            LaddaAllaKunder();
        }

        private string HämtaAttribut(string key)
        {
            kontaktInfo.TryGetValue(key, out string value);
            return value;
        }

        private void SättAttribut(string key, string value)
        {
            if (kontaktInfo.ContainsKey(key))
            {
                if (kontaktInfo[key] != value)
                {
                    kontaktInfo[key] = value;
                    OnPropertyChanged(key);
                    
                }
            }
            else
            {
                kontaktInfo.Add(key, value);
                OnPropertyChanged(key);
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void KolumnClick(object x)
        {
            if(x != null)
            {

                KundDataViewModel kundDataViewModel = (KundDataViewModel)x;
                Bil bil = SökBil(kundDataViewModel.Bilar);
                SättAttribut("Namn", kundDataViewModel.Namn);
                SättAttribut("Epost", kundDataViewModel.Epost);
                SättAttribut("TelefonNr", kundDataViewModel.TelefonNr);
                SättAttribut("Personnummer", kundDataViewModel.Personnummer);
                SättAttribut("Adress", kundDataViewModel.Adress);
                SättAttribut("RegNr", bil.RegNr);
                SättAttribut("Årsmodell", bil.Årsmodell.ToString());
                SättAttribut("Märke", bil.Märke);
                korrektInput = true;
            }
            
            
        }

      
        private KundDataViewModel _selectedItem;

        public KundDataViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }
        private Bil SökBil(string regNr) => _personService.SökBil(regNr);


        private Kund NyKund()
        {
            Kund kund = new Kund
            {
                Namn = HämtaAttribut("Namn"),
                Adress = HämtaAttribut("Adress"),
                TelefonNr = Convert.ToInt64(HämtaAttribut("TelefonNr")),
                Personnummer = HämtaAttribut("Personnummer"),
                Epost = HämtaAttribut("Epost")
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

        private List<Kund> SökResultatFrånDb(string sökTerm) => _personService.SökKund(sökTerm);


        private void SökKund(object x)
        {
            //Alla kunder som matchar input skickas in i FylliFält och skrivs ut till användaren
            List<Kund> sökResultat = SökResultatFrånDb(_sök);
            FylliFält(sökResultat);
        }
        
        private bool IFylldaFällt()
        {
            try
            {
                string[] textboxar = [
                    kontaktInfo["Namn"], kontaktInfo["Personnummer"], kontaktInfo["TelefonNr"],
                    kontaktInfo["Epost"], kontaktInfo["Adress"], kontaktInfo["Märke"], kontaktInfo["RegNr"],
                    kontaktInfo["Årsmodell"]
                ];

                return textboxar.All(x => !string.IsNullOrEmpty(x));
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public void BtnNyKund(object x)
        {
            if (IFylldaFällt() && korrektInput == true)
            {
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
                        //TODO 
                        //Ful lösning
                        BtnÅterställ(x);
                        LaddaAllaKunder();
                    }
                    else
                    {
                        MessageBox.Show("Bil finns redan");
                    }
                }
            }
            else
            {
                MessageBox.Show("Fält ej korrekt ifyllda");
            }
        }


        private bool InputNumerisk(string x)
        {
            return Regex.IsMatch(x, "^[0-9]*$");
           
        }

        private void KontrolleraInputModell()
        {
            if (kontaktInfo.ContainsKey("Årsmodell"))
            {

                if (kontaktInfo["Årsmodell"].Length != 4)
                {
                    LabelColorModell = Brushes.Red;
                    korrektInput = false;
                }
                else
                {
                    LabelColorModell = new(Color.FromRgb(255, 170, 238));
                    korrektInput = true;

                }
            }
        }
        private void KontrolleraInputPerson()
        {
            if (kontaktInfo.ContainsKey("Personnummer"))
            {

                if (kontaktInfo["Personnummer"].Length != 12)
                {
                    LabelColorPersonNr = Brushes.Red;
                    korrektInput = false;
                }
                else
                {
                    LabelColorPersonNr = new(Color.FromRgb(255, 170, 238));
                    korrektInput = true;

                }
            }
        }

        private void KontrolleraInputTelefon()
        {
            if (kontaktInfo.ContainsKey("TelefonNr"))
            {
                if (kontaktInfo["TelefonNr"].Length != 10)
                {
                    LabelColorTelefonNr = Brushes.Red;
                }
                else
                {
                    LabelColorTelefonNr = new(Color.FromRgb(255, 170, 238));
                }
            }
            
        }

        #region Attribut


        private SolidColorBrush _labelColor = new(Color.FromRgb(255, 170, 238));

        public SolidColorBrush LabelColorModell
        {
            get { return _labelColor; }
            set
            {
                _labelColor = value;
                OnPropertyChanged();
            }
        }
        public SolidColorBrush LabelColorPersonNr
        {
            get { return _labelColor; }
            set
            {
                _labelColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush LabelColorTelefonNr
        {
            get { return _labelColor; }
            set
            {
                _labelColor = value;
                OnPropertyChanged();
            }
        }
      
      
        public string Sök
        {
            get { return _sök; }
            set
            {
                if (_sök != value)
                {
                    _sök = value;
                    OnPropertyChanged(nameof(Sök));
                }
            }
        }

        public string Namn
        {
            get { return HämtaAttribut(nameof(Namn)); }
            set { SättAttribut(nameof(Namn), value); }
        }

        public string Adress
        {
            get { return HämtaAttribut(nameof(Adress)); }
            set { SättAttribut(nameof(Adress), value);}
        }

        public string Personnummer
        {
            get { return HämtaAttribut(nameof(Personnummer)); }
            set 
            {
                if (InputNumerisk(value))
                {
                    SättAttribut(nameof(Personnummer), value);
                    KontrolleraInputPerson(); 
                }
            }
        }

        public string TelefonNr
        {
            get { return HämtaAttribut(nameof(TelefonNr)); }
            set 
            {
                if (InputNumerisk(value))
                {
                    SättAttribut(nameof(TelefonNr), value);
                    KontrolleraInputTelefon();
                }
                
            }
        }

        public string Epost
        {
            get { return HämtaAttribut(nameof(Epost)); }
            set { SättAttribut(nameof(Epost), value); }
        }

        public string Märke
        {
            get { return HämtaAttribut(nameof(Märke)); }
            set { SättAttribut(nameof(Märke), value); }
        }
        public string Årsmodell
        {
            get { return HämtaAttribut(nameof(Årsmodell)); }
            set 
            {
                if (InputNumerisk(value))
                {
                    SättAttribut(nameof(Årsmodell), value);
                    KontrolleraInputModell();
                }
            }
        }
        public string RegNr
        {
            get { return HämtaAttribut(nameof(RegNr)); }
            set { SättAttribut(nameof(RegNr), value); }
        }

        #endregion
        public void BtnUppdatera(object x)
        {
            if (IFylldaFällt() && korrektInput == true)
            {
                _personService.UppdateraKund(NyKund());
                MessageBox.Show("Kund uppdaterad");
                //TODO 
                //Ful lösning
                BtnÅterställ(x);
                LaddaAllaKunder();
            }
            else
            {
                MessageBox.Show("Inkorrekta fält");
            }

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
