using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
       
      
        public Menu(string användarnamn, PasswordBox lösenord)
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

        private void BtnKund_Click(object sender, RoutedEventArgs e)
        {
            PresentationLager.KundVy kundWindow = new PresentationLager.KundVy();
            kundWindow.ShowDialog();
        }

        private void BtnReservdel_Click(object sender, RoutedEventArgs e)
        {
            PresentationLager.ReservDelVy reservdelWindow = new PresentationLager.ReservDelVy();
            reservdelWindow.ShowDialog();
        }

        private void BtnBokning_Click(object sender, RoutedEventArgs e)
        {
            PresentationLager.BokningVy bokningWindow = new PresentationLager.BokningVy();
            bokningWindow.ShowDialog();
        }
    }
}
