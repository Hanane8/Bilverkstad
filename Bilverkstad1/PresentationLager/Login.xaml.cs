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
                .BuildServiceProvider();
           

            _personService = serviceProvider.GetRequiredService<PersonService>();
            var ensureCreated = serviceProvider.GetRequiredService<EntityFramework>();
            ensureCreated.Database.EnsureCreated();

            //TempContainer(ensureCreated); 

            
            //var test = ensureCreated.Journaler.Include(j => j.JournalReservDelar).ThenInclude(jr => jr.ReservDel)
            //    .FirstOrDefault(j => j.JournalNr == 2);
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
           

            if (_personService.VerifieraInloggning(textUser.Text, Marshal.PtrToStringUni(valuePtr)))
            {
                menuWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Inkorrekt information");
            }

        }

        private void TempContainer(EntityFramework entityFramework)
        {

            List<Bil> cars = new List<Bil>()
            {
                new Bil() { Årsmodell = 2013, Märke = "BMW", KundNr = 1, RegNr = "ABC123" },
                new Bil() { Årsmodell = 2015, Märke = "Toyota", KundNr = 2, RegNr = "DEF456" },
                new Bil() { Årsmodell = 2010, Märke = "Honda", KundNr = 3, RegNr = "GHI789" },
                new Bil() { Årsmodell = 2018, Märke = "Ford", KundNr = 4, RegNr = "JKL012" },
                new Bil() { Årsmodell = 2016, Märke = "Volvo", KundNr = 5, RegNr = "MNO345" }
            };

            List<Kund> kunder = new List<Kund>()
            {
                new Kund()
                {
                    Namn = "Noel",
                    Adress = "Gatan 1",
                    Epost = "noel@test.com",
                    Personnummer = "0303030303",
                    TelefonNr = 0701234567,
                },
                new Kund()
                {
                    Namn = "Emma",
                    Adress = "Storgatan 5",
                    Epost = "emma@test.com",
                    Personnummer = "0404040404",
                    TelefonNr = 0702345678,
                },
                new Kund()
                {
                    Namn = "Liam",
                    Adress = "Köpmangatan 3",
                    Epost = "liam@test.com",
                    Personnummer = "0505050505",
                    TelefonNr = 0703456789,
                },
                new Kund()
                {
                    Namn = "Olivia",
                    Adress = "Lillgatan 7",
                    Epost = "olivia@test.com",
                    Personnummer = "0606060606",
                    TelefonNr = 0704567890,
                },
                new Kund()
                {
                    Namn = "William",
                    Adress = "Västergatan 12",
                    Epost = "william@test.com",
                    Personnummer = "0707070707",
                    TelefonNr = 0705678901,
                }
            };

            List<Mekaniker> mekanikerList = new List<Mekaniker>()
            {
                new Mekaniker()
                {
                    Adress = "Vägen 2",
                    Epost = "jons@gmail.com",
                    TelefonNr = 0701234566,
                    Lösenord = "test",
                    Namn = "Jöns",
                    Personnummer = "0010011234",
                    Specialisering = "Motorreparationsspecialist",
                    Yrkesroll = "Bilmekaniker"
                },
                new Mekaniker()
                {
                    Adress = "Skogsvägen 10",
                    Epost = "lisa_mekaniker@gmail.com",
                    TelefonNr = 0702345678,
                    Lösenord = "pass123",
                    Namn = "Lisa",
                    Personnummer = "0020022345",
                    Specialisering = "Däckexpert",
                    Yrkesroll = "Bilmekaniker"
                },
                new Mekaniker()
                {
                    Adress = "Havsutsikten 7",
                    Epost = "erik_mekaniker@gmail.com",
                    TelefonNr = 0703456789,
                    Lösenord = "mekaniker321",
                    Namn = "Erik",
                    Personnummer = "0030033456",
                    Specialisering = "Elbiltekniker",
                    Yrkesroll = "Bilmekaniker"
                },
                new Mekaniker()
                {
                    Adress = "Björklundsgatan 3",
                    Epost = "anna_mekaniker@gmail.com",
                    TelefonNr = 0704567890,
                    Lösenord = "mekanikerpwd",
                    Namn = "Anna",
                    Personnummer = "0040044567",
                    Specialisering = "Karossreparatör",
                    Yrkesroll = "Bilmekaniker"
                },
                new Mekaniker()
                {
                    Adress = "Ängsvägen 15",
                    Epost = "oscar_mekaniker@gmail.com",
                    TelefonNr = 0705678901,
                    Lösenord = "mekanikerpass",
                    Namn = "Oscar",
                    Personnummer = "0050055678",
                    Specialisering = "Bränslesystemexpert",
                    Yrkesroll = "Bilmekaniker"
                }
            };

            List<Bokning> bokningList = new List<Bokning>()
            {
                new Bokning()
                {
                    InlämningsDatum = DateTime.Now.AddDays(5),
                    UtlämningsDatum = DateTime.Now.AddDays(10),
                    AnställningsNr = 1,
                    KundNr = 1
                },
                new Bokning()
                {
                    InlämningsDatum = DateTime.Now.AddDays(7),
                    UtlämningsDatum = DateTime.Now.AddDays(12),
                    AnställningsNr = 2,
                    KundNr = 2
                },
                new Bokning()
                {
                    InlämningsDatum = DateTime.Now.AddDays(6),
                    UtlämningsDatum = DateTime.Now.AddDays(11),
                    AnställningsNr = 3,
                    KundNr = 3
                },
                new Bokning()
                {
                    InlämningsDatum = DateTime.Now.AddDays(8),
                    UtlämningsDatum = DateTime.Now.AddDays(13),
                    AnställningsNr = 4,
                    KundNr = 4
                },
                new Bokning()
                {
                    InlämningsDatum = DateTime.Now.AddDays(9),
                    UtlämningsDatum = DateTime.Now.AddDays(14),
                    AnställningsNr = 5,
                    KundNr = 5
                }
            };


            List<ReservDel> reservDels = new List<ReservDel>()
            {
                new ReservDel() { Namn = "Cylinder", Pris = 1000.0, Kvantitet = 30 },
                new ReservDel() { Namn = "Piston", Pris = 500.0, Kvantitet = 20 },
                new ReservDel() { Namn = "Connecting Rod", Pris = 300.0, Kvantitet = 15 },
                new ReservDel() { Namn = "Crankshaft", Pris = 800.0, Kvantitet = 10 },
                new ReservDel() { Namn = "Camshaft", Pris = 600.0, Kvantitet = 8 }
            };

            List<Journal> journals = new List<Journal>();

            for (int i = 0; i < 5; i++)
            {
                // Randomly select a car from the cars list
                Bil car = cars.ElementAt(i);

                // Randomly select a mekaniker from the mekanikerList
                Mekaniker mekaniker = mekanikerList.ElementAt(i);

                // Randomly select a bokning from the bokningList
                Bokning bokning = bokningList.ElementAt(i);

                // Randomly select up to 3 reservDel from the reservDels list
                List<ReservDel> reservDelar = reservDels.OrderBy(rd => rd.Pris).Take(3).Distinct().ToList();

                // Generate a made-up action based on the selected car, mekaniker, bokning, and reservDels
                string[] actions = new string[]
                {
                    $"Byte av olja och filter på {car.Märke} {car.Årsmodell}",
                    $"Diagnos av motorproblem på {car.Märke} {car.Årsmodell}",
                    $"Däcksbyte och hjulinställning på {car.Märke} {car.Årsmodell}",
                    $"Service och besiktning på {car.Märke} {car.Årsmodell}",
                    $"Reparation av bromssystem på {car.Märke} {car.Årsmodell}"
                };

                string action = actions[i % actions.Length];

                // Create a new Journal object
                Journal journal = new Journal()
                {
                    Åtgärder = action,
                    RegNr = car.RegNr,
                    Bokning = bokning,
                    AnställningsNr = i
                };
                JournalReservDel del1 = new JournalReservDel
                {
                    Journal = journal,
                    ReservDel = reservDelar[0]
                };
                JournalReservDel del2 = new JournalReservDel
                {
                    Journal = journal,
                    ReservDel = reservDelar[1]
                };

                JournalReservDel del3 = new JournalReservDel
                {
                    Journal = journal,
                    ReservDel = reservDelar[2]
                };
                journal.JournalReservDelar = new List<JournalReservDel> { del1, del2, del3 };
                journals.Add(journal);
            }


            foreach (var kund in kunder)
            {
                entityFramework.Kunder.Add(kund);
            }

            foreach (var bil in cars)
            {
                entityFramework.Bilar.Add(bil);
            }
            foreach (var mekaniker in mekanikerList)
            {
                entityFramework.Mekaniker.Add(mekaniker);
            }
            foreach (var reservDel in reservDels)
            {
                entityFramework.ReservDelar.Add(reservDel);
            }

            foreach (var bokning in bokningList)
            {
                entityFramework.Bokningar.Add(bokning);
            }

            foreach (var journal in journals)
            {
                entityFramework.Journaler.Add(journal);
            }
            entityFramework.SaveChanges();
        }
    }
}
