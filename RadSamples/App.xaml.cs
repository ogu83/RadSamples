using System.Windows;
using Telerik.Windows.Controls;

namespace RadSamples
{
    public sealed partial class App : Application
    {
        public App()
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(typeof(RadTileViewItem).TypeHandle);
            
            this.InitializeComponent();

            var mainPage = new MainPage();
            Window.Current.Content = mainPage;
        }
    }
}
