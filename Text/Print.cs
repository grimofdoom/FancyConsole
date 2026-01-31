using System.Threading;

namespace FancyConsole.Text {
    /// <summary>Print out text to screen</summary>
    public static partial class Print {

        /// <summary>Print out zero-markup text without starting a new line, with a specific font color</summary>
        /// <param name="text"></param><param name="tempFontColor"></param>
        public static void Write(string text, Theme theme) {
            SetTheme(theme);
            Console.Write(theme.Apply(text));
            SetTheme(NormalTheme);
        }

        /// <summary>Print out zero-markup text without starting a new line</summary>
        /// <remarks>Uses default color theme</remarks>
        public static void Write(string text) {
            Write(text, NormalTheme);
            SetTheme(NormalTheme);
        }



        /// <summary>Print out zero-markup text with new line, with specific color theme</summary>
        /// <param name="text"></param><param name="tempFontColor"></param>
        public static void Line(string text, Theme theme) {
            SetTheme(theme);
            Console.WriteLine(theme.Apply(text));
            SetTheme(NormalTheme);
        }

        /// <summary>Print out zero-markup text with starting a new line</summary>
        public static void Line(string text) {
            Line(text, NormalTheme);
        }

        /// <summary>Print to console, without any extra effects</summary>
        public static void RawLine(string text) {
            Console.WriteLine(text);
        }

        /// <summary>Writes the specified text to the standard output stream without a newline character.</summary>
        public static void RawWrite(string text) {
            Console.Write(text);
        }

        /// <summary>Clear only the screen, leaving the print cache still filled</summary>
        public static void Clear() {
            Console.Clear();
            Console.Write("\x1b[3J");
            Console.Clear();
            int w = Console.BufferWidth;
            int h = Console.BufferHeight;

            Console.BackgroundColor = NormalTheme.backgroundEffect;
            SetTheme(NormalTheme.GetFlipped());

            for (int y = 0; y < h; y++) {
                Console.SetCursorPosition(0, y);
                Console.Write(new string('█', w));
            }

            Console.SetCursorPosition(0, 0);
            SetTheme(NormalTheme);
        }

        /// <summary>Press Enter to continue</summary>
        public static void Pause() {
            Pause("Press ENTER to continue");
        }

        /// <summary>Pause with custom message</summary>
        public static void Pause(string message) {
            // Clear any keys the user may have pressed while text was printing
            while (Console.KeyAvailable)
                Console.ReadKey(true);
            Line(message);
            Console.ReadLine();
        }

        /// <summary>Print out text as if it was being written</summary>
        /// <param name="text">Entire body of text to print out</param>
        /// <param name="delay">milliseconds of delay between each letter</param>
        public static void WriteOut(string text, int delay) {
            foreach(char letter in text) {
                Write(letter.ToString());
                Thread.Sleep(delay);
                //TODO: Update to support hitting ENTER to instantly write out rest of text
            }
            Write("\n");
        }

        /// <summary>Write out a chunk of text over a specific length of time</summary>
        /// <param name="text">Entire body of text to print out</param>
        /// <param name="seconds">How many seconds overall to print body of text</param>
        public static void WriteOverTime(string text, float seconds) {
            float ms = (seconds / text.Length) * 1000;
            WriteOut(text, (int)ms);
        }

        /// <summary>Reset active line instead of clearing whole console</summary>
        public static void ClearCurrentLine() {
            int prev = Console.CursorTop - 1;
            if (prev >= 0) {
                Console.SetCursorPosition(0, prev);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, prev);
            }
        }
    }
}
