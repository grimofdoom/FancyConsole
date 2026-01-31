using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FancyConsole.Text {
    public static partial class Read {
        /// <summary>Potential player responses for a "yes" answer</summary>
        public static readonly HashSet<string> YesList = new(StringComparer.OrdinalIgnoreCase) {
           // Core
            "yes", "y",
            "ya", "yah", "ye", "yeh",
            // Casual
            "yep","yup","yuh","yap",
            // Fun / Playful
            "yeppers","yuppers","yessiree","yessir",
            "heckyes","surething",
            // Logical / Boolean
            "true", "tru", "t","1","positive",
            // Agreement
            "sure","ok","k","okay","okey",
            // Confirmation
            "confirm","confirmed","accept","accepted",
            "agree","agreed",
            // Slang / Friendly
            "alright","alrigh","alri","aight",
            // Strong / Confident
            "always","definitely","absolutely",
            "certainly",
            // Toggle / System
            "on","enable","enabled","go","start",
            // Extra fun flavor (still clean)
            "doit","sendit","letsgo"
        };

        /// <summary>Potential player responses for a "no" answer</summary>
        public static readonly HashSet<string> NoList = new(StringComparer.OrdinalIgnoreCase) {
            // Core
            "no", "n","nope","nah", "na",
            // Logical / Boolean
            "false", "f","0","off",
            // Formal / System
            "negative","deny", "denied",
            "decline", "declined","reject",
            "rejected","cancel","abort",
            "stop","disable", "disabled",
            // Strong / Emphatic
            "never","not","nothing",
            // Casual / Playful
            "noooo","nooo","noo",
            "nahhh","naah","nop",
            // Fun Personality (clean)
            "nopearoo","nahfam","notachance",
            "hardno","pass",
            // Tech / Terminal flavored
            "fail","failed","invalid","null",
            // Soft refusal
            "skip","later","notnow"
        };


        /// <summary>Get a boolean response from player</summary>
        public static bool GetBool(bool defaultValue) {
            int tries = 0;
            while (true && (tries < MaxPlayerTries)) {


                string? playerResponse = Console.ReadLine();

                if (playerResponse == null) {
                    Print.ClearCurrentLine();
                    continue;
                }

                //Compare against the list of potential yes answers
                if (YesList.Contains(playerResponse)) return true;

                //Compare against the list of potential no answwers
                if (NoList.Contains(playerResponse)) return false;

                //No potential answer found, try again
                Print.ClearCurrentLine();
                continue;
            }

            return defaultValue;
        }
    }
}
