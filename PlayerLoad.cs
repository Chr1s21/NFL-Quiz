using System;
using System.Collections.ObjectModel; 
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace NFL_Quiz
{
    public static class PlayerLoad
    {
        public static ObservableCollection<Player> LoadPlayers(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new ObservableCollection<Player>();
            }

            try
            {
                string json = File.ReadAllText(filePath);
                var playerList = JsonSerializer.Deserialize<List<Player>>(json);

                return playerList != null
                    ? new ObservableCollection<Player>(playerList)
                    : new ObservableCollection<Player>();
            }
            catch (Exception e)
            {
                return new ObservableCollection<Player>();
            }
        }

        public static void SavePlayers(string filePath, ObservableCollection<Player> players)
        {
            string json = JsonSerializer.Serialize(players, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}