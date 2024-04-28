using Affärslager;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bilverkstad.PresentationLager
{
   
    public partial class BokningVy : Window
    {
        private BokningsService _bokningsService;
        private PersonService _personService;
       

        public BokningVy(IServiceProvider serviceProvider)
        {
            InitializeComponent();
           
            _bokningsService = (BokningsService)serviceProvider.GetService(typeof(BokningsService));
            _personService = (PersonService)serviceProvider.GetService(typeof(PersonService));
            UppdateraBokningDelGrid();
            FillComboBoxes();
        }
        private void FillComboBoxes()
        {
            
            var allaKunder = _personService.HämtaAllaKunder();
            cmbKund.ItemsSource = allaKunder;
            cmbKund.DisplayMemberPath = "Namn";

            
            var allaMekaniker = _personService.HämtaAllaMekaniker();
            cmbMekaniker.ItemsSource = allaMekaniker;
            cmbMekaniker.DisplayMemberPath = "Namn"; 
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }

        private void BtnNyTid_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void BtnUppdatera_Click(object sender, RoutedEventArgs e)
        {
            if (BokningsDataGrid.SelectedItem != null)
            {
                Bokning selectedBokning = (Bokning)BokningsDataGrid.SelectedItem;
                string inlämningsDatumStr = InlämningsDatum.Text;
                string utlämningsDatumStr = UtlämningsDatum.Text;

                DateTime inlämningsDatum;
                DateTime utlämningsDatum;

                if (DateTime.TryParse(inlämningsDatumStr, out inlämningsDatum) && DateTime.TryParse(utlämningsDatumStr, out utlämningsDatum))
                {
                    var valdKund = cmbKund.SelectedItem as Kund;
                    var valdMekaniker = cmbMekaniker.SelectedItem as Mekaniker;

                    if (valdKund != null && valdMekaniker != null)
                    {
                        selectedBokning.KundNr = valdKund.KundNr;
                        selectedBokning.AnställningsNr = valdMekaniker.AnställningsNr;
                        selectedBokning.InlämningsDatum = inlämningsDatum;
                        selectedBokning.UtlämningsDatum = utlämningsDatum;

                        try
                        {
                            _bokningsService.UppdateraBokning(selectedBokning);
                            MessageBox.Show("Bokning uppdaterad!");
                            UppdateraBokningDelGrid();
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
                    MessageBox.Show("Inmatade datum är inte giltiga. Ange datum i rätt format (ÅÅÅÅ-MM-DD).");
                }
            }
            else
            {
                MessageBox.Show("Ingen bokning är vald för uppdatering.");
            }
        }


        private void BtnSpara_Click(object sender, RoutedEventArgs e)
        {
            
            string inlämningsDatumStr = InlämningsDatum.Text;
            string utlämningsDatumStr = UtlämningsDatum.Text;

           
            DateTime inlämningsDatum;
            DateTime utlämningsDatum;

            
            if (DateTime.TryParse(inlämningsDatumStr, out inlämningsDatum) && DateTime.TryParse(utlämningsDatumStr, out utlämningsDatum))
            {
                var valdKund = cmbKund.SelectedItem as Kund;
                var valdMekaniker = cmbMekaniker.SelectedItem as Mekaniker;

                if (valdKund != null && valdMekaniker != null)
                {
                    var nyBokning = new Bokning
                    {
                        KundNr = valdKund.KundNr,
                        AnställningsNr = valdMekaniker.AnställningsNr,
                        InlämningsDatum = inlämningsDatum, 
                        UtlämningsDatum = utlämningsDatum 
                    };

                    _bokningsService.SkapaBokning(nyBokning);

                    MessageBox.Show("Bokningen har sparats!");
                }
                else
                {
                    MessageBox.Show("Välj både kund, mekaniker, inlämningsdatum och utlämningsdatum för att spara bokningen.");
                }
            }
            else
            {
                MessageBox.Show("Inmatade datum är inte giltiga. Ange datum i rätt format (ÅÅÅÅ-MM-DD).");
            }
            UppdateraBokningDelGrid();
        }
        private void UppdateraBokningDelGrid()
        {

            BokningsDataGrid.ItemsSource = _bokningsService.HämtaAllaBokningar();
            ClearTextBoxes();
        }



        private void BtnAvboka_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BokningsDataGrid.SelectedItem != null)
                {
                    Bokning selectedBokning = (Bokning)BokningsDataGrid.SelectedItem;
                    _bokningsService.AvbokaBokning(selectedBokning);
                    MessageBox.Show("Bokning avbokad!");
                    UppdateraBokningDelGrid();
                }
                else
                {
                    MessageBox.Show("Ingen bokning är vald för avbokning.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid avbokning av bokning: " + ex.Message);
            }
        }


        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BokningsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BokningsDataGrid.SelectedItems.Count > 0)
            {
                Bokning selectedBokning = (Bokning)BokningsDataGrid.SelectedItems[0];

                // Kontrollera för null innan du använder ToString() för att undvika NullReferenceException
                if (selectedBokning.InlämningsDatum != null)
                {
                    InlämningsDatum.Text = selectedBokning.InlämningsDatum.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    InlämningsDatum.Text = string.Empty; // Om InlämningsDatum är null, sätt texten till tom sträng
                }

                if (selectedBokning.UtlämningsDatum != null)
                {
                    UtlämningsDatum.Text = selectedBokning.UtlämningsDatum.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    UtlämningsDatum.Text = string.Empty; // Om UtlämningsDatum är null, sätt texten till tom sträng
                }

                // Uppdatera ComboBoxar med de valda värdena
                cmbMekaniker.SelectedItem = selectedBokning.AnställningsNr;
                cmbKund.SelectedItem = selectedBokning.KundNr;
            }
        }


        private void cmbKund_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbKund.SelectedItem != null)
            {
                Kund selectedKund = (Kund)cmbKund.SelectedItem;

                MessageBox.Show($"Vald kund: {selectedKund.Namn}, Kundnummer: {selectedKund.KundNr}");
            }

        }

        private void cmbMekaniker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMekaniker.SelectedItem != null)
            {
                Mekaniker selectedMekaniker = (Mekaniker)cmbMekaniker.SelectedItem;

                MessageBox.Show($"Vald mekaniker: {selectedMekaniker.Namn}, Anställningsnummer: {selectedMekaniker.AnställningsNr}");
            }
        }
        private void ClearTextBoxes()
        {
            InlämningsDatum.Text = "";
            UtlämningsDatum.Text = "";
            cmbMekaniker.Text = "";
            cmbKund.Text = "";
        }

        private void InlämningsDatum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UtlämningsDatum_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
