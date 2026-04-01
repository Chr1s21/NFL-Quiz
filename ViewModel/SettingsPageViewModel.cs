using NFL_Quiz.Model;
using NFL_Quiz.MVVM;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;


namespace NFL_Quiz.ViewModel
{
    internal class SettingsPageViewModel : ViewModelBase
    {
        private string filePath = "Data/NflPlayer.json";
        public ObservableCollection<Player> Players { get; set; }

        public ICommand AddPlayerCommand { get; }
        public ICommand DeletePlayerCommand { get; }
        public ICommand SaveCommand { get; }
        public SettingsPageViewModel()
        {
            Players = PlayerLoad.LoadPlayers(filePath);
            AddPlayerCommand = new RelayCommand(o => ExecuteAddPlayer());
            DeletePlayerCommand = new RelayCommand(p => ExecuteDeletePlayer(p));
            SaveCommand = new RelayCommand(o => ExecuteSave());

        }

        private void ExecuteAddPlayer()
        {
            Players.Add(new Player { Name = "Neuer Spieler" });
        }

        private void ExecuteDeletePlayer(object paramter)
        {
            if (paramter is Player selectedPlayer)
            {
                Players.Remove(selectedPlayer);
            }
        }

        private void ExecuteSave()
        {
            PlayerLoad.SavePlayers(filePath, Players);
            MessageBox.Show("Daten erfolgreich gespeichert!");
        }
    }
}
