using System.Windows.Input;

namespace Bilverkstad1.ViewModel;

public class Commands(KundViewModel viewModel)
{
    public ICommand ClickCommand { get; } = new RelayCommand(viewModel.KolumnClick);
    public ICommand NyKundCommand { get; } = new RelayCommand(viewModel.BtnNyKund);
    public ICommand UppdateraCommand { get; } = new RelayCommand(viewModel.BtnUppdatera);
    public ICommand ÅterställCommand { get; } = new RelayCommand(viewModel.BtnÅterställ);
    public ICommand SökCommand { get; } = new RelayCommand(viewModel.SökKund);
    public ICommand ToggleCommand { get; } = new RelayCommand(viewModel.ToggleTextBoxEnabled);
}