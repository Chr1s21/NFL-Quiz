using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using NFL_Quiz.Model;
using NFL_Quiz.ViewModel;

namespace NFL_Quiz
{
    public partial class SettingsPage : Page
    {
        public ObservableCollection<Player> Players { get; set; }

        private string filePath = "Data/NflPlayer.json";

        public SettingsPage()
        {
            InitializeComponent();
            Players = PlayerLoad.LoadPlayers(filePath);
            PlayerGrid.ItemsSource = Players;
        }

        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            Players.Add(new Player { Name = "Neuer Spieler" });
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            PlayerLoad.SavePlayers(filePath, Players);
            MessageBox.Show("Daten erfolgreich gespeichert!");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerGrid.SelectedItem is Player selectedPlayer)
            {
                Players.Remove(selectedPlayer);
            }
        }
    }
}