using System.Windows.Controls;
using VirtualKeyboardSample.VirtualKeyboard;

namespace VirtualKeyboardSample.Messages
{
    public class ShowKeyboardMessage
    {
        public bool IsKeyboardVisible { get; }
        public TextBox TextBox { get; }
        public VirtualKeyboardType Type { get; }

        public ShowKeyboardMessage(bool isVisible, VirtualKeyboardType type, TextBox textBox = null)
        {
            IsKeyboardVisible = isVisible;
            TextBox = textBox;
            Type = type;
        }

        public ShowKeyboardMessage(bool isVisible)
        {
            IsKeyboardVisible = isVisible;
        }
    }
}