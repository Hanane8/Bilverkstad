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
using Affärslager;
using DataLager;
using Entitetslager.Entiteter;
using Microsoft.Extensions.DependencyInjection;


namespace Bilverkstad.PresentationLager
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window

    {
        
        private readonly KundService _KundService;
        public ServiceProvider serviceProvider; 
        public Login()
        {
            serviceProvider = new ServiceCollection().AddScoped<UnitOfWork>().AddScoped<KundService>().AddScoped<EntityFramework>().BuildServiceProvider();
            _KundService = serviceProvider.GetRequiredService<KundService>();
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
            
            PresentationLager.Menu menuWindow = new PresentationLager.Menu(textUser.Text,textPass);
            Kund kund = new Kund()
            {
                Namn = "Noel",
                Adress = "Gatan 1",
                Epost = "noel@test.com",
                Personnummer = "0303030303",
                TelefonNr = 0701234567
            };
            _KundService.SkapaKund(kund);
            menuWindow.ShowDialog();

        }
    }
}
