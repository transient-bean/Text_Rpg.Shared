using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using Microsoft.Windows.Themes;

namespace Text_Rpg.UI.Themes
{
    internal class ThemesDictionary
    {
        public static Dictionary<string, Theme> themes = new Dictionary<string, Theme>();

        static ThemesDictionary()
        {
            Theme lightTheme = new Theme("Light")
            {
                BackgroundColor = "#FFFFFF",  // White (remains the same)
                TextColor = "#333333",      // Black (remains the same)
                ButtonColor = "#CCCCCC",    // Light gray (remains the same)
                ComboBoxColor = "#CCCCCC",    // Light gray (remains the same)
                BorderColor = "#DDDDDD",    // Very light gray (border)
                CursorColor = "#777777",      // Gray (darker gray)
                HighlightColor = "#DDDDDD",   // Very light gray (subtle highlight)
            };

            Theme darkTheme = new Theme("Dark")
            {
                BackgroundColor = "#303030",  // Light black
                TextColor = "#FFFFFF",      // White
                ButtonColor = "#666666",      // Dark gray (change to dark gray)
                ComboBoxColor = "#303030",      // Dark gray (change to dark gray)
                BorderColor = "#666666",    // Dark gray (border) (change to dark gray)
                CursorColor = "#FFFFFF",      // White (cursor) (remains the same)
                HighlightColor = "#333333",   // Dark gray (remains the same)
            };

            Theme modernTheme = new Theme("Modern")
            {
                BackgroundColor = "#ECEFF1", // Light blue-gray

                TextColor = "#000000",      // Black
                ButtonColor = "#FFA500",      // Orange (vibrant orange)
                ComboBoxColor = "#FFA500",      // Orange (vibrant orange)
                BorderColor = "#BDBDBD",    // Light gray (border)
                CursorColor = "#0000FF",      // Blue (bright blue)
                HighlightColor = "#FF00FF",   // Pink (fuchsia)
            };


            // Add more themes as needed

            themes.Add(lightTheme.Name, lightTheme);
            themes.Add(darkTheme.Name, darkTheme);
            themes.Add(modernTheme.Name, modernTheme);
        }

        public static void SaveThemes()
        {
            string themesJson = JsonSerializer.Serialize(themes);
            File.WriteAllText("settings.json", themesJson);
        }

        public static void LoadThemes()
        {
            if (File.Exists("settings.json"))
            {
                string themesJson = File.ReadAllText("settings.json");
                themes = JsonSerializer.Deserialize<Dictionary<string, Theme>>(themesJson) ?? new Dictionary<string, Theme>();
            }
            else
            {
                themes = new Dictionary<string, Theme>();
            }
        }

        public static Dictionary<string, Theme> GetThemes()
        {
            return themes;
        }
    }

    internal class Theme
    {
        public string Name { get; }
        public string? BackgroundColor { get; set; }
        public string? TextColor { get; set; }
        public string? ButtonColor { get; set; }
        public string? ComboBoxColor { get; set; }
        public string? BorderColor { get; set; }
        public string? CursorColor { get; set; }
        public string? HighlightColor { get; set; }

        public Theme(string name)
        {
            Name = name;
        }
    }
}
