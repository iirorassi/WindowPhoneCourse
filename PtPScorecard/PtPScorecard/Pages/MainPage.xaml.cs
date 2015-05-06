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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace PtPScorecard.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Common.NavigationHelper _navigationHelper = null;
        Windows.ApplicationModel.Resources.ResourceLoader _loader = null;
        private AppBarButton _pinButton = null;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            _navigationHelper = new Common.NavigationHelper(this);
            _loader = new Windows.ApplicationModel.Resources.ResourceLoader();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (MainPageButton1.Name.Equals(b.Name))
            {
                //Navigate to MatchSetup
                this.Frame.Navigate(typeof(Pages.MatchSetup));
            }
            else if (MainPageButton2.Name.Equals(b.Name))
            {
                //Navigate to Archive
                this.Frame.Navigate(typeof(Pages.ArchivePage));
            }
        }

        private void CreateAppBarButton()
        {
            if (_pinButton == null)
            {
                _pinButton = new AppBarButton();
                _pinButton.Label = _loader.GetString("AppBarButton_Pin");
                _pinButton.Click += PinButton_Click;
                _pinButton.Icon = new SymbolIcon(Symbol.Pin);
            }

            MainAppBar.PrimaryCommands.Clear();
            MainAppBar.PrimaryCommands.Add(_pinButton);
        }

        private async void PinButton_Click(object sender, RoutedEventArgs e)
        {
            bool createSecondaryTile = true;

            // Find all secondary tiles
            var secondaryTiles = await Windows.UI.StartScreen.SecondaryTile.FindAllAsync();
            foreach (var secondaryTile in secondaryTiles)
            {
                // Check for existing secondary tiles
                if (secondaryTile.TileId == "PtpTile")
                {
                    // Delete the secondary tile.
                    createSecondaryTile = false;
                }
            }

            if (createSecondaryTile)
            {
                string s =_loader.GetString("ApplicationName");
                var tile = new Windows.UI.StartScreen.SecondaryTile("PtpTile",s,"PinTime:" + DateTime.Now.ToString(),new Uri("ms-appx:///Assets/ptp150.png"),Windows.UI.StartScreen.TileSize.Default);
                tile.VisualElements.ShowNameOnSquare150x150Logo = true;
                await tile.RequestCreateAsync();
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CreateAppBarButton();
        }

        private async void About_Click(object sender, RoutedEventArgs e)
        {
            string aboutinfo = _loader.GetString("AboutInfo");
            MessageDialog msgbox = new MessageDialog(aboutinfo);
            await msgbox.ShowAsync();
        }
    }
}
