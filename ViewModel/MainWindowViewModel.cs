using NFL_Quiz.MVVM;
using System.Windows.Input;

namespace NFL_Quiz.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private object _currentView;
        private object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowGameCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        public MainWindowViewModel()
        {
            CurrentView = new GamePage();

            ShowGameCommand = new RelayCommand(o => CurrentView = new GamePage());
            ShowSettingsCommand = new RelayCommand(o => CurrentView = new SettingsPage());
        }
    }
}
