using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using static System.Double;

namespace VirtualKeyboardSample.VirtualKeyboard
{
    public enum VirtualKeyType
    {
        Character,
        Spacebar,
        Backspace,
        Shift,
        Enter,
        Evaluate,
        EvalEnter,
        EvalPercent,
        EvalNegative,
        ToggleSymbols,
        DecimalSeparator,
        CapsLock,
        Close,
        Clear,
        Spacer
    }

    public enum VirtualKeyboardType
    {
        AlphaNumeric,
        NumPad
    }

    [AddINotifyPropertyChangedInterface]
    public class VirtualKeyboardKey
    {
        public VirtualKeyType Type { get; set; } = VirtualKeyType.Character;
        public string Value { get; set; }
        public double Width { get; set; }
    }
    public class VirtualKeyboardRow : ObservableCollection<VirtualKeyboardKey>{}
    public class VirtualKeyboardLayout : ObservableCollection<VirtualKeyboardRow>{}

    /// <summary>
    /// On-Screen Virtual Keyboard control for touch input
    /// </summary>

    [AddINotifyPropertyChangedInterface]
    public sealed class VirtualKeyboard : ContentControl
    {
        #region TextBox Dependency Property

        public static readonly DependencyProperty TextBoxProperty = 
            DependencyProperty.Register(
                "TextBox", 
                typeof(TextBox), 
                typeof(VirtualKeyboard), 
                new FrameworkPropertyMetadata(OnTextBoxChanged));

        private static void OnTextBoxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is VirtualKeyboard virtualKeyboard && e.NewValue is TextBox tb)
            {
                //if (e.OldValue is TextBox oldTextBox) virtualKeyboard.OnCloseKeyboard(true);

                virtualKeyboard.LayoutType = (VirtualKeyboardType) tb.GetValue(LayoutTypeProperty);

                switch (virtualKeyboard.LayoutType)
                {
                    case VirtualKeyboardType.NumPad:
                        virtualKeyboard.CurrentLayout = KeyboardLayout.NumPad;
                        break;

                    case VirtualKeyboardType.AlphaNumeric:
                        virtualKeyboard.CurrentLayout = KeyboardLayout.En;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                virtualKeyboard.InitKeyboard();
                virtualKeyboard.SetTextBinding();
            }
        }

        public TextBox TextBox
        {
            get => (TextBox) GetValue(TextBoxProperty);
            set => SetValue(TextBoxProperty, value);
        }

        #endregion

        #region Window Dependency Property

        public static readonly DependencyProperty WindowProperty = 
            DependencyProperty.Register(
                "Window", 
                typeof(Window), 
                typeof(VirtualKeyboard), 
                new FrameworkPropertyMetadata());

        public Window Window
        {
            get => (Window) GetValue(WindowProperty);
            set => SetValue(WindowProperty, value);
        }

        #endregion

        #region  LayoutType Attached Property

        public static readonly DependencyProperty LayoutTypeProperty = 
            DependencyProperty.RegisterAttached(
                "LayoutType", 
                typeof(VirtualKeyboardType), 
                typeof(VirtualKeyboard));
        
        public static void SetLayoutType(UIElement element, VirtualKeyboardType value)
        {
            element.SetValue(LayoutTypeProperty, value);
        }

        public static VirtualKeyboardType GetLayoutType(UIElement element)
        {
            return (VirtualKeyboardType) element.GetValue(LayoutTypeProperty);
        }

        public VirtualKeyboardType LayoutType { get; set; }

        //private static void OnLayoutTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    if (d is VirtualKeyboard virtualKeyboard)
        //    {
        //        if (virtualKeyboard.TextBox != null 
        //            && virtualKeyboard.TextBox.GetBindingExpression(TextBox.TextProperty) is BindingExpression expr)
        //        {
        //            var propertyType = expr.DataItem.GetType().GetProperty(expr.ParentBinding.Path.Path)?.PropertyType;

        //            virtualKeyboard.CurrentLayout = propertyType == typeof(double)
        //                ? KeyboardLayout.NumPad
        //                : KeyboardLayout.En;
        //        }
        //    }
        //}

        #endregion

        #region  Properties

        public VirtualKeyboardLayout CurrentLayout{ get; set; }

        public string OldText { get; set; }
        public string Text { get; set; }
        
        public bool IsShiftActive { get; set; }
        public bool IsCapsLockActive { get; set; }
        public bool IsSymbolsLayoutActive { get; set; }

        private Binding TextBoxBinding { get; set; }

        public string DecimalSeparator { get; set; }
        public NumberFormatInfo NumberFormat { get; set; }

        public RelayCommand<VirtualKeyboardKey> RegisterKeyPressCommand =>
            new RelayCommand<VirtualKeyboardKey>(RegisterKeyPress);

        #endregion

        #region  Ctor

        public VirtualKeyboard()
        {
           // IsVisibleChanged += VirtualKeyboard_IsVisibleChanged;
        }

        #endregion

        #region  Methods

        // Set position and height of Virtual Keyboard

        private void InitKeyboard()
        {
            if (Window!= null && CurrentLayout != null)
            {
                var textBoxPosition = TextBox.TransformToAncestor(Window).Transform(new Point(0, 0));

                switch (LayoutType)
                {
                    case VirtualKeyboardType.AlphaNumeric:

                        ToUpper(false);

                        MaxWidth = PositiveInfinity;
                        MaxHeight = Window.ActualHeight / 2 - 48;

                        HorizontalAlignment = HorizontalAlignment.Center;
                        VerticalAlignment = MaxHeight > textBoxPosition.Y
                            ? VerticalAlignment.Bottom
                            : VerticalAlignment.Top;

                        break;

                    case VirtualKeyboardType.NumPad:

                        MaxHeight = PositiveInfinity;
                        MaxWidth = Window.ActualWidth / 2 - 48;

                        VerticalAlignment = VerticalAlignment.Bottom;
                        HorizontalAlignment = MaxWidth > textBoxPosition.X
                            ? HorizontalAlignment.Right
                            : HorizontalAlignment.Left;

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                TextBox.Foreground = (Brush)Application.Current.TryFindResource("TextBox.Focus.Foreground");
                TextBox.Background = (Brush)Application.Current.TryFindResource("TextBox.Focus.Background");
                TextBox.BorderBrush = (Brush)Application.Current.TryFindResource("TextBox.Focus.Border");
                TextBox.BorderThickness = (Thickness)Application.Current.TryFindResource("TextBox.Focused.BorderThickness");

                NumberFormat = CultureInfo.CurrentCulture.NumberFormat;
                DecimalSeparator = NumberFormat.NumberDecimalSeparator;
            }
        }

        private void SetTextBinding()
        {
            OldText = TextBox.Text;
            Text = TextBox.Text;

            TextBoxBinding = TextBox.GetBindingExpression(TextBox.TextProperty)?.ParentBinding;

            var textBinding = new Binding("Text") { Source = this };
            TextBox.SetBinding(TextBox.TextProperty, textBinding);

            TextBox.CaretIndex = TextBox.Text.Length;
            TextBox.ScrollToEnd();
        }

        private void OnCloseKeyboard(bool allowSaveValue)
        {
            TextBox.Foreground = (Brush) Application.Current.TryFindResource("TextBox.Static.Foreground");
            TextBox.Background = (Brush) Application.Current.TryFindResource("TextBox.Static.Background");
            TextBox.BorderBrush = (Brush) Application.Current.TryFindResource("TextBox.Static.Border");
            TextBox.BorderThickness = (Thickness) Application.Current.TryFindResource("TextBox.Static.BorderThickness");

            var scope = FocusManager.GetFocusScope(TextBox);
            FocusManager.SetFocusedElement(scope, null);

            if (TextBoxBinding != null)
            {
                TextBox.SetBinding(TextBox.TextProperty, TextBoxBinding);

                var textBoxBindingExpression = TextBox.GetBindingExpression(TextBox.TextProperty);
                if (textBoxBindingExpression != null && allowSaveValue)
                {
                    TextBox.Text = Text;
                    textBoxBindingExpression.UpdateSource();

                    if (textBoxBindingExpression.HasError) TextBox.Text = OldText;
                }
                else TextBox.Text = OldText;
            }
            else
            {
                BindingOperations.ClearBinding(TextBox, TextBox.TextProperty);
                TextBox.Text = Text;
            }

            OldText = "";
            Text = "";

            IsSymbolsLayoutActive = false;
            IsCapsLockActive = false;
            IsShiftActive = false;

            Keyboard.ClearFocus();
        }

        private void RegisterKeyPress(VirtualKeyboardKey key)
        {
            switch (key.Type)
            {
                case VirtualKeyType.Character:
                case VirtualKeyType.Spacebar:

                    PressCharKey(key.Value);
                    if (IsShiftActive)
                    {
                        ToUpper(IsCapsLockActive);
                        IsShiftActive = false;
                    }

                    break;

                case VirtualKeyType.ToggleSymbols:

                    if (IsSymbolsLayoutActive) UntoggleSymbolsLayout();
                    else ToggleSymbolsLayout();

                    break;

                case VirtualKeyType.CapsLock:
                    if (IsCapsLockActive)
                    {
                        ToUpper(IsShiftActive);
                        IsCapsLockActive = false;

                    }
                    else
                    {
                        ToUpper(!IsShiftActive);
                        IsCapsLockActive = true;
                    }

                    break;

                case VirtualKeyType.Shift:

                    if (IsShiftActive)
                    {
                        ToUpper(IsCapsLockActive);
                        IsShiftActive = false;

                    }
                    else
                    {
                        ToUpper(!IsCapsLockActive);
                        IsShiftActive = true;
                    }

                    break;

                case VirtualKeyType.Backspace:
                    PressBackspace();
                    break;

                case VirtualKeyType.Enter:
                    PressEnter();
                    break;

                case VirtualKeyType.Evaluate:
                    EvaluateExpression();
                    break;

                case VirtualKeyType.Close:
                    PressClose();
                    break;

                case VirtualKeyType.Spacer:
                    break;

                case VirtualKeyType.Clear:
                    Text = "";
                    break;

                case VirtualKeyType.DecimalSeparator:
                    PressCharKey(DecimalSeparator);
                    break;

                case VirtualKeyType.EvalEnter:
                    if (EvaluateExpression()) PressEnter();
                    break;

                case VirtualKeyType.EvalPercent:

                    Text = Text.Replace(DecimalSeparator, ".");

                    var textMatch = Regex.Match(Text, @"^(.*)([+\-*/])(\d+(\.\d+)?)", RegexOptions.RightToLeft);

                    if (textMatch.Success)
                    {
                        var baseExpression = textMatch.Groups[1].Value;
                        var mathOperation = textMatch.Groups[2].Value;
                        var percentText = textMatch.Groups[3].Value;

                        var expr = new NCalc.Expression(baseExpression);

                        if (!expr.HasErrors())
                        {
                            if (TryParse(percentText,
                                         NumberStyles.AllowDecimalPoint,
                                         CultureInfo.InvariantCulture,
                                         out var percentValue))
                            {
                                if (TryParse(expr.Evaluate().ToString(), out var evaluatedExpression))
                                {
                                    percentValue = evaluatedExpression / 100 * percentValue;

                                    var f = evaluatedExpression.ToString(CultureInfo.CurrentCulture);
                                    var p = percentValue.ToString(CultureInfo.CurrentCulture);

                                    Text = f + mathOperation + p;
                                }
                            }
                        }
                    }

                    Text = Text.Replace(".", DecimalSeparator);

                    TextBox.CaretIndex = TextBox.Text.Length;
                    TextBox.ScrollToEnd();

                    break;

                case VirtualKeyType.EvalNegative:
                    if (EvaluateExpression())
                    {
                        Text = Text[0] == '-' ? Text.Substring(1) : "-" + Text;
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool EvaluateExpression()
        {
            if (string.IsNullOrEmpty(Text)) return false;

            var text = Text.Replace(DecimalSeparator, ".");

            try
            {
                var expr = new NCalc.Expression(text);

                if (expr.HasErrors())
                {
                    Text = "Incorrect expression!";
                    return false;
                }

                text = expr.Evaluate().ToString();

                if (!TryParse(text, out var value) || IsInfinity(value) || IsNaN(value))
                {
                    Text = "Expression is not a number!";
                    return false;
                }

                Text = text.Replace(".", DecimalSeparator);

                TextBox.CaretIndex = TextBox.Text.Length;
                TextBox.ScrollToEnd();
                return true;
            }
            catch (Exception e)
            {
                Text = "Unable to evaluate expression!";
                return false;
            }
        }

        private void ToggleSymbolsLayout()
        {
            IsSymbolsLayoutActive = true;
            CurrentLayout = KeyboardLayout.Symbols;
        }

        private void UntoggleSymbolsLayout()
        {
            IsSymbolsLayoutActive = false;
            CurrentLayout = KeyboardLayout.En;
        }

        private void ToUpper(bool toUpper)
        {
            foreach (var row in CurrentLayout)
            {
                foreach (var key in row)
                {
                    if (key.Type == VirtualKeyType.Character)
                        key.Value = toUpper ? key.Value.ToUpper() : key.Value.ToLower();
                }
            }
        }

        private void PressBackspace()
        {
            //if (TextBox != null && TextBox.Tag is string text && text.Length > 0)
            if (TextBox != null && Text.Length > 0)
            {
                Text = Text.Remove(Text.Length - 1);

                TextBox.CaretIndex = TextBox.Text.Length;
                TextBox.ScrollToEnd();
            }
        }

        private void PressCharKey(string text)
        {
            if (TextBox != null)
            {
                Text += text;
                TextBox.CaretIndex = TextBox.Text.Length;
                TextBox.ScrollToEnd();
            }
        }

        private void PressEnter()
        {
            OnCloseKeyboard(true);
        }

        private void PressClose()
        {
            OnCloseKeyboard(false);
        }

        #endregion
    }
}