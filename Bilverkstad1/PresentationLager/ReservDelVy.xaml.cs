using DataLager;
using Entitetslager.Entiteter;
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
        private readonly ReservDelRepository _reservDelRepository;
        public ReservDelVy()
        {
            InitializeComponent();
            _reservDelRepository = new ReservDelRepository(new EntityFramework());
            UppdateraReservDelGrid();
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

        private void BtnNyReservdel_Click(object sender, RoutedEventArgs e)
        {
            
            Namn.Text = "";
            Pris.Text = "";
            Kvantitet.Text = "";

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
                    _reservDelRepository.UppdateraReservDel(selectedReservDel);
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

        private void BtnSpara_Click(object sender, RoutedEventArgs e)
        {
            string namn = Namn.Text;
            double pris = double.Parse(Pris.Text); 
            int kvantitet = int.Parse(Kvantitet.Text);

            ReservDel newReservDel = new ReservDel
            {
                Namn = namn,
                Pris = pris,
                Kvantitet = kvantitet,
            };

            try
            {
                _reservDelRepository.SparaReservDel(newReservDel);
                MessageBox.Show("Reservdel sparad!");
                UppdateraReservDelGrid(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel vid sparande av reservdel: " + ex.Message);
            }
            ClearTextBoxes();
        }

        private void UppdateraReservDelGrid()
        {
            
            ReservDelDataGrid.ItemsSource = _reservDelRepository.HämtaAllaReservDelar();
        }
        private void ClearTextBoxes()
        {
            Namn.Text = "";
            Pris.Text = "";
            Kvantitet.Text = "";
        }


        private void ReservDelDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ReservDelDataGrid.SelectedItems.Count > 0)
            {
                ReservDel selectedReservDel = (ReservDel)ReservDelDataGrid.SelectedItems[0];

                Namn.Text = selectedReservDel.Namn;
                Pris.Text = selectedReservDel.Pris.ToString();
                Kvantitet.Text = selectedReservDel.Kvantitet.ToString();
                
            }

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        
    }
}
