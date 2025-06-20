using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace USA
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {
        public AddItem()
        {
            InitializeComponent();

        }

        public String testName => testNameInput.Text;
        public bool headerEnable => isHeader.IsEnabled;
        public string headerText => headerValue.Text;
        public String commandID =>commandNumber.Text;
        public String payLength => payloadLength.Text;
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // At this point the native window exists but hasn't been shown yet,
            // so setting Size here will be picked up immediately.
            this.Width = 600;
            this.Height = 400;
            this.WindowState = WindowState.Normal;
        }

        private void submitClick(object sender, RoutedEventArgs e)
        {
            if (Owner is MainWindow mainWindow)
            {
                mainWindow.AddTestData();
                MessageBox.Show("Test added successfully!");
                this.Close();
            }
        }

        private void AddHeaderField(object sender, RoutedEventArgs e)
        {

            headerValue.Visibility = Visibility.Visible;

        }
        private void RemoveHeaderField(object sender, RoutedEventArgs e)
        {

            headerValue.Visibility = Visibility.Hidden;
        }
    }
}
