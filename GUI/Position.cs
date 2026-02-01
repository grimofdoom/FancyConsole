using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FancyConsole.GUI {
    public static class Position {
        /// <summary>Convert a chunk of text to be center alligned by adding spaces</summary>
        public static string Center(string text) {
            string[] lines = text.Split('\n');
            string newLines = "";
            foreach (string line in lines) {
                //Check if usable line
                if (line == "" || line == string.Empty || line == null || line.Length == 0) continue;

                if (line.Length >= Window.Size.Width){
                    //If wider than screen, use an alternate method with word wrap
                    newLines += CenterLineChunk(line);
                } else {
                    //Fits within width, use simplified method
                    newLines += CenterLine(line) + '\n';
                }
            }

            return newLines;
        }

        /// <summary>Center a single line of text, unknown if it will fit in 1 line</summary>
        /// <remarks>newline CANNOT exist within chunk, otherwise will return untouched if any found</remarks>
        public static string CenterLineChunk(string chunk) {
            if (chunk.Contains("\n")) return chunk;

            //Fits in one line, just use CenterLine instead
            if (chunk.Length < Window.Size.Width) {
                return CenterLine(chunk);
            }

            //Separate by word/symbol
            string[] split = TextToWordArray(chunk);
            List<string> createdLines = [];

            //Separate into lines that fit within width, for word wrap
            string workingLine = "";
            foreach (string part in split) {
                //Somehow something bad slipped in
                if (part == "" || part == string.Empty || part == null) continue;

                //If adding this part is not longer than window width, add it
                if (workingLine.Length + part.Length <= Window.Size.Width) {
                    workingLine += part;
                    continue;
                }

                //Line grew large enough, start new line
                createdLines.Add(workingLine);
                workingLine = part;
            }

            //Center each line created
            List<string> centeredLines = [];
            foreach (string line in createdLines) {
                //Something bad somehow slipped in
                if (line == "" || line == string.Empty || line == null) continue;
                centeredLines.Add(CenterLine(line));
            }

            //Get everything back into a single string to return
            string final = "";
            foreach (string line in centeredLines) {
                final += line + "\n";
            }

            return final;
        }

        /// <summary>Center a single line of text, already known to fit within Window.Size.Width</summary>
        /// <remarks>Newline CANNOT exist within this chunk of text. Returns untouched if any newline exists</remarks>
        public static string CenterLine(string line) {
            if (line.Contains("\n")) return line;

            bool oddNumber = line.Length % 2 != 0;//If odd, will reduce 1 from a side to fit length
            int gapSpaceLeft = (Window.Size.Width - line.Length) / 2;
            int gapSpaceRight = (oddNumber) ? gapSpaceLeft : gapSpaceLeft - 1;

            //Handle negative gap space, just in case. Often from not predetermining length is larger than window
            if (gapSpaceLeft < 0)
                gapSpaceLeft = 0;
            if (gapSpaceRight < 0)
                gapSpaceRight = 0;

            return new string(' ', gapSpaceLeft) +
                line +
                //Remove 1 space if oddNumber
                new string(' ', gapSpaceRight);
        }




        public static string Right(string text) {
            string[] lines = text.Split('\n');
            string newLines = "";

            foreach(string line in lines) {
                if (line == "" || line == string.Empty || line == null || line.Length == 0) continue;
                if (line.Length >= Window.Size.Width) {
                    //Length is too large, use more complex method
                    newLines += RightLineChunk(line);
                } else {
                    //Length fits within width, use simplified method
                    newLines += RightLine(line) + '\n';
                }
            }

            return newLines;
        }

        /// <summary>Right allign a line of text, unsure if it fits within Window.Size.Width</summary>
        /// <remarks>Should this text contain any Newline, will return unmodified text</remarks>>
        public static string RightLineChunk(string text) {
            if (text.Contains('\n')) return text;
            

            //If text within window size, use simpler method
            if (text.Length < Window.Size.Width) {
                return RightLine(text);
            }

            //Wordwrap separate text
            string[] parts = TextToWordArray(text);
            List<string> lines = [];
            string workingLine = "";

            //Create lines to fit window size, word wrapping them
            foreach (string part in parts) {
                //Adding to current line is within window width, add it and continue
                if (workingLine.Length + part.Length <= Window.Size.Width) {
                    workingLine += part;
                    continue;
                }

                //Line is maxed, add to list and start new line with part
                lines.Add(workingLine);
                workingLine = part;
            }

            //Right allign each final line generated
            List<string> finalLines = [];
            foreach (string line in lines) {
                finalLines.Add(RightLine(line));
            }

            //Rebuild lines into a single string to return
            string newLines = "";
            foreach (string line in finalLines) {
                newLines += line + '\n';
            }

            return newLines;
        }

        /// <summary>Right allign a line of text, predetermined to fit within width.</summary>
        /// <remarks>Should this text contain any Newline, will return unmodified text</remarks>>
        public static string RightLine(string text) {
            if (text.Contains("\n")) return text;
            int gapSpace = Window.Size.Width - text.Length;
            
            if (gapSpace < 0) 
                gapSpace = 0;

            return new string(' ', gapSpace) + text;
        }


        /// <summary>Helper class to convert a text into an array seperated by symbols and spaces</summary>
        public static string[] TextToWordArray(string text) {
            //fancy regex pattern match, thanks google AI. Wish I had proper source to cite
            string pattern = @"(\w+)|(\W+)";
            MatchCollection matches = Regex.Matches(text, pattern);

            //cast matches values to a string array
            return [.. matches.Cast<Match>().Select(match => match.Value)];
        }
    }
}
