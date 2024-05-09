using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Text_Rpg.Data;
using Text_Rpg.UI;
using Text_Rpg.UI.Themes;


namespace Text_Rpg.CharacterCreator
{
    /// <summary>
    /// Interaction logic for CharacterCreatorMenu.xaml
    /// </summary>
    public partial class CharacterCreatorMenu : Window
    {
        private bool isClosing = false;
        private CreatorStatsManager statsManager = new CreatorStatsManager();

        private bool isStatsSelectionChanged = false;

        public CharacterCreatorMenu()
        {
            InitializeComponent();

            InitializeDropdowns();

            OriginsDropdown.SelectionChanged += OnDropdownSelectionChanged;
            RacesDropdown.SelectionChanged += OnDropdownSelectionChanged;
            MotivationsDropdown.SelectionChanged += OnDropdownSelectionChanged;
            PerksDropdown.SelectionChanged += OnDropdownSelectionChanged;
            StatsDropdown.SelectionChanged += OnDropdownSelectionChanged;

            this.Loaded += CharacterCreatorMenu_Loaded;
            this.Closing += OnWindowClosing;

            AddStatButton.Click += AddStatButton_Click;
            RemoveStatButton.Click += RemoveStatButton_Click;
        }

        private void InitializeDropdowns()
        {
            InitializeDropdown(PronounsDropdown, CharacterDataDictionary.Pronouns, "Choose the pronouns the player identifies with.");
            InitializeDropdown<string[]>(OriginsDropdown, CharacterDataDictionary.Origins.ToDictionary(kv => kv.Key, kv => kv.Value as string[]!)!, "Select the player's origin story.");
            InitializeDropdown<string[]>(RacesDropdown, CharacterDataDictionary.Races.ToDictionary(kv => kv.Key, kv => kv.Value as string[]!)!, "Pick the player's race.");
            InitializeDropdown<string[]>(PerksDropdown, PerksDictionary.Perks.ToDictionary(kv => kv.Key, kv => kv.Value as string[]!)!, "Grant the player a special starting ability.");
            InitializeDropdown<string[]>(MotivationsDropdown, CharacterDataDictionary.Motivations.ToDictionary(kv => kv.Key, kv => kv.Value as string[]!)!, "Define the driving force behind the player's actions.");
            InitializeDropdown<string[]>(StatsDropdown, CharacterDataDictionary.Stats.ToDictionary(kv => kv.Key, kv => kv.Value as string[]!)!, "Modify the player's core attributes.");
        }

        private void InitializeDropdown<T>(ComboBox dropdown, IReadOnlyDictionary<string, T> dataDictionary, string tooltip)
        {
            dropdown.Items.Clear(); // Clear existing items

            foreach (var item in dataDictionary.Keys)
            {
                dropdown.Items.Add(item);
            }

            dropdown.ToolTip = tooltip;
            dropdown.SelectedIndex = dropdown == StatsDropdown ? 0 : -1; // Set selected index based on dropdown type
        }
        private void CharacterCreatorMenu_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the selected theme name from the settings
            string? selectedThemeName = SettingsDictionary.GetSetting("Theme");

            // Create an instance of the OptionsWindow to access the UpdateWindowTheme method
            OptionsWindow optionsWindow = new OptionsWindow();

            // Call the UpdateWindowTheme method with the selected theme name
            optionsWindow.UpdateWindowTheme(selectedThemeName);
        }

        private void OnDropdownSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string? selectedItem = (sender as ComboBox)?.SelectedItem?.ToString();

            if (selectedItem != null)
            {
                // Check which dropdown triggered the event and perform the corresponding actions
                if (sender == OriginsDropdown)
                {
                    isStatsSelectionChanged = false;
                    UpdateDescriptionBlock(CharacterDataDictionary.Origins, selectedItem);
                    TrackStatModifiers(CharacterDataDictionary.Origins, selectedItem);
                }
                else if (sender == RacesDropdown)
                {
                    isStatsSelectionChanged = false;
                    UpdateDescriptionBlock(CharacterDataDictionary.Races, selectedItem);
                    TrackStatModifiers(CharacterDataDictionary.Races, selectedItem);
                }
                else if (sender == MotivationsDropdown)
                {
                    isStatsSelectionChanged = false;
                    UpdateDescriptionBlock(CharacterDataDictionary.Motivations, selectedItem);
                }
                else if (sender == PerksDropdown)
                {
                    isStatsSelectionChanged = false;
                    UpdateDescriptionBlock(PerksDictionary.Perks.ToDictionary(kv => kv.Key.ToString(), kv => kv.Value as object), selectedItem);
                }
                else if (sender == StatsDropdown)
                {
                    isStatsSelectionChanged = true;
                    UpdateDescriptionBlock(CharacterDataDictionary.Stats, selectedItem);
                }
            }
        }

        private void AddStatButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatsDropdown.SelectedItem != null)
            {
                isStatsSelectionChanged = true;
                string? statName = StatsDropdown.SelectedItem?.ToString();
                if (statName != null)
                {
                    statsManager.AddStat(statName, 1);
                    UpdateDescriptionBlock(CharacterDataDictionary.Stats, statName);
                }
            }
        }

        private void RemoveStatButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatsDropdown.SelectedItem != null)
            {
                isStatsSelectionChanged = true;
                string? statName = StatsDropdown.SelectedItem.ToString();
                if (statName != null)
                {
                    statsManager.RemoveStat(statName, 1);
                    UpdateDescriptionBlock(CharacterDataDictionary.Stats, statName);
                }
            }
        }

        private void UpdateDescriptionBlock(IReadOnlyDictionary<string, object> dataDictionary, string selectedItem)
        {
            // Check if the selected item exists in the data dictionary
            if (dataDictionary.ContainsKey(selectedItem))
            {
                var characterData = (Dictionary<string, object>)dataDictionary[selectedItem];

                // Extract the necessary data from the characterData dictionary
                string description = (string)characterData["Description"];
                string traits = characterData.ContainsKey("Traits") ? (string)characterData["Traits"] : string.Empty;
                string drawbacks = characterData.ContainsKey("Drawbacks") ? (string)characterData["Drawbacks"] : string.Empty;
                string goal = characterData.ContainsKey("Goal") ? (string)characterData["Goal"] : string.Empty;
                string effect = characterData.ContainsKey("Effect") ? (string)characterData["Effect"] : string.Empty;
                string stat = characterData.ContainsKey("Stat") ? (string)characterData["Stat"] : string.Empty;

                StringBuilder formattedDescription = new StringBuilder();
                formattedDescription.AppendLine($"{description}");

                // Add stat values and remaining points to the formatted description if the stats selection has changed
                if (isStatsSelectionChanged)
                {
                    formattedDescription.AppendLine("\n\nStat Values:");
                    int totalStatPointsSpent = statsManager.GetTotalStatPoints();
                    int remainingPoints = CreatorStatsManager.MaxStatTotal - totalStatPointsSpent;

                    foreach (var statEntry in statsManager.stats)
                    {
                        int statValue = statsManager.GetStatValue(statEntry.Key);
                        int bonusStatValue = 0;

                        // Check if the selected dropdowns have corresponding modifiers in the CharacterDataDictionary
                        if (OriginsDropdown.SelectedItem != null && CharacterDataDictionary.Origins.TryGetValue(OriginsDropdown.SelectedItem.ToString(), out var originData) && originData is Dictionary<string, object> originDataDictionary)
                        {
                            if (originDataDictionary.ContainsKey("TotalStatChanges") && originDataDictionary["TotalStatChanges"] is Dictionary<string, int> originStatChanges && originStatChanges.ContainsKey(statEntry.Key))
                            {
                                bonusStatValue += originStatChanges[statEntry.Key];
                            }
                        }
                        if (RacesDropdown.SelectedItem != null && CharacterDataDictionary.Races.TryGetValue(RacesDropdown.SelectedItem.ToString(), out var raceData) && raceData is Dictionary<string, object> raceDataDictionary)
                        {
                            if (raceDataDictionary.ContainsKey("TotalStatChanges") && raceDataDictionary["TotalStatChanges"] is Dictionary<string, int> raceStatChanges && raceStatChanges.ContainsKey(statEntry.Key))
                            {
                                bonusStatValue += raceStatChanges[statEntry.Key];
                            }
                        }

                        if (bonusStatValue != 0) // Check if the bonusStatValue is not equal to 0
                        {
                            string bonusStatValueString = bonusStatValue >= 0 ? $"+{bonusStatValue}" : $"{bonusStatValue}";
                            formattedDescription.AppendLine($"{statEntry.Key}: {statValue} ({bonusStatValueString})");
                        }
                        else
                        {
                            formattedDescription.AppendLine($"{statEntry.Key}: {statValue}");
                        }
                    }

                    formattedDescription.AppendLine($"\nRemaining Points: {remainingPoints}");
                }

                // Add traits to the formatted description if they exist
                if (!string.IsNullOrEmpty(traits))
                {
                    formattedDescription.AppendLine($"\nTraits: {traits}");
                }
                // Add drawbacks to the formatted description if they exist
                if (!string.IsNullOrEmpty(drawbacks))
                {
                    formattedDescription.AppendLine($"\nDrawbacks: {drawbacks}");
                }
                // Add goal to the formatted description if it exists
                if (!string.IsNullOrEmpty(goal))
                {
                    formattedDescription.AppendLine($"\nGoal: {goal}");
                }
                // Add effect to the formatted description if it exists
                if (!string.IsNullOrEmpty(effect))
                {
                    formattedDescription.AppendLine($"\nEffect: {effect}");
                }

                // Update the CreatorDescriptionBlock with the formatted description and make it visible
                CreatorDescriptionBlock.Text = formattedDescription.ToString();
                CreatorDescriptionBlock.Visibility = Visibility.Visible;
            }
        }

        private void TrackStatModifiers(IReadOnlyDictionary<string, object> dataDictionary, string selectedItem)
        {
            if (dataDictionary.ContainsKey(selectedItem))
            {
                var characterData = (Dictionary<string, object>)dataDictionary[selectedItem];

                // Check if the characterData dictionary contains the "StatModifiers" key
                if (characterData.ContainsKey("StatModifiers"))
                {
                    var statModifiers = (Dictionary<string, int>)characterData["StatModifiers"];

                    // Loop through the stat modifiers and track them
                    foreach (var statModifier in statModifiers)
                    {
                        // You can do whatever you want with the stat modifiers here
                        // For example, you can update the statsManager with the new modifiers
                        statsManager.AddStat(statModifier.Key, statModifier.Value);
                    }
                }
            }
        }

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosing)
            {
                MainMenu mainMenu = new MainMenu();
                mainMenu.Width = this.Width;
                mainMenu.Height = this.Height;
                mainMenu.WindowState = this.WindowState;
                mainMenu.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                mainMenu.Show();

                isClosing = true;
            }
        }

        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            isClosing = true;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if all dropdowns have been chosen
            if (OriginsDropdown.SelectedItem != null &&
                RacesDropdown.SelectedItem != null &&
                MotivationsDropdown.SelectedItem != null &&
                PerksDropdown.SelectedItem != null &&
                StatsDropdown.SelectedItem != null &&
                !string.IsNullOrEmpty(PlayerNameTextBox.Text))
            {
                // Check if all skill points have been used
                if (statsManager.GetTotalStatPoints() == CreatorStatsManager.MaxStatTotal)
                {
                    // Open the CreatorConfirmMenu
                    ShowPopup();
                }
            }
        }

        private void ShowPopup()
        {
            // Calculate the center position relative to the main window
            double popupWidth = CreatorConfirmMenu.ActualWidth;
            double popupHeight = CreatorConfirmMenu.ActualHeight;
            double ownerLeft = this.Left + (this.ActualWidth - popupWidth) / 2;
            double ownerTop = this.Top + (this.ActualHeight - popupHeight) / 2;

            // Set the popup's position and open it
            CreatorConfirmMenu.PlacementTarget = this;
            CreatorConfirmMenu.Placement = PlacementMode.Center;
            CreatorConfirmMenu.IsOpen = true;
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            CreatorConfirmMenu.IsOpen = false;
        }

        private void PlayerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
    
