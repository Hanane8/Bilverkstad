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

        public KundVy(IServiceProvider serviceProvider)
        {

            InitializeComponent();
            var textBoxar = new TextBox[]{Adress, TelefonNr, Namn, Epost};

            foreach (var textBox in textBoxar)
                textBox.TextChanged += KollaFältOchUppdateraKnapp;

            _serviceProvider = serviceProvider;
            _personService   = _serviceProvider.GetRequiredService<PersonService>();
            LaddaAllaKunder();

        }

        private void LaddaAllaKunder()
        {
           
            IEnumerable<Kund> kunder = _personService.HämtaAllaKunder();
            FylliFält(kunder.ToList());
            
        }

        private void FylliFält(List<Kund> kunder)
        {
            KunderDataGrid.Items.Clear();
            KunderDataGrid.Columns.Clear();
            List<string> egenskaperAttVisa = new List<string> { "Personnummer", "Namn", "Adress", "TelefonNr", "Epost", "Bokningar" };


            foreach (var egenskap in egenskaperAttVisa)
            {
                var kolumn = new DataGridTextColumn { Header = egenskap, Binding = new Binding(egenskap) };
                KunderDataGrid.Columns.Add(kolumn);
            }

            foreach (var kund in kunder)
            {
                var antalBokningar = kund.Bokningar?.Count ?? 0;
                KunderDataGrid.Items.Add(new
                {
                    Personnummer = kund.Personnummer,
                    Namn = kund.Namn,
                    Adress = kund.Adress,
                    TelefonNr = kund.TelefonNr,
                    Epost = kund.Epost,
                    Bokningar = antalBokningar,
                });

            }

        }
        private void KollaFältOchUppdateraKnapp(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            var allaFältOk =
                new[] { Personnummer, Adress, Namn, TelefonNr, Epost }.All(x => !string.IsNullOrEmpty(x.Text));

            BtnUppdatera.IsEnabled = BtnNykund.IsEnabled = allaFältOk;
        }

        private void ÅterställFält()
        {

            Personnummer.Text = Adress.Text = Epost.Text = Namn.Text = TelefonNr.Text = "";
            PersonnummerLbl.Foreground = TelefonnrLbl.Foreground = Brushes.Black;
            Personnummer.IsEnabled = true;

        }
        private void Siffror_Kontroll(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
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


        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

            List<Kund> sökResultat = SökResultatFrånDb(txtSearch.Text.Trim());
            FylliFält(sökResultat);
        }

        private List<Kund> SökResultatFrånDb(string sökTerm) => _personService.SökKund(sökTerm);
        


        private void BtnUppdatera_Click(object sender, RoutedEventArgs e)
        {
            _personService.UppdateraKund(NyKund());

            MessageBox.Show("Kund uppdaterad");
            ÅterställFält();
            LaddaAllaKunder();
        }

      

        private void BtnNykund_Click(object sender, RoutedEventArgs e)
        {
            Kund? kund = _personService.HämtaKund(NyKund().Personnummer);
            _personService.SkapaKund(NyKund());
            
            MessageBox.Show("Kund skapad");
            ÅterställFält();
            LaddaAllaKunder();
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e, Label label)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 10)
                label.Foreground = Brushes.Red;
            else
                label.Foreground = Brushes.Black;

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


        private void KolumnRubrik_OnClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow rad = sender as DataGridRow;
            if (rad != null)
            {

                var item = rad.Item as dynamic;

                if (item != null)
                {
                    Personnummer.Text = item.Personnummer;
                    Personnummer.IsEnabled = false;
                    Adress.Text = item.Adress;
                    Namn.Text = item.Namn;
                    TelefonNr.Text = "0" + item.TelefonNr.ToString();
                    Epost.Text = item.Epost;
                }
            }
        }


        private void BtnÅterställ_OnClick_Click(object sender, RoutedEventArgs e)
        {
            ÅterställFält();
        }
    }
}
