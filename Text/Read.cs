using System;
namespace FancyConsole.Text {

    /// <summary>Used to get player input</summary>
    public static partial class Read {
        /// <summary>The maximum times a player can try to perform a check before returning default</summary>
        public static int MaxPlayerTries { get; set; } = 10;

        public static void ClearReadBuffer() {
            while (Console.KeyAvailable) {
                Console.ReadKey(true);
            }
        }
    }
}