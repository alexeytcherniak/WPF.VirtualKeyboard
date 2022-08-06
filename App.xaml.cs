using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using VirtualKeyboardSample.Messages;
using VirtualKeyboardSample.VirtualKeyboard;

namespace VirtualKeyboardSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(System.Windows.Controls.TextBox),
                                              UIElement.GotKeyboardFocusEvent, new RoutedEventHandler(TextBox_GotFocus));

            EventManager.RegisterClassHandler(typeof(System.Windows.Controls.TextBox),
                                              UIElement.LostKeyboardFocusEvent, new RoutedEventHandler(TextBox_LostFocus));

            base.OnStartup(e);
        }
        
        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox textBox &&
                textBox.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty) is BindingExpression expr)
            {
                var propertyType = expr.DataItem.GetType().GetProperty(expr.ParentBinding.Path.Path)?.PropertyType;

                var keyboardType = propertyType == typeof(double)
                    ? VirtualKeyboardType.NumPad
                    : VirtualKeyboardType.AlphaNumeric;

                Messenger.Default.Send(new ShowKeyboardMessage(true, keyboardType, textBox));
            }
        }

        private static void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send(new ShowKeyboardMessage(false));
        }

    }
}
