namespace FancyConsole.Text {
    public static partial class Print {
        /// <summary>Theme for warning text</summary>
        public static Theme WarningTheme { get; set; } = new(new(255, 255, 100), new(40, 40, 12), TextEffects.BLINK | TextEffects.BOLD);

        /// <summary>Theme for error text</summary>
        public static Theme ErrorTheme { get; set; } = new(new(255, 100, 100), new(32, 12, 12), TextEffects.BLINK | TextEffects.BOLD);

        /// <summary>List of all errors so far</summary>
        public static List<string> ErrorLog { get; } = [];

        /// <summary>Whether or not to print errors AFTER adding to log</summary>
        public static bool PrintErrors { get; set; } = true;

        /// <summary>List of all warnings so far</summary>
        public static List<string> WarningLog { get; } = [];

        /// <summary>Whether or not to print warnings AFTER adding to log</summary>
        public static bool PrintWarnings { get; set; } = true;

        /// <summary>Whether or not to log warnings</summary>
        public static bool LogWarnings { get; set; } = true;

        /// <summary>Produce a warning message, with logging</summary>
        public static void Error(string error) {
            //Log error
            ErrorLog.Add(error);
            //Pring error if enabled
            if (PrintErrors) Line($"[ERROR]: {error}", ErrorTheme);
        }

        /// <summary>Produce a warning message, with logging and pringing it enabled</summary>
        public static void Warning(string warning) {
            //Log warnings if enabled
            if (LogWarnings) WarningLog.Add(warning);
            //Print warning if enabled
            if (PrintWarnings) Line($"[WARNING]: {warning}", WarningTheme);
        }

        /// <summary>Take errors and warnings and log them to a file for later review</summary>
        public static void LogsToFile() {
            System.Text.StringBuilder logBuilder = new();
            //Gather errors
            logBuilder.AppendLine("----------------- ERRORS -----------------");
            foreach (string error in ErrorLog) {
                logBuilder.AppendLine(error);
            }
            
            logBuilder.AppendLine();

            //Gather warnings
            logBuilder.AppendLine("----------------- WARNINGS -----------------");
            foreach (string warning in WarningLog) {
                logBuilder.AppendLine(warning);
            }

            //Folder for logs to be held, generate if not existing
            string logFolder = "Logs";
            Directory.CreateDirectory(logFolder); // creates folder if it doesn't exist

            // Generate log file path inside Logs folder
            string logFileName = $"Log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
            string logFilePath = System.IO.Path.Combine(logFolder, logFileName);

            //Write to file
            File.WriteAllText(logFilePath, logBuilder.ToString());
        }
    }
}
