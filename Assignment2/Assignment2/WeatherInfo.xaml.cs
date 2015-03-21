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


namespace Assignment2
{
    public sealed partial class WeatherInfo : Page
    {
        
        public WeatherInfo()
        {
            this.InitializeComponent();

        }


        //When navigating to this page, parameter is received and put into ContentBox
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            var parameter = e.Parameter as String;
            //When navigating to WeatherInfo page without doing a new search, old data must be retrieved
                if (parameter == null)
                {
                    //Null parameter = check for saved data in LocalSetting
                    Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    if (localSettings.Values.ContainsKey("Val"))
                    {
                        ContentBox.Text = (localSettings.Values["Val"] as string).ToString();
                        //Debug info
                        System.Diagnostics.Debug.WriteLine("*_*_*_This data loaded from LocalSettings:");
                        System.Diagnostics.Debug.WriteLine((localSettings.Values["Val"] as string).ToString());
                    }

                }
                else { 

                    //If not null, there is weatherdata in the parameter
                    //Display this data in ContentBox
                ContentBox.Text = parameter;
                    //Save ContentBox Text to LocalSettings
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                localSettings.Values["Val"] = ContentBox.Text;
                    //Debug info
                System.Diagnostics.Debug.WriteLine("*_*_*_This data saved to LocalSettings:");
                System.Diagnostics.Debug.WriteLine((localSettings.Values["Val"] as string).ToString());
                }

        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
  
        }



        //Functionality for navigation back to MainPage
        //This is not good WP8.1 design, next step implementing bottom menubar or physical back button controls.
        private void Button_Back(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
