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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PtPScorecard.Pages
{

    public sealed partial class MatchSetup : Page
    {
        Common.NavigationHelper _navigationHelper = null;
        public MatchSetup()
        {
            this.InitializeComponent();
            _navigationHelper = new Common.NavigationHelper(this);

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Set the timestamp
            string Timestamp = DateTime.Now.ToString();
            SetupTextBlockTime.Text = Timestamp;
        }

        private async void SetupButtonStart_Click(object sender, RoutedEventArgs e)
        {
            //Check that at least one player has been named
            if (string.IsNullOrWhiteSpace(SetupTextBoxP1.Text) && string.IsNullOrWhiteSpace(SetupTextBoxP2.Text) && string.IsNullOrWhiteSpace(SetupTextBoxP3.Text) && string.IsNullOrWhiteSpace(SetupTextBoxP4.Text))
            {
                Windows.UI.Popups.MessageDialog msgbox = new Windows.UI.Popups.MessageDialog("Please enter at least one player");
                
                await msgbox.ShowAsync();
            }
            else { 


            //Create a new instance of class Match to pass variable to MatchPage

            Models.Match m = new Models.Match();
            m.P1Name = SetupTextBoxP1.Text;
            m.P2Name = SetupTextBoxP2.Text;
            m.P3Name = SetupTextBoxP3.Text;
            m.P4Name = SetupTextBoxP4.Text;
            m.TimeStamp = SetupTextBlockTime.Text;
            m.OtherInfo = SetupTextBoxInfo.Text;
            int RoundInt;
            bool result = Int32.TryParse(SetupTextBoxRounds.Text, out RoundInt);
            if (result){
                            m.NoOfRounds = RoundInt;
            }
            else
            {
                m.NoOfRounds = 10;
            }

            this.Frame.Navigate(typeof(Pages.MatchPage),m);
            }
        }
    }
}
