using System.Windows;

namespace RadSamples
{
    public partial class RadWindow1
    {
        public RadWindow1()
        {
            DataContext = new ScrollOrdersViewModel() { Request = new ScrollOrdersModelRequest() { CompletionDateEnd = System.DateTime.Now } };
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}