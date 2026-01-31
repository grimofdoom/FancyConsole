using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Text {
    public static partial class Read {
        /// <summary>Get a general int from player response</summary>
        public static int GetInt(int defaultValue) {
            int tries = 0;

            while (true && tries < MaxPlayerTries) {
                tries++;
                string? playerResponse = Console.ReadLine();

                //Empty response is a failed response
                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                //Check if string is number
                if (int.TryParse(playerResponse, out int results)) {
                    return results;//Success, returning 
                }

                Print.ClearCurrentLine();
                //Failed to find number from player response, try again
                continue;
            }

            //Reached maximum number of tries, return default value
            return defaultValue;
        }

        /// <summary>Get int from player existing in a value range</summary>
        public static int GetIntRange(int defaultValue, int min, int max) {
            int tries = 0;
            while (true && tries < MaxPlayerTries) {
                tries++;

                string? playerResponse = Console.ReadLine();

                //Empty response, try again
                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                //Try to convert it to int
                if (int.TryParse(playerResponse, out int results)) {
                    //check if in range
                    if (results <= max && results >= min) return results;
                }

                Print.ClearCurrentLine();
                //String could not be converted to int, try again
                continue;
            }
            return defaultValue;
        }

    }
}
