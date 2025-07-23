using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pozdravlyator
{
    public class BirthdayManager
    {
        private const string FileName = "data.json";
        public List<BirthdayEntry> Entries { get; private set; } = new();

        public void SaveToFile(string fileName)
        {
            var json = JsonSerializer.Serialize(Entries, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileName, json);
        }

        public void LoadFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                Entries = JsonSerializer.Deserialize<List<BirthdayEntry>>(json) ?? new();
            }
            else
            {
                Entries = new List<BirthdayEntry>();
            }
        }


        public void AddEntry(BirthdayEntry entry) => Entries.Add(entry);

        public void RemoveEntry(int index)
        {
            if (index >= 0 && index < Entries.Count)
                Entries.RemoveAt(index);
        }

        public void EditEntry(int index, BirthdayEntry newEntry)
        {
            if (index >= 0 && index < Entries.Count)
                Entries[index] = newEntry;
        }

        public List<BirthdayEntry> GetAll() => Entries.OrderBy(e => e.DateOfBirth.Month).ThenBy(e => e.DateOfBirth.Day).ToList();

        public List<BirthdayEntry> GetUpcoming(int daysAhead = 7)
        {
            var now = DateTime.Now;
            return Entries.Where(e =>
            {
                var nextBirthday = new DateTime(now.Year, e.DateOfBirth.Month, e.DateOfBirth.Day);
                if (nextBirthday < now) nextBirthday = nextBirthday.AddYears(1);
                return (nextBirthday - now).Days <= daysAhead;
            }).OrderBy(e => e.DateOfBirth.Month).ThenBy(e => e.DateOfBirth.Day).ToList();
        }

        public List<BirthdayEntry> GetTodayBirthdays()
        {
            var today = DateTime.Today;
            return Entries.Where(e => e.DateOfBirth.Day == today.Day && e.DateOfBirth.Month == today.Month).ToList();
        }
    }
}
