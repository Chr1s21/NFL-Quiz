using NFL_Quiz.MVVM;
using System.Windows.Input;

namespace NFL_Quiz.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowGameCommand { get; }
        public ICommand ShowSettingsCommand { get; }

        public MainWindowViewModel()
        {
            CurrentViewModel = new GamePageViewModel();

            ShowGameCommand = new RelayCommand(o => CurrentViewModel = new GamePageViewModel());
            ShowSettingsCommand = new RelayCommand(o => CurrentViewModel = new SettingsPageViewModel());
        }
    }
}
