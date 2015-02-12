using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Assignment1.Resources;


namespace Assignment1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Variables
        private String feedbackMessage;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

          
        }

        //Clicking the login button shows messagebox with feedback
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "")
            {
                feedbackMessage = AppResources.LoginFail;
            }
            else
            {
                feedbackMessage = AppResources.LoginOK;
            }
            MessageBox.Show(feedbackMessage);
        }

        //Functionality for showing and hiding warning for Radio button 2 
        private void Radio2_Click(object sender, RoutedEventArgs e)
        {
            if (Radio2.IsChecked == true)
            {
                Radio2Warning.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                Radio2Warning.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        //Functionality for showing and hiding the information related to Radio buttons
        private void ButtonShowInfo_Click(object sender, RoutedEventArgs e)
        {
            if (Radio1Info.Visibility == System.Windows.Visibility.Collapsed)
            {
                Radio1Info.Visibility = System.Windows.Visibility.Visible;
                Radio2Info.Visibility = System.Windows.Visibility.Visible;
                ButtonShowInfo.Content = AppResources.SecurityHide;
            }
            else
            {
                Radio1Info.Visibility = System.Windows.Visibility.Collapsed;
                Radio2Info.Visibility = System.Windows.Visibility.Collapsed;
                ButtonShowInfo.Content = AppResources.SecurityShow;
            }
        }

        //Functionality for showing and hiding the information for CheckBox1
        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBox1.IsChecked == true)
            {
                CheckBox1Info.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                CheckBox1Info.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

   
    }
}