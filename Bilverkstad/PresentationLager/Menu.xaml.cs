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
        private string _användarnamn { get; set; }
        private SecureString _lösenord { get; set; }

        String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
            return Marshal.PtrToStringUni(valuePtr);

        }
        public Menu(string användarnamn, PasswordBox lösenord)
        {
            //KundService service = new KundService();
            //Testa att skicka in securesträng i databasen
            _användarnamn = användarnamn;
            _lösenord = lösenord.SecurePassword;
            var test = "test";
            bool lika = test == SecureStringToString(_lösenord);
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
