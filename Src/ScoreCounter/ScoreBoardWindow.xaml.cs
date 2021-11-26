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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using System.Diagnostics;

namespace ScoreCounter
{
    /// <summary>
    /// Interaction logic for ScoreBoardWindow.xaml
    /// </summary>
    public partial class ScoreBoardWindow : Window
    {
        // Every 5 serves are switch of server.
        public int RedPoints { get; set; } = 0;
        public int BluePoints { get; set; } = 0;
        public int ScoreToWin { get; set; }
        public bool IsVoicePromptsEnabled { get; set; }
        public bool IsFullScreen { get; set; } = false;

        public ScoreBoardWindow(int ScoreToWin,bool IsVoicePromptsEnabled)
        {
            this.ScoreToWin = ScoreToWin;
            this.IsVoicePromptsEnabled = IsVoicePromptsEnabled;
            InitializeComponent();
        }

        private void AddPointBtnRed_Click(object sender, RoutedEventArgs e)
        {
            RedPoints += 1;
            RedTeamLbl.Content = RedPoints;
            CheckForWin();
            AnnounceScore();
        }

        private void AddPointBtnBlue_Click(object sender, RoutedEventArgs e)
        {
            BluePoints += 1;
            BlueTeamLbl.Content = BluePoints;
            CheckForWin();
            AnnounceScore();
        }

        private void ResetPointBtnRed_Click(object sender, RoutedEventArgs e)
        {
            RedPoints = 0;
            RedTeamLbl.Content = 0;
        }

        private void ResetPointBtnBlue_Click(object sender, RoutedEventArgs e)
        {
            BluePoints = 0;
            BlueTeamLbl.Content = 0;
        }

        private void RevokePointBtnBlue_Click(object sender, RoutedEventArgs e)
        {
            if (BluePoints == 0)
                return;
            BluePoints -= 1;
            BlueTeamLbl.Content = BluePoints;
            AnnounceScore();
        }

        private void RevokePointBtnRed_Click(object sender, RoutedEventArgs e)
        {
            if (RedPoints == 0)
                return;
            RedPoints -= 1;
            RedTeamLbl.Content = RedPoints;
            AnnounceScore();
        }

        private void ResetBoard()
        {
            BluePoints = 0;
            BlueTeamLbl.Content = 0;
            RedPoints = 0;
            RedTeamLbl.Content = 0;
        }

        private void CheckForWin()
        {
            if (RedPoints < ScoreToWin && BluePoints < ScoreToWin)
                return;
            if (RedPoints >= ScoreToWin)
            {
                MessageBox.Show("Red Team Wins!","Game Over", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                ResetBoard();
            }
            if (BluePoints >= ScoreToWin)
            {
                MessageBox.Show("Blue Team Wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                ResetBoard();
            }
        }

        private void FullScrnButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFullScreen)
            {
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
                this.Topmost = true;
                IsFullScreen = true;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.SingleBorderWindow;
                this.Topmost = false;
                IsFullScreen = false;
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            QueryWindow queryWindow = new();
            queryWindow.Show();
            this.Close();
        }

        private async void AnnounceScore()
        {
            if (!IsVoicePromptsEnabled)
                return;
            using SpeechSynthesizer synth = new SpeechSynthesizer();
            // Microsoft David Desktop
            // Microsoft Zira Desktop
            synth.SelectVoice("Microsoft David Desktop");
            synth.SpeakAsync($"Red {RedPoints}, Blue {BluePoints}.");
            await Task.Delay(10000);
        }
    }
}
