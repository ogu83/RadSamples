using RadSamples;
using System.Windows;
using System.Windows.Controls;

namespace RadGridViewIsBusyIndicator
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void btnRadWindow_Click(object sender, RoutedEventArgs e)
        {
            var radWindow = new RadWindow1();
            radWindow.Show();
        }
    }
}