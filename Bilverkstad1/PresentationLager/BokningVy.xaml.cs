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
    /// Interaction logic for BokningVy.xaml
    /// </summary>
    public partial class BokningVy : Window
    {
        public BokningVy()
        {
            InitializeComponent();
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

        }

        private void BtnUppdatera_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSpara_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAvboka_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BokningsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AnsvarigMekaniker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Kund_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
