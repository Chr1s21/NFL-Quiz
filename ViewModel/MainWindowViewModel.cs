using NFL_Quiz.MVVM;
using System.Printing;
using System.Windows.Input;

namespace NFL_Quiz.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private bool _isDarkMode;
        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                OnPropertyChanged();
                UpdateTheme(value);
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

        private void UpdateTheme(bool isDarkMode)
        {
            var app = System.Windows.Application.Current;
            if (isDarkMode)
            {
                app.Resources.MergedDictionaries.Clear();
                app.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary { Source = new Uri("pack://application:,,,/Themes/DarkMode.xaml") });
            }
            else
            {
                app.Resources.MergedDictionaries.Clear();
                app.Resources.MergedDictionaries.Add(new System.Windows.ResourceDictionary { Source = new Uri("pack://application:,,,/Themes/LightMode.xaml") });
            }
        }



    }
}
