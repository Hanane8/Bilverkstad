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
            Personnummer.TextChanged += Personnummer_TextChanged;
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
            List<DataGridTextColumn> kolumner = new List<DataGridTextColumn>();

            int antalBokningar = 0;

            foreach (string egenskap in egenskaperAttVisa)
            {
                DataGridTextColumn kolumn = new DataGridTextColumn();
                kolumn.Header = egenskap;
                kolumn.Binding = new Binding(egenskap);
                kolumner.Add(kolumn);
            }

            foreach (var kolumn in kolumner)
            {
                KunderDataGrid.Columns.Add(kolumn);
            }
            foreach (Kund kund in kunder)
            {
                if (kund.Bokningar != null)
                {
                    antalBokningar = kund.Bokningar.Count;
                }

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
        private void KollaFältOchUppdateraKnapp()
        {
            if (Personnummer != null && Adress != null && Namn != null && TelefonNr != null && Epost != null)
            {

                 bool allaFältOK = !string.IsNullOrEmpty(Personnummer.Text) &&
                                  !string.IsNullOrEmpty(Adress.Text) &&
                                  !string.IsNullOrEmpty(Namn.Text) &&
                                  !string.IsNullOrEmpty(TelefonNr.Text) &&
                                  !string.IsNullOrEmpty(Epost.Text);

                BtnUppdatera.IsEnabled = allaFältOK;
                BtnNykund.IsEnabled = allaFältOK;
            }
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
            string sökTerm = txtSearch.Text.Trim();

            List<Kund> sökResultat = SökResultatFrånDb(sökTerm);
            FylliFält(sökResultat);
        }

        private List<Kund> SökResultatFrånDb(string sökTerm)
        {
            return _personService.SökKund(sökTerm);
        }



        private void BtnUppdatera_Click(object sender, RoutedEventArgs e)
        {
            _personService.UppdateraKund(NyKund());

            MessageBox.Show("NyKund uppdaterad");
            ÅterställFält();
            LaddaAllaKunder();
        }


        private void Personnummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox personTextBox = (TextBox)sender;

            if (personTextBox.Text.Length != 10)
            {
                PersonnummerLbl.Foreground = Brushes.Red;
            }
            else
            {
                PersonnummerLbl.Foreground = Brushes.Black;
            }
        }

      

        private void BtnNykund_Click(object sender, RoutedEventArgs e)
        {
            Kund? kund = _personService.HämtaKund(NyKund().Personnummer);
            if (kund != null)
            {
                MessageBox.Show("NyKund finns redan");

            }
            else
            {
                _personService.SkapaKund(NyKund());

                MessageBox.Show("NyKund skapad");
                ÅterställFält();
                LaddaAllaKunder();
            }
           

        }


        private void Namn_TextChanged(object sender, TextChangedEventArgs e)
        {

            KollaFältOchUppdateraKnapp();

        }

        private void Adress_TextChanged(object sender, TextChangedEventArgs e)
        {

            KollaFältOchUppdateraKnapp();

        }

        private void TelefonNr_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox telefonnrTextBox = (TextBox)sender;

            if (TelefonNr.Text.Length != 10)
            {
                TelefonnrLbl.Foreground = Brushes.Red;
            }
            else
            {
                KollaFältOchUppdateraKnapp();
                TelefonnrLbl.Foreground = Brushes.Black;
            }

        }

        private void Epost_TextChanged(object sender, TextChangedEventArgs e)
        {
            KollaFältOchUppdateraKnapp();
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
