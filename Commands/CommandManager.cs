using FancyConsole.Text;
using System.Security.Cryptography;
using System.Text;

namespace FancyConsole.Commands {
    /// <summary>Used to parse user commands into actions to perform</summary>
    public static class CommandManager {
        /// <summary>All registered CommandSets </summary>
        private static List<CommandSetBase> Sets { get; set; } = [];

        /// <summary>View what the current active layer is</summary>
        public static Layer? ActiveLayer => activeLayer;
        private static Layer? activeLayer { get; set; } = CommonLayers.All;


        public static void Init() {
            //Force FancyConsoleCommands to be added, can be removed in code
            Register(new BaseCommands.FancyConsoleCommandSet());
        }



        /// <summary>Register a new CommandSet into the CommandManager</summary>
        public static void Register(CommandSetBase commandSet) {
            if (Sets.Contains(commandSet)) {
                Logging.Log.Warning($"<{commandSet.Name}> CommandSet has already been registered into the CommandManager");
            }
            Sets.Add(commandSet);
        }

        public static void Remove(CommandSetBase commandSet) {
            Sets.Remove(commandSet);
        }

        public static void Remove(string commandName) {
            if (Sets.Find(x =>  x.Name == commandName) is CommandSetBase cmdSet && cmdSet is not null) {
                Sets.Remove(cmdSet);
            }
        }

        /// <summary>Set the current layer which CommandManager is operating in</summary>
        public static void SetCommandLayer(Layer newLayer) {
            activeLayer = newLayer;
        }

        /// <summary>Take user input, and try to perform an action from commands</summary>
        public static CommandResults? PerformCommand(string userInput, CommandContextBase ctx) {
            //No layer exists
            if (activeLayer == null) {
                return new CommandResults(CommandStatus.NOLAYER, "No layer exists to operating within");
            }
            //Empty input from user
            if (userInput == string.Empty || userInput == "" || userInput == null) {
                return new CommandResults(CommandStatus.NOINPUT, "No input was provided by user");
            }

            //Sanitize user input to clean it up for any issues to compare to invokers
            userInput = Sanitize(userInput);


            //Split up user input into args
            string[] args = userInput.Trim().Split(' ');

            List<ICommand> possibleCommands = FindAllCommands(args[0]);

            //Was any command found
            if (possibleCommands.Count == 0) return new(CommandStatus.NOTFOUND, "No command could be found under <{userInput}>");

            foreach(ICommand cmd in possibleCommands) {
                if (cmd == null) continue;
                //Did we get enough args from user/player to perform command?
                if (args.Length - 1 < cmd.MinimumParameterCount) continue;

                //Command was found and can be performed
                return cmd.Perform(args,ctx);
            }

            //Everything passes, but no command found
            return new CommandResults(CommandStatus.LOWARGS, $"Commands were found, but not enough arguments passed");
        }

        /// <summary>Get a command by its invoker</summary>
        /// <param name="invoker"></param>
        /// <returns></returns>
        public static ICommand? FindCommand(string invoker) {
            //No invoker requested
            if (invoker == string.Empty || invoker == "" || invoker == null) return null;

            foreach(CommandSetBase set in Sets) {
                //if invoker exists in set, then return first found
                if (set.GetCommand(invoker) is ICommand foundCMD && foundCMD is not null){
                    return foundCMD;
                }
            }

            //No command found
            return null;
        }

        /// <summary>Across all sets, find all commands that use invoker</summary>
        public static List<ICommand> FindAllCommandFree(string invoker) {
            List<ICommand> foundCommands = [];

            foreach(CommandSetBase set in Sets) {
                if (set.GetCommand(invoker) is ICommand cmd && cmd != null) {
                    foundCommands.Add(cmd);
                }
            }

            return foundCommands;
        }

        /// <summary>Find all commands that use invoker, in activeLayer</summary>
        public static List<ICommand> FindAllCommands(string invoker) {
            List<ICommand> foundCommands = [];

            //Return empty list if no activeLayer exists
            if (activeLayer == null) return foundCommands;

            foreach(CommandSetBase set in Sets.FindAll(x => x.Layers.Contains(activeLayer)) ){
                if (set.GetCommand(invoker) is ICommand cmd && cmd != null) {
                    foundCommands.Add(cmd);
                }
            }

            return foundCommands;
        }

        /// <summary>Sanitize a string tolower, trim, normalize and clear out any newline or control characters</summary>
        public static string Sanitize(string s) {
            string normalized = s.Trim().ToLower().Normalize(NormalizationForm.FormC);

            System.Text.StringBuilder results = new(normalized.Length);

            foreach (char c in normalized) {
                if (!Char.IsControl(c) || c == '\n') {
                    results.Append(c);
                }
            }

            return results.ToString();
        }
    }
}
