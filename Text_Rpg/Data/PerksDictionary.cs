using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Rpg.Data
{
    internal class PerksDictionary
    {
        public static readonly IReadOnlyDictionary<string, object> Perks = new Dictionary<string, object>
        {
            ["Skilled Fighter"] = new Dictionary<string, object>
            {
                ["Description"] = "Skilled Fighter: \nGrants a +2 bonus to attack rolls.",
                ["Effect"] = "+2 Attack"
            },
            ["Master Scavenger"] = new Dictionary<string, object>
            {
                ["Description"] = "Master Scavenger: \nIncreases the chance of finding rare materials while scavenging.",
                ["Effect"] = "Increased chance of rare materials"
            },
            ["Stealthy Assassin"] = new Dictionary<string, object>
            {
                ["Description"] = "Stealthy Assassin: \nEnhances stealth abilities and critical hit damage.",
                ["Effect"] = "Increased stealth and critical hit damage"
            },
            ["Tech Genius"] = new Dictionary<string, object>
            {
                ["Description"] = "Tech Genius: \nMastery of technology and access to advanced gadgets.",
                ["Effect"] = "Access to advanced gadgets and increased technological skills"
            }
            // Add more perks here
        };
    }
}
