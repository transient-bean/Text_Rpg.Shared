using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using Text_Rpg.UI.Themes;
using Text_Rpg.Data;
using System.IO;
using Microsoft.Windows.Themes;

namespace Text_Rpg.UI;

public partial class OptionsWindow : Window
{
     public OptionsWindow()
     {
        InitializeComponent();
        LoadThemes(); // Load themes before loading the selected theme
        this.Loaded += OptionsWindow_Loaded;
        SettingsDictionary.Instance.OnThemeChanged += OnThemeChanged;
     }


    private void OptionsWindow_Loaded(object sender, RoutedEventArgs e)
    {
        // Get the selected theme name from the settings
        LoadSelectedTheme();

        // Update the GUI scale with the selected value
        UpdateGuiScale((double)SettingsDictionary.settings["GuiScale"].Value);

        // Call the UpdateWindowTheme method with the selected theme name
        UpdateWindowTheme(ThemeDropdown.SelectedItem?.ToString());
    }

    private void LoadThemes()
    {
        ThemeDropdown.Items.Clear(); // Clear existing items before populating

        foreach (var theme in ThemesDictionary.GetThemes().Values)
        {
            ThemeDropdown.Items.Add(theme.Name);
        }
    }

    private void OnThemeChanged(string newThemeName)
    {
        UpdateWindowTheme(newThemeName); // Update theme based on notification
    }

    private void LoadSelectedTheme()
    {
        string? selectedThemeName = SettingsDictionary.GetSetting("Theme");
        if (selectedThemeName != null)
        {
            ThemeDropdown.SelectedItem = selectedThemeName;
        }
        else
        {
            selectedThemeName = SettingsDictionary.settings["Theme"].DefaultValue.ToString();
            ThemeDropdown.SelectedItem = selectedThemeName;
        }
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ThemeDropdown.SelectedItem != null)
        {
            string? selectedThemeName = ThemeDropdown.SelectedItem?.ToString();
            if (selectedThemeName != null)
            {
                SettingsDictionary.SetSetting(SettingsDictionary.Instance, "Theme", selectedThemeName);
                SettingsDictionary.SaveSettings(); // Save settings to file

                UpdateWindowTheme(selectedThemeName); // Update the theme of all windows
            }
        }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        ThemesDictionary.SaveThemes(); // Save themes before closing the application
    }

    // ...

    public void UpdateWindowTheme(string? newThemeName)
    {
        var windows = Application.Current.Windows;
        foreach (Window window in windows)
        {
            if (ThemesDictionary.themes.TryGetValue(newThemeName, out Theme? selectedTheme))
            {
                if (selectedTheme != null)
                {
                    // Update background color
                    if (selectedTheme.BackgroundColor != null)
                    {
                        window.SetValue(Window.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(selectedTheme.BackgroundColor)));
                    }

                    // Update text color
                    if (selectedTheme.TextColor != null)
                    {
                        // Update text color for all text elements in the window
                        UpdateElementsColor(window, selectedTheme.TextColor, (element, color) =>
                        {
                            if (element is TextBlock textBlock)
                            {
                                textBlock.SetValue(TextBlock.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)));
                            }
                        });
                    }

                    // Update button color
                    if (selectedTheme.ButtonColor != null)
                    {
                        // Update button color for all button elements in the window
                        UpdateElementsColor(window, selectedTheme.ButtonColor, (element, color) =>
                        {
                            if (element is Button button)
                            {
                                button.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)));
                            }
                        });
                    }

                    // Update combo box color
                    if (selectedTheme.ComboBoxColor != null)
                    {
                        // Update combo box color for all combo box elements in the window
                        UpdateElementsColor(window, selectedTheme.ComboBoxColor, (element, color) =>
                        {
                            if (element is ComboBox comboBox)
                            {
                                comboBox.SetValue(ComboBox.BackgroundProperty, System.Windows.SystemColors.ControlBrush);
                                comboBox.SetValue(ComboBox.FocusVisualStyleProperty, null);
                            }
                        });
                    }

                    // Update border color
                    if (selectedTheme.BorderColor != null)
                    {
                        // Update border color for all border elements in the window
                        UpdateElementsColor(window, selectedTheme.BorderColor, (element, color) =>
                        {
                            if (element is Border border)
                            {
                                border.SetValue(Border.BorderBrushProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)));
                            }
                        });
                    }

                    // Update cursor color
                    if (selectedTheme.CursorColor != null)
                    {
                        // Update cursor color for all cursor elements in the window
                        UpdateElementsColor(window, selectedTheme.CursorColor, (element, color) =>
                        {
                            if (element is TextBox textBox)
                            {
                                textBox.SetValue(TextBox.CaretBrushProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)));
                            }
                        });
                    }

                    // Update highlight color
                    if (selectedTheme.HighlightColor != null)
                    {
                        // Update highlight color for all highlight elements in the window
                        UpdateElementsColor(window, selectedTheme.HighlightColor, (element, color) =>
                        {
                            if (element is TextBox textBox)
                            {
                                textBox.SetValue(TextBox.SelectionBrushProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)));
                            }
                        });
                    }
                }
            }
            else
            {
                // If the selected theme is not found, use the default theme from SettingsDictionary
                string defaultThemeName = SettingsDictionary.GetSetting("Theme") ?? SettingsDictionary.settings["Theme"].DefaultValue.ToString();
                if (ThemesDictionary.themes.TryGetValue(defaultThemeName, out Theme? defaultTheme))
                {
                    // Apply the default theme
                    // ...
                }
            }
        }
    }

    private void UpdateElementsColor(DependencyObject parent, string color, Action<DependencyObject, string> updateAction)
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            updateAction(child, color);
            UpdateElementsColor(child, color, updateAction);
        }
    }

    private void GuiScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        double guiScaleValue = GuiScaleSlider.Value;
        double? minGuiScale = SettingsDictionary.settings["GuiScale"].MinValue;
        double? maxGuiScale = SettingsDictionary.settings["GuiScale"].MaxValue;
        object defaultGuiScale = SettingsDictionary.settings["GuiScale"].DefaultValue;

        double guiScale = Math.Max(Math.Min(guiScaleValue, maxGuiScale ?? double.MaxValue), minGuiScale ?? double.MinValue);
        GuiScaleSlider.Value = guiScale;

        // Check if the value has changed
        if (guiScale != (double)SettingsDictionary.settings["GuiScale"].Value)
        {
            // Update the setting value
            SettingsDictionary.SetSetting(SettingsDictionary.Instance, "GuiScale", guiScale.ToString());

            // Save the settings to file
            SettingsDictionary.SaveSettings();
        }

        UpdateGuiScale(guiScale);
    }

    private void UpdateGuiScale(double guiScale)
    {

        // Create a ScaleTransform object
        var scaleTransform = new ScaleTransform(guiScale, guiScale);

        // Create a TransformGroup to hold the transform
        var transformGroup = new TransformGroup();
        transformGroup.Children.Add(scaleTransform);

        // Assign the TransformGroup to Viewbox's RenderTransform
        foreach (Window window in Application.Current.Windows)
        {
            if (window.Content is Viewbox viewbox)
            {
                viewbox.RenderTransform = transformGroup;
            }


        // Update the ScaleSlider value with the new position
        GuiScaleSlider.Value = guiScale;
        }
    }

    private void OptionsResetButton_Click(object sender, RoutedEventArgs e)
    {
        SettingsDictionary.LoadSettings(); // Load the settings from file
        SettingsDictionary.SetDefaultSettings(); // Reset all settings to default values
        SettingsDictionary.SaveSettings(); // Save the updated settings to file
        UpdateWindowTheme(SettingsDictionary.GetSetting("Theme")); // Update the window theme
        UpdateGuiScale(double.Parse(SettingsDictionary.GetSetting("GuiScale"))); // Update the GUI scale
    }
}