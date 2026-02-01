using FancyConsole.Text;
using System.Text.RegularExpressions;

namespace FancyConsole.GUI {
    public static class Typewriter {
        /// <summary>Print out character by character from text, with a millisecond pause between each.</summary>
        /// <remarks>If skippable set to false, skipping by pressig any key is impossible. Default true</remarks>
        public static void ByCharacter(string text, int msPause, bool skippable = true) {
            //Used to skip a lot of unecessary repeating of skip section
            bool skipStarted = false;

            foreach (var c in text) {
                Print.Write(c);
                Thread.Sleep(msPause);

                //Skip unecessary work if already started or not skippable
                if (skipStarted || !skippable) continue;

                //If there is any key pressed, then disable pause and finish quick
                //TODO(Typewriter):Rewrite to instead instantly write out the rest of text
                if (Console.KeyAvailable) {
                    msPause = 0;
                    Read.ClearReadBuffer();
                }
            }

            //Force a newline at the end if one not already existing
            if (text.Last<char>() != '\n') {
                Print.Write('\n');
            }
        }

        /// <summary>Print out character by character from text, over the span of totalTime in milliseconds</summary>
        public static void ByCharacterOverTime(string text, int msTotalTime, bool skippable = true) {
            int msPause = msTotalTime / text.Length;
            ByCharacter(text, msPause, skippable);
        }

        /// <summary>Write out text word/symbol at a time</summary>
        public static void ByWord(string text, int msPause, bool skippable = true) {
            //Used to skip a lot of unecessary repeating of skip section
            bool skipStarted = false;
            
            foreach (string part in GUI.Position.TextToWordArray(text)) {
                Print.Write(part);
                Thread.Sleep(msPause);

                //Skip unecessary work if already started or not skippable
                if (skipStarted || !skippable) continue;

                if (Console.KeyAvailable) {
                    msPause = 0;
                    Read.ClearReadBuffer();
                    skipStarted = true;
                }
            }
        }

        /// <summary>Print out text word/symbol at a time, over span of TotalTime in milliseconds</summary>
        public static void ByWordOverTime(string text, int msTotalTime, bool skippable = true) {
            int msPause = msTotalTime / GUI.Position.TextToWordArray(text).Length;
            ByWord(text, msPause, skippable);
        }
    }
}
