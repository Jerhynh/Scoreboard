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

namespace ScoreCounter
{
    /// <summary>
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {
        public QueryWindow()
        {
            InitializeComponent();
        }

        private void SubmitButtonClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ScoreToWinTB.Text, out int res))
            {
                if (res <= 0 || res > 100)
                {
                    MessageBox.Show("Please enter a valid number! Accepted Range:(1-100)", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (VoicePromptsCB.IsChecked == null)
                {
                    MessageBox.Show("Something went wrong, try again!");
                    return;
                }
                ScoreBoardWindow scoreBoardWindow = new(res, (bool)VoicePromptsCB.IsChecked);
                scoreBoardWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid number! Accepted Range:(1-100)", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
