using System.Text.Json;
using JournalApp.Models;

namespace JournalApp.Services
{
    public static class JournalStorage
    {
        private static readonly string _filePath = "App_Data/journal.json";

        public static List<JournalEntry> LoadEntries()
        {
            if (!File.Exists(_filePath))
            {
                Directory.CreateDirectory("App_Data");
                File.WriteAllText(_filePath, "[]");
            }

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<JournalEntry>>(json) ?? new List<JournalEntry>();
        }

        public static void SaveEntries(List<JournalEntry> entries)
        {
            string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
