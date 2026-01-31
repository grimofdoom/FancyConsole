using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FancyConsole.Text {
    //This section was generated via ChatGPT. It enables ANSI escape codes in Windows consoles.
    //This area could use a human touch of someone more capable of performing this kernel32 imports
    //I (Tim/Grim) lack knowledge in handling this area properly, so please improve if possible.
    /// <summary>Enables ANSI escape code support in Windows consoles</summary>
    public static class ANSIConsole {
        private const int STD_OUTPUT_HANDLE = -11;
        private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        [DllImport("kernel32.dll", SetLastError = true)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
        private static extern IntPtr GetStdHandle(int nStdHandle);
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

        [DllImport("kernel32.dll", SetLastError = true)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
        private static extern bool GetConsoleMode(
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
            IntPtr hConsoleHandle,
            out uint lpMode
        );

        [DllImport("kernel32.dll", SetLastError = true)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
        private static extern bool SetConsoleMode(
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
            IntPtr hConsoleHandle,
            uint dwMode
        );

        public static void Enable() {
            var handle = GetStdHandle(STD_OUTPUT_HANDLE);
            if (handle == IntPtr.Zero)
                return;

            if (!GetConsoleMode(handle, out uint mode))
                return;

            SetConsoleMode(handle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
        }
    }
}
