using Affärslager;
using Bilverkstad1.ViewModel;
using DataLager;
using Entitetslager.Entiteter;
using Microsoft.Extensions.DependencyInjection;
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
    public partial class ReservDelVy : Window
    {
       
        public static IServiceProvider _serviceProvider;

        public ReservDelVy(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
           
            InitializeComponent();
            //UppdateraReservDelGrid();
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
            var window = Window.GetWindow(this);
            window.Close();
        }

        /*private void BtnNyReservdel_Click(object sender, RoutedEventArgs e)
        {

            ClearTextBoxes();

        }

        private void BtnUppdatera_Click(object sender, RoutedEventArgs e)
        {
           
            if (ReservDelDataGrid.SelectedItem != null)
            {
                ReservDel selectedReservDel = (ReservDel)ReservDelDataGrid.SelectedItem;
                selectedReservDel.Namn = Namn.Text;
                selectedReservDel.Pris = double.Parse(Pris.Text);
                selectedReservDel.Kvantitet = int.Parse(Kvantitet.Text);

                try
                {
                    _reservdelService.UppdateraReservDel(selectedReservDel);
                    MessageBox.Show("Reservdel uppdaterad!");
                    UppdateraReservDelGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fel vid uppdatering av reservdel: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Ingen reservdel är vald för uppdatering.");
            }
            ClearTextBoxes();

        }

        /// <summary>
        /// Här implementeras Spara funktionen där nya reservdelar
        /// kan sparas till databasen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void BtnSpara_Click(object sender, RoutedEventArgs e)
        {
            ReservDel newReservDel = new ReservDel
            {
                Namn = Namn.Text,
                Pris = double.Parse(Pris.Text),
                Kvantitet = int.Parse(Kvantitet.Text),
            };

            try
            {
                // Hämta alla befintliga reservdelar från ReservDelService
                var reservdelar = _reservdelService.HämtaAllaReservdel();

                // Kontrollera om den nya reservdelen redan finns
                bool finnsRedan = reservdelar.Any(reservdel => reservdel.Namn.Equals(newReservDel.Namn));

                if (finnsRedan)
                {
                    MessageBox.Show("Reservdel finns redan");
                }
                else
                {
                    // Om reservdelen inte redan finns, spara den
                    _reservdelService.SkapaReservDel(newReservDel);
                    MessageBox.Show("Reservdel sparad!");
                    UppdateraReservDelGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid sparande av reservdel: " + ex.Message);
            }
            ClearTextBoxes();
        }

        /// <summary>
        /// Uppdaterar fälten med korrekt information
        /// </summary>
        private void UppdateraReservDelGrid()
        {
            
            ReservDelDataGrid.ItemsSource = _reservdelService.HämtaAllaReservdel();
            ClearTextBoxes() ;
        }
        /// <summary>
        /// Rensar alla textboxar
        /// </summary>
        private void ClearTextBoxes()
        {
           Namn.Text = "";
            Pris.Text = "";
             Kvantitet.Text = "";
        }

        /// <summary>
        /// Metoden som hanterar knapp tryck på en rad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReservDelDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //Fyller fält med information
            if (ReservDelDataGrid.SelectedItems.Count > 0)
            {
                ReservDel selectedReservDel = (ReservDel)ReservDelDataGrid.SelectedItems[0];

                Namn.Text = selectedReservDel.Namn;
                Pris.Text = selectedReservDel.Pris.ToString();
                Kvantitet.Text = selectedReservDel.Kvantitet.ToString();
                
            }

        }
       
        /// <summary>
        /// Hanterar sökfunktionen när användaren kan skriva in önskat filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string sökterm = txtSearch.Text.ToLower();

            var allaReservdelar = _reservdelService.HämtaAllaReservdel();

            var matchandeReservdelar = allaReservdelar.Where(reservdel =>
                reservdel.Namn.ToLower().Contains(sökterm) 
               
            ).ToList();

            ReservDelDataGrid.ItemsSource = matchandeReservdelar;
        }*/


    }
}
