using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Affärslager;
using Bilverkstad1;
using DataLager;
using Entitetslager.Entiteter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Bilverkstad.PresentationLager
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        
        private readonly PersonService _personService;
        public ServiceProvider serviceProvider; 
        public Login()
        {
            serviceProvider = new ServiceCollection()
                .AddScoped<UnitOfWork>()
                .AddScoped<PersonService>()
                .AddScoped<EntityFramework>()
                .AddScoped<ReservDelService>()
                .AddScoped<BokningsService>()
                .AddScoped<JournalService>()
                .BuildServiceProvider();
           

            _personService = serviceProvider.GetRequiredService<PersonService>();

            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState= WindowState.Minimized;

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {
           

            PresentationLager.Menu menuWindow = new PresentationLager.Menu(textUser.Text,textPass, serviceProvider);

            IntPtr valuePtr = Marshal.SecureStringToGlobalAllocUnicode(textPass.SecurePassword);
           
            //Kontrollerar om informationen är giltig eller inte
            if (_personService.VerifieraInloggning(textUser.Text, Marshal.PtrToStringUni(valuePtr)))
            {
                GetWindow(this)?.Close();
                menuWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Inkorrekt information");
            }

        }


            
        
    }
}
