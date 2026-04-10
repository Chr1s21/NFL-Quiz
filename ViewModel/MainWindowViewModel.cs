using NFL_Quiz.MVVM;
using System.Printing;
using System.Windows;
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
            IsDarkMode = false;
        }

        private void UpdateTheme(bool isDarkMode)
        {
            // 1. Den richtigen Pfad festlegen (basierend auf deiner Projektstruktur)
            // Laut deinen Dateien liegen sie im Ordner "Ressources" (mit Doppel-s)
            string themeName = isDarkMode ? "DarkMode.xaml" : "LightMode.xaml";
            string uriPath = $"pack://application:,,,/Ressources/{themeName}";

            var newDict = new ResourceDictionary { Source = new Uri(uriPath) };

            // 2. Das bestehende Dictionary in der App finden und ersetzen
            var dictionaries = Application.Current.Resources.MergedDictionaries;

            // Wir suchen nach einem Dictionary, das entweder LightMode oder DarkMode im Namen hat
            var oldDict = dictionaries.FirstOrDefault(d =>
                d.Source != null &&
                (d.Source.OriginalString.Contains("LightMode.xaml") ||
                 d.Source.OriginalString.Contains("DarkMode.xaml")));

            if (oldDict != null)
            {
                int index = dictionaries.IndexOf(oldDict);
                dictionaries[index] = newDict; // An der gleichen Stelle austauschen
            }
            else
            {
                dictionaries.Add(newDict); // Falls keins gefunden wurde, neu hinzufügen
            }
        }



    }
}
