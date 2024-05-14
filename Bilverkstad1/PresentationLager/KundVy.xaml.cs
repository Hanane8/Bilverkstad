using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
using Bilverkstad1.ViewModel;
using Entitetslager.Entiteter;
using Microsoft.Extensions.DependencyInjection;

namespace Bilverkstad.PresentationLager
{
    /// <summary>
    /// Interaction logic for KundVy.xaml
    /// </summary>
    public partial class KundVy : Window
    {
        public static IServiceProvider _serviceProvider;
        private PersonService _personService;
        private BokningsService _bokingService;

        public KundVy(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            
        }

        private void Siffror_Kontroll(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
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
            var window = GetWindow(this);
            window.Close();
        }
      
        private void Personnummer_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 12)
            {
                PersonnummerLbl.Foreground = Brushes.Red;

            }
            else
            {
                //En egen brush med egen färg
                Color customColor = Color.FromRgb(255, 170, 238);
                SolidColorBrush brush = new SolidColorBrush(customColor);
                PersonnummerLbl.Foreground = brush;
            }

        }

        private void TelefonNr_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 10)
            {
                TelefonnrLbl.Foreground = Brushes.Red;

            }
            else
            {
                //En egen brush med egen färg
                Color customColor = Color.FromRgb(255, 170, 238);
                SolidColorBrush brush = new SolidColorBrush(customColor);
                TelefonnrLbl.Foreground = brush;
            }

        }

        /// <summary>
        /// Metoden som sköter vad som händer om man trycker på en rad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="selectionChangedEventArgs"></param>
        private void KolumnRubrik_OnClick(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            //try
            //{ 
            //    //Tar in vald rad och gör om till önskat format
            //    object rad = KunderDataGrid.SelectedItems[0];
            //    var valdRad = new
            //    {
            //        Personnummer = ((dynamic)rad).Personnummer,
            //        Namn = ((dynamic)rad).Namn,
            //        Adress = ((dynamic)rad).Adress,
            //        TelefonNr = ((dynamic)rad).TelefonNr,
            //        Epost = ((dynamic)rad).Epost,
            //        RegNr = ((dynamic)rad).Bilar
            //    };
            //    Bil bil = SökBil(valdRad.RegNr);
            //    //Om bilen inte redan finns
            //    if (valdRad != null)
            //    {
                    
            //        Personnummer.Text = valdRad.Personnummer;
            //        Personnummer.IsEnabled = false;
            //        Adress.Text = valdRad.Adress;
            //        Namn.Text = valdRad.Namn;
            //        TelefonNr.Text = "0" + valdRad.TelefonNr.ToString();
            //        Epost.Text = valdRad.Epost;
            //        RegNr.Text = bil.RegNr;
            //        Märke.Text = bil.Märke;
            //        Årsmodell.Text = bil.Årsmodell.ToString();
            //    }
            //}
            //catch { }
        }

       
    }
}
