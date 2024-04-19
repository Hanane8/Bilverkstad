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
    /// <summary>
    /// Interaction logic for ReservDelVy.xaml
    /// </summary>
    public partial class ReservDelVy : Window
    {
        public ReservDelVy()
        {
            InitializeComponent();
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

        }

        private void BtnUppdatera_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSpara_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
