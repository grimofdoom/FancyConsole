using FancyConsole.Text;
using System.Runtime.InteropServices;

namespace FancyConsole.Window {
    public static class Size {
        /// <summary>Get the width size of console</summary>
        public static int Width => Console.WindowWidth;
        /// <summary>Get the height size of the console</summary>
        public static int Height => Console.WindowHeight;


        /// <summary>Get the max buffer width size of console</summary>
        public static int BufferWidth => Console.BufferWidth;
        /// <summary>Get the mac buffer height size of console</summary>
        public static int BufferHeight => Console.BufferHeight;

        /// <summary>minimum width to check for</summary>
        public static int MinimumWidth { get; set; } = 100;
        /// <summary>Minimum height to check for</summary>
        public static int MinimumHeight { get; set; } = 30;

        private static int _LastWidth = Console.WindowWidth;
        private static int _LastHeight = Console.WindowHeight;


        /// <summary>Will for sure work on windows, however other platforms will varry. Do not depend if multiplatform</summary>
        public static void SetSize(int width, int height) {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                // Ensure buffer is large enough first (Windows needs this order)
                Console.SetBufferSize(
                    Math.Max(Console.BufferWidth, width),
                    Math.Max(Console.BufferHeight, height)
                );
                Console.SetWindowSize(width, height);
            } else {
                // try to use ANSI to get around system limits, not always will work
                Console.Write($"\x1b[8;{height};{width}t");
            }

            //We manually tried to adjust width/height, reset resize check if actually changed
            _LastHeight = height;
            _LastWidth = width;
        }

        /// <summary>Check to see whether or not the window is large enough</summary>
        /// <returns>Returns True if window is large enough, and False if too small</returns>
        public static bool WindowSizeCheck() {
            return Width >= MinimumWidth && Height >= MinimumHeight;
        }

        /// <summary>Make sure window is large enough, will loop until window has reached an appropriate size</summary>
        /// <remarks>Each attempt is locked behind a Print.Pause()</remarks>
        public static void EnforceMinimumSize() {
            while (!WindowSizeCheck()) {
                Print.Clear();
                Print.Line("Window size is too small.");
                Print.Line($"Current Width/goal: [{Width}/{MinimumWidth}]");
                Print.Line($"Goal height/goal [{Height}/{MinimumHeight}]");
                Print.Pause("Press [ENTER] to try again.");
            }
            Print.Clear();
        }

        /// <summary>Check whether or not the console has changed height at all</summary>
        public static bool HasResize() {
            if (_LastHeight != Height || _LastWidth != Width) {
                _LastHeight = Height;
                _LastWidth = Width;
                return true;
            }
            return false;
        }
    }
}
