using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Text_Rpg.CharacterCreator;
using Text_Rpg.Data;
using Text_Rpg.UI.Themes;

namespace Text_Rpg.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();

            // Attach the Loaded event handler to the window
           this.Loaded += MainMenu_Loaded;
        }

        private void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the selected theme name from the settings
            string? selectedThemeName = SettingsDictionary.GetSetting("Theme");

            // Create an instance of the OptionsWindow to access the UpdateWindowTheme method
            OptionsWindow optionsWindow = new OptionsWindow();

            // Call the UpdateWindowTheme method with the selected theme name
            optionsWindow.UpdateWindowTheme(selectedThemeName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the CharacterCreatorMenu class
            CharacterCreatorMenu characterCreatorMenu = new CharacterCreatorMenu();

            // Set the size and state of the character creator menu to match this window
            characterCreatorMenu.Width = this.Width;
            characterCreatorMenu.Height = this.Height;
            characterCreatorMenu.WindowState = this.WindowState;

            // Attach an event handler to CharacterCreatorMenu's Loaded event
            characterCreatorMenu.Loaded += OnCharacterCreatorLoaded;

            // Set the startup location to center on the owner window
            characterCreatorMenu.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            // Show the new window
            characterCreatorMenu.ShowDialog();
        }

        private void OnCharacterCreatorLoaded(object sender, RoutedEventArgs e)
        {
            // This code executes when the CharacterCreatorMenu finishes loading
            this.Close(); // Reference 'this' for the current MainMenu window
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the OptionsWindow class
            OptionsWindow optionsWindow = new OptionsWindow();

            // Set the size and state of the options window to match this window
            optionsWindow.Width = this.Width / 2;
            optionsWindow.Height = this.Height / 2;
            optionsWindow.WindowState = this.WindowState;

            // Set the startup location to center on the owner window
            optionsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            // Set the owner of the options window to be this window
            optionsWindow.Owner = this;

            // Show the new window
            optionsWindow.Show();
        }
    }
}
