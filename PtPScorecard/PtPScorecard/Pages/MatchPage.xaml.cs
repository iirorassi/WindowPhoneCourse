using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using PtPScorecard.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PtPScorecard.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MatchPage : Page
    {
        Common.NavigationHelper _navigationHelper = null;
        private ViewModel.MatchViewModel _MatchViewModel = null;
        private AppBarButton _finishButton = null;
        private int _noOfRounds = 10;
        private Score _current = null;
        private Match _matchSetup = null;
        private string _winnerName = "";
        public int P1Total = 0;
        public int P2Total = 0;
        public int P3Total = 0;
        public int P4Total = 0;

        public MatchPage()
        {
            this.InitializeComponent();

            _navigationHelper = new Common.NavigationHelper(this);
            Loaded += MatchPage_Loaded;

        }

        void MatchPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Create new ViewModel and set it as the DataContext
            _MatchViewModel = new ViewModel.MatchViewModel();
            _MatchViewModel.SetRounds(_noOfRounds);
            this.DataContext = _MatchViewModel;
            _current = _MatchViewModel.P1Scores[0];
            CurrentRoundColorChange();

        }

        //Method for creating the AppBarButtons
        private void CreateAppBarButton()
        {
            if (_finishButton == null)
            {
                _finishButton = new AppBarButton();
                _finishButton.Label = "Finish match";
                _finishButton.Click += FinishButton_Click;
                _finishButton.Icon = new SymbolIcon(Symbol.Flag);
            }

            MatchAppBar.PrimaryCommands.Clear();
            MatchAppBar.PrimaryCommands.Add(_finishButton);
        }

        //Method for finishing the match
        private async void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            string WinnerName = "";
            int WinnerScore = 0;
            //Find Winner

            List<int> totals = new List<int>();
            totals.Add(P1Total);
            totals.Add(P2Total);
            totals.Add(P3Total);
            totals.Add(P4Total);
            int highest = totals.Max();

            //Check that there is only one highest score to catch ties
            int i1 = totals.FindIndex(i => i == highest);
            int i2 = totals.FindLastIndex(i => i == highest);
            if (i1 == i2) {
                switch (i1) {

                    case 0:
                        WinnerName = MatchTextBlockP1.Text;
                        WinnerScore = P1Total;
                        break;

                    case 1:
                        WinnerName = MatchTextBlockP2.Text;
                        WinnerScore = P2Total;
                        break;

                    case 2:
                        WinnerName = MatchTextBlockP3.Text;
                        WinnerScore = P3Total;
                        break;

                    case 3:
                        WinnerName = MatchTextBlockP4.Text;
                        WinnerScore = P4Total;
                        break;
                }

                _winnerName = WinnerName;

                //Show MessageBox
                MessageDialog msgbox = new MessageDialog("Winner is " + WinnerName + " with score " + WinnerScore + "!");
                msgbox.Commands.Add(new UICommand("Save",new UICommandInvokedHandler(this.CommandInvokedHandler_Save)));
                msgbox.Commands.Add(new UICommand("Exit", new UICommandInvokedHandler(this.CommandInvokedHandler_Exit)));

                await msgbox.ShowAsync();

            } else {
                MessageDialog msgbox = new  MessageDialog("Match was a tie, no winners :(");
                msgbox.Commands.Add(new UICommand("Save", new UICommandInvokedHandler(this.CommandInvokedHandler_Save)));
                msgbox.Commands.Add(new UICommand("Exit", new UICommandInvokedHandler(this.CommandInvokedHandler_Exit)));
                await msgbox.ShowAsync();
            }
      
        }
        //Handling of MessageDialog Commands
        private void CommandInvokedHandler_Save(IUICommand command)
        {
            _MatchViewModel.SaveMatch(_matchSetup, _winnerName);
            SaveSuccessful();
        }

        private void CommandInvokedHandler_Exit(IUICommand command)
        {
            //Return to mainpage
            this.Frame.Navigate(typeof(Pages.MainPage));
        }

        private async void SaveSuccessful()
        {
            MessageDialog msgbox = new MessageDialog("Match Saved!");
            msgbox.Commands.Add(new UICommand("Exit", new UICommandInvokedHandler(this.CommandInvokedHandler_Exit)));
            await msgbox.ShowAsync();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CreateAppBarButton();

            //Get the Match variables
            if (e.Parameter != null) { 
            Models.Match MatchSetupVar = e.Parameter as Models.Match;
            MatchTextBlockTime.Text = MatchSetupVar.TimeStamp;
            MatchTextBlockInfo.Text = MatchSetupVar.OtherInfo;
            //Check if Player name has been set and disable Scores if not
            if (string.IsNullOrWhiteSpace(MatchSetupVar.P1Name))
            {
                IC1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MatchTextBlockP1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MatchTextBlockP1Total.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else { 
                MatchTextBlockP1.Text = MatchSetupVar.P1Name;
            }

            if (string.IsNullOrWhiteSpace(MatchSetupVar.P2Name))
            {
                IC2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MatchTextBlockP2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MatchTextBlockP2Total.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else {
                MatchTextBlockP2.Text = MatchSetupVar.P2Name;
            }

            if (string.IsNullOrWhiteSpace(MatchSetupVar.P3Name))
            {
                IC3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MatchTextBlockP3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MatchTextBlockP3Total.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else {
                MatchTextBlockP3.Text = MatchSetupVar.P3Name;
            }
            if (string.IsNullOrWhiteSpace(MatchSetupVar.P4Name))
            {
                IC4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MatchTextBlockP4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MatchTextBlockP4Total.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else {
                MatchTextBlockP4.Text = MatchSetupVar.P4Name;
            }
            _noOfRounds = MatchSetupVar.NoOfRounds;
            _matchSetup = MatchSetupVar;
            System.Diagnostics.Debug.WriteLine("###NAVIGATED: _noOfRounds = " + _noOfRounds.ToString());
            }
            
        }

        //Method that handles actions when user clicks Button in Score List ItemsControl
        private void Score_Click(object sender, RoutedEventArgs e)
        {

            Button item = sender as Button;

            //Get the RoundNo ID from the Score bound to the Button
            if (item != null)
            {
                _current = item.DataContext as Models.Score;

                CurrentRoundColorChange(); 

            }
        }

        //Method for changing Background color of MatchTextBoxCurrentRound
        private void CurrentRoundColorChange()
        {
            MatchTextBlockCurrentRound.Text = _current.RoundNo.ToString();
            Windows.UI.Color color = new Windows.UI.Color();

                switch (_current.Player)
                {
                    case 1:
                        color = Windows.UI.Colors.Red;
                        MatchTextBlockCurrentRound.Background.SetValue(SolidColorBrush.ColorProperty, color);
                        break;
                    case 2:
                        color = Windows.UI.Colors.Green;
                        MatchTextBlockCurrentRound.Background.SetValue(SolidColorBrush.ColorProperty, color);
                        break;
                    case 3:
                        color = Windows.UI.Colors.Blue;
                        MatchTextBlockCurrentRound.Background.SetValue(SolidColorBrush.ColorProperty, color);
                        break;
                    default:
                        color = Windows.UI.Colors.Yellow;
                        MatchTextBlockCurrentRound.Background.SetValue(SolidColorBrush.ColorProperty, color);
                        break;
                }
            }
        //Method for the logic of changing players after Submit
        private void NextPlayer()
        {
            switch (_current.Player)
            {
                case 1:
                    if (MatchTextBlockP2.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        int i = _current.RoundNo - 1;
                        _current = _MatchViewModel.P2Scores[i];
                    }
                    else if (MatchTextBlockP3.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        int i = _current.RoundNo - 1;
                        _current = _MatchViewModel.P3Scores[i];
                    }
                    else if (MatchTextBlockP4.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        int i = _current.RoundNo - 1;
                        _current = _MatchViewModel.P4Scores[i];
                    }
                    else if (_current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P1Scores[_current.RoundNo];
                        }
                    break;
                case 2:
                    if (MatchTextBlockP3.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        int i = _current.RoundNo - 1;
                        _current = _MatchViewModel.P3Scores[i];
                    }
                    else if (MatchTextBlockP4.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        int i = _current.RoundNo - 1;
                        _current = _MatchViewModel.P4Scores[i];
                    }
                    else if (MatchTextBlockP1.Visibility == Windows.UI.Xaml.Visibility.Visible && _current.RoundNo != _noOfRounds)
                    {

                        _current = _MatchViewModel.P1Scores[_current.RoundNo];
                    }
                    else if (_current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P2Scores[_current.RoundNo];
                    }
                    break;
                case 3:
                    if (MatchTextBlockP4.Visibility == Windows.UI.Xaml.Visibility.Visible)
                    {
                        int i = _current.RoundNo - 1;
                        _current = _MatchViewModel.P4Scores[i];
                    }
                    else if (MatchTextBlockP1.Visibility == Windows.UI.Xaml.Visibility.Visible && _current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P1Scores[_current.RoundNo];
                    }
                    else if (MatchTextBlockP2.Visibility == Windows.UI.Xaml.Visibility.Visible && _current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P2Scores[_current.RoundNo];
                    }
                    else if (_current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P3Scores[_current.RoundNo];
                    }
                    break;
                default:
                    if (MatchTextBlockP1.Visibility == Windows.UI.Xaml.Visibility.Visible && _current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P1Scores[_current.RoundNo];
                    }
                    else if (MatchTextBlockP2.Visibility == Windows.UI.Xaml.Visibility.Visible && _current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P2Scores[_current.RoundNo];
                    }
                    else if (MatchTextBlockP3.Visibility == Windows.UI.Xaml.Visibility.Visible && _current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P3Scores[_current.RoundNo];
                    }
                    else if (_current.RoundNo != _noOfRounds)
                    {
                        _current = _MatchViewModel.P4Scores[_current.RoundNo];
                    }
                    break;
            }
        }


        //Method for updating the sum of Score values for each player
        private void UpdateTotals()
        {
            P1Total = _MatchViewModel.P1Scores.Sum(item => item.RoundScore);
            P2Total = _MatchViewModel.P2Scores.Sum(item => item.RoundScore);
            P3Total = _MatchViewModel.P3Scores.Sum(item => item.RoundScore);
            P4Total = _MatchViewModel.P4Scores.Sum(item => item.RoundScore);
            MatchTextBlockP1Total.Text = P1Total.ToString();
            MatchTextBlockP2Total.Text = P2Total.ToString();
            MatchTextBlockP3Total.Text = P3Total.ToString();
            MatchTextBlockP4Total.Text = P4Total.ToString();

        }       

        //Method for updating the Score for _current round
        private void MatchButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_current != null)
            {
                int InputScore;
                bool result = Int32.TryParse(MatchTextBoxInputScore.Text, out InputScore);
                if (result)
                {
                    _current.RoundScore = InputScore;
                    MatchTextBoxInputScore.Text = "";
                    System.Diagnostics.Debug.WriteLine("###SUBMIT: Round Score updated = " + InputScore.ToString());
                    UpdateTotals();
                    NextPlayer();
                    CurrentRoundColorChange();

                }
                else
                {
                    MatchTextBoxInputScore.Text = "Invalid input";
                }
            }
        }


    }

}
