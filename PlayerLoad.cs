using NFL_Quiz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class PlayerLoad
{
    public static List<Player> LoadPlayers(string filePath)
    {
        if (!File.Exists(filePath))
            return new List<Player>();

        string json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<Player>>(json)
               ?? new List<Player>();
    }

}
