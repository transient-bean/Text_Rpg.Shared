namespace Text_Rpg.Data
{
    public static class CharacterDataDictionary
    {
        public static readonly IReadOnlyDictionary<string, string[]> Pronouns = new Dictionary<string, string[]>
        {
            ["He"] = ["he", "him", "his"],
            ["She"] = ["she", "her", "hers"],
            ["They"] = ["they", "them", "theirs"]
        };

        public static readonly IReadOnlyDictionary<string, object> Origins = new Dictionary<string, object>
        {
            ["Skyborn"] = new Dictionary<string, object>
            {
                ["Description"] = "Skyborn: \nBorn high above the clouds in a floating city, Skyborn are often diplomats, scholars, or traders. They possess a natural affinity for wind magic and a yearning for exploration.",
                ["Traits"] = "\n  Dexterity (+1) \n  Intelligence (+1)",
                ["Drawbacks"] = "\n  Strength (-1)\n  Constitution (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = -1,
                    ["Dexterity"] = 1,
                    ["Constitution"] = -1,
                    ["Intelligence"] = 1,
                    ["Wisdom"] = 0,
                    ["Charisma"] = 0
                }
            },
            ["Wastelander"] = new Dictionary<string, object>
            {
                ["Description"] = "Wastelander: \nRaised in the harsh realities of the wasteland, Wastelanders are known for their resilience and resourcefulness. They are skilled scavengers and adept at surviving in unforgiving environments.",
                ["Traits"] = "\n  Constitution (+1)\n  Wisdom (+1)",
                ["Drawbacks"] = "\n  Charisma (-1)\n  Dexterity (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = 0,
                    ["Dexterity"] = -1,
                    ["Constitution"] = 1,
                    ["Intelligence"] = 0,
                    ["Wisdom"] = 1,
                    ["Charisma"] = -1
                }
            },
            ["Nomad"] = new Dictionary<string, object>
            {
                ["Description"] = "Nomad: \nBorn into a wandering tribe, Nomads are skilled survivalists and expert trackers. They have a deep connection to the land and possess a wealth of knowledge about the wilderness.",
                ["Traits"] = "\n  Wisdom (+1)\n  Dexterity (+1)",
                ["Drawbacks"] = "\n  Intelligence (-1)\n  Charisma (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = 0,
                    ["Dexterity"] = 1,
                    ["Constitution"] = 0,
                    ["Intelligence"] = -1,
                    ["Wisdom"] = 1,
                    ["Charisma"] = -1
                }
            },
            ["Tech Savant"] = new Dictionary<string, object>
            {
                ["Description"] = "Tech Savant: \nRaised in a technologically advanced society, Tech Savants are masters of machinery and innovation. They possess a deep understanding of technology and excel in engineering and problem-solving.",
                ["Traits"] = "\n  Intelligence (+1)\n  Dexterity (+1)",
                ["Drawbacks"] = "\n  Strength (-1)\n  Wisdom (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = -1,
                    ["Dexterity"] = 1,
                    ["Constitution"] = 0,
                    ["Intelligence"] = 1,
                    ["Wisdom"] = -1,
                    ["Charisma"] = 0
                }
            },
            ["Shadowborn"] = new Dictionary<string, object>
            {
                ["Description"] = "Shadowborn: \nBorn in the shadows, Shadowborn individuals are skilled infiltrators and masters of stealth. They possess an innate ability to manipulate darkness and excel in espionage and subterfuge.",
                ["Traits"] = "\n  Dexterity (+1)\n  Charisma (+2)",
                ["Drawbacks"] = "\n  Strength (-2)\n  Constitution (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = -2,
                    ["Dexterity"] = 1,
                    ["Constitution"] = -1,
                    ["Intelligence"] = 0,
                    ["Wisdom"] = 0,
                    ["Charisma"] = 2
                }
            }
            // Add more origins here
        };

        public static readonly IReadOnlyDictionary<string, object> Races = new Dictionary<string, object>
        {
            ["Human"] = new Dictionary<string, object>
            {
                ["Description"] = "Human: \nHumans are the most adaptable and versatile race. They possess no inherent racial bonuses but can excel in any role through hard work and determination.",
                ["Traits"] = "\n  None",
                ["Drawbacks"] = "\n  None",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = 0,
                    ["Dexterity"] = 0,
                    ["Constitution"] = 0,
                    ["Intelligence"] = 0,
                    ["Wisdom"] = 0,
                    ["Charisma"] = 0
                }
            },
            ["Mutant"] = new Dictionary<string, object>
            {
                ["Description"] = "Mutant: \nMutants are individuals who have been affected by radiation and have developed unique abilities or physical mutations. They possess extraordinary powers but also face social stigma and discrimination.",
                ["Traits"] = "\n  Constitution (+2)\n  Strength (+1)",
                ["Drawbacks"] = "\n  Intelligence (-2)\n  Charisma (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = 1,
                    ["Dexterity"] = 0,
                    ["Constitution"] = 2,
                    ["Intelligence"] = -2,
                    ["Wisdom"] = 0,
                    ["Charisma"] = -1
                }
            },
            ["Scavenger"] = new Dictionary<string, object>
            {
                ["Description"] = "Scavenger: \nScavengers are survivors who have learned to thrive in the post-apocalyptic world. They excel at finding valuable resources and have honed their skills in scavenging and survival.",
                ["Traits"] = "\n  Wisdom (+1)\n  Dexterity (+1)",
                ["Drawbacks"] = "\n  Strength (+1)\n  Constitution (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = -1,
                    ["Dexterity"] = 1,
                    ["Constitution"] = -1,
                    ["Intelligence"] = 0,
                    ["Wisdom"] = 1,
                    ["Charisma"] = 0
                }
            },
            ["Cybernetic"] = new Dictionary<string, object>
            {
                ["Description"] = "Cybernetic: \nCybernetics are individuals who have integrated advanced technology into their bodies. They possess enhanced physical abilities and have access to a wide range of technological enhancements.",
                ["Traits"] = "\n  Strength (+2)\n  Intelligence (+1)",
                ["Drawbacks"] = "\n  Dexterity (-2)\n Charisma (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = 2,
                    ["Dexterity"] = -2,
                    ["Constitution"] = 0,
                    ["Intelligence"] = 1,
                    ["Wisdom"] = 0,
                    ["Charisma"] = -1
                }
            },
            ["Marauder"] = new Dictionary<string, object>
            {
                ["Description"] = "Marauder: \nMarauders are ruthless warriors who thrive in the lawless wastelands. They are skilled in combat and excel at raiding and pillaging. Fearless and brutal, they strike fear into the hearts of their enemies.",
                ["Traits"] = "\n  Wisdom (+2)\n  Strength (+1)",
                ["Drawbacks"] = "\n  Charisma (-2)\n  Intelligence (-1)",
                ["TotalStatChanges"] = new Dictionary<string, int>
                {
                    ["Strength"] = 1,
                    ["Dexterity"] = 0,
                    ["Constitution"] = 0,
                    ["Intelligence"] = -1,
                    ["Wisdom"] = 2,
                    ["Charisma"] = -2
                }
            }
            // Add more races here
        };

        public static readonly IReadOnlyDictionary<string, object> Motivations = new Dictionary<string, object>
        {
            ["The Pathfinder"] = new Dictionary<string, object>
            {
                ["Description"] = "The Pathfinder: \nDriven by an insatiable curiosity and a thirst for knowledge, the Pathfinder seeks to uncover the lost secrets of the wasteland. Ancient ruins, forgotten lore, and hidden wonders fuel their relentless exploration.",
                ["Goal"] = "Become a renowned explorer, unearth the mysteries of world."
            },
            ["The Vengeful"] = new Dictionary<string, object>
            {
                ["Description"] = "The Vengeful: \nConsumed by a burning desire for revenge, the Vengeful seeks to punish those who wronged them. Their past haunts them, fueling their determination to make their enemies pay.",
                ["Goal"] = "Track down and exact revenge on their enemies."
            },
            ["The Redeemer"] = new Dictionary<string, object>
            {
                ["Description"] = "The Redeemer: \nMotivated by a strong sense of justice and a desire to help others, the Redeemer strives to make the wasteland a better place. They fight for the downtrodden and protect the innocent from harm.",
                ["Goal"] = "Become a beacon of hope, bring order and justice to the wasteland."
            },
            ["The Survivor"] = new Dictionary<string, object>
            {
                ["Description"] = "The Survivor: \nHaving endured unimaginable hardships, the Survivor is driven by the will to live and overcome. They have witnessed the worst of the wasteland and are determined to persevere against all odds.",
                ["Goal"] = "Survive against the harsh conditions of the wasteland, find a place to call home."
            },
            ["The Opportunist"] = new Dictionary<string, object>
            {
                ["Description"] = "The Opportunist: \nAlways on the lookout for the next big opportunity, the Opportunist is driven by ambition and a desire for wealth and power. They are willing to do whatever it takes to get ahead in the wasteland.",
                ["Goal"] = "Amass wealth and influence, become a prominent figure in the wasteland."
            }
            // Add more motivations here
        };

        public static readonly IReadOnlyDictionary<string, object> Stats = new Dictionary<string, object>()
        {
            ["Strength"] = new Dictionary<string, object>()
            {
                ["Description"] = "Strength: \nPhysical power and prowess in close combat. A high strength value increases melee damage and carrying capacity.",
                ["Value"] = 5
            },
            ["Dexterity"] = new Dictionary<string, object>()
            {
                ["Description"] = "Dexterity: \nAgility, reflexes, and nimbleness. A high dexterity value improves ranged combat accuracy, evasion, and lockpicking.",
                ["Value"] = 5
            },
            ["Constitution"] = new Dictionary<string, object>()
            {
                ["Description"] = "Constitution: \nPhysical resilience and endurance. A high constitution value increases maximum health and resistance to physical damage.",
                ["Value"] = 5
            },
            ["Intelligence"] = new Dictionary<string, object>()
            {
                ["Description"] = "Intelligence: \nMental capacity for learning, reasoning, and problem-solving. A high intelligence value improves skill points gained per level and increases effectiveness of technical skills.",
                ["Value"] = 5
            },
            ["Wisdom"] = new Dictionary<string, object>()
            {
                ["Description"] = "Wisdom: \nInsight, perception, and experience. A high wisdom value improves perception, increases effectiveness of magical skills, and enhances resistance to magical effects.",
                ["Value"] = 5
            },
            ["Charisma"] = new Dictionary<string, object>()
            {
                ["Description"] = "Charisma: \nPresence, persuasiveness, and social influence. A high charisma value improves bartering, speech checks, and NPC reactions.",
                ["Value"] = 5
            },
        };
    }
}