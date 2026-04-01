using NFL_Quiz.MVVM;
using NFL_Quiz.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace NFL_Quiz.ViewModel
{
    internal class GamePageViewModel : ViewModelBase
    {
        private Player targetPlayer;
        private string seartchText;
        private ObservableCollection<string> suggestions;
        private ObservableCollection<GuessResult> guesses;
        private List<Player> allPlayers;

        public string SearchText
        {
            get => seartchText;
            set { seartchText = value; OnPropertyChanged(); UpdateSuggestions(); }
        }

        public ObservableCollection<string> Suggestions
        {
            get => suggestions;
            set { suggestions = value; OnPropertyChanged(); }
        }

        public ObservableCollection <GuessResult> Guesses => guesses;

        public ICommand SubmitCommand { get; }

        public GamePageViewModel() 
        { 
            guesses = new ObservableCollection<GuessResult>();
            suggestions = new ObservableCollection<string>();

            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "NflPlayer.json");
            allPlayers = PlayerLoad.LoadPlayers(path).ToList();

            StartNewGame();
            SubmitCommand = new RelayCommand(o => ExecuteSubmit());
        }

        private void StartNewGame()
        {
            guesses.Clear();
            targetPlayer = allPlayers[Random.Shared.Next(allPlayers.Count)];
        }

        private void UpdateSuggestions()
        {
            if(string.IsNullOrWhiteSpace(SearchText) || SearchText.Length < 3)
            {
                Suggestions = new ObservableCollection<string>();
                return;
            }

            var results = FuzzySharp.Process.ExtractTop(SearchText, allPlayers.Select(p => p.Name), limit: 3)
                            .Where(r => r.Score >= 70)
                            .Select(r => r.Value);
            Suggestions = new ObservableCollection<string>(results);
        }

        private void ExecuteSubmit()
        {
            var guessedPlayer = allPlayers.FirstOrDefault(p => p.Name.Equals(SearchText, StringComparison.OrdinalIgnoreCase));
            if (guessedPlayer == null)
            {
                return;
            }
            var result = new GuessResult { Player = guessedPlayer };
            result.nameBrush = guessedPlayer.Name == targetPlayer.Name ? Brushes.Green : Brushes.Red;
            int gNr = int.Parse(guessedPlayer.Trikotnr);
            int tNr = int.Parse(targetPlayer.Trikotnr);
            if (gNr == tNr) 
            {
                result.trikotBrush = Brushes.Lime; result.trikotDisplay = gNr.ToString();
            }
            else 
            {
                result.trikotBrush = Brushes.Red;
                result.trikotDisplay = gNr + (gNr > tNr ? " ↓" : " ↑");
            }

            if (guessedPlayer.Position == targetPlayer.Position)
            { 
                result.positionBrush = Brushes.Lime;
            }
            else
            {
                result.positionBrush = Brushes.Red;
            }

            if(guessedPlayer.Team == targetPlayer.Team)
            {
                result.teamBrush = Brushes.Lime;
            }
            else
            {
                result.teamBrush = Brushes.Red;
            }

            if (guessedPlayer.Conference == targetPlayer.Conference)
            {
                result.conferenceBrush = Brushes.Lime;
            }
            else
            {
                result.conferenceBrush = Brushes.Red;
            }

            if(guessedPlayer.Division == targetPlayer.Division)
            {
                result.divisionBrush = Brushes.Lime;
            }
            else
            {
                result.divisionBrush = Brushes.Red;
            }

            Guesses.Add(result);
            SearchText = "";

            if(guessedPlayer.Name == targetPlayer.Name)
            {
                MessageBox.Show($"Congratulations! You've guessed the player {targetPlayer.Name} correctly in {Guesses.Count} attempts!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                StartNewGame();
            }

        }
    }
}
