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
            set
            {
                if (seartchText != value)
                {
                    seartchText = value;
                    OnPropertyChanged();
                    UpdateSuggestions();
                }
            }
        }

        private string selectedSuggestion;
        public string SelectedSuggestion
        {
            get => selectedSuggestion;
            set
            {
                selectedSuggestion = value;
                OnPropertyChanged();

                if (!string.IsNullOrWhiteSpace(value))
                {
                    SearchText = value;
                    Suggestions.Clear();
                }
            }
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
            Suggestions.Clear();
            if (string.IsNullOrWhiteSpace(SearchText) || SearchText.Length < 3)
            {
                return;
            }

            if (allPlayers.Any(p => p.Name.Equals(SearchText, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            var results = FuzzySharp.Process.ExtractTop(SearchText, allPlayers.Select(p => p.Name), limit: 3)
                    .Where(r => r.Score >= 70)
                    .Select(r => r.Value);

            foreach (var result in results)
                Suggestions.Add(result);

        }



        private void ExecuteSubmit()
        {
            var guessedPlayer = allPlayers.FirstOrDefault(p => p.Name.Equals(SearchText, StringComparison.OrdinalIgnoreCase));
            if (guessedPlayer == null)
            {
                MessageBox.Show("Spieler nicht gefunden");
                return;
            }
            var result = new GuessResult { Player = guessedPlayer };
            result.nameBrush = guessedPlayer.Name == targetPlayer.Name ? Brushes.Lime : Brushes.Red;
            result.positionBrush = guessedPlayer.Position == targetPlayer.Position ? Brushes.Lime : Brushes.Red;
            result.teamBrush = guessedPlayer.Team == targetPlayer.Team ? Brushes.Lime : Brushes.Red;
            result.conferenceBrush = guessedPlayer.Conference == targetPlayer.Conference ? Brushes.Lime : Brushes.Red;
            result.divisionBrush = guessedPlayer.Division == targetPlayer.Division ? Brushes.Lime : Brushes.Red;

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

            Guesses.Add(result);
            SearchText = "";

            if(guesses.Count == 5)
            {
                MessageBox.Show($"Game Over! You've used all 5 attempts. The correct player was {targetPlayer.Name}.");
                StartNewGame();
                return;
            }

            if(guessedPlayer.Name == targetPlayer.Name)
            {
                MessageBox.Show($"Congratulations! You've guessed the player {targetPlayer.Name} correctly in {Guesses.Count} attempts!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                StartNewGame();
            }

        }
    }
}
