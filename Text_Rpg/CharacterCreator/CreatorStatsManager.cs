using Text_Rpg.Data;

namespace Text_Rpg.CharacterCreator
{
    public class CreatorStatsManager
    {
        public Dictionary<string, int> stats;
        public const int MaxStatTotal = 42; // Maximum sum of all stats

        public CreatorStatsManager()
        {
            stats = new Dictionary<string, int>(CharacterDataDictionary.Stats.Count);
            // Set all stats to a default value (e.g., 5)
            foreach (var stat in CharacterDataDictionary.Stats.Keys)
            {
                stats.Add(stat, 5);
            }
        }

        public int GetStatValue(string statName)
        {
            if (stats.TryGetValue(statName, out int value))
            {
                return value;
            }
            else
            {
                // Handle stat not found (e.g., throw an exception or return a default value)
                return 0;
            }
        }

        public bool AddStat(string statName, int value)
        {
            if (!stats.ContainsKey(statName))
            {
                return false; // Stat doesn't exist
            }

            int currentTotal = GetTotalStatPoints();
            int newStatValue = stats[statName] + value;

            // Check if adding points would exceed the maximum total
            if (currentTotal + value > MaxStatTotal)
            {
                return false; // Exceeded max points
            }

            stats[statName] = newStatValue;
            return true;
        }

        public bool RemoveStat(string statName, int value)
        {
            if (!stats.ContainsKey(statName))
            {
                return false; // Stat doesn't exist
            }

            int newStatValue = stats[statName] - value;

            // Check if removing the value would go below 0
            if (newStatValue < 0)
            {
                return false; // Can't remove more than current value
            }

            stats[statName] = newStatValue;
            return true;
        }

        public int GetTotalStatPoints()
        {
            int total = 0;
            foreach (var statValue in stats.Values)
            {
                total += statValue;
            }
            return total;
        }
    }
}
