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
                .BuildServiceProvider();
            
           

            _personService = serviceProvider.GetRequiredService<PersonService>();
            var ensureCreated = serviceProvider.GetRequiredService<EntityFramework>();
            ensureCreated.Database.EnsureCreated();
          


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

        private void TextUser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Btnlogin_Click(object sender, RoutedEventArgs e)
        {
           

            PresentationLager.Menu menuWindow = new PresentationLager.Menu(textUser.Text,textPass, serviceProvider);

            IntPtr valuePtr = Marshal.SecureStringToGlobalAllocUnicode(textPass.SecurePassword);
           

            if (_personService.VerifieraInloggning(textUser.Text, Marshal.PtrToStringUni(valuePtr)))
            {
                menuWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Inkorrekt information");
            }

        }

        private void TempContainer()
        {
            Kund kund = new Kund()
            {
                Namn = "Noel",
                Adress = "Gatan 1",
                Epost = "noel@test.com",
                Personnummer = "0303030303",
                TelefonNr = 0701234567
            };

            Mekaniker mekaniker = new Mekaniker()
            {
                Adress = "Vägen 2",
                Epost = "jons@gmail.com",
                TelefonNr = 0701234566,
                Lösenord = "test",
                Namn = "Jöns",
                Personnummer = "0010011234",
                Specialisering = "Motorreparationsspecialist",
                Yrkesroll = "Bilmekaniker"
            };

            Bokning bokning = new Bokning()
            {
                DatumTid = DateTime.Now,
                InlämningsDatum = DateTime.Now.AddDays(5),
                UtlämningsDatum = DateTime.Now.AddDays(10),
                AnsvarigMekaniker = mekaniker
            };

            ReservDel reservDel = new ReservDel()
            {
                Namn = "Cylinder",
                Pris = 1000.0,
                Kvantitet = 30,
            };
            
        }
    }
}
