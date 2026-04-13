using NFL_Quiz.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace NFL_Quiz.ViewModel
{
    public static class PlayerLoad
    {

        private static string connectionString = "Host=localhost;Username=postgres;Password=211337;Database=player";

        public static ObservableCollection<Player> LoadPlayers()
        {
            var players = new ObservableCollection<Player>();

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT \"name\", \"trikotnr\", \"position\", \"team\", \"conference\", \"division\" FROM nfl_spieler";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            players.Add(new Player
                            {
                                Name = reader.GetString(0),
                                Trikotnr = reader.GetString(1),
                                Position = reader.GetString(2),
                                Team = reader.GetString(3),
                                Conference = reader.GetString(4),
                                Division = reader.GetString(5)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Fehler beim Laden der Spieler: {ex.Message}");
            }

            return players;
        }

        public static void SavePlayers(ObservableCollection<Player> players)
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var trans = conn.BeginTransaction())
                    {
                        try
                        {
                            
                            using (var delCmd = new NpgsqlCommand("DELETE FROM nfl_spieler", conn))
                            {
                                delCmd.ExecuteNonQuery();
                            }

                           
                            foreach (var p in players)
                            {
                                string sql = "INSERT INTO nfl_spieler (\"name\", \"trikotnr\", \"position\", \"team\", \"conference\", \"division\") " +
                                             "VALUES (@n, @t, @pos, @team, @conf, @div)";

                                using (var cmd = new NpgsqlCommand(sql, conn))
                                {
                                    
                                    cmd.Parameters.AddWithValue("n", p.Name ?? "");
                                    cmd.Parameters.AddWithValue("t", p.Trikotnr ?? "");
                                    cmd.Parameters.AddWithValue("pos", p.Position ?? "");
                                    cmd.Parameters.AddWithValue("team", p.Team ?? "");
                                    cmd.Parameters.AddWithValue("conf", p.Conference ?? "");
                                    cmd.Parameters.AddWithValue("div", p.Division ?? "");

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            trans.Commit(); 
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Fehler beim Speichern in die Datenbank: {ex.Message}");
            }
        }

    }
}