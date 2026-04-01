using NFL_Quiz.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace NFL_Quiz.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private object currentPage;

        public object CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowGameCommand { get; }
        public ICommand ShowSettingsCommand { get; }
        public MainWindowViewModel() 
        {
            CurrentPage = new GamePage();

            ShowGameCommand = new RelayCommand(o => CurrentPage = new GamePage());

            ShowSettingsCommand = new RelayCommand(o => CurrentPage = new SettingsPage());
        }

    }
}
