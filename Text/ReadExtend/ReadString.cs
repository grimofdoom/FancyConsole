using System;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Text {
    public static partial class Read {
        /// <summary>Get general string value from player</summary>
        public static string GetString(string defaultValue) {
            int tries = 0;
            while (true && tries < MaxPlayerTries) {
                tries++;

                string? playerResponse = Console.ReadLine();

                //Null line, try again
                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                //Not null, so fulfills requirements
                return playerResponse;
            }

            return defaultValue;
        }

        /// <summary>Get a string with a minimum length from player</summary>
        public static string GetStringMinLength(string defaultValue, int stringLength) {
            int tries = 0;
            while (true && tries < MaxPlayerTries) {
                tries++;

                string? playerResponse = Console.ReadLine();

                //Empty string is failed response
                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                //Check if string is long enough, return if so
                if (playerResponse.Length >= stringLength) return playerResponse;

                Print.ClearCurrentLine();
                continue;
            }

            return defaultValue;
        }

        /// <summary>Get a string with a maximum length from player</summary>
        public static string GetStringMaxLength(string defaultValue, int stringLength) {
            int tries = 0;
            while (true && tries < MaxPlayerTries) {
                tries++;

                string? playerResponse = Console.ReadLine();

                //Empty response, try again
                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                //Check if string is short enough, return if so
                if (playerResponse.Length >= stringLength) return playerResponse;

                Print.ClearCurrentLine();
                continue;
            }

            return defaultValue;
        }

        /// <summary>Get a string from player, that exists in range of length
        /// <para>Example: Player name of specific size</para></summary>
        public static string GetStringRangeLength(string defaultValue, int min, int max) {
            int tries = 0;
            while (true && tries < MaxPlayerTries) {
                tries++;

                string? playerResponse = Console.ReadLine();

                //Empty response, try again
                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                //shorten the check line
                int length = playerResponse.Length;

                //Check if string length in range, return if so
                if (length >= min && length <= max) return playerResponse;

                Print.ClearCurrentLine();
                continue;
            }
            return defaultValue;
        }

        /// <summary>Get player response from a limited selection of items</summary>
        /// <param name="defaultValue"></param><param name="stringList"></param>
        /// <returns></returns>
        public static string GetStringList(string defaultValue, string[] stringList) {
            int tries = 0;

            while (true && tries < MaxPlayerTries) {
                tries++;

                string? playerResponse = Console.ReadLine();

                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                foreach (string item in stringList) {
                    if (item == null) continue;
                    if (playerResponse.Equals(item, StringComparison.CurrentCultureIgnoreCase)) return playerResponse;
                }

                Print.ClearCurrentLine();
                continue;
            }
            return defaultValue;
        }

        /// <summary>Get player response from a limited selection of items, TILL player selects item from list
        /// <para>This is dangeroud infinite loop, make sure you have supplied the player with displayed options spelled correctly</para></summary>
        /// <param name="defaultValue"></param><param name="stringList"></param>
        /// <returns></returns>
        public static string GetStringListInf(string[] stringList) {
            while (true) {

                string? playerResponse = Console.ReadLine();

                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                foreach (string item in stringList) {
                    if (item == null) continue;
                    if (playerResponse.Equals(item, StringComparison.CurrentCultureIgnoreCase)) return playerResponse;
                }

                Print.ClearCurrentLine();
                continue;
            }
        }
    }
}
