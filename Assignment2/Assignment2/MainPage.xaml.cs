using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           
        }

        //Functionality for searching for weather information
        private async void Button_Search(object sender, RoutedEventArgs e)
        {
            //Send a request for weather info from openweathermap.org
            try {
                using (HttpClient hc = new HttpClient()){
                    hc.BaseAddress = new Uri("http://api.openweathermap.org");
                    //This defines what is queried "weather", mode "json, units "metric" and developer specific "API key"
                    var url = "data/2.5/weather?q={0}&mode=json&units=metric&APPID=b9dc06ad1c836d309308e41729e41a39";
                    hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await hc.GetAsync(String.Format(url, InputBox.Text));
                    
                    //Write debug information
                    System.Diagnostics.Debug.WriteLine("Response: " + response.StatusCode + " ---- " + response.ToString());

                    //If HttpResponseMessage returns success code write received data to string and send to WeatherInfo page
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync();
                        String SendContent = data.Result.ToString();

                        //Here it would be useful to parse the Json data

                        //Navigate to WeatherInfo page and pass the weatherinformation as a parameter
                        this.Frame.Navigate(typeof(WeatherInfo),SendContent);
                    }
                }
            } 
                //If not successful, give feedback to user
            catch (Exception ex){
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.ToString());
                InputBox.Text = "Error occured, sorry.";
            }
            
        }

        // Functionality for navigating to WeatherInfo page
        private void Button_Weather(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WeatherInfo));
        }
    }
}

