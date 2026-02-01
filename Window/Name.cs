using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Window {
    public static class Name {
        /// <summary>Set the name of the title/tab of the program. Not 100% all the time will work, depends on console</summary>
        public static void SetName(string windowName) {
            //Make sure ANSI console is enabled
            Text.ANSIConsole.Enable();

            //First, try and set console window name using defailt systems
            Console.Title = windowName;

            //Then try and set via ANSI for console title
            Console.WriteLine($"\x1b]0;{windowName}\x07");

            //Read and clear out anything extra
            Console.ReadLine();
            Text.Print.Clear();
        }
    }
}
