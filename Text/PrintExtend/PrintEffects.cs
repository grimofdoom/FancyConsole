namespace FancyConsole.Text {
    public static partial class Print {
        /// <summary>Convert a string to be bold</summary>
        public static string Bold(string text) {
            return $"\x1b[1m{text}\x1b[0m";
        }

        /// <summary>Converts a string to be dim</summary>
        public static string Dim(string text) {
            return $"\x1b[2m{text}\x1b[0m";
        }

        /// <summary>Converts a string to be italic</summary>
        public static string Italic(string text) {
            return $"\x1b[3m{text}\x1b[0m";
        }

        /// <summary>Converts a string to be underlined</summary>
        public static string Underline(string text) {
            return $"\x1b[4m{text}\x1b[0m";
        }

        /// <summary>Converts a string to blink</summary>
        public static string Blink(string text) {
            return $"\x1b[5m{text}\x1b[0m";
        }

        /// <summary>Converts a string to fast blink</summary>
        public static string FastBlink(string text) {
            return $"\x1b[6m{text}\x1b[0m";
        }

        /// <summary>Converts a string to inverse colors</summary>
        public static string Inverse(string text) {
            return $"\x1b[7m{text}\x1b[0m";
        }

        /// <summary>Converts a string to concealed (hidden) text</summary>
        public static string Conceal(string text) {
            return $"\x1b[8m{text}\x1b[0m";
        }

        /// <summary>Converts a string to strikethrough text</summary>
        public static string Strikethrough(string text) {
            return $"\x1b[9m{text}\x1b[0m";
        }

        /// <summary>Applies true color (24-bit) ANSI escape codes to the specified text using the provided RGB values.</summary>
        public static string TrueColor(string text, int r, int g, int b) {
            //Cap colors to 0-255 range
            r = int.Min(Math.Max(r, 0), 255);
            g = int.Min(Math.Max(g, 0), 255);
            b = int.Min(Math.Max(b, 0), 255);
            return $"\x1b[38;2;{r};{g};{b}m{text}\x1b[0m";
        }

        /// <summary>Applies true color (24-bit) ANSI escape codes to the specified text using the provided RGB values.</summary>
        public static string TrueColor(string text, Color color) {
            return TrueColor(text, color.R, color.G, color.B);
        }
    }
}
