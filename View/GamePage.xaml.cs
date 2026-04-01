using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;
using FuzzySharp;
using NFL_Quiz.Model;
using NFL_Quiz.ViewModel;

namespace NFL_Quiz
{
    /// <summary>
    /// Interaktionslogik für GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private List<TextBlock> name_tb;
        private List<TextBlock> trikotnr_tb;
        private List<TextBlock> position_tb;
        private List<TextBlock> team_tb;
        private List<TextBlock> conference_tb;
        private List<TextBlock> division_tb;
        private Player _randomPlayer;
        int i = 0;
        private ObservableCollection<Player> _players;
        public GamePage()
        {
            InitializeComponent();
           


            name_tb = new List<TextBlock> { tb20, tb30, tb40, tb50, tb60, tb70, tb80 };
            trikotnr_tb = new List<TextBlock> { tb21, tb31, tb41, tb51, tb61, tb71, tb81 };
            position_tb = new List<TextBlock> { tb22, tb32, tb42, tb52, tb62, tb72, tb82 };
            team_tb = new List<TextBlock> { tb23, tb33, tb43, tb53, tb63, tb73, tb83 };
            conference_tb = new List<TextBlock> { tb24, tb34, tb44, tb54, tb64, tb74, tb84 };
            division_tb = new List<TextBlock> { tb25, tb35, tb45, tb55, tb65, tb75, tb85 };

            string path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "NflPlayer.json"
            );

            _players = PlayerLoad.LoadPlayers(path);

            _randomPlayer = _players[Random.Shared.Next(_players.Count)];
        }

        private void initUI()
        {
            _randomPlayer = _players[Random.Shared.Next(_players.Count)];
            foreach (UIElement element in MainGrid.Children)
            {
                int row = Grid.GetRow(element);
                if (row >= 2) // alles ab Zeile 2 zurücksetzen
                {
                    if (element is TextBlock tb)
                    {
                        tb.Text = "";
                        tb.Background = Brushes.Transparent;
                    }
                }
            }
            i = 0;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string guess = txtInput.Text.Trim();

            Player player = _players
                .FirstOrDefault(p => p.Name.Contains(guess, StringComparison.OrdinalIgnoreCase));

            if (player != null)
            {
                if (player.Name == _randomPlayer.Name)
                {
                    name_tb[i].Text = player.Name;
                    name_tb[i].Background = Brushes.Lime;
                }
                else
                {
                    name_tb[i].Text = player.Name;
                    name_tb[i].Background = Brushes.Red;
                }
                int playertnr = int.Parse(player.Trikotnr);
                int randomPlayertnr = int.Parse(_randomPlayer.Trikotnr);

                if (playertnr == randomPlayertnr)
                {
                    trikotnr_tb[i].Text = player.Trikotnr;
                    trikotnr_tb[i].Background = Brushes.Lime;

                }
                else if (playertnr >= randomPlayertnr)
                {
                    trikotnr_tb[i].Text = player.Trikotnr + "↓";
                    trikotnr_tb[i].Background = Brushes.Red;
                }
                else
                {
                    trikotnr_tb[i].Text = player.Trikotnr + "↑";
                    trikotnr_tb[i].Background = Brushes.Red;
                }


                if (player.Position == _randomPlayer.Position)
                {
                    position_tb[i].Text = player.Position;
                    position_tb[i].Background = Brushes.Lime;
                }
                else
                {
                    position_tb[i].Text = player.Position;
                    position_tb[i].Background = Brushes.Red;
                }

                if (player.Team == _randomPlayer.Team)
                {
                    team_tb[i].Text = player.Team;
                    team_tb[i].Background = Brushes.Lime;
                }
                else
                {
                    team_tb[i].Text = player.Team;
                    team_tb[i].Background = Brushes.Red;
                }

                if (player.Conference == _randomPlayer.Conference)
                {
                    conference_tb[i].Text = player.Conference;
                    conference_tb[i].Background = Brushes.Lime;
                }
                else
                {
                    conference_tb[i].Text = player.Conference;
                    conference_tb[i].Background = Brushes.Red;
                }

                if (player.Division == _randomPlayer.Division)
                {
                    division_tb[i].Text = player.Division;
                    division_tb[i].Background = Brushes.Lime;
                }
                else
                {
                    division_tb[i].Text = player.Division;
                    division_tb[i].Background = Brushes.Red;
                }
               
                if (player == _randomPlayer)
                {
                    SystemSounds.Hand.Play();
                    MessageBox.Show("Glückwunsch! Du hast den Spieler erraten! \nDas gesuchte Spieler war: " + _randomPlayer.Name + ". \nKlicke Okay um das Spiel neu zu starten");
                    initUI();

                }

                var aktuelleFelder = new List<TextBlock>
                    {
                        name_tb[i],
                        trikotnr_tb[i],
                        position_tb[i],
                        team_tb[i],
                        conference_tb[i],
                        division_tb[i]
                    };
                i++;
                int grüneFelder = aktuelleFelder.Count(tb => tb.Background == Brushes.Lime);

                if (i == 5 && grüneFelder >= 3)
                {
                    MessageBox.Show("Du bist nah dran, du bekommst nochmal 2 Versuche");

                }
                else if(i == 5 && grüneFelder < 3)
                {
                    MessageBox.Show("Du hast alle 5 Versuche aufgebraucht! \nDas gesuchte Spieler war: " + _randomPlayer.Name + ". \nKlicke Okay um das Spiel neu zu starten");
                    initUI();
                }
                
                if (i == 7)
                {
                    MessageBox.Show("Du hast alle 7 Versuche aufgebraucht! \nDas gesuchte Spieler war: " + _randomPlayer.Name + ". \nKlicke Okay um das Spiel neu zu starten");
                    initUI();
                }
            }
            else
            {
                SystemSounds.Asterisk.Play();
                MessageBox.Show("Der Spieler wurde nicht gefunden. \nBitte versuche es erneut.");
            }

            txtInput.Text = "";
        }

        private void txtInput_Changed(object sender, TextChangedEventArgs e)
        {
            string input = txtInput.Text.Trim();
            if (input.Length < 3)
            {
                lstSuggestions.Visibility = Visibility.Collapsed;
                return;
            }

            var namesOnly = _players.Select(p => p.Name).ToList();
            var results = FuzzySharp.Process.ExtractTop(input, namesOnly, limit: 3)
                            .Where(m => m.Score >= 70)
                            .Select(m => m.Value)
                            .ToList();

            if (results.Any())
            {
                lstSuggestions.ItemsSource = results;
                lstSuggestions.Visibility = Visibility.Visible;
            }
            else
            {
                lstSuggestions.Visibility = Visibility.Collapsed;
            }
        }

        private void lstSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSuggestions.SelectedItem != null)
            {
                txtInput.Text = lstSuggestions.SelectedItem.ToString();
                lstSuggestions.Visibility = Visibility.Collapsed;
                btnSubmit_Click(null, null);
            }
        }
    }
}

