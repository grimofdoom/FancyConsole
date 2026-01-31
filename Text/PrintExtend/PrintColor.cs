namespace FancyConsole.Text {
    public static partial class Print {
        public static Theme DefaultTheme { get; set; } = new(new(0, 200, 200), new(18, 20, 26)) {backgroundEffect = ConsoleColor.DarkGreen};

        /// <summary>Normal color theme for printing text out, system defaults back to this</summary>
        public static Theme NormalTheme { get; set; } = new(new(0, 200, 200), new(18, 20, 26)) {backgroundEffect = ConsoleColor.DarkGreen };




        /// <summary>Set the theme of the console colors</summary>
        public static void SetTheme(Theme theme) {
            Console.BackgroundColor = theme.backgroundEffect;
            SetFontColor(theme.Font);
            SetBackgroundColor(theme.Background);
        }

        /// <summary>Using TrueColor, set the background color of the console</summary>
        public static void SetBackgroundColor(Color color) {
            Console.Write($"\x1b[48;2;{color.R};{color.G};{color.B}m");
        }

        /// <summary>Set the font color sing TrueColor</summary>
        public static void SetFontColor(Color color) {
            Console.Write($"\x1b[38;2;{color.R};{color.G};{color.B}m");
        }

        /// <summary>Clear all color and text effects, resetting to console defaults</summary>
        public static void EndColorAndEffects() {
            Console.Write("\x1b[0m");
        }

        /// <summary>Get string for resetting console colors to a theme</summary>
        private static string GetThemeString(Theme theme) {
            return $"\x1b[38;2;{theme.Font.R};{theme.Font.G};{theme.Font.B}m\x1b[48;2;{theme.Background.R};{theme.Background.G};{theme.Background.B}m";
        }
    }
}
