using System.Text.Json;
using Text_Rpg.UI.Themes;
using System.IO;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using Text_Rpg.Data;
using System.Xml.Serialization;
public sealed class SettingsDictionary
{
    public delegate void ThemeChangedEventHandler(string newThemeName);
    public event ThemeChangedEventHandler? OnThemeChanged;

    public static readonly SettingsDictionary instance = new SettingsDictionary();

    public static SettingsDictionary Instance
    {
        get { return instance; }
    }

    public static Dictionary<string, Setting> settings { get; private set; } = new Dictionary<string, Setting>();

    public static void InitializeSettings()
    {
        // Check if settings.xml file exists
        if (File.Exists("settings.xml"))
        {
            // Load settings from the file
            LoadSettings();
        }
        else
        {
            // If settings.xml doesn't exist, set the default values for all settings
            SetDefaultSettings();

            // Save the default settings to the file
            SaveSettings();
        }

        // Add more settings as needed
        InitializeMusicVolumeSetting();
        InitializeShowHintsSetting();
        InitializeThemeSetting();
        InitializeFullscreenSetting();
        InitializeGuiScaleSetting();
        InitializeDifficultySetting();
    }

    private static void InitializeMusicVolumeSetting()
    {
        if (!settings.ContainsKey("MusicVolume"))
        {
            Setting musicVolumeSetting = new Setting("MusicVolume");
            musicVolumeSetting.DefaultValue = 50;
            musicVolumeSetting.MinValue = 0;
            musicVolumeSetting.MaxValue = 100;
            settings.Add(musicVolumeSetting.Key, musicVolumeSetting);
        }
    }

    private static void InitializeShowHintsSetting()
    {
        if (!settings.ContainsKey("ShowHints"))
        {
            Setting showHintsSetting = new Setting("ShowHints");
            showHintsSetting.DefaultValue = true;
            settings.Add(showHintsSetting.Key, showHintsSetting);
        }
    }

    private static void InitializeThemeSetting()
    {
        if (!settings.ContainsKey("Theme"))
        {
            Setting themeSetting = new Setting("Theme");
            // Retrieve the possible theme values from the ThemesDictionary class
            List<object> possibleThemes = GetThemes();
            themeSetting.PossibleValues = possibleThemes;
            themeSetting.DefaultValue = "Dark"; // Set default theme to "Light"
            settings.Add(themeSetting.Key, themeSetting);
        }
    }

    private static void InitializeFullscreenSetting()
    {
        if (!settings.ContainsKey("Fullscreen"))
        {
            Setting fullscreenSetting = new Setting("Fullscreen");
            fullscreenSetting.DefaultValue = false;
            settings.Add(fullscreenSetting.Key, fullscreenSetting);
        }
    }

    private static void InitializeGuiScaleSetting()
    {
        if (!settings.ContainsKey("GuiScale"))
        {
            Setting guiScaleSetting = new Setting("GuiScale");
            guiScaleSetting.DefaultValue = 0.5;
            guiScaleSetting.MinValue = 0.25;
            guiScaleSetting.MaxValue = 1.0;
            settings.Add(guiScaleSetting.Key, guiScaleSetting);
        }
    }

    private static void InitializeDifficultySetting()
    {
        if (!settings.ContainsKey("Difficulty"))
        {
            Setting difficultySetting = new Setting("Difficulty");
            difficultySetting.DefaultValue = "Normal";
            difficultySetting.PossibleValues = new List<object> { "Normal", "Easy", "Hard" };
            settings.Add(difficultySetting.Key, difficultySetting);
        }
    }

    public static void LoadSettings()
    {
        if (File.Exists("settings.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Setting>));
            using (FileStream stream = File.OpenRead("settings.xml"))
            {
                var list = (List<Setting>)serializer.Deserialize(stream);
                settings = list.ToDictionary(item => item.Key, item => item);
            }
        }
        else
        {
            // If settings.xml doesn't exist, set the default values for all settings
            SetDefaultSettings();
        }
    }

    public static void SaveSettings()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Setting>));
        using (FileStream stream = File.Create("settings.xml"))
        {
            serializer.Serialize(stream, settings.Values.ToList());
        }
    }

    public static string GetSetting(string key)
    {
        if (settings.ContainsKey(key))
        {
            return settings[key].Value?.ToString() ?? string.Empty;
        }
        return string.Empty;
    }

    public static void SetSetting(SettingsDictionary instance, string key, string value)
    {
        if (key == "Theme")
        {
            SettingsDictionary.settings[key].Value = value;
            instance.OnThemeChanged?.Invoke(value);
        }
        else if (key == "GuiScale")
        {
            double guiScaleValue;
            if (double.TryParse(value, out guiScaleValue))
            {
                if (guiScaleValue >= settings[key].MinValue && guiScaleValue <= settings[key].MaxValue)
                {
                    settings[key].Value = guiScaleValue;
                }
            }
            else
            {
                Console.WriteLine("Invalid GuiScale value. Please enter a valid numeric value.");
            }
        }
        else
        {
            settings.Add(key, new Setting(key) { Value = value });
        }
    }

    public static List<object> GetThemes()
    {
        List<object> themes = new List<object>();
        ThemesDictionary themesDictionary = new ThemesDictionary();
        foreach (var theme in ThemesDictionary.themes.Values)
        {
            themes.Add(theme.Name);
        }
        return themes;
    }

    public static void SetDefaultSettings()
    {
        foreach (var setting in settings.Values)
        {
            setting.Value = setting.DefaultValue;
        }
    }
}


public class Setting
{
    public string Key { get; set; }
    public object? Value { get; set; }
    public object? DefaultValue { get; set; }
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
    public List<object>? PossibleValues { get; set; }

    public Setting()
    {

    }

    public Setting(string key)
    {

    }
}
