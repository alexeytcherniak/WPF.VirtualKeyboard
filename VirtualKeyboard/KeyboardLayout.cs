namespace VirtualKeyboardSample.VirtualKeyboard
{
    public static class KeyboardLayout
    {
        public static VirtualKeyboardLayout NumPad { get; } =
            new VirtualKeyboardLayout
            {
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "Close", Type = VirtualKeyType.Close},
                    new VirtualKeyboardKey {Value = "("},
                    new VirtualKeyboardKey {Value = ")"},
                    new VirtualKeyboardKey {Value = "C", Type = VirtualKeyType.Clear},
                    new VirtualKeyboardKey {Value = "Backspace", Type = VirtualKeyType.Backspace},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "7"},
                    new VirtualKeyboardKey {Value = "8"},
                    new VirtualKeyboardKey {Value = "9"},
                    new VirtualKeyboardKey {Value = "/"},
                    new VirtualKeyboardKey {Value = "*"},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "4"},
                    new VirtualKeyboardKey {Value = "5"},
                    new VirtualKeyboardKey {Value = "6"},
                    new VirtualKeyboardKey {Value = "-"},
                    new VirtualKeyboardKey {Value = "+"},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "1"},
                    new VirtualKeyboardKey {Value = "2"},
                    new VirtualKeyboardKey {Value = "3"},
                    new VirtualKeyboardKey {Value = "%", Type = VirtualKeyType.EvalPercent},
                    new VirtualKeyboardKey {Value = "±", Type = VirtualKeyType.EvalNegative},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "0"},
                    new VirtualKeyboardKey {Value = ".", Type = VirtualKeyType.DecimalSeparator},
                    new VirtualKeyboardKey {Value = "=", Type = VirtualKeyType.Evaluate},
                    new VirtualKeyboardKey {Value = "Enter", Type = VirtualKeyType.EvalEnter},
                },
            };

        public static VirtualKeyboardLayout Symbols { get; } =
            new VirtualKeyboardLayout
            {
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "Close", Type = VirtualKeyType.Close},
                    new VirtualKeyboardKey {Value = "1"},
                    new VirtualKeyboardKey {Value = "2"},
                    new VirtualKeyboardKey {Value = "3"},
                    new VirtualKeyboardKey {Value = "4"},
                    new VirtualKeyboardKey {Value = "5"},
                    new VirtualKeyboardKey {Value = "6"},
                    new VirtualKeyboardKey {Value = "7"},
                    new VirtualKeyboardKey {Value = "8"},
                    new VirtualKeyboardKey {Value = "9"},
                    new VirtualKeyboardKey {Value = "0"},
                    new VirtualKeyboardKey {Value = ","},
                    new VirtualKeyboardKey {Value = "Backspace", Type = VirtualKeyType.Backspace},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Width = 34, Type = VirtualKeyType.Spacer},
                    new VirtualKeyboardKey {Value = "|"},
                    new VirtualKeyboardKey {Value = "/"},
                    new VirtualKeyboardKey {Value = "\\"},
                    new VirtualKeyboardKey {Value = "("},
                    new VirtualKeyboardKey {Value = ")"},
                    new VirtualKeyboardKey {Value = "["},
                    new VirtualKeyboardKey {Value = "]"},
                    new VirtualKeyboardKey {Value = "{"},
                    new VirtualKeyboardKey {Value = "}"},
                    new VirtualKeyboardKey {Value = "<"},
                    new VirtualKeyboardKey {Value = ">"},
                    new VirtualKeyboardKey {Value = "Enter", Type = VirtualKeyType.Enter},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "_"},
                    new VirtualKeyboardKey {Value = "*"},
                    new VirtualKeyboardKey {Value = "~"},
                    new VirtualKeyboardKey {Value = "!"},
                    new VirtualKeyboardKey {Value = "@"},
                    new VirtualKeyboardKey {Value = "#"},
                    new VirtualKeyboardKey {Value = "$"},
                    new VirtualKeyboardKey {Value = "€"},
                    new VirtualKeyboardKey {Value = "%"},
                    new VirtualKeyboardKey {Value = "^"},
                    new VirtualKeyboardKey {Value = "&"},
                    new VirtualKeyboardKey {Value = "§"},
                    new VirtualKeyboardKey {Value = "`"},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "ABC", Type = VirtualKeyType.ToggleSymbols},
                    new VirtualKeyboardKey {Value = "-"},
                    new VirtualKeyboardKey {Value = "+"},
                    new VirtualKeyboardKey {Value = "="},
                    new VirtualKeyboardKey {Value = "?"},
                    new VirtualKeyboardKey {Value = ":"},
                    new VirtualKeyboardKey {Value = ";"},
                    new VirtualKeyboardKey {Value = "\""},
                    new VirtualKeyboardKey {Value = "\'"},
                    new VirtualKeyboardKey {Value = "."},
                    new VirtualKeyboardKey {Value = " ", Type = VirtualKeyType.Spacebar},
                },
            };

        public static VirtualKeyboardLayout En { get; } =
            new VirtualKeyboardLayout
            {
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "Close", Type = VirtualKeyType.Close},
                    new VirtualKeyboardKey {Value = "1"},
                    new VirtualKeyboardKey {Value = "2"},
                    new VirtualKeyboardKey {Value = "3"},
                    new VirtualKeyboardKey {Value = "4"},
                    new VirtualKeyboardKey {Value = "5"},
                    new VirtualKeyboardKey {Value = "6"},
                    new VirtualKeyboardKey {Value = "7"},
                    new VirtualKeyboardKey {Value = "8"},
                    new VirtualKeyboardKey {Value = "9"},
                    new VirtualKeyboardKey {Value = "0"},
                    new VirtualKeyboardKey {Value = ","},
                    new VirtualKeyboardKey {Value = "Backspace", Type = VirtualKeyType.Backspace},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "CapsLock", Type = VirtualKeyType.CapsLock},
                    new VirtualKeyboardKey {Value = "q"},
                    new VirtualKeyboardKey {Value = "w"},
                    new VirtualKeyboardKey {Value = "e"},
                    new VirtualKeyboardKey {Value = "r"},
                    new VirtualKeyboardKey {Value = "t"},
                    new VirtualKeyboardKey {Value = "y"},
                    new VirtualKeyboardKey {Value = "u"},
                    new VirtualKeyboardKey {Value = "i"},
                    new VirtualKeyboardKey {Value = "o"},
                    new VirtualKeyboardKey {Value = "p"},
                    new VirtualKeyboardKey {Value = "Enter", Type = VirtualKeyType.Enter},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "Shift", Type = VirtualKeyType.Shift},
                    new VirtualKeyboardKey {Value = "a"},
                    new VirtualKeyboardKey {Value = "s"},
                    new VirtualKeyboardKey {Value = "d"},
                    new VirtualKeyboardKey {Value = "f"},
                    new VirtualKeyboardKey {Value = "g"},
                    new VirtualKeyboardKey {Value = "h"},
                    new VirtualKeyboardKey {Value = "j"},
                    new VirtualKeyboardKey {Value = "k"},
                    new VirtualKeyboardKey {Value = "l"},
                    new VirtualKeyboardKey {Value = "Shift", Type = VirtualKeyType.Shift},
                },
                new VirtualKeyboardRow
                {
                    new VirtualKeyboardKey {Value = "Symbols", Type = VirtualKeyType.ToggleSymbols},
                    new VirtualKeyboardKey {Value = "-"},
                    new VirtualKeyboardKey {Value = "z"},
                    new VirtualKeyboardKey {Value = "x"},
                    new VirtualKeyboardKey {Value = "c"},
                    new VirtualKeyboardKey {Value = "v"},
                    new VirtualKeyboardKey {Value = "b"},
                    new VirtualKeyboardKey {Value = "n"},
                    new VirtualKeyboardKey {Value = "m"},
                    new VirtualKeyboardKey {Value = "."},
                    new VirtualKeyboardKey {Value = " ", Type = VirtualKeyType.Spacebar},
                },
            };
    }
}