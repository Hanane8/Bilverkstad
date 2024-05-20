using Affärslager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Bilverkstad.PresentationLager;
using DataLager;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Bilverkstad1.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public ICommand LoginCommand { get; }
        public ServiceProvider serviceProvider;
        private readonly PersonService _personService;

        private string _username;
        private string _password;

        public string UserName
        {
            get { return _username; }

            set
            {
                _username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public LoginViewModel()
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
            LoginCommand = new RelayCommand(VeriferaLogin);
        }
        
        //TODO
        // Fixa textbox så att texten är gömd
        private void VeriferaLogin(object x)
        {
            Menu menuWindow = new Menu(serviceProvider);

            //IntPtr valuePtr = Marshal.SecureStringToGlobalAllocUnicode(_password);

            //Kontrollerar om informationen är giltig eller inte
            //if (_personService.VerifieraInloggning(_username, Marshal.PtrToStringUni(valuePtr)))
            //{

            //    menuWindow.ShowDialog();
            //}
            if (_personService.VerifieraInloggning(_username, _password))
            {

                menuWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Inkorrekt information");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      

    }
}
