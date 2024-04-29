﻿using System;
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

        private void LaddaAllaKunder()
        {
           
            IEnumerable<Kund> kunder = _personService.HämtaAllaKunder();
            FylliFält(kunder.ToList());
            
        }

        private void FylliFält(List<Kund> kunder)
        {
            KunderDataGrid.Items.Clear();
            KunderDataGrid.Columns.Clear();
            List<string> egenskaperAttVisa = new List<string> { "Personnummer", "Namn", "Adress", "TelefonNr", "Epost", "Bokningar", "Bilar" };


            foreach (var egenskap in egenskaperAttVisa)
            {
                var kolumn = new DataGridTextColumn { Header = egenskap, Binding = new Binding(egenskap) };
                KunderDataGrid.Columns.Add(kolumn);
            }

            foreach (var kund in kunder)
            {
                var antalBokningar = _bokingService.HämtaBokning(kund).Count;
                var bilar = _personService.HämtaBilar(kund);
                string bilarRegNr = string.Join(", ", bilar.Select(b => b.RegNr));
                //var antalBokningar = kund.Bokningar?.Count ?? 0;
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
        private Bil SökBil(string regNr) => _personService.SökBil(regNr);


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
            if (kund != null)
            {
                MessageBox.Show("Kund finns");
            }
            else
            {

                _personService.SkapaKund(NyKund());
                if (SökBil(RegNr.Text) == null)
                {

                    Bil bil = new Bil()
                    {
                        Årsmodell = Convert.ToInt16(Årsmodell.Text),
                        KundNr = _personService.HämtaKund(NyKund().Personnummer).KundNr,
                        Märke = Märke.Text,
                        RegNr = RegNr.Text
                    };
                    _personService.SkapaBil(bil);
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
                    Epost = ((dynamic)rad).Epost
                };
                if (valdRad != null)
                {
                    
                        Personnummer.Text = valdRad.Personnummer;
                        Personnummer.IsEnabled = false;
                        Adress.Text = valdRad.Adress;
                        Namn.Text = valdRad.Namn;
                        TelefonNr.Text = "0" + valdRad.TelefonNr.ToString();
                        Epost.Text = valdRad.Epost;
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
