using System.Configuration;
using System.Data;
using System.Windows;

namespace Text_Rpg
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SettingsDictionary.InitializeSettings();
            SettingsDictionary.LoadSettings();
        }

        private void InitializeSettings()
        {
            // Add code here to initialize any additional settings
        }
    }
}

