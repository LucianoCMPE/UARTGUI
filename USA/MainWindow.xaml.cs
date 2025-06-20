using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

namespace USA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort serialPort;
        private string[] portsAvailable;
        public YourCollection MyObjects { get; } = new YourCollection();
        public ComCollection MyComObjects { get; } = new ComCollection();
        public MainWindow()
        {
            // set datacontext to the window's instance.
            this.DataContext = this;
            InitializeComponent();
            //serialPort = new SerialPort("COM12", 9600, Parity.None, 8, StopBits.One);
            portsAvailable = SerialPort.GetPortNames();
            MyComObjects.Add("Choose a COM");
            MyComObjects.AddRange(portsAvailable);
            comChoice.SelectedIndex = 0; // Set default selection to "Choose a COM"
        }
        
        public class ComCollection : ObservableCollection<string>
        {
            public void AddRange(IEnumerable<string> portNames)
            {
                foreach (var port in portNames)
                {
                    this.Add(port);
                }
            }
        }

        public class MyComObject : INotifyPropertyChanged
        {
            private string _portName;
            public string PortName
            {
                get { return _portName; }
                set
                {
                    if (_portName != value)
                    {
                        _portName = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PortName)));
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }


        public class YourCollection : ObservableCollection<MyObject>
        {
            // some wrapper functions for example:
            public void Add(string title, bool header, string headerText, string commandNumber, string payLength){
                    this.Add(new MyObject { Title = title, Header = header, HeaderText = headerText, commandNum = commandNumber, payLoadLen = payLength });
            }
        }

        // by implementing the INotifyPropertyChanged, changes to properties
        // will update the listbox on-the-fly
        public class MyObject : INotifyPropertyChanged
        {
            private string _title;
            private bool _Header;
            private string _headerText;
            private string _commandNumber;
            private string _payLoadLength;

            // a property.
            public string Title
            {
                get { return _title; }
                set
                {
                    if (_title != value)
                    {
                        _title = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                    }
                }
            }
            public bool Header
            {
                get => _Header;
                set
                {
                    if (_Header != value)
                    {
                        _Header = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Header)));
                    }
                }
            }

            public string HeaderText
            {
                get { return _headerText; }
                set
                {
                    if (_headerText != value)
                    {
                        _headerText = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeaderText)));
                    }
                }
            }

            public string commandNum
            {
                get { return _commandNumber; }
                set
                {
                    if (_commandNumber != value)
                    {
                        _commandNumber = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(commandNum)));
                    }
                }
            }

            public string payLoadLen
            {
                get { return _payLoadLength; }
                set
                {
                    if (_payLoadLength != value)
                    {
                        _payLoadLength = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(payLoadLen)));
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
        }
        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog of = new OpenFileDialog();
            //bool? result = of.ShowDialog();
            //if (result == true && of.FileName.EndsWith(".ini") == true) {
            //    string fullPath = of.FileName;
            //    string fileNameOnly = System.IO.Path.GetFileName(fullPath);
            //    MyObjects.Add(fileNameOnly);
            //}
 

        }

        private AddItem wnd;
        private void AddTest_Click(object sender, RoutedEventArgs e)
        {
            wnd = new AddItem
            {
                Owner = this,                                     // centers on main window
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            wnd.Show();  // or ShowDialog()

        }

        private void Test_Click(object sender, MouseButtonEventArgs e)
        {

            var grid = sender as Grid;
            if (grid == null) return;

            var listBox = FindParent<ListBox>(grid);
            if (listBox == null) return;

            var listBoxItem = ItemsControl.ContainerFromElement(listBox, grid) as ListBoxItem;
            if (listBoxItem != null)
            {
                listBox.SelectedItem = listBoxItem.DataContext;
            }
        }

        // Helper method to find parent of a specific type in the visual tree
        private static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            var selectedTest = e.AddedItems[0] as MyObject;
            if (selectedTest == null) return;

            CheckboxPanel.Children.Clear();

            for (int p = 0; p < int.Parse(selectedTest.payLoadLen); p++)
            {
                for (int i = 7; i >= 0; i--){
                    var checkbox = new CheckBox
                    {
                        Content = i,
                        Margin = new Thickness(12),
                        Name = $"CheckBox_{p}_{i}",
                    };

                    CheckboxPanel.Children.Add(checkbox);
                }
            }
            runTestButton.Visibility = Visibility.Visible;
        }
        public void AddTestData ()
        {
            MyObjects.Add(wnd.testName, wnd.headerEnable, wnd.headerText, wnd.commandID, wnd.payLength);
            //headerEnable
            //commandID
            //payLength

        }

        private void runTest(object sender, RoutedEventArgs e)
        {
            //Button btn = sender as Button;

            byte header = 0;
            byte payloadData = 0;
            var selectedTest = TestListBox.SelectedItem as MyObject;
            if (wnd.isHeader.IsChecked == true) {
                header = Convert.ToByte(selectedTest.HeaderText, 16);
            }

            byte commandNumInt = Convert.ToByte(selectedTest.commandNum);
            byte payLoadLenInt = Convert.ToByte(selectedTest.payLoadLen);

            int totalBytes = Convert.ToInt32(selectedTest.payLoadLen); // Total bytes 
            int totalBits = totalBytes * 8; // Total bits in the payload    
            byte[] payloadBytes = new byte[totalBytes];
            for (int i = 0; i < totalBits; i++)
            {
                CheckBox checkBox = CheckboxPanel.Children[i] as CheckBox;
                if (checkBox != null && checkBox.IsChecked == true)
                {
                    int byteIndex = i / 8; // Determine which byte to set
                    payloadBytes [byteIndex] |= (byte)(1 << (7 - (i % 8))); // Set the bit corresponding to the checkbox index
                }
            }

            int totalMessageBytes = (3 + totalBytes); // 2 bytes for header and command number, plus payload length
            byte[] messageBytes = new byte[totalMessageBytes];

            messageBytes[0] = header; // Set header byte
            messageBytes[1] = commandNumInt; // Set command number byte 
            messageBytes[2] = payLoadLenInt; // Set payload length byte
            for (int i = 0; i < totalBytes; i++)
            {
                messageBytes[i + 3] = payloadBytes[i]; // set header byte, command number byte, and payload bytes
            }


            if ((string)comChoice.SelectedValue == null || comChoice.SelectedIndex == 0)
            {
                MessageBox.Show(this, "Please select a COM port.");
                return;
            }
            else
            {
                serialPort = new SerialPort((string)comChoice.SelectedValue, 9600, Parity.None, 8, StopBits.One);
            }


            try
            {
                serialPort.Open();
                serialPort.Write(messageBytes, 0, messageBytes.Length);
                serialPort.Close();
                MessageBox.Show(this,"Complete!");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(this, "COM port is in use by another application.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Error: {ex.Message}");
            }
        }
    }
}