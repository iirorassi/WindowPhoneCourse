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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace PtPScorecard.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArchivePage : Page
    {
        Common.NavigationHelper _navigationHelper = null;
        Windows.ApplicationModel.Resources.ResourceLoader _loader = null;


        public ArchivePage()
        {
            this.InitializeComponent();
            _navigationHelper = new Common.NavigationHelper(this);
            _loader = new Windows.ApplicationModel.Resources.ResourceLoader();


        }

        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ReadFromFile();
        }

        public async void ReadFromFile() { 
        string fileContents = await Common.FileHandler.ReadFromFile("PtP-SaveFile.txt");
        if (string.IsNullOrWhiteSpace(fileContents))
        {
            string s = _loader.GetString("ArchiveFileError");
            ArchiveTextBox.Text = "File is empty or not found";
        } else {
        ArchiveTextBox.Text = fileContents;
        }
        }
    }
}
