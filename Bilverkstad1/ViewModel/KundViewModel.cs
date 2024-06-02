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
using System.Windows.Media;
using Affärslager;
using Bilverkstad.PresentationLager;
using Entitetslager.Entiteter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Bilverkstad1.ViewModel
{
    public class KundViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        private PersonService _personService;
        private BokningsService _bokningsService;
        private BilService _bilService;


        private Dictionary<string, string> kontaktInfo = new();
        private SolidColorBrush _labelColor = new(Color.FromRgb(255, 170, 238));
        public Commands Commands { get; }
        public ObservableCollection<KundDataViewModel> KundData { get; set; } = new();
        private KundDataViewModel _selectedItem;

        private string _sök;
        private bool korrektInput, gammalKund, _isTextBoxEnabled;
        public KundViewModel()
        {
            IServiceProvider serviceProvider = KundVy._serviceProvider;
            _personService = serviceProvider.GetRequiredService<PersonService>();
            _bokningsService = serviceProvider.GetRequiredService<BokningsService>();
            _bilService = serviceProvider.GetRequiredService<BilService >();

            Commands = new Commands(this);

            LaddaAllaKunder();
        }
        /// <summary>
        /// Metod som bestämmer om Personnummer och Telefonnummer fält 
        /// är aktiva eller inte
        /// </summary>
        /// <param name="x"></param>
        public void ToggleTextBoxEnabled(object x)
        {
            if (gammalKund)
                IsTextBoxEnabled = false;
            else
                IsTextBoxEnabled = true;

        }
        private string HämtaAttribut(string key) => kontaktInfo.TryGetValue(key, out var value) ? value : string.Empty;
        /// <summary>
        /// Metod som skapar nya/uppdaterar key value pairs i dictionary
        /// </summary>
        /// <param name="key">nyckeln t.ex 'Namn'</param>
        /// <param name="value">värdet t.ex 'Jöns'</param>
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

        /// <summary>
        /// Hanterar vad som händer när man trycker på 
        /// en rad
        /// </summary>
        public void KolumnClick(object x)
        {
            if(x != null)
            {

                KundDataViewModel kundDataViewModel = (KundDataViewModel)x;
                Bil bil = SökBil(kundDataViewModel.Bilar);
                SättAttribut("Namn", kundDataViewModel.Namn);
                SättAttribut("Epost", kundDataViewModel.Epost);
                SättAttribut("TelefonNr", kundDataViewModel.TelefonNr);
                SättAttribut("Personnummer", kundDataViewModel.Personnummer.ToString());
                SättAttribut("Adress", kundDataViewModel.Adress);
                SättAttribut("RegNr", bil.RegNr);
                SättAttribut("Årsmodell", bil.Årsmodell.ToString());
                SättAttribut("Märke", bil.Märke);
                korrektInput = true;
                gammalKund = true;
            }
            
            
        }

      


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
        private Bil SökBil(string regNr) => _bilService.SökBil(regNr);


        private Kund NyKund() => new()
        {
            Namn = HämtaAttribut("Namn"),
            Adress = HämtaAttribut("Adress"),
            TelefonNr = Convert.ToInt64(HämtaAttribut("TelefonNr")),
            Personnummer = HämtaAttribut("Personnummer"),
            Epost = HämtaAttribut("Epost")
        };

        private Bil NyBil() => new()
        {
            Årsmodell = Convert.ToInt16(HämtaAttribut("Årsmodell")),
            KundNr = _personService.HämtaKund(NyKund().Personnummer).KundNr,
            Märke = HämtaAttribut("Märke"),
            RegNr = HämtaAttribut("RegNr")
        };
        private void LaddaAllaKunder() => FylliFält(_personService.HämtaAllaKunder().ToList());
        private void FylliFält(List<Kund> kunder)
        {
            KundData.Clear();
            foreach (var kund in kunder)
            {
                var antalBokningar = _bokningsService.HämtaBokning(kund).Count;
                var bilar = _bilService.HämtaBilar(kund);
                var bilarRegNr = string.Join(", ", bilar.Select(b => b.RegNr));
                KundData.Add(new KundDataViewModel
                {
                    Personnummer = kund.Personnummer,
                    Namn = kund.Namn,
                    Adress = kund.Adress,
                    TelefonNr = kund.TelefonNr.ToString(),
                    Epost = kund.Epost,
                    Bokingar = antalBokningar,
                    Bilar = bilarRegNr
                });
            }

        }

        private List<Kund> SökResultatFrånDb(string sökTerm) => _personService.SökKund(sökTerm);


        public void SökKund(object x)
        {
            //Alla kunder som matchar input skickas in i FylliFält och skrivs ut till användaren
            List<Kund> sökResultat = SökResultatFrånDb(_sök);
            FylliFält(sökResultat);
        }
        
        private bool IFylldaFällt()
        {
            string[] requiredFields = { "Namn", "Personnummer", "TelefonNr", "Epost", "Adress", "Märke", "RegNr", "Årsmodell" };
            return requiredFields.All(key => !string.IsNullOrEmpty(HämtaAttribut(key)));
        }
        public void BtnNyKund(object x)
        {
            if (IFylldaFällt() && korrektInput)
            {
                var kund = _personService.HämtaKund(NyKund().Personnummer);
                if (kund != null)
                {
                    MessageBox.Show("Kund finns");
                }
                else
                {
                    _personService.SkapaKund(NyKund());
                    if (SökBil(HämtaAttribut("RegNr")) == null)
                    {
                        _bilService.SkapaBil(NyBil());
                        MessageBox.Show("Kund skapad");
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
        public void BtnUppdatera(object x)
        {
            if (IFylldaFällt() && korrektInput)
            {
                var kund = _personService.HämtaKund(NyKund().Personnummer);
                var bilar = _bilService.HämtaBilar(kund);
                var bilarRegNr = string.Join(", ", bilar.Select(b => b.RegNr));
                if (!_bilService.UppdateraBil(bilarRegNr, NyBil()))
                {
                    MessageBox.Show("Du kan inte uppdatera RegNr");
                }
                else
                {
                    _personService.UppdateraKund(NyKund());
                    MessageBox.Show("Kund uppdaterad");
                    BtnÅterställ(x);
                    LaddaAllaKunder();
                }
            }
            else
            {
                MessageBox.Show("Inkorrekta fält");
            }

        }

        private bool InputNumerisk(string x)
        {
            return Regex.IsMatch(x, "^[0-9]*$");
           
        }

        private void KontrolleraInputModell()
        {
            if (kontaktInfo.TryGetValue("Årsmodell", out string? value))
            {

                if (value.Length != 4)
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
            if (kontaktInfo.TryGetValue("Personnummer", out string? value))
            {

                if (value.Length != 12)
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
        public void BtnÅterställ(object x)
        {
            foreach (var key in kontaktInfo.Keys.ToList())
            {
                kontaktInfo[key] = "";
                OnPropertyChanged(key);
            }

            gammalKund = false;
            ToggleTextBoxEnabled(x);
        }

        #region Attribut



        public SolidColorBrush LabelColorModell
        {
            get { return _labelColor; }
            set { _labelColor = value; OnPropertyChanged(); }
        }
        public SolidColorBrush LabelColorPersonNr
        {
            get { return _labelColor; }
            set { _labelColor = value; OnPropertyChanged(); }
        }

        public SolidColorBrush LabelColorTelefonNr
        {
            get { return _labelColor; }
            set { _labelColor = value; OnPropertyChanged(); }
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

        public bool IsTextBoxEnabled
        {
            get { return _isTextBoxEnabled; }
            set { _isTextBoxEnabled = value; OnPropertyChanged(); }
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
       

    }   
}
