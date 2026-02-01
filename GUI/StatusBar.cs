using System.Diagnostics;

namespace FancyConsole.Text {
    public static class Bar {
        /// <summary>Generate a status bar with lengTotal with length being filled</summary>
        public static string Basic(int length, int lengthTotal) {
            return $"[{new string('█', length)}{new string(' ', lengthTotal - length)}]";
        }

        /// <summary>Generate a status bar with barLength based on amount/amountMax</summary>
        public static string BasicScaled(int amount, int amountMax, int barLength) {
            int filledLength = (int)((amount / (double)amountMax) * barLength);
            return Basic(filledLength, barLength);
        }

        /// <summary>A string of status bar that fades from solid █▓▒░ to empty ' ' based on length</summary>
        public static string Faded(int length, int lengthTotal, int fadeSize = 3) {
            if (lengthTotal <= 0)
                return $"|{new string(' ', lengthTotal)}|";

            length = Math.Clamp(length, 0, lengthTotal);

            char[] fade = ['█', '▓', '▒', '░', ' '];
            char solid = fade[0];
            char empty = fade[^1];
            char[] fadeChars = [.. fade.Skip(1).Take(fade.Length - 2)];

            string bar = "|";

            if (length > 0) {
                //Solid portion (may be reduced by fade)
                int fadeLength = Math.Min(fadeSize, length);
                int solidLength = length - fadeLength;

                if (solidLength > 0)
                    bar += new string(solid, solidLength);

                //Fade portion (trailing)
                for (int i = 0; i < fadeLength; i++) {
                    int fadeIndex = fadeChars.Length - fadeLength + i;
                    fadeIndex = Math.Clamp(fadeIndex, 0, fadeChars.Length - 1);
                    bar += fadeChars[fadeIndex];
                }
            }

            //Empty portion
            int emptyLength = lengthTotal - length;
            if (emptyLength > 0)
                bar += new string(empty, emptyLength);

            bar += "|";
            return bar;
        }

        /// <summary>generate a status bar scaled, directly into FadedStatusBar</summary>
        public static string FadedScaled(int length, long partialAmount, long fullAmount, int fadeSize = 3) {
            if (fullAmount <= 0) return Faded(0, length, fadeSize);

            double ratio = (double)partialAmount / fullAmount;
            ratio = Math.Clamp(ratio, 0.0, 1.0);

            int scaledPartial = (int)Math.Round(ratio * length);

            return Faded(scaledPartial, length, fadeSize);
        }
    }
}