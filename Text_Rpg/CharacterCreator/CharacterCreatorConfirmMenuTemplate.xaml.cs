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

namespace Text_Rpg.UI
{
    /// <summary>
    /// Interaction logic for CharacterCreatorConfirmMenuTemplate.xaml
    /// </summary>
    public partial class CharacterCreatorConfirmMenuTemplate : Window
    {
        public CharacterCreatorConfirmMenuTemplate()
        {
            InitializeComponent();
        }

        private void ConfirmCharacterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CancelCharacterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateCCCMLeftSide(string text)
        {
            CCCMLeftSide.Text = text;
        }

        private void UpdateCCCMRightSide(string text)
        {
            CCCMRightSide.Text = text;
        }
    }
}
