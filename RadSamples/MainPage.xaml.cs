using System;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace RadSamples
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void mainRTV_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            UserControl1 uc1 = mainRTV.FindChildByType<UserControl1>();
            if (uc1 != null)
            {
                Console.WriteLine("FindChildByType Success");
            }
            else
            {
                Console.WriteLine("FindChildByType Failed");
            }
        }
    }
}