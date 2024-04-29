using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Affärslager;
using Entitetslager.Entiteter;
using Microsoft.Extensions.DependencyInjection;

namespace Bilverkstad.PresentationLager
{
    /// <summary>
    /// Interaction logic for KundVy.xaml
    /// </summary>
    public partial class KundVy : Window
    {
        private static IServiceProvider _serviceProvider;
        private PersonService _personService;
        private BokningsService _bokingService;

        public KundVy(IServiceProvider serviceProvider)
        {

            InitializeComponent();
            var textBoxar = new TextBox[]{Adress, TelefonNr, Namn, Epost};

            foreach (var textBox in textBoxar)
                textBox.TextChanged += KollaFältOchUppdateraKnapp;

            _serviceProvider = serviceProvider;
            _personService   = _serviceProvider.GetRequiredService<PersonService>();
            _bokingService   = _serviceProvider.GetRequiredService<BokningsService>();
            LaddaAllaKunder();

        }
        /// <summary>
        /// Metoden hämtar alla kunder från databasen
        /// och visualiserar datan
        /// </summary>
        private void LaddaAllaKunder()
        {
            IEnumerable<Kund> kunder = _personService.HämtaAllaKunder();
            FylliFält(kunder.ToList());
        }

        /// <summary>
        /// Metoden har i syfte att skriva ut all information som
        /// finns i listan 'kunder'
        /// </summary>
        /// <param name="kunder"></param>
        private void FylliFält(List<Kund> kunder)
        {
            //Rensar tidigare information för uppdatering
            KunderDataGrid.Items.Clear();
            KunderDataGrid.Columns.Clear();
            List<string> egenskaperAttVisa = new List<string> { "Personnummer", "Namn", "Adress", "TelefonNr", "Epost", "Bokningar", "Bilar" };

            //Loopen skapar en kolumn per egenskap som önskas
            foreach (var egenskap in egenskaperAttVisa)
            {
                var kolumn = new DataGridTextColumn { Header = egenskap, Binding = new Binding(egenskap) };
                KunderDataGrid.Columns.Add(kolumn);
            }

            //Loopen går igenom varje kund som finns i databasen
            foreach (var kund in kunder)
            {
                var antalBokningar = _bokingService.HämtaBokning(kund).Count;
                var bilar = _personService.HämtaBilar(kund);
                string bilarRegNr = string.Join(", ", bilar.Select(b => b.RegNr));
                
                //Det skapas en ny rad med all information från en kund
                KunderDataGrid.Items.Add(new
                {
                    Personnummer = kund.Personnummer,
                    Namn = kund.Namn,
                    Adress = kund.Adress,
                    TelefonNr = kund.TelefonNr,
                    Epost = kund.Epost,
                    Bokningar = antalBokningar,
                    Bilar = bilarRegNr
                });

            }

        }

        /// <summary>
        /// Sker en konstant kontroll där det kontrolleras
        /// om alla fälten är ifyllda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="textChangedEventArgs"></param>
        private void KollaFältOchUppdateraKnapp(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var allaFältOk =
                new[] { Personnummer, Adress, Namn, TelefonNr, Epost }.All(x => !string.IsNullOrEmpty(x.Text));

            //Om alla fält är korrekt ifyllda så går det att trycka på respektive knapp
            BtnUppdatera.IsEnabled = BtnNykund.IsEnabled = allaFältOk;
        }

        /// <summary>
        /// Återställer alla fält för enkelt skapa/uppdatera ny kund
        /// </summary>
        private void ÅterställFält()
        {
            Color customColor = Color.FromRgb(255, 170, 238);
            SolidColorBrush brush = new SolidColorBrush(customColor);
            Personnummer.Text = Adress.Text = Epost.Text = Namn.Text = TelefonNr.Text = "";
            PersonnummerLbl.Foreground = TelefonnrLbl.Foreground = brush;
            Personnummer.IsEnabled = true;

        }

        /// <summary>
        /// Ser till så att 3 fält endast accepterar siffror
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Siffror_Kontroll(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Platshållare där en ny kund skapas
        /// med information från textboxar
        /// </summary>
        /// <returns></returns>
        private Kund NyKund()
        {
            Kund kund = new Kund()
            {
                Personnummer = Personnummer.Text,
                Adress = Adress.Text,
                Epost = Epost.Text,
                Namn = Namn.Text,
                TelefonNr = Convert.ToInt32(TelefonNr.Text)
            };
            return kund;
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }
        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Metoden hanterar sökfältet där
        /// användaren söker efter en kund
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Alla kunder som matchar input skickas in i FylliFält och skrivs ut till användaren
            List<Kund> sökResultat = SökResultatFrånDb(txtSearch.Text.Trim());
            FylliFält(sökResultat);
        }

        /// <summary>
        /// Metoden som ansvarar för att söka i databasen efter kunder
        /// som matchar input i sökfält
        /// </summary>
        /// <param name="sökTerm">Det som användaren skrivit in</param>
        /// <returns></returns>
        private List<Kund> SökResultatFrånDb(string sökTerm) => _personService.SökKund(sökTerm);

        /// <summary>
        /// Metoden som ansvarar för att söka i databasen efter bilar
        /// </summary>
        /// <param name="regNr"></param>
        /// <returns></returns>
        private Bil SökBil(string regNr) => _personService.SökBil(regNr);

        /// <summary>
        /// Metoden nedan ansvarar för att uppdatera en kund
        /// vid knapptryckning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnUppdatera_Click(object sender, RoutedEventArgs e)
        {
            _personService.UppdateraKund(NyKund());

            //Efter att kund har uppdaterats så återställs alla fält och kunder laddas om igen
            MessageBox.Show("Kund uppdaterad");
            ÅterställFält();
            LaddaAllaKunder();
        }

      
        /// <summary>
        /// Metoden har i uppgift att skapa ny kund
        /// vid knapptryckning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNykund_Click(object sender, RoutedEventArgs e)
        {
            //Kontrollerar om kunden redan finns
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
                if (SökBil(RegNr.Text) == null) 
                {
                    //Skapar en ny bil
                    Bil bil = new Bil()
                    {
                        Årsmodell = Convert.ToInt16(Årsmodell.Text),
                        KundNr = _personService.HämtaKund(NyKund().Personnummer).KundNr,
                        Märke = Märke.Text,
                        RegNr = RegNr.Text
                    };
                    _personService.SkapaBil(bil);

                    //Efter skapad och sparad bil nollställs fält och kunder laddas in igen
                    MessageBox.Show("Kund skapad");
                    ÅterställFält();
                    LaddaAllaKunder();
                }
                else
                {
                    MessageBox.Show("Bil finns redan");
                }
            }
            
        }

        /// <summary>
        /// Metoden nedan är för Personnummer samt Telefonnummer
        /// och kontrollerar om rätt antal siffror har skrivits in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="label"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e, Label label)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 10)
            {
                label.Foreground = Brushes.Red;

            }
            else
            {
                Color customColor = Color.FromRgb(255, 170, 238);
                SolidColorBrush brush = new SolidColorBrush(customColor);
                label.Foreground = brush;
            }

            KollaFältOchUppdateraKnapp(sender, e);
        }
        private void Personnummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_TextChanged(sender, e, PersonnummerLbl);
        }

        private void TelefonNr_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_TextChanged(sender, e, TelefonnrLbl);
        }


        private void KolumnRubrik_OnClick(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            try
            { 
                object rad = KunderDataGrid.SelectedItems[0];
                var valdRad = new
                {
                    Personnummer = ((dynamic)rad).Personnummer,
                    Namn = ((dynamic)rad).Namn,
                    Adress = ((dynamic)rad).Adress,
                    TelefonNr = ((dynamic)rad).TelefonNr,
                    Epost = ((dynamic)rad).Epost,
                    RegNr = ((dynamic)rad).Bilar
                };
                Bil bil = SökBil(valdRad.RegNr);
                
                if (valdRad != null)
                {
                    
                    Personnummer.Text = valdRad.Personnummer;
                    Personnummer.IsEnabled = false;
                    Adress.Text = valdRad.Adress;
                    Namn.Text = valdRad.Namn;
                    TelefonNr.Text = "0" + valdRad.TelefonNr.ToString();
                    Epost.Text = valdRad.Epost;
                    RegNr.Text = bil.RegNr;
                    Märke.Text = bil.Märke;
                    Årsmodell.Text = bil.Årsmodell.ToString();
                }
            }
            catch { }
        }


        private void BtnÅterställ_OnClick_Click(object sender, RoutedEventArgs e)
        {
            ÅterställFält();
        }

       
    }
}
