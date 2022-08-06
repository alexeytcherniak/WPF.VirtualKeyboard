using System;
using System.Collections.Generic;
using System.Globalization;
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
using GalaSoft.MvvmLight.Messaging;
using PropertyChanged;
using VirtualKeyboardSample.Messages;
using VirtualKeyboardSample.VirtualKeyboard;

namespace VirtualKeyboardSample
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainWindow : Window
    {
        public string InputText { get; set; }
        public string SearchText { get; set; }
        public double NumericValue { get; set; }

        public TextBox InputElement{ get; set; }

        public Visibility KeyboardVisibility { get; set; } = Visibility.Collapsed;
        public VirtualKeyboardType KeyboardType { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<ShowKeyboardMessage>(this, ShowKeyboard);


            InputText = "Enter Search Text";
            SearchText = "Press Search Button to show entered values";

            DataContext = this;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            SearchText = $"Result is: {InputText}, {NumericValue.ToString(CultureInfo.InvariantCulture)}"; 
        }

        public void ShowKeyboard(ShowKeyboardMessage message)
        {
            InputElement = message.TextBox;
            KeyboardVisibility = message.IsKeyboardVisible ?  Visibility.Visible : Visibility.Collapsed;
            KeyboardType = message.Type;
        }

    }
}
